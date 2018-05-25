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


    class SelectSalaryScoringDateViewModel : EditViewModelBase<ListItemViewModel>
    {
        public SelectSalaryScoringDateViewModel()
            : base(null)
        {
            this.Title = "Hakediş Parametre Seçimi";
            this.AcceptButtonTitle = "Tamam";
            this.IsAllEmployee = true;

            this.Loaded += new Action(SelectSalaryScoringDateViewModel_Loaded);
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SelectSalaryScoringDateViewModel_PropertyChanged);
        }

        void SelectSalaryScoringDateViewModel_Loaded()
        {
            this.Years.Clear();
            this.Years.Add(new ListItemViewModel(DateTime.Now.Year, DateTime.Now.Year.ToString()));
            this.Years.Add(new ListItemViewModel(DateTime.Now.Year - 1, (DateTime.Now.Year - 1).ToString()));

            this.SelectYear = this.Years.FirstOrDefault(y => y.ID == DateTime.Now.Year);

            this.ListMonth.Clear();

            string[] months = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames;

            for (int index = 0; index < months.Length - 1; index++)
            {
                this.ListMonth.Add(new ListItemViewModel(index + 1, months[index]));
            }

            this.SelectMonth = this.ListMonth.FirstOrDefault(m => m.ID == DateTime.Now.Month);
        }

        void SelectSalaryScoringDateViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => this.IsSelectableEmployee))
            {
                if (!this.IsSelectableEmployee)
                    this.Employee = null;
                else
                    this.Validator.Validate(() => this.Employee);

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

        }


        public static void OpenSelectionWindow(Action<Employee,int, int, string> selectaction)
        {
            SelectSalaryScoringDateViewModel modelview = new SelectSalaryScoringDateViewModel();
            
            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = modelview;
            windowParam.OnClose = (result) =>
            {
                if (result)
                {
                    selectaction(modelview.Employee, modelview.SelectYear.ID, modelview.SelectMonth.ID, modelview.SelectMonth.DisplayName);
                }
            };

            modelview.ServicePresenter.OpenWindow(windowParam);

        }


        #region Page Property

        #region Property SelectYear

        private ListItemViewModel _SelectYear;

        public ListItemViewModel SelectYear
        {
            get { return this._SelectYear; }
            set
            {
                if (!object.Equals(this._SelectYear, value))
                {
                    this._SelectYear = value;
                    this.OnPropertyChanged(() => this.SelectYear);
                    this.Validator.Validate(() => this.SelectYear);
                }
            }
        }

        #endregion

        #region Years

        ObservableCollection<ListItemViewModel> _years;
        public ObservableCollection<ListItemViewModel> Years
        {
            get { return this._years = this._years ?? new ObservableCollection<ListItemViewModel>(); }
            private set { this._years = value; }
        }

        #endregion


        #region Property SelectMonth

        private ListItemViewModel _SelectMonth;

        public ListItemViewModel SelectMonth
        {
            get { return this._SelectMonth; }
            set
            {
                if (!object.Equals(this._SelectMonth, value))
                {
                    this._SelectMonth = value;
                    this.OnPropertyChanged(() => this.SelectMonth);
                    this.Validator.Validate(() => this.SelectMonth);
                }
            }
        }

        #endregion

        #region Property ListMonth Collection

        private ObservableCollection<ListItemViewModel> _ListMonth;

        public ObservableCollection<ListItemViewModel> ListMonth
        {
            get { return this._ListMonth = this._ListMonth ?? new ObservableCollection<ListItemViewModel>(); }
            private set { this._ListMonth = value; }
        }

        #endregion

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
        #endregion 

    }

   
}
