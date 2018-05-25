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


namespace PDYS.Report.ViewModels
{
    public class RptInputOutputViewModel : ReportViewModelBase<RptInputOutput>
    {

        public RptInputOutputViewModel()
        {
            this.Loaded += new Action(RptInputOutputViewModel_Loaded);
            
        }

        void RptInputOutputViewModel_Loaded()
        {
            #region InputOutput Type List

            var queryState = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "InputOutputType"
                             orderby item.Text
                             select item;

            queryState.ToList().ForEach(item => this.ListInputOutputType.Add(item));

            Parameter allParam = new Parameter() { Name = "InputOutputType", Text = "Tümü", Value = -1 };
            this.ListInputOutputType.Insert(0, allParam);
            this.SelectedInputOutput = allParam;

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

        #region Property SelectedInputOutput

        private Parameter _SelectedInputOutput;

        public Parameter SelectedInputOutput
        {
            get { return this._SelectedInputOutput; }
            set
            {
                if (!object.Equals(this._SelectedInputOutput, value))
                {
                    this._SelectedInputOutput = value;
                    this.OnPropertyChanged(() => this.SelectedInputOutput);
                    this.Validator.Validate(() => this.SelectedInputOutput);
                }
            }
        }

        #endregion

        #region Property ListInputOutputType Collection

        private ObservableCollection<Parameter> _ListInputOutputType;

        public ObservableCollection<Parameter> ListInputOutputType
        {
            get { return this._ListInputOutputType = this._ListInputOutputType ?? new ObservableCollection<Parameter>(); }
            private set { this._ListInputOutputType = value; }
        }

        #endregion

        #endregion
        

        public override System.Collections.IEnumerable LoadReportDataSource()
        {
            if (!PDYSEntities.DataContext.ParameterSet.Local.Any())
            {
                PDYSEntities.DataContext.ParameterSet.Load();
            }


            var query = PDYSEntities.DataContext.EmployeeInputOutputSet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate >= item.InOutDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => this.EndDate <= item.InOutDate);

            if (this.SelectedInputOutput != null && this.SelectedInputOutput.Value != -1)
                query = query.Where(item => item.InOutType == this.SelectedInputOutput.Value);


            if (this.EndDate.HasValue && this.StartDate.HasValue)
                query = query.Where(item => this.StartDate <= item.InOutDate && item.InOutDate <= this.EndDate);

            query = query.OrderByDescending(item => item.InOutDate);

            #endregion


            List<RptInputOutputModel> result = new List<RptInputOutputModel>();

            query.ToList().ForEach(item =>
                result.Add(new RptInputOutputModel()
                {
                    EmployeeName = (item.Employee!=null) ? item.Employee.DisplayName :"",
                    Device = (item.ReaderDevice != null) ? item.ReaderDevice.DisplayName : "",
                    Date = string.Format("{0:dd.MM.yyyy}",item.InOutDate),
                    Time = string.Format("{0:HH:mm}", item.InOutDate),
                    IsScoring = (item.IsProcessed) ? "Evet" : "Hayır",
                    ActionType = PDYSEntities.DataContext.ParameterSet.Local.FirstOrDefault(p => p.Name == "InputOutputType" && p.Value == item.InOutType).Text
                
                })
                );
                                                   
            return result;
        }
    }
}
