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

namespace PDYS.ViewModels
{
    public class EmployeeHolidayViewModel : EditViewModelBase<EmployeeHoliday>
    {
        public EmployeeHolidayViewModel(EmployeeHoliday DataModel) :base(DataModel)
        {
            this.Title = "Personel İzin Bilgisi";
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";

            this.Loaded+=new Action(OnLoad);
            
        }

        void OnLoad()
        {
            #region Load ListHolidayType Parameter

            var inputOutputType = from item in PDYSEntities.DataContext.ParameterSet
                                  where item.Name == "HolidayType"
                                  orderby item.Value
                                  select item;

            inputOutputType.ToList().ForEach(item => this.ListHolidayType.Add(item));

            this.SelectedHolidayType = this.ListHolidayType.FirstOrDefault();

            #endregion

            SetViewModel(this.DataModel);
        }


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Employee, ValidationMessage.RequiredText("Personel"));
            this.Validator.AddRequiredRule(() => this.StartDate, ValidationMessage.RequiredText("Başlangıç Tarihi"));
            this.Validator.AddRequiredRule(() => this.EndDate, ValidationMessage.RequiredText("Bitiş Tarihi"));

            this.Validator.AddRule(() =>
                {
                    if (this.StartDate.HasValue && this.EndDate.HasValue && this.EndDate>this.StartDate)
                         return RuleResult.Valid();
                    else
                        return RuleResult.Invalid("Bitiş Tarihi, Başlangıç Tarihinden küçük yada eşit olamaz.");
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

        #region Property IsNotPayment

        private bool? isNotPayment = false;

        public bool? IsNotPayment
        {
            get { return this.isNotPayment; }
            set
            {
                if (!object.Equals(this.isNotPayment, value))
                {
                    this.isNotPayment = value;
                    this.OnPropertyChanged(() => this.IsNotPayment);
                    this.Validator.Validate(() => this.IsNotPayment);
                }
            }
        }

        #endregion

        #region Property Description

        private string _description;

        public string Description
        {
            get { return this._description; }
            set
            {
                if (!object.Equals(this._description, value))
                {
                    this._description = value;
                    this.OnPropertyChanged(() => this.Description);
                    this.Validator.Validate(() => this.Description);
                }
            }
        }

        #endregion

        #endregion

        #region Property LookupEmployee

        private LookupViewModel<EmployeeListViewModel> _lookupEmployee;

        public LookupViewModel<EmployeeListViewModel> LookupEmployee
        {
            get
            {
                if (this._lookupEmployee == null)
                    this._lookupEmployee = new LookupViewModel<EmployeeListViewModel>() { Title = "Personel Seçimi" };

                return this._lookupEmployee;
            }
        }

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(EmployeeHoliday employeeholiday)
        {
            this.Employee = employeeholiday.Employee;
            
            if (employeeholiday.StartDate != DateTime.MinValue)
                this.StartDate = employeeholiday.StartDate;
            if (employeeholiday.EndDate != DateTime.MinValue)
                this.EndDate = employeeholiday.EndDate;

            this.IsNotPayment = employeeholiday.IsNotPayment;
            this.Description = employeeholiday.Description;
            this.SelectedState = this.ListState.SingleOrDefault(p => employeeholiday.State == p.Value);

            if (employeeholiday.Type.HasValue)
                this.SelectedHolidayType = this.ListHolidayType.SingleOrDefault(io => employeeholiday.Type == io.Value);
        }

        private void SetDataModel(EmployeeHoliday employeeholiday)
        {
            employeeholiday.Employee = this.Employee;
            employeeholiday.StartDate = this.StartDate.Value;
            employeeholiday.EndDate = this.EndDate.Value;
            employeeholiday.IsNotPayment = (this.IsNotPayment.HasValue) ? this.IsNotPayment.Value : false;
            employeeholiday.Description = this.Description;
            employeeholiday.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
            employeeholiday.Type = (this.SelectedHolidayType != null) ? this.SelectedHolidayType.Value : 0;
        }

        #endregion

        #region  Command

        protected override void ExcuteAcceptCommand()
        {
            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            if (this.DataModel.ID == 0)
            {

                PDYSEntities.DataContext.EmployeeHolidaySet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }

       

        #endregion


    }
}
