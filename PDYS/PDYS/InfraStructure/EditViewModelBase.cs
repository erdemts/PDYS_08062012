using System.Linq;
using Mvvm;
using PDYS.Models;
using System.Data.Entity;
using System.Collections.ObjectModel;
using PDYS.Interfaces;
using System;
using System.Text;

namespace PDYS.InfraStructure
{
    public abstract class EditViewModelBase<TEntity> : ViewModelBase where TEntity : IDataItem
    {

        public EditViewModelBase(TEntity DataModel)
        {
            if (DataModel is Author)
                VisibleState = true;

            this.DataModel = (DataModel != null) ? DataModel : Activator.CreateInstance<TEntity>(); ;
            Initialize();
        }

        void Initialize()
        {
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";

            this.Loaded += new System.Action(OnLoad);
        }

        void OnLoad()
        {
            #region Load State Parameter

            if (DataModel is Author)
            {
                var queryState = from item in PDYSEntities.DataContext.ParameterSet
                                 where item.Name == "State"
                                 orderby item.Text
                                 select item;

                queryState.ToList().ForEach(item => this.ListState.Add(item));

                this.SelectedState = this.ListState.FirstOrDefault();
            }

            #endregion
        }

        #region Property DataModel

        private TEntity _dataItem;

        public TEntity DataModel
        {
            get { return this._dataItem; }
            set
            {
                if (!object.Equals(this._dataItem, value))
                {
                    this._dataItem = value;
                    this.OnPropertyChanged(() => this.DataModel);
                    //this.Validator.Validate(() => this.DataItem);
                }
            }
        }

        #endregion

        #region Property IsSavedData

        private bool isSavedData;

        public bool IsSavedData
        {
            get { return this.isSavedData; }
            set
            {
                if (!object.Equals(this.isSavedData, value))
                {
                    this.isSavedData = value;
                    this.OnPropertyChanged(() => this.IsSavedData);
                }
            }
        }

        #endregion

        #region Property Title

        private string _title;

        public string Title
        {
            get { return this._title; }
            set
            {
                if (!object.Equals(this._title, value))
                {
                    this._title = value;
                    this.OnPropertyChanged(() => this.Title);
                    this.Validator.Validate(() => this.Title);
                }
            }
        }

        #endregion

        #region State Propperty & List

        #region Property SelectedState

        private Parameter _selectedState;

        public Parameter SelectedState
        {
            get { return this._selectedState; }
            set
            {
                if (!object.Equals(this._selectedState, value))
                {
                    this._selectedState = value;
                    this.OnPropertyChanged(() => this.SelectedState);
                    this.Validator.Validate(() => this.SelectedState);
                }
            }
        }

        #endregion

        #region Property ListState Collection

        private ObservableCollection<Parameter> _listState;

        public ObservableCollection<Parameter> ListState
        {
            get { return this._listState = this._listState ?? new ObservableCollection<Parameter>(); }
            private set { this._listState = value; }
        }

        #endregion

        #endregion

        #region Accept & Cancel & VisibleState

        #region Property VisibleState

        private bool _VisibleState;

        public bool VisibleState
        {
            get { return this._VisibleState; }
            set
            {
                if (!object.Equals(this._VisibleState, value))
                {
                    this._VisibleState = value;
                    this.OnPropertyChanged(() => this.VisibleState);
                }
            }
        }

        #endregion

        #region Property AcceptButtonTitle

        private string _acceptButtonTitle;

        public string AcceptButtonTitle
        {
            get { return this._acceptButtonTitle; }
            set
            {
                if (!object.Equals(this._acceptButtonTitle, value))
                {
                    this._acceptButtonTitle = value;
                    this.OnPropertyChanged(() => this.AcceptButtonTitle);
                    this.Validator.Validate(() => this.AcceptButtonTitle);
                }
            }
        }

        #endregion

        #region Property CancelCommandTitle

        private string _cancelCommandTitle;

        public string CancelCommandTitle
        {
            get { return this._cancelCommandTitle; }
            set
            {
                if (!object.Equals(this._cancelCommandTitle, value))
                {
                    this._cancelCommandTitle = value;
                    this.OnPropertyChanged(() => this.CancelCommandTitle);
                    this.Validator.Validate(() => this.CancelCommandTitle);
                }
            }
        }

        #endregion

        #region Accept Command

        private RelayCommand _acceptCommand;
        /// <summary>
        /// Save & Close Window Command
        /// </summary>
        public RelayCommand AcceptCommand
        {
            get
            {
                if (this._acceptCommand == null)
                    this._acceptCommand = new RelayCommand(ExcuteAcceptCommand);
                return this._acceptCommand;
            }
        }

        protected virtual void ExcuteAcceptCommand()
        {
            this.ServicePresenter.CloseWindow(true);
        }

        #endregion

        #region Cancel Command

        private RelayCommand _cancelCommand;
        /// <summary>
        /// Close Window Command
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (this._cancelCommand == null)
                    this._cancelCommand = new RelayCommand(ExcuteCancelCommand);
                return this._cancelCommand;
            }
        }

        protected virtual void ExcuteCancelCommand()
        {
            this.ServicePresenter.CloseWindow(IsSavedData);
        }

        #endregion

        #region Property InfoCommand

        private RelayCommand _InfoCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand InfoCommand
        {
            get
            {
                if (this._InfoCommand == null)
                    this._InfoCommand = new RelayCommand(ExcuteInfoCommand);
                return this._InfoCommand;
            }
        }

        void ExcuteInfoCommand()
        {
            Author author = this.DataModel as Author;
            if (author != null)
            {
                StringBuilder sb = new StringBuilder();

                string createduser = (author.CreatedBy == null) ? string.Empty : author.CreatedBy.DisplayName;
                string modifieduser = (author.CreatedBy == null) ? string.Empty : author.ModifiedBy.DisplayName;


                sb.AppendLine("Oluşturan Kullanıcı  : " + createduser);
                sb.AppendLine("Oluşturma Tarihi     : " + string.Format("{0:dd.MM.yyyy HH:mm}", author.CreatedOn));
                sb.AppendLine("");
                sb.AppendLine("Değiştiren Kullanıcı : " + modifieduser);
                sb.AppendLine("Değiştirme Tarihi    : " + string.Format("{0:dd.MM.yyyy HH:mm}", author.ModifiedOn));
                sb.AppendLine("");
                sb.AppendLine("Kayıt ID              : " + ((this.DataModel != null && this.DataModel.ID > 0) ? this.DataModel.ID.ToString() : ""));

                this.ServicePresenter.ShowInformationMessage(sb.ToString());
            }
        }

        #endregion

        #endregion



    }
}
