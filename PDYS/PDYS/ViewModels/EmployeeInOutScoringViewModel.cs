using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using Mvvm;
using Mvvm.Validation;
using PDYS.Helper;
using PDYS.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DeviceManagement;
using System.Globalization;

namespace PDYS.ViewModels
{
    public class EmployeeInOutScoringViewModel : EditViewModelBase<EmployeeInOutScoring>
    {

        public EmployeeInOutScoringViewModel(EmployeeInOutScoring DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Puantaj Bilgisi";
            if (this.DataModel.ID == 0)
            {
                this.DataModel.ProcessState = 1;
            }


            this.Loaded += new Action(InternalLoaded);

            #region Input Output List


            this.EmployeeInputOutputListVM.IsNewCommand = false;
            this.EmployeeInputOutputListVM.IsOpenCommand = false;
            this.EmployeeInputOutputListVM.IsAppendCommand = false;
            this.EmployeeInputOutputListVM.IsDeleteCommand = false;
            this.EmployeeInputOutputListVM.IsMultiSelect = false;

            this.EmployeeInputOutputListVM.QueryExpression = this.DataModel.EmployeeInputOutputs.AsQueryable().OrderBy(item => item.InOutDate);

            #endregion
        }


        void InternalLoaded()
        {
            #region Load ProcessState Parameter

            this.ListState.Clear();

            var queryState = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "ProcessState"
                             orderby item.Text
                             select item;

            queryState.ToList().ForEach(item => this.ListState.Add(item));

            #endregion

            this.SetViewModel(this.DataModel);

            this.EmployeeInputOutputListVM.LoadData();
        }

    



        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Employee, ValidationMessage.RequiredText("Personel"));
            this.Validator.AddRequiredRule(() => this.ScoringDate, ValidationMessage.RequiredText("Puantaj Tarihi"));

            this.Validator.AddRule(() => this.WeeklyOvertime, () =>
            {
                if (!this.IsWeeklyOvertime)
                    return RuleResult.Valid();
                else
                    return (this.WeeklyOvertime != null) ? RuleResult.Valid() : RuleResult.Invalid(ValidationMessage.RequiredText("Haftalık Mesai"));
            });


