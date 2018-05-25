using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mvvm;
using PDYS.Models;
using PDYS.Helper;
using PDYS.Services;
using PDYS.Interfaces;
using System.Collections;
using PDYS.Services.ServiceParam;

namespace PDYS.InfraStructure
{
    public abstract class ListViewModelBase<TEntity, TEntityForm> : ViewModelBase, IDataList
        where TEntity : IDataItem
    {
        public event Action<TEntity> OnOpening;
        public event Action<IEnumerable<TEntity>> OnInserted;
        public event Action<IEnumerable<TEntity>> OnDeleted;


        public ListViewModelBase(bool autoloaddata)
        {
            this.PageNumber = 1;
            this.IsAutoLoadData = autoloaddata;

            this.IsNewCommand = true;
            this.IsOpenCommand = true;
            this.IsDeleteCommand = false;
            this.IsAppendCommand = false;

            this.ListPageSize = new ObservableCollection<Parameter>()
                                {
                                    new Parameter(){ Name="PageSize", Text = "10", Value = 10 },
                                    new Parameter(){ Name="PageSize", Text = "20", Value = 20 },
                                    new Parameter(){ Name="PageSize", Text = "50", Value = 50 },
                                    new Parameter(){ Name="PageSize", Text = "100", Value = 100 },
                                };

            this.SelectedPageSize = this.ListPageSize[3];

            this.MouseDoubleClickCommand = new RelayCommand(ExecuteOpenRecordCommand);
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ListViewModelBase_PropertyChanged);
        }

        

