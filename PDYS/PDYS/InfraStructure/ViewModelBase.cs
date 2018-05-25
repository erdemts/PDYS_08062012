using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvvm;
using System.ComponentModel;
using Mvvm.Validation;
using PDYS.Services;
using System.Linq.Expressions;
using PDYS.Helper;
using PDYS.Interfaces;



namespace PDYS.InfraStructure
{
    [Serializable]
    public abstract class ViewModelBase : ObservableObject, IDataErrorInfo, IValidatable
    {
        
        [NonSerialized]
        private ValidationHelper _validator;
        public ValidationHelper Validator
        {
            get { return _validator; }
            private set { _validator = value; }
        }

        [NonSerialized]
        private DataErrorInfoAdapter _dataerrorinfoadapter;
        public DataErrorInfoAdapter DataErrorInfoAdapter
        {
            get { return _dataerrorinfoadapter; }
            private set { _dataerrorinfoadapter = value; }
        }

        [NonSerialized]
        private IDialogService _servicepresenter;
        public IDialogService ServicePresenter
        {
            get { return _servicepresenter; }
            private set { _servicepresenter = value; }
        }
        


        public event Action Loaded;

        public ViewModelBase()
        {
           
           
            this.ServicePresenter = new DialogServiceSender();

            this.Validator = new ValidationHelper();
            this.DataErrorInfoAdapter = new DataErrorInfoAdapter(Validator);
            
            // Validation Process
            this.ValidationNotification();
            this.InitValidation();
            
            //this.Loaded += new Action(ViewModelBase_Loaded);
            
            ThreadingUtils.RunOnUI(internalLoad);
            
        }

        void internalLoad()
        {
            if (this.Loaded != null)
                this.Loaded();

            this.Validator.ValidateAllAsync();
        }

        void ViewModelBase_Loaded()
        {
            this.Validator.ValidateAllAsync();
        }


        #region Property OnLoadCommand
        [NonSerialized]
        private RelayCommand _onloadCommand;
        public RelayCommand OnLoadCommand
        {
            get
            {
                if (this._onloadCommand == null)
                    this._onloadCommand = new RelayCommand(ExcuteOnLoadCommand);
                return this._onloadCommand;
            }
        }

        bool isLoaded = false;
        void ExcuteOnLoadCommand()
        {
            if (this.Loaded != null && !isLoaded)
            {
                isLoaded = true;
                //Loaded();
                App.Current.Dispatcher.BeginInvoke(
                    new Action(() => { 
                        Loaded(); 
                        this.Validator.ValidateAllAsync(); 
                        }), 
                    System.Windows.Threading.DispatcherPriority.Background,
                    null);
            }
        }

        #endregion

        protected virtual void InitValidation()
        { }


        private void ValidationNotification()
        {
            Validator.ResultChanged += (o, e) =>
            {
                var propertyExpression = e.Expression;

                if (propertyExpression != null && propertyExpression.Body is System.Linq.Expressions.MemberExpression)
                {
                    this.OnPropertyChanged(propertyExpression);
                }
            };
        }

        protected string GetPropertyName<T>(Expression<Func<T>> prop)
        {
            return Common.PropertyName.For(prop);
        }

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get { return DataErrorInfoAdapter.Error; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return DataErrorInfoAdapter[columnName]; }
        }

        #endregion

        #region IValidatable

        void IValidatable.Validate(Action<ValidationResult> onCompleted)
        {
            Validator.ValidateAllAsync(onCompleted);
        }

        #endregion
    }
}
