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
    public abstract class BaseWindowViewModelBase : ViewModelBase
    {

        public BaseWindowViewModelBase()
        {
            Initialize();
        }

        void Initialize()
        {
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";
        }

        

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

        #region Accept & Cancel & VisibleState

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
            this.ServicePresenter.CloseWindow(false);
        }

        #endregion

        #endregion


     
    }
}
