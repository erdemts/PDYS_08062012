using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;
using PDYS.InfraStructure;
using PDYS.Interfaces;
using Mvvm;
using System.Collections.ObjectModel;

namespace PDYS.ViewModels
{
    public class EmployeeHolidayListViewModel : ListViewModelBase<EmployeeHoliday,EmployeeHolidayViewModel>
    {

        public EmployeeHolidayListViewModel(bool autoload)
            : base(autoload)
        {
            this.Loaded += new Action(EmployeeHolidayListViewModel_Loaded);
        }

        void EmployeeHolidayListViewModel_Loaded()
        {
            #region Load ListHolidayType Parameter

            var inputOutputType = from item in PDYSEntities.DataContext.ParameterSet
                                  where item.Name == "HolidayType"
                                  orderby item.Value
                                  select item;

            inputOutputType.ToList().ForEach(item => this.ListHolidayType.Add(item));

            this.ListHolidayType.Insert(0, new Parameter() 
            {
                Name = "HolidayType",
                Text = "Tümü",
                Value = -1,
            });

            this.SelectedHolidayType = this.ListHolidayType.FirstOrDefault();

            #endregion

            if (IsAutoLoadData)
                this.SearchCommand.Execute();
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

        #region Property SelectedHolidayType

        private Parameter _SelectedHolidayType;

        public Parameter SelectedHolidayType
        {
            get { return this._SelectedHolidayType; }
            set
            {
                if (!object.Equals(this._SelectedHolidayType, value))
                {
                    this._SelectedHolidayType = value;
                    this.OnPropertyChanged(() => this.SelectedHolidayType);
                    this.Validator.Validate(() => this.SelectedHolidayType);
                }
            }
        }

        #endregion

        #region Property ListInputOutputType Collection

        private ObservableCollection<Parameter> _ListHolidayType;

        public ObservableCollection<Parameter> ListHolidayType
        {
            get { return this._ListHolidayType = this._ListHolidayType ?? new ObservableCollection<Parameter>(); }
            private set { this._ListHolidayType = value; }
        }

        #endregion

        #endregion

        #region Property SearchCommand

        private RelayCommand _searchCommand;
        /// <summary>
        /// 
        /// </summary>
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
            var query = PDYSEntities.DataContext.EmployeeHolidaySet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate >= item.StartDate && this.StartDate <= item.EndDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => this.EndDate >= item.StartDate && this.EndDate <= item.EndDate);

            if (this.EndDate.HasValue && this.StartDate.HasValue)
                query = query.Where(item => (this.StartDate <= item.StartDate && item.StartDate <= this.EndDate)
                    || (this.StartDate <= item.EndDate && item.EndDate <= this.EndDate)
                    );

            if (this.SelectedHolidayType != null && this.SelectedHolidayType.Value !=-1)
                query = query.Where(item => item.Type == this.SelectedHolidayType.Value);

            query = query.OrderBy(item => item.ID);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion

       
    }
}