            this.Validator.AddRule(() => this.OutSourceOvertime, () =>
            {
                if (!this.IsOutSourceOvertime)
                    return RuleResult.Valid();
                else
                    return (this.OutSourceOvertime != null) ? RuleResult.Valid() : RuleResult.Invalid(ValidationMessage.RequiredText("Kümulatif Mesai"));
            });
        }

        #region Page Property

        #region Property Employee

        private Employee _employee;

        public Employee Employee
        {
            get { return this._employee; }
            set
            {
                if (!object.Equals(this._employee, value))
                {
                    this._employee = value;
                    this.OnPropertyChanged(() => this.Employee);
                    this.Validator.Validate(() => this.Employee);
                }
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


        #region Property WeeklyOvertime

        private WeeklyOvertime _weeklyovertime;

        public WeeklyOvertime WeeklyOvertime
        {
            get { return this._weeklyovertime; }
            set
            {
                if (!object.Equals(this._weeklyovertime, value))
                {
                    this._weeklyovertime = value;
                    this.OnPropertyChanged(() => this.WeeklyOvertime);
                    this.Validator.Validate(() => this.WeeklyOvertime);
                }
            }
        }

        #endregion

        #region Property LookupWeeklyOvertime

        private LookupViewModel<WeeklyOvertimeListViewModel> _lookupweeklyovertime;

        public LookupViewModel<WeeklyOvertimeListViewModel> LookupWeeklyOvertime
        {
            get
            {
                if (this._lookupweeklyovertime == null)
                    this._lookupweeklyovertime = new LookupViewModel<WeeklyOvertimeListViewModel>() { Title = "Haftalık Mesai Seçimi" };

                return this._lookupweeklyovertime;
            }
        }

        #endregion


        #region Property OutSourceOvertime

        private OutSourceOvertime _outsourceovertime;

        public OutSourceOvertime OutSourceOvertime
        {
            get { return this._outsourceovertime; }
            set
            {
                if (!object.Equals(this._outsourceovertime, value))
                {
                    this._outsourceovertime = value;
                    this.OnPropertyChanged(() => this.OutSourceOvertime);
                    this.Validator.Validate(() => this.OutSourceOvertime);
                }
            }
        }

        #endregion

        #region Property LookupOutSourceOvertime

        private LookupViewModel<OutSourceOvertimeListViewModel> _lookupoutsourceovertime;

        public LookupViewModel<OutSourceOvertimeListViewModel> LookupOutSourceOvertime
        {
            get
            {
                if (this._lookupoutsourceovertime == null)
                    this._lookupoutsourceovertime = new LookupViewModel<OutSourceOvertimeListViewModel>() { Title = "Kümulatif Mesai Seçimi" };

                return this._lookupoutsourceovertime;
            }
        }

        #endregion

        #region Property IsManualEdit

        private bool _IsManualEdit;

        public bool IsManualEdit
        {
            get { return this._IsManualEdit; }
            set
            {
                if (!object.Equals(this._IsManualEdit, value))
                {
                    this._IsManualEdit = value;
                    this.OnPropertyChanged(() => this.IsManualEdit);
                    this.Validator.Validate(() => this.IsManualEdit);
                }
            }
        }

        #endregion


        #region Property ScoringDate

        private DateTime? _ScoringDate;

        public DateTime? ScoringDate
        {
            get { return this._ScoringDate; }
            set
            {
                if (!object.Equals(this._ScoringDate, value))
                {
                    this._ScoringDate = value;
                    this.OnPropertyChanged(() => this.ScoringDate);
                    this.Validator.Validate(() => this.ScoringDate);
                }
            }
        }

        #endregion

        #region Property StartTime

        PropertyObserver<WorkingTimeViewModel> StartTimeObServer { get; set; }

        private WorkingTimeViewModel _StartTime;

        public WorkingTimeViewModel StartTime
        {
            get { return this._StartTime; }
            set
            {
                if (!object.Equals(this._StartTime, value))
                {
                    this._StartTime = value;
                    this.OnPropertyChanged(() => this.StartTime);
                    this.Validator.Validate(() => this.StartTime);
                }
            }
        }

        #endregion

        #region Property LunchOut

        PropertyObserver<WorkingTimeViewModel> LunchOutObServer { get; set; }

        private WorkingTimeViewModel _LunchOut;

        public WorkingTimeViewModel LunchOut
        {
            get { return this._LunchOut; }
            set
            {
                if (!object.Equals(this._LunchOut, value))
                {
                    this._LunchOut = value;
                    this.OnPropertyChanged(() => this.LunchOut);
                    this.Validator.Validate(() => this.LunchOut);
                }
            }
        }

        #endregion

        #region Property LunchIn

        PropertyObserver<WorkingTimeViewModel> LunchInObServer { get; set; }

        private WorkingTimeViewModel _LunchIn;

        public WorkingTimeViewModel LunchIn
        {
            get { return this._LunchIn; }
            set
            {
                if (!object.Equals(this._LunchIn, value))
                {
                    this._LunchIn = value;
                    this.OnPropertyChanged(() => this.LunchIn);
                    this.Validator.Validate(() => this.LunchIn);
                }
            }
        }

        #endregion

        #region Property EndTime

        PropertyObserver<WorkingTimeViewModel> EndTimeObServer { get; set; }

        private WorkingTimeViewModel _EndTime;

        public WorkingTimeViewModel EndTime
        {
            get { return this._EndTime; }
            set
            {
                if (!object.Equals(this._EndTime, value))
                {
                    this._EndTime = value;
                    this.OnPropertyChanged(() => this.EndTime);
                    this.Validator.Validate(() => this.EndTime);
                }
            }
        }

        #endregion


        #region Property WorkRegularTime

        private double _WorkRegularTime;

        public double WorkRegularTime
        {
            get { return this._WorkRegularTime; }
            set
            {
                if (!object.Equals(this._WorkRegularTime, value))
                {
                    this._WorkRegularTime = value;
                    this.OnPropertyChanged(() => this.WorkRegularTime);
                    this.Validator.Validate(() => this.WorkRegularTime);
                }
            }
        }

        #endregion

        #region Property WorkTime

        private double _WorkTime;

        public double WorkTime
        {
            get { return this._WorkTime; }
            set
            {
                if (!object.Equals(this._WorkTime, value))
                {
                    this._WorkTime = value;
                    this.OnPropertyChanged(() => this.WorkTime);
                    this.Validator.Validate(() => this.WorkTime);
                }
            }
        }

        #endregion

        #region Property LessTime

        private double _LessTime;

        public double LessTime
        {
            get { return this._LessTime; }
            set
            {
                if (!object.Equals(this._LessTime, value))
                {
                    this._LessTime = value;
                    this.OnPropertyChanged(() => this.LessTime);
                    this.Validator.Validate(() => this.LessTime);
                }
            }
        }

        #endregion

        #region Property OverTime

        private double _OverTime;

        public double OverTime
        {
            get { return this._OverTime; }
            set
            {
                if (!object.Equals(this._OverTime, value))
                {
                    this._OverTime = value;
                    this.OnPropertyChanged(() => this.OverTime);
                    this.Validator.Validate(() => this.OverTime);
                }
            }
        }

        #endregion
        

        #region Property IsHoliday

        private bool _IsHoliday;

        public bool IsHoliday
        {
            get { return this._IsHoliday; }
            set
            {
                if (!object.Equals(this._IsHoliday, value))
                {
                    this._IsHoliday = value;
                    this.OnPropertyChanged(() => this.IsHoliday);
                    this.Validator.Validate(() => this.IsHoliday);
                }
            }
        }

        #endregion

        #region Property IsNotPaymentHoliday

        private bool _IsNotPaymentHoliday;

        public bool IsNotPaymentHoliday
        {
            get { return this._IsNotPaymentHoliday; }
            set
            {
                if (!object.Equals(this._IsNotPaymentHoliday, value))
                {
                    this._IsNotPaymentHoliday = value;
                    this.OnPropertyChanged(() => this.IsNotPaymentHoliday);
                    this.Validator.Validate(() => this.IsNotPaymentHoliday);
                }
            }
        }

        #endregion

        #region Property IsPublicHoliday

        private bool _IsPublicHoliday;

        public bool IsPublicHoliday
        {
            get { return this._IsPublicHoliday; }
            set
            {
                if (!object.Equals(this._IsPublicHoliday, value))
                {
                    this._IsPublicHoliday = value;
                    this.OnPropertyChanged(() => this.IsPublicHoliday);
                    this.Validator.Validate(() => this.IsPublicHoliday);
                }
            }
        }

        #endregion

        #region Property IsEditable

        private bool _IsEditable;

        public bool IsEditable
        {
            get { return this._IsEditable; }
            set
            {
                if (!object.Equals(this._IsEditable, value))
                {
                    this._IsEditable = value;
                    this.OnPropertyChanged(() => this.IsEditable);
                }
            }
        }

        #endregion

        #region Property Description

        private string _Description;

        public string Description
        {
            get { return this._Description; }
            set
            {
                if (!object.Equals(this._Description, value))
                {
                    this._Description = value;
                    this.OnPropertyChanged(() => this.Description);
                    this.Validator.Validate(() => this.Description);
                }
            }
        }

        #endregion


        #region EmployeeInputOutputListVM

        private EmployeeInputOutputListViewModel _EmployeeInputOutputListVM;

        public EmployeeInputOutputListViewModel EmployeeInputOutputListVM
        {
            get
            {
                if (this._EmployeeInputOutputListVM == null)
                {
                    this._EmployeeInputOutputListVM = new EmployeeInputOutputListViewModel(false);
                }

                return this._EmployeeInputOutputListVM;
            }

        }

        #endregion

        #endregion

        #region UI Property

        #region Property IsWeeklyOvertime

        private bool _isWeeklyOvertime;

        public bool IsWeeklyOvertime
        {
            get { return this._isWeeklyOvertime; }
            set
            {
                if (!object.Equals(this._isWeeklyOvertime, value))
                {
                    this._isWeeklyOvertime = value;
                    this.OnPropertyChanged(() => this.IsWeeklyOvertime);
                    this.Validator.Validate(() => this.WeeklyOvertime);
                }
            }
        }

        #endregion

        #region Property IsOutSourceOvertime

        private bool _isOutSourceOvertime;

        public bool IsOutSourceOvertime
        {
            get { return this._isOutSourceOvertime; }
            set
            {
                if (!object.Equals(this._isOutSourceOvertime, value))
                {
                    this._isOutSourceOvertime = value;
                    this.OnPropertyChanged(() => this.IsOutSourceOvertime);
                    this.Validator.Validate(() => this.OutSourceOvertime);
                }
            }
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(EmployeeInOutScoring inoutscoring)
        {

            this.IsEditable = !inoutscoring.IsSalaryScoring;

            this.IsManualEdit = inoutscoring.IsManualEdit;

            this.Employee = inoutscoring.Employee;

            if (inoutscoring.ScoringDate != DateTime.MinValue)
                this.ScoringDate = inoutscoring.ScoringDate;

            if (inoutscoring.WeeklyOvertime != null)
            {
                this.WeeklyOvertime = inoutscoring.WeeklyOvertime;
                this.IsWeeklyOvertime = true;
            }
            else if (inoutscoring.OutSourceOvertime != null)
            {
                this.OutSourceOvertime = inoutscoring.OutSourceOvertime;
                this.IsOutSourceOvertime = true;

            }

            this.StartTime = new WorkingTimeViewModel(inoutscoring.StartTime);
            this.LunchOut = new WorkingTimeViewModel(inoutscoring.LunchOut);
            this.LunchIn = new WorkingTimeViewModel(inoutscoring.LunchIn);
            this.EndTime = new WorkingTimeViewModel(inoutscoring.EndTime);

            this.WorkRegularTime = inoutscoring.WorkRegularTime;
            this.WorkTime = inoutscoring.WorkTime;
            this.LessTime = inoutscoring.LessTime;
            this.OverTime = inoutscoring.OverTime;

            this.IsHoliday = inoutscoring.IsHoliday;
            this.IsNotPaymentHoliday = inoutscoring.IsNotPaymentHoliday;
            this.IsPublicHoliday = inoutscoring.IsPublicHoliday;

            this.Description = inoutscoring.Description;
            this.SelectedState = this.ListState.SingleOrDefault(p => inoutscoring.ProcessState == p.Value);

            for (int i = this.ListState.Count - 1; i > -1; i--)
            {
                var item = this.ListState[i];

                if (!object.Equals(item, this.SelectedState))
                    this.ListState.Remove(item);
            }


            // Hesaplama icin 
            this.overtimescoring = new OverTimeScoring(this.Employee, this.ScoringDate.Value);
            
            
            this.overtimescoring.EmployeeStartTime = inoutscoring.StartTime;
            this.overtimescoring.EmployeeLunchOut = inoutscoring.LunchOut;
            this.overtimescoring.EmployeeLunchIn = inoutscoring.LunchIn;
            this.overtimescoring.EmployeeEndTime = inoutscoring.EndTime;



            this.overtimescoring.WorkRegularTime = new TimeSpan(0, (int)inoutscoring.WorkRegularTime, 0);



            if (this.WeeklyOvertime != null)
            {
                Overtime regularOverTime = this.overtimescoring.GetDailyOverTime(this.WeeklyOvertime);
                this.overtimescoring.SetRegularTime(regularOverTime);
                //DateTime reqularStartTime = this.ScoringDate.Value.AddTicks(regularOverTime.Start);
                //DateTime reqularLunchOut = this.ScoringDate.Value.AddTicks(regularOverTime.LunchOut);
                //DateTime reqularLunchIn = this.ScoringDate.Value.AddTicks(regularOverTime.LunchIn);
                //DateTime reqularEnd = this.ScoringDate.Value.AddTicks(regularOverTime.End);

                //this.overtimescoring.WorkRegularTime = (reqularLunchOut - reqularStartTime) + (reqularEnd - reqularLunchIn);


                this.StartTimeObServer = new PropertyObserver<WorkingTimeViewModel>(this.StartTime);
                this.StartTimeObServer.RegisterHandler(m => m.Time, (m) => { this.overtimescoring.EmployeeStartTime = CalculateWorkingTime(m, this.overtimescoring.reqularStartTime, true); SetTotalTime(); this.IsManualEdit = true; });

                this.LunchOutObServer = new PropertyObserver<WorkingTimeViewModel>(this.LunchOut);
                this.LunchOutObServer.RegisterHandler(m => m.Time, (m) => { this.overtimescoring.EmployeeLunchOut = CalculateWorkingTime(m, this.overtimescoring.reqularLunchOut, false); SetTotalTime(); this.IsManualEdit = true; });

                this.LunchInObServer = new PropertyObserver<WorkingTimeViewModel>(this.LunchIn);
                this.LunchInObServer.RegisterHandler(m => m.Time, (m) => { this.overtimescoring.EmployeeLunchIn = CalculateWorkingTime(m, this.overtimescoring.reqularLunchIn, true); SetTotalTime(); this.IsManualEdit = true; });

                this.EndTimeObServer = new PropertyObserver<WorkingTimeViewModel>(this.EndTime);
                this.EndTimeObServer.RegisterHandler(m => m.Time, (m) => { this.overtimescoring.EmployeeEndTime = CalculateWorkingTime(m, this.overtimescoring.reqularEnd, false); SetTotalTime(); this.IsManualEdit = true; });
            }

        }

        #region Time Calculate

        private WorkingTime CalculateWorkingTime(WorkingTimeViewModel workingTime, DateTime regularTime, bool IsInput)
        {
            if (workingTime.Time.HasValue)
            {
                if (workingTime.Time.Value.Date != this.ScoringDate)
                {
                    workingTime.Time = this.ScoringDate.Value.AddTicks(workingTime.Time.Value.TimeOfDay.Ticks);
                }

                WorkingTime wt = WorkingTime.Create(workingTime.Time.Value, regularTime, IsInput);
                workingTime.Difference = wt.Difference;
                workingTime.IsValid = wt.IsValid;
            }
            else
            {
                workingTime.Difference = 0;
                workingTime.IsValid = false;
            }

            return workingTime.GetWorkingTime();
        }

        private void SetTotalTime()
        {
            // Giris Cikis Bilgilerine gore toplam sureler hesaplaniyor
            this.overtimescoring.SetWorkTotalTime();

            this.WorkRegularTime = this.overtimescoring.WorkRegularTime.TotalMinutes;
            this.WorkTime = this.overtimescoring.WorkTime.TotalMinutes;
            this.LessTime = this.overtimescoring.LessTime.TotalMinutes;
            this.OverTime = this.overtimescoring.OverTime.TotalMinutes;
        }

        public OverTimeScoring overtimescoring { get; set; }

        #endregion

        private void SetDataModel(EmployeeInOutScoring employeeinoutscoring)
        {

            employeeinoutscoring.StartTime = this.StartTime.GetWorkingTime();
            employeeinoutscoring.LunchOut = this.LunchOut.GetWorkingTime();
            employeeinoutscoring.LunchIn = this.LunchIn.GetWorkingTime();
            employeeinoutscoring.EndTime = this.EndTime.GetWorkingTime();

           

            employeeinoutscoring.WorkRegularTime = this.WorkRegularTime;
            employeeinoutscoring.WorkTime = this.WorkTime;
            employeeinoutscoring.LessTime = this.LessTime;
            employeeinoutscoring.OverTime = this.OverTime;

            employeeinoutscoring.IsManualEdit = this.IsManualEdit;

            employeeinoutscoring.Description = this.Description;

            WorkingTime empty = WorkingTime.GetEmptyWorking();

            if (this.WeeklyOvertime != null)
            {
                if (
                    (!this.StartTime.Equals(empty)
                    && !this.LunchOut.Equals(empty)
                    && !this.LunchIn.Equals(empty)
                    && !this.EndTime.Equals(empty))
                    ||
                    (!this.StartTime.Equals(empty)
                    && !this.EndTime.Equals(empty))
                    )
                {
                    employeeinoutscoring.ProcessState = 1;
                    employeeinoutscoring.ProcessMessage = "İşlem Tamamlandı.";
                }
                else
                {
                    employeeinoutscoring.ProcessState = 2;
                    employeeinoutscoring.ProcessMessage = "Eksik giriş çıkış bilgisi.";
                }
            }
            else if (this.OutSourceOvertime != null)
            {
                if (object.Equals(this.WorkTime, 0))
                {
                    employeeinoutscoring.ProcessState = 2;
                    employeeinoutscoring.ProcessMessage = "Eksik giriş çıkış bilgisi.";
                }
                else
                {
                    employeeinoutscoring.ProcessState = 1;
                    employeeinoutscoring.ProcessMessage = "İşlem Tamamlandı.";
                }
            }
        }

        #endregion

        #region  Command



        protected override void ExcuteAcceptCommand()
        {
            if (!IsEditable)
            {
                this.ServicePresenter.ShowAlertMessage("Hakediş hesaplaması yapılan puantaj bilgisinde değişiklik yapılamaz.");
                return;
            }

            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            if (this.DataModel.ID == 0)
            {

                //PDYSEntities.DataContext.EmployeeInOutScoringSet.Add(this.DataModel);
            }


            

            SetDataModel(this.DataModel);

          

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }



        #endregion


    }
}
