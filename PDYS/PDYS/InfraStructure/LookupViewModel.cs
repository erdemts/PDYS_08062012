using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;
using Mvvm;
using PDYS.Services;
using PDYS.Interfaces;
using System.Linq.Expressions;
using System.Reflection;
using PDYS.Services.ServiceParam;

namespace PDYS.InfraStructure
{
    public class LookupViewModel<TListViewModel> : ViewModelBase, IPopup
    {

        public event ClosedPopup Closed;
        public event Action Clean;

        public LookupViewModel()
            : this(typeof(TListViewModel))
        {
            this.IsCleanCommand = true;
        }

        public LookupViewModel(Type ListViewModelType)
        {
            this.ListViewModel = (IDataList)Activator.CreateInstance(ListViewModelType, new object[] { false });
            this.ListViewModel.MouseDoubleClickCommand = new RelayCommand(ExecuteMouseDoubleClickCommand);
            this.ListViewModel.IsMultiSelect = this.IsMultiSelect;

            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(LookupViewModel_PropertyChanged);
        }

        void LookupViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => this.IsMultiSelect))
            {
                if (this.ListViewModel != null)
                    this.ListViewModel.IsMultiSelect = this.IsMultiSelect;
            }
        }

        public IDataList ListViewModel { get; private set; }

        public Expression<Func<IDataItem, IDataItem>> ColumnProperty { get; set; }

        #region MouseDoubleClickCommand

        void ExecuteMouseDoubleClickCommand()
        {
            this.AcceptCommand.Execute();
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

        public void OpenSelectedRecord(IDataItem SelectedItem)
        {
            if (this.ListViewModel != null && SelectedItem != null)
                this.ListViewModel.OpenRecord(SelectedItem);
        }

        public void OpenPopup()
        {

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = this;

            this.ServicePresenter.OpenLookupWindow(windowParam);
        }


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
                    this._acceptCommand = new RelayCommand(ExecuteAcceptCommand);
                return this._acceptCommand;
            }
        }

        void ExecuteAcceptCommand()
        {
            if (this.ListViewModel.SelectedItems.Any())
            {
                this.Closed(this.ListViewModel.SelectedItems);
                this.ServicePresenter.CloseWindow(true);
            }

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
            this.ServicePresenter.CloseWindow(false);
        }

        #endregion

        #region Clean Command

        private RelayCommand _cleanCommand;
        /// <summary>
        /// Clean Command
        /// </summary>
        public RelayCommand CleanCommand
        {
            get
            {
                if (this._cleanCommand == null)
                    this._cleanCommand = new RelayCommand(ExcuteCleanCommand);
                return this._cleanCommand;
            }
        }

        void ExcuteCleanCommand()
        {
            if (this.Clean != null)
                this.Clean();

            this.ServicePresenter.CloseWindow(true);
        }

        #endregion

        #region Property IsCleanCommand

        private bool isCleanCommand;

        public bool IsCleanCommand
        {
            get { return this.isCleanCommand; }
            set
            {
                if (!object.Equals(this.isCleanCommand, value))
                {
                    this.isCleanCommand = value;
                    this.OnPropertyChanged(() => this.IsCleanCommand);
                }
            }
        }

        #endregion
    }
}
