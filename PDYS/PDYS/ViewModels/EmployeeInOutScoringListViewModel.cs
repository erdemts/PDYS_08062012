using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;
using PDYS.InfraStructure;
using PDYS.Interfaces;
using Mvvm;
using PDYS.Services;
using PDYS.Services.ServiceParam;
using DeviceManagement;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace PDYS.ViewModels
{
    public class EmployeeInOutScoringListViewModel : ListViewModelBase<EmployeeInOutScoring, EmployeeInOutScoringViewModel>
    {

        public EmployeeInOutScoringListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.IsNewCommand = false;
            this.IsMultiSelect = true;
            this.IsDeleteCommand = true;
            this.OnDeleted += new Action<IEnumerable<EmployeeInOutScoring>>(UIDeleteInOutScoring);
            this.Loaded += new Action(InternalLoad);
        }



        void InternalLoad()
        {

             #region InputOutput Type List

            var queryState = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "ProcessState"
                             orderby item.Text
                             select item;

            queryState.ToList().ForEach(item => this.ListProcessState.Add(item));

            Parameter allParam = new Parameter() { Name = "ProcessState", Text = "Tümü", Value = -1 };
            this.ListProcessState.Insert(0, allParam);
            this.SelectedProcessState = allParam;

            #endregion


            if (IsAutoLoadData)
                this.SearchCommand.Execute();


            
        }

        void UIDeleteInOutScoring(IEnumerable<EmployeeInOutScoring> deleteditems)
        {
            bool iscontinue = true;

            ConfirmParam param = new ConfirmParam();
            param.Message = "Silme işlemini onaylıyormusunuz ?";
            param.OnConfirmResult = (result) =>
            {
                if (result == ConfirmParam.ConfirmResult.No)
                    iscontinue = false;
            };

            this.ServicePresenter.ConfirmMessage(param);

            if (!iscontinue)
                return;

            ProcessManager.Execute("Puantaj Siliniyor...", new Action<IEnumerable<EmployeeInOutScoring>>(DeleteInOutScoring), deleteditems);
        }

        void DeleteInOutScoring(IEnumerable<EmployeeInOutScoring> deleteditems)
        {
            foreach (var inoutScoring in deleteditems)
            {
                if (inoutScoring.IsSalaryScoring)
                    continue;

                var inouts = PDYSEntities.DataContext.EmployeeInputOutputSet.Where(io => io.EmployeeInOutScoringID == inoutScoring.ID);

                foreach (EmployeeInputOutput inout in inouts)
                {
                    inout.IsProcessed = false;
                    inout.EmployeeInOutScoringID = null;
                    inout.EmployeeInOutScoring = null;
                }

                PDYSEntities.DataContext.EmployeeInOutScoringSet.Remove(inoutScoring);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.LoadData();

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

        #region Property StartDate

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return this._startDate; }
            set
            {
                if (!object.Equals(this._startDate, value))
                {
                    this._startDate = value;
                    this.OnPropertyChanged(() => this.StartDate);
                    this.Validator.Validate(() => this.StartDate);
                }
            }
        }

        #endregion

        #region Property EndDate

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get { return this._endDate; }
            set
            {
                if (!object.Equals(this._endDate, value))
                {
                    this._endDate = value;
                    this.OnPropertyChanged(() => this.EndDate);
                    this.Validator.Validate(() => this.EndDate);
                }
            }
        }

        #endregion
        
        #region Property SelectedProcessState

        private Parameter _SelectedProcessState;

        public Parameter SelectedProcessState
        {
            get { return this._SelectedProcessState; }
            set
            {
                if (!object.Equals(this._SelectedProcessState, value))
                {
                    this._SelectedProcessState = value;
                    this.OnPropertyChanged(() => this.SelectedProcessState);
                    this.Validator.Validate(() => this.SelectedProcessState);
                }
            }
        }

        #endregion

        #region Property ListProcessState Collection

        private ObservableCollection<Parameter> _ListProcessState;

        public ObservableCollection<Parameter> ListProcessState
        {
            get { return this._ListProcessState = this._ListProcessState ?? new ObservableCollection<Parameter>(); }
            private set { this._ListProcessState = value; }
        }

        #endregion

        #region Property IsInvalidStart

        private bool _IsInvalidStart;

        public bool IsInvalidStart
        {
            get { return this._IsInvalidStart; }
            set
            {
                if (!object.Equals(this._IsInvalidStart, value))
                {
                    this._IsInvalidStart = value;
                    this.OnPropertyChanged(() => this.IsInvalidStart);
                    this.Validator.Validate(() => this.IsInvalidStart);
                }
            }
        }

        #endregion

        #region Property IsInvalidLunchOut

        private bool _IsInvalidLunchOut;

        public bool IsInvalidLunchOut
        {
            get { return this._IsInvalidLunchOut; }
            set
            {
                if (!object.Equals(this._IsInvalidLunchOut, value))
                {
                    this._IsInvalidLunchOut = value;
                    this.OnPropertyChanged(() => this.IsInvalidLunchOut);
                    this.Validator.Validate(() => this.IsInvalidLunchOut);
                }
            }
        }

        #endregion

        #region Property IsInvalidLunchIn

        private bool _IsInvalidLunchIn;

        public bool IsInvalidLunchIn
        {
            get { return this._IsInvalidLunchIn; }
            set
            {
                if (!object.Equals(this._IsInvalidLunchIn, value))
                {
                    this._IsInvalidLunchIn = value;
                    this.OnPropertyChanged(() => this.IsInvalidLunchIn);
                    this.Validator.Validate(() => this.IsInvalidLunchIn);
                }
            }
        }

        #endregion

        #region Property IsInvalidEnd

        private bool _IsInvalidEnd;

        public bool IsInvalidEnd
        {
            get { return this._IsInvalidEnd; }
            set
            {
                if (!object.Equals(this._IsInvalidEnd, value))
                {
                    this._IsInvalidEnd = value;
                    this.OnPropertyChanged(() => this.IsInvalidEnd);
                    this.Validator.Validate(() => this.IsInvalidEnd);
                }
            }
        }

        #endregion

        #endregion

        #region Property SearchCommand

        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (this._searchCommand == null)
                    this._searchCommand = new RelayCommand(ExcuteSearchCommand);
                return this._searchCommand;
            }
        }

        void ExcuteSearchCommand()
        {
            var query = PDYSEntities.DataContext.EmployeeInOutScoringSet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate <= item.ScoringDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => item.ScoringDate <= this.EndDate);


            if (this.EndDate.HasValue && this.StartDate.HasValue)
                query = query.Where(item => this.StartDate <= item.ScoringDate && item.ScoringDate <= this.EndDate);

            if (this.SelectedProcessState != null && this.SelectedProcessState.Value != -1)
                query = query.Where(item => item.ProcessState == this.SelectedProcessState.Value);


            if (this.IsInvalidStart)
                query = query.Where(item => item.StartTime.Time.HasValue && !item.StartTime.IsValid);

            if (this.IsInvalidLunchOut)
                query = query.Where(item => item.LunchOut.Time.HasValue && !item.LunchOut.IsValid);

            if (this.IsInvalidLunchIn)
                query = query.Where(item => item.LunchIn.Time.HasValue && !item.LunchIn.IsValid);

            if (this.IsInvalidEnd)
                query = query.Where(item => item.EndTime.Time.HasValue && !item.EndTime.IsValid);


            query = query.OrderByDescending(item => item.ScoringDate);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion



        #region Property ScoringCommand

        private RelayCommand _ScoringCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ScoringCommand
        {
            get
            {
                if (this._ScoringCommand == null)
                    this._ScoringCommand = new RelayCommand(ExcuteScoringCommand);
                return this._ScoringCommand;
            }
        }

        void ExcuteScoringCommand()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(SelectParameter), DispatcherPriority.Background, null);
        }

        void SelectParameter()
        {
            SelectInOutScoringViewModel.OpenSelectionWindow(UICalculateScoring);
        }

        void UICalculateScoring(Employee SelectedEmployee, DateTime? SelectedStartDate, DateTime? SelectedEndDate)
        {
            ProcessManager.Execute("Puantaj Hesaplanıyor...", new Action<Employee, DateTime?, DateTime?>(CalculateScoring), SelectedEmployee, SelectedStartDate, SelectedEndDate);
        }

        void CalculateScoring(Employee SelectedEmployee, DateTime? SelectedStartDate, DateTime? SelectedEndDate)
        {
            IEnumerable<Employee> employeList = null;

            if (SelectedEmployee != null)
                employeList = new Employee[] { SelectedEmployee };
            else
                employeList =  PDYSEntities.DataContext.EmployeeSet.Where(e => e.State == 0);

            foreach (var employee in employeList)
            {
                DateTime? startdate = null;
                DateTime? enddate = null;


                if (SelectedStartDate.HasValue)
                    startdate = SelectedStartDate;
                else
                {

                    // En son islem yapılan scoring kaydın tarihi alınıyor
                    var maxstartdatequery = from io in PDYSEntities.DataContext.EmployeeInOutScoringSet
                                            where io.EmployeeID == employee.ID
                                            select new { io.ScoringDate };

                    // Hiç işlem yapılmamışsa Scoring personelin işe başladığı tarihden başlar
                    // Scoring işlem yapılan en son tarihden bir sonraki gün başlar
                    startdate = (!maxstartdatequery.Any()) ? employee.WorkingStartDate : maxstartdatequery.Max(item => item.ScoringDate);//.AddDays(1);
                }

                if (SelectedEndDate.HasValue)
                    enddate = SelectedEndDate;
                else
                {
                    // Personelin işten ayrılma tarihi bugünün tarihinden küçükse hesaplama işten ayrılış tarihine kadar yapılır.
                    enddate = (employee.WorkingEndDate.HasValue && employee.WorkingEndDate.Value < DateTime.Now.Date) ? employee.WorkingEndDate : DateTime.Now.Date;
                }

                DateTime processDate = startdate.Value;

                // Başlangıçtan bitişe kadar her bir gün için işlem yapılıyor
                while (true)
                {
                    if (processDate > enddate.Value)
                        break;

                    #region Delete Exists Record

                    var existsScorings = PDYSEntities.DataContext.EmployeeInOutScoringSet.Where(ios => ios.EmployeeID == employee.ID && ios.ScoringDate == processDate.Date);

                    this.DeleteInOutScoring(existsScorings);

                    if (existsScorings.Any())
                    {
                        // Bir sonraki güne geçilir
                        processDate = processDate.AddDays(1);
                        continue;
                    }

                    #endregion

                    OverTimeScoring scoring = new OverTimeScoring(employee, processDate);
                    scoring.Calculate();

                    #region Create Scoring Record

                    EmployeeInOutScoring employeeinoutscoring = new EmployeeInOutScoring();
                    
                    employeeinoutscoring.Employee = employee;
                    employeeinoutscoring.ScoringDate = processDate.Date;
                    
                    employeeinoutscoring.ProcessState = scoring.State;
                    employeeinoutscoring.ProcessMessage = scoring.StateMessage;

                    employeeinoutscoring.OutSourceOvertime = scoring.OutSourceOvertime;
                    employeeinoutscoring.WeeklyOvertime = scoring.WeeklyOvertime;
                    
                    employeeinoutscoring.StartTime = scoring.EmployeeStartTime;
                    employeeinoutscoring.LunchOut = scoring.EmployeeLunchOut;
                    employeeinoutscoring.LunchIn = scoring.EmployeeLunchIn;
                    employeeinoutscoring.EndTime = scoring.EmployeeEndTime;

                    employeeinoutscoring.WorkRegularTime = scoring.WorkRegularTime.TotalMinutes; 
                    employeeinoutscoring.WorkTime = scoring.WorkTime.TotalMinutes;
                    employeeinoutscoring.LessTime = scoring.LessTime.TotalMinutes;
                    employeeinoutscoring.OverTime = scoring.OverTime.TotalMinutes;
                    
                    employeeinoutscoring.IsPublicHoliday = scoring.IsPublicHoliday;
                    employeeinoutscoring.IsNotPaymentHoliday = scoring.IsEmployeeNotPaymentHoliday;
                    employeeinoutscoring.IsHoliday = (scoring.IsWorkingHoliday || scoring.IsEmployeeHoliday);


                    PDYSEntities.DataContext.EmployeeInOutScoringSet.Add(employeeinoutscoring);
                    PDYSEntities.DataContext.SaveChanges();

                    #endregion

                    #region Update InOutAction

                    foreach (var item in scoring.ListInOutActions)
                    {
                        EmployeeInputOutput inputoutput = PDYSEntities.DataContext.EmployeeInputOutputSet.Find(item.RefID);
                        inputoutput.IsProcessed = true;
                        inputoutput.EmployeeInOutScoring = employeeinoutscoring;
                    }

                    PDYSEntities.DataContext.SaveChanges();
                    
                    #endregion

                    // Bir sonraki güne geçilir
                    processDate = processDate.AddDays(1);
                }

            }

            this.LoadData();
        }

        // 0:Giriş & Çıkış, 1:Giriş, 2:Çıkış

        #endregion


    }
}
