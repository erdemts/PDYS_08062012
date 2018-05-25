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
using System.Collections;


namespace PDYS.Report.ViewModels
{
    public class RptEmployeeInOutScoringViewModel : ReportViewModelBase<RptEmployeeInOutScoring>
    {

        public RptEmployeeInOutScoringViewModel()
        {
            this.Loaded += new Action(RptEmployeeInOutScoringViewModel_Loaded);
            
        }

        void RptEmployeeInOutScoringViewModel_Loaded()
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
        

        public override IEnumerable LoadReportDataSource()
        {

            var query = PDYSEntities.DataContext.EmployeeInOutScoringSet.AsQueryable();

             #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate >= item.ScoringDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => this.EndDate <= item.ScoringDate);


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

            


            if (!PDYSEntities.DataContext.ParameterSet.Local.Any())
            {
                PDYSEntities.DataContext.ParameterSet.Load();
            }

            var list = query.Select(item => new RptEmployeeInOutScoringModel()
            {
                EmployeeName = item.Employee.FirstName + " " + item.Employee.LastName,
                ScoringDate = item.ScoringDate,
                StartTime = item.StartTime,
                LunchOut = item.LunchOut,
                LunchIn = item.LunchIn,
                EndTime = item.EndTime,
                WorkTime = item.WorkTime,
                LessTime = item.LessTime,
                OverTime = item.OverTime,
                ProcessState = item.ProcessState,
                ProcessMessage = item.ProcessMessage
            
            }).ToList();

            list.ForEach(item =>
            {
                item.FormatedProcessState =  PDYSEntities.DataContext.ParameterSet.Local.FirstOrDefault(p => p.Name == "ProcessState" && p.Value == item.ProcessState).Text;
                item.FormatedWorkTime = TimeSpan.FromMinutes(item.WorkTime).ToString("c");
                item.FormatedLessTime = TimeSpan.FromMinutes(item.LessTime).ToString("c");
                item.FormatedOverTime = TimeSpan.FromMinutes(item.OverTime).ToString("c");
            });

            return list;
        }
    }
}

