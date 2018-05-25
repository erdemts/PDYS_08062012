using System;
using System.Linq;
using PDYS.InfraStructure;
using System.Data.Entity;
using PDYS.Models;
using PDYS.Report.Design;
using System.Collections.Generic;
using PDYS.Report.Model;
using PDYS.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using Mvvm.Validation;


namespace PDYS.Report.ViewModels
{
    public class RptEmployeeSalaryScoringViewModel : ReportViewModelBase<RptEmployeeSalaryScoring>
    {

        public RptEmployeeSalaryScoringViewModel()
        {
            this.Loaded += new Action(RptEmployeeSalaryScoringViewModel_Loaded);
        }

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Personal, "Personel Bilgisi Zorunlu Alan.");
            this.Validator.AddRequiredRule(() => this.SelectYear, "Yıl Bilgisi Zorunlu Alan.");
            this.Validator.AddRequiredRule(() => this.SelectMonth, "Ay Bilgisi Zorunlu Alan.");

            this.Validator.ValidateAll();
        }

        void RptEmployeeSalaryScoringViewModel_Loaded()
        {
            #region Month & Year

            this.ListYear.Clear();
            this.ListYear.Add(new ListItemViewModel(DateTime.Now.Year, DateTime.Now.Year.ToString()));
            this.ListYear.Add(new ListItemViewModel(DateTime.Now.Year - 1, (DateTime.Now.Year - 1).ToString()));
            
            
            this.ListMonth.Clear();

            string[] months = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames;

            for (int index = 0; index < months.Length - 1; index++)
            {
                this.ListMonth.Add(new ListItemViewModel(index + 1, months[index]));
            }

            #endregion    
        }

        #region Parameter

        #region Property Personal

        private Employee _personal;

        public Employee Personal
        {
            get { return this._personal; }
            set
            {
                if (!object.Equals(this._personal, value))
                {
                    this._personal = value;
                    this.OnPropertyChanged(() => this.Personal);
                    this.Validator.Validate(() => this.Personal);
                }
            }
        }

        #endregion

        #region Property LookupPersonal

        private LookupViewModel<EmployeeListViewModel> _lookupPersonal;

        public LookupViewModel<EmployeeListViewModel> LookupPersonal
        {
            get
            {
                if (this._lookupPersonal == null)
                    this._lookupPersonal = new LookupViewModel<EmployeeListViewModel>() { Title = "Personel Seçimi" };

                return this._lookupPersonal;
            }
        }

        #endregion

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

        #region Property ListYear Collection

        ObservableCollection<ListItemViewModel> _ListYear;
        public ObservableCollection<ListItemViewModel> ListYear
        {
            get { return this._ListYear = this._ListYear ?? new ObservableCollection<ListItemViewModel>(); }
            private set { this._ListYear = value; }
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

        #endregion


        protected override void ExcuteExecuteReportCommand()
        {
            ValidationResult valResult = this.Validator.ValidateAll();

            if (!valResult.IsValid)
            {
                this.ServicePresenter.ShowAlertMessage(valResult.ToString());
                return;
            }

            base.ExcuteExecuteReportCommand();
        }

        public override System.Collections.IEnumerable LoadReportDataSource()
        {
            var query = PDYSEntities.DataContext.EmployeeInOutScoringSet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeSalaryScoring.EmployeeID == this.Personal.ID);

            if (this.SelectYear != null && this.SelectYear.ID != -1)
                query = query.Where(item => item.EmployeeSalaryScoring.PeriodYear == this.SelectYear.ID);

            if (this.SelectMonth != null && this.SelectMonth.ID != -1)
                query = query.Where(item => item.EmployeeSalaryScoring.PeriodMonth == this.SelectMonth.ID);

            query = query.OrderBy(item => item.ScoringDate);

            #endregion

            List<EmployeeInOutScoring> result = query.ToList();
                                                   
            return result;
        }
    }
}
