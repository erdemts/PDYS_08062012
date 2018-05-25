using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using System.Collections.ObjectModel;
using PDYS.Interfaces;
using PDYS.Services.ServiceParam;
using DeviceManagement;
using System.Windows.Threading;
using System.Threading;
using System.Globalization;
using Mvvm.Validation;
using PDYS.Helper;

namespace PDYS.ViewModels
{


    class SelectInOutScoringViewModel : EditViewModelBase<ListItemViewModel>
    {
        public SelectInOutScoringViewModel()
            : base(null)
        {
            this.Title = "Puantaj Parametre Seçimi";
            this.AcceptButtonTitle = "Tamam";

            this.IsAllEmployee = true;
            this.IsAllDate = true;

            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SelectInOutScoringViewModel_PropertyChanged);
        }

        void SelectInOutScoringViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => this.IsSelectableEmployee))
            {
                if (!this.IsSelectableEmployee)
                    this.Employee = null;
                else
                    this.Validator.Validate(() => this.Employee);

            }


            else if (e.PropertyName == this.GetPropertyName(() => this.IsSelectableDate))
            {
                if (!this.IsSelectableDate)
                {
                    this.StartDate = null;
                    this.EndDate = null;
                }
                else
                {
                    this.StartDate = DateTime.Now;
                    this.EndDate = DateTime.Now;
                }
            }

        }

        protected override void InitValidation()
        {
            this.Validator.AddRule(() => this.Employee, () =>
                {
                    if (this.IsSelectableEmployee && this.Employee == null)
                        return RuleResult.Invalid(ValidationMessage.RequiredText("Personel"));
                    else
                        return RuleResult.Valid();
                });


            this.Validator.AddRule(() => this.StartDate, () =>
               {
                   if (this.IsSelectableDate && !this.StartDate.HasValue)
                       return RuleResult.Invalid(ValidationMessage.RequiredText("Başlangıç Tarihi"));
                   else
                       return RuleResult.Valid();
               });

            this.Validator.AddRule(() => this.EndDate, () =>
            {
                if (this.IsSelectableDate && !this.EndDate.HasValue)
                    return RuleResult.Invalid(ValidationMessage.RequiredText("Bitiş Tarihi"));
                else
                    return RuleResult.Valid();
            });
        }


        public static void OpenSelectionWindow(Action<Employee, DateTime?, DateTime?> selectaction)
        {
            SelectInOutScoringViewModel modelview = new SelectInOutScoringViewModel();

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = modelview;
            windowParam.OnClose = (result) =>
            {
                if (result)
                {
                    if (modelview.StartDate.HasValue && modelview.EndDate.HasValue)
                        selectaction(modelview.Employee, modelview.StartDate.Value.Date, modelview.EndDate.Value.Date);
                    else
                        selectaction(modelview.Employee, null, null);
                }
            };

            modelview.ServicePresenter.OpenWindow(windowParam);

        }

        #region Page Property

        #region Property Employee

        private Employee _employee;

        public Employee Employee
        {
            get { return this._employee; }
            set
            {
                    this._employee = value;
                    this.OnPropertyChanged(() => this.Employee);
                    this.Validator.Validate(() => this.Employee);
            }
        }

        #endregion

        #region Property LookupEmployee

        private LookupViewModel<EmployeeListViewModel> _lookupemployee;

        public LookupViewModel<EmployeeListViewModel> LookupEmployee
        {
            get
            {
                if (this._lookupemployee == null)
                    this._lookupemployee = new LookupViewModel<EmployeeListViewModel>() { Title = "Personel Seçimi" };

                return this._lookupemployee;
            }
        }

        #endregion


        #region Property StartDate

        private DateTime? _StartDate;

        public DateTime? StartDate
        {
            get { return this._StartDate; }
            set
            {
                    this._StartDate = value;
                    this.OnPropertyChanged(() => this.StartDate);
                    this.Validator.Validate(() => this.StartDate);
            }
        }

        #endregion

        #region Property EndDate

        private DateTime? _EndDate;

        public DateTime? EndDate
        {
            get { return this._EndDate; }
            set
            {
                this._EndDate = value;
                this.OnPropertyChanged(() => this.EndDate);
                this.Validator.Validate(() => this.EndDate);
            }
        }

        #endregion

        #endregion

        #region UI Property

        #region Property IsAllEmployee

        private bool _IsAllEmployee;

        public bool IsAllEmployee
        {
            get { return this._IsAllEmployee; }
            set
            {
                if (!object.Equals(this._IsAllEmployee, value))
                {
                    this._IsAllEmployee = value;
                    this.OnPropertyChanged(() => this.IsAllEmployee);
                }
            }
        }

        #endregion

        #region Property IsSelectableEmployee

        private bool _IsSelectableEmployee;

        public bool IsSelectableEmployee
        {
            get { return this._IsSelectableEmployee; }
            set
            {
                if (!object.Equals(this._IsSelectableEmployee, value))
                {
                    this._IsSelectableEmployee = value;
                    this.OnPropertyChanged(() => this.IsSelectableEmployee);
                }
            }
        }

        #endregion

        #region Property IsAllDate

        private bool _IsAllDate;

        public bool IsAllDate
        {
            get { return this._IsAllDate; }
            set
            {
                if (!object.Equals(this._IsAllDate, value))
                {
                    this._IsAllDate = value;
                    this.OnPropertyChanged(() => this.IsAllDate);
                }
            }
        }

        #endregion

        #region Property IsSelectableDate

        private bool _IsSelectableDate;

        public bool IsSelectableDate
        {
            get { return this._IsSelectableDate; }
            set
            {
                if (!object.Equals(this._IsSelectableDate, value))
                {
                    this._IsSelectableDate = value;
                    this.OnPropertyChanged(() => this.IsSelectableDate);
                }
            }
        }

        #endregion

        #endregion

        protected override void ExcuteAcceptCommand()
        {
            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            this.ServicePresenter.CloseWindow(true);
        }

    }


}