        void ListViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => this.SelectedPageSize))
            {
                this.PageNumber = 1;
                LoadData();
            }
        }


        #region Filter Expression

        public IQueryable<TEntity> QueryExpression {get;set;}

        #endregion

        public void LoadData()
        {
            this.Items.Clear();
            
            this.PageNumber = (this.PageNumber < 0) ? 1 : this.PageNumber;
            int pagesize = (this.SelectedPageSize == null) ? 10 : this.SelectedPageSize.Value;

            int skip = ((this.PageNumber - 1) * pagesize);
            int take = pagesize + 1;

            List<TEntity> listResult = (this.QueryExpression == null) ? new List<TEntity>() : this.QueryExpression.Skip(skip).Take(take).ToList();

            this.HasPrevRecord = (this.PageNumber > 1);
            this.HasNextRecord = (listResult.Count > pagesize);

            if (this.HasNextRecord)
                listResult.RemoveAt(listResult.Count - 1);
            
            listResult.ForEach(item => this.Items.Add(item));
        }

       

        #region Select Item & List Property (Global Event Handler içide Set edildi)

        #region Property IsMultiSelect

        private bool _IsMultiSelect;

        public bool IsMultiSelect
        {
            get { return this._IsMultiSelect; }
            set
            {
                if (!object.Equals(this._IsMultiSelect, value))
                {
                    this._IsMultiSelect = value;
                    this.OnPropertyChanged(() => this.IsMultiSelect);
                }
            }
        }

        #endregion

        #region Property SelectItem

        private TEntity _selectedItem;

        public TEntity SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                if (!object.Equals(this._selectedItem, value))
                {
                    this._selectedItem = value;
                    this.OnPropertyChanged(() => this.SelectedItem);
                }
            }
        }

        #endregion

        #region Property SelectedItems Collection

        private ObservableCollection<TEntity> _selecteditems;

        public ObservableCollection<TEntity> SelectedItems
        {
            get { return this._selecteditems = this._selecteditems ?? new ObservableCollection<TEntity>(); }
            set { this._selecteditems = value; }
        }

        #endregion

        #region Property Items Collection

        private ObservableCollection<TEntity> _items;

        public ObservableCollection<TEntity> Items
        {
            get { return this._items = this._items ?? new ObservableCollection<TEntity>(); }
            private set { this._items = value; }
        }

        #endregion

        #endregion

        #region Page Property

      

        #region Property IsAutoLoad

        private bool _IsAutoLoad;

        public bool IsAutoLoadData
        {
            get { return this._IsAutoLoad; }
            set
            {
                if (!object.Equals(this._IsAutoLoad, value))
                {
                    this._IsAutoLoad = value;
                    this.OnPropertyChanged(() => this.IsAutoLoadData);
                    this.Validator.Validate(() => this.IsAutoLoadData);
                }
            }
        }

        #endregion

        #region Property PageNumber

        private int _pageNumber = 1;

        public int PageNumber
        {
            get { return this._pageNumber; }
            set
            {
                if (!object.Equals(this._pageNumber, value) )
                {
                    this._pageNumber = value;
                    this.OnPropertyChanged(() => this.PageNumber);
                    this.Validator.Validate(() => this.PageNumber);
                }
            }
        }

        #endregion

        #region Property HasNextRecord

        private bool _hasNextRecord;

        public bool HasNextRecord
        {
            get { return this._hasNextRecord; }
            set
            {
                if (!object.Equals(this._hasNextRecord, value))
                {
                    this._hasNextRecord = value;
                    this.OnPropertyChanged(() => this.HasNextRecord);
                    this.Validator.Validate(() => this.HasNextRecord);
                }
            }
        }

        #endregion

        #region Property HasPrevRecord

        private bool _hasPrevRecord;

        public bool HasPrevRecord
        {
            get { return this._hasPrevRecord; }
            set
            {
                if (!object.Equals(this._hasPrevRecord, value))
                {
                    this._hasPrevRecord = value;
                    this.OnPropertyChanged(() => this.HasPrevRecord);
                    this.Validator.Validate(() => this.HasPrevRecord);
                }
            }
        }

        #endregion

        #region Page Size Property

        #region Property SelectedPageSize

        private Parameter _selectedPageSize;

        public Parameter SelectedPageSize
        {
            get { return this._selectedPageSize; }
            set
            {
                if (!object.Equals(this._selectedPageSize, value))
                {
                    this._selectedPageSize = value;
                    this.OnPropertyChanged(() => this.SelectedPageSize);
                    this.Validator.Validate(() => this.SelectedPageSize);
                }
            }
        }

        #endregion

        #region Property ListPageSize Collection

        private ObservableCollection<Parameter> _ListPageSize;

        public ObservableCollection<Parameter> ListPageSize
        {
            get { return this._ListPageSize = this._ListPageSize ?? new ObservableCollection<Parameter>(); }
            private set { this._ListPageSize = value; }
        }

        #endregion

        #endregion

        #endregion

        #region Page Command

        #region Property NextPageCommand

        private RelayCommand _nextPageCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand NextPageCommand
        {
            get
            {
                if (this._nextPageCommand == null)
                    this._nextPageCommand = new RelayCommand(ExcuteNextPageCommand);
                return this._nextPageCommand;
            }
        }

        void ExcuteNextPageCommand()
        {
            if (this.HasNextRecord)
            {
                this.PageNumber++;
                LoadData();
            }
        }

        #endregion

        #region Property PrevPageCommand

        private RelayCommand _prevPageCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand PrevPageCommand
        {
            get
            {
                if (this._prevPageCommand == null)
                    this._prevPageCommand = new RelayCommand(ExcutePrevPageCommand);
                return this._prevPageCommand;
            }
        }

        void ExcutePrevPageCommand()
        {
            if (this.HasPrevRecord)
            {
                this.PageNumber--;
                LoadData();
            }
        }

        #endregion

        

        #endregion

        public void OpenRecord(IDataItem SelectedRecord)
        {
            this.OpenRecord((TEntity)SelectedRecord);
        }

        public void OpenRecord(TEntity DataModel)
        {
            ViewModelBase viewModel = (ViewModelBase)Activator.CreateInstance(typeof(TEntityForm), DataModel);

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = viewModel;
            windowParam.OnClose = (result) =>
            {
                if (result)
                {
                    LoadData();
                }
            };

            this.ServicePresenter.OpenWindow(windowParam);
        }

        #region Entity Command

        #region MouseDoubleClickCommand

        private RelayCommand _mouseDoubleClickCommand;
        public RelayCommand MouseDoubleClickCommand
        {
            get
            {
                return this._mouseDoubleClickCommand;
            }
            set
            {
                _mouseDoubleClickCommand = value;
            }
        }

        #endregion

        
        
        #region Property NewRecordCommand

        private RelayCommand _newRecordCommand;
        public RelayCommand NewRecordCommand
        {
            get
            {
                if (this._newRecordCommand == null)
                    this._newRecordCommand = new RelayCommand(ExcuteNewRecordCommand);
                return this._newRecordCommand;
            }
        }

        protected virtual void ExcuteNewRecordCommand()
        {
            TEntity DataModel = Activator.CreateInstance<TEntity>();

            if (this.OnOpening!=null)
                this.OnOpening((TEntity)DataModel);

            OpenRecord(DataModel);
        }


        #endregion

        #region Property OpenRecordCommand

        private RelayCommand _openRecordCommand;
        public RelayCommand OpenRecordCommand
        {
            get
            {
                if (this._openRecordCommand == null)
                    this._openRecordCommand = new RelayCommand(ExecuteOpenRecordCommand);
                return this._openRecordCommand;
            }
        }

        void ExecuteOpenRecordCommand()
        {
            if (this.SelectedItem is IDataItem)
            {
                OpenRecord(this.SelectedItem);
            }
        }

        #endregion

        #region Property AppendRecordCommand

        private RelayCommand _appendrecordcommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand AppendRecordCommand
        {
            get
            {
                if (this._appendrecordcommand == null)
                    this._appendrecordcommand = new RelayCommand(ExcuteAppendRecordCommand);
                return this._appendrecordcommand;
            }
        }

        void ExcuteAppendRecordCommand()
        {
            LookupAppend.OpenPopup();
        }

        void _lookupappend_Closed(IEnumerable<IDataItem> Selectedltems)
        {
            if (Selectedltems != null)
            {
                List<TEntity> inserted = new List<TEntity>();

                foreach (var item in Selectedltems)
                {
                    var newitem = (TEntity)item;

                    if (!this.Items.Contains(newitem))
                    {
                        inserted.Add(newitem);
                    }
                }

                if (this.OnInserted != null && inserted.Any())
                    this.OnInserted(inserted);
            }
        }


        #region Property Lookup

        private LookupViewModel<IDataItem> _lookupappend;

        public LookupViewModel<IDataItem> LookupAppend
        {
            get
            {
                if (this._lookupappend == null)
                {
                    this._lookupappend = new LookupViewModel<IDataItem>(this.GetType()) { Title = "Kayıt Ekle" };
                    this._lookupappend.Closed += new ClosedPopup(_lookupappend_Closed);
                    this._lookupappend.ListViewModel.IsNewCommand = false;
                    this._lookupappend.ListViewModel.IsOpenCommand = false;
                    this._lookupappend.IsCleanCommand = false;
                    this._lookupappend.IsMultiSelect = true; ;
                }

                return this._lookupappend;
            }
        }

        

        

        #endregion

        #endregion

        #region Property DeleteRecordCommand

        private RelayCommand _deleteRecordCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand DeleteRecordCommand
        {
            get
            {
                if (this._deleteRecordCommand == null)
                    this._deleteRecordCommand = new RelayCommand(ExcuteDeleteRecordCommand);
                return this._deleteRecordCommand;
            }
        }

        void ExcuteDeleteRecordCommand()
        {
            if (this.SelectedItems != null)
            {
                if (this.OnDeleted != null)
                    this.OnDeleted(this.SelectedItems);
            }
            else
            {
                this.ServicePresenter.ShowAlertMessage("Lütfen Kayıt Seçiniz.");
            }
        }

        #endregion

        #endregion


        #region Command Visibility

        #region Property IsNewCommand

        private bool isNewCommand;

        public bool IsNewCommand
        {
            get { return this.isNewCommand; }
            set
            {
                if (!object.Equals(this.isNewCommand, value))
                {
                    this.isNewCommand = value;
                    this.OnPropertyChanged(() => this.IsNewCommand);
                }
            }
        }

        #endregion

        #region Property IsAppendCommand

        private bool isAppendCommand;

        public bool IsAppendCommand
        {
            get { return this.isAppendCommand; }
            set
            {
                if (!object.Equals(this.isAppendCommand, value))
                {
                    this.isAppendCommand = value;
                    this.OnPropertyChanged(() => this.IsAppendCommand);
                }
            }
        }

        #endregion

        #region Property IsDeleteCommand

        private bool isDeleteCommand;

        public bool IsDeleteCommand
        {
            get { return this.isDeleteCommand; }
            set
            {
                if (!object.Equals(this.isDeleteCommand, value))
                {
                    this.isDeleteCommand = value;
                    this.OnPropertyChanged(() => this.IsDeleteCommand);
                }
            }
        }

        #endregion

        #region Property IsOpenCommand

        private bool isOpenCommand;

        public bool IsOpenCommand
        {
            get { return this.isOpenCommand; }
            set
            {
                if (!object.Equals(this.isOpenCommand, value))
                {
                    this.isOpenCommand = value;
                    this.OnPropertyChanged(() => this.IsOpenCommand);
                }
            }
        }

        #endregion

        #endregion

        #region IDataItem

        IDataItem IDataList.SelectedItem
        {
            get
            {
                return this.SelectedItem;
            }
            set
            {
                this.SelectedItem = (TEntity)value;
            }
        }

        IEnumerable<IDataItem> IDataList.SelectedItems
        {
            get
            {
                return (IEnumerable<IDataItem>)this.SelectedItems;
            }
        }


        IEnumerable<IDataItem> IDataList.Items
        {
            get 
            {
                return (IEnumerable<IDataItem>)this.Items; 
            }
        }

        #endregion

    }
}
