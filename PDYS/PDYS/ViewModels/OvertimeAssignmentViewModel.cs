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

namespace PDYS.ViewModels
{
    public class OvertimeAssignmentViewModel : EditViewModelBase<OvertimeAssignment>
    {
        public OvertimeAssignmentViewModel(OvertimeAssignment DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Mesai Atama";

            this.Loaded+=new Action(OnLoad);
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OvertimeAssignmentViewModel_PropertyChanged);

            this.IsWeeklyOvertime = true;
            
        }

        void OvertimeAssignmentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.GetPropertyName(()=>this.IsWeeklyOvertime) == e.PropertyName && this.IsWeeklyOvertime)
            {
                this.OutSourceOvertime = null;
            }
            else if (this.GetPropertyName(() => this.IsOutSourceOvertime) == e.PropertyName && this.IsOutSourceOvertime)
            {
                this.WeeklyOvertime = null;
            }

        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Employee, ValidationMessage.RequiredText("Personel"));
            this.Validator.AddRequiredRule(() => this.StartDate, ValidationMessage.RequiredText("Başlangıç Tarihi"));
            //this.Validator.AddRequiredRule(() => this.EndDate, ValidationMessage.RequiredText("Bitiş Tarihi"));

            this.Validator.AddRule(() => this.WeeklyOvertime, () =>
            {
                if (!this.IsWeeklyOvertime)
                    return RuleResult.Valid();
                else
                    return (this.WeeklyOvertime != null) ? RuleResult.Valid() : RuleResult.Invalid(ValidationMessage.RequiredText("Haftalık Mesai"));
            });


            this.Validator.AddRule(() => this.OutSourceOvertime,() =>
                {
                    if (!this.IsOutSourceOvertime)
                        return RuleResult.Valid();
                    else
                        return (this.OutSourceOvertime != null) ? RuleResult.Valid() : RuleResult.Invalid(ValidationMessage.RequiredText("Kümulatif Mesai"));
                });


            //this.Validator.AddRule(() =>
            //    {
            //        if (this.StartDate.HasValue && this.EndDate.HasValue && this.EndDate > this.StartDate)
            //            return RuleResult.Valid();
            //        else
            //            return RuleResult.Invalid("Bitiş Tarihi, Başlangıç Tarihinden küçük yada eşit olamaz.");
            //    });

            //Validate OverTime Date
            this.Validator.AddRule(() =>
            {
                bool preValidation = this.Validator.Validate(() => this.Employee).IsValid
                                        && this.Validator.Validate(() => this.StartDate).IsValid
                                        && this.Validator.Validate(() => this.EndDate).IsValid;

                if (preValidation)
                {
                    var queryCheckAssigntment = from item in PDYSEntities.DataContext.OvertimeAssignmentSet.AsQueryable()
                                                where item.EmployeeID == this.Employee.ID
                                                && (
                                                    (this.StartDate <= item.StartDate && item.StartDate <= this.EndDate)
                                                    || (this.StartDate <= item.EndDate && item.EndDate <= this.EndDate)
                                                   )
                                                select item;

                    bool exists = queryCheckAssigntment.Any();

                    return (exists) ? RuleResult.Invalid(string.Format("Tarih çakışması olmuştur. {0} için bu tarihler arasında atama yapılamaz.",this.Employee.DisplayName)) : RuleResult.Valid();
                }
                else
                    return RuleResult.Valid();

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

        private void SetViewModel(OvertimeAssignment overtimerelation)
        {
            this.Employee = overtimerelation.Employee;

            if (overtimerelation.StartDate != DateTime.MinValue)
                this.StartDate = overtimerelation.StartDate;
            if (overtimerelation.EndDate != DateTime.MinValue)
                this.EndDate = overtimerelation.EndDate;

            if (overtimerelation.WeeklyOvertime != null)
            {
                this.WeeklyOvertime = overtimerelation.WeeklyOvertime;
                this.IsWeeklyOvertime = true;
            }
            else if  (overtimerelation.OutSourceOvertime != null)
            {
                this.OutSourceOvertime = overtimerelation.OutSourceOvertime;
                this.IsOutSourceOvertime= true;
            }
            
            this.Description = overtimerelation.Description;
            this.SelectedState = this.ListState.SingleOrDefault(p => overtimerelation.State == p.Value);
        }

        private void SetDataModel(OvertimeAssignment overtimerelation)
        {
            overtimerelation.Employee = this.Employee;
            overtimerelation.StartDate = this.StartDate.Value;
            overtimerelation.EndDate = (this.EndDate.HasValue) ? this.EndDate.Value : this.StartDate.Value.AddYears(10);

            if (IsWeeklyOvertime)
            {
                overtimerelation.WeeklyOvertime = this.WeeklyOvertime;
                overtimerelation.OutSourceOvertime = null;
            }
            else if (IsOutSourceOvertime)
            {
                overtimerelation.OutSourceOvertime = this.OutSourceOvertime;
                overtimerelation.WeeklyOvertime = null;
            }

            overtimerelation.Description = this.Description;
            overtimerelation.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
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

                PDYSEntities.DataContext.OvertimeAssignmentSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.IsSavedData = true;
            this.ServicePresenter.ShowInformationMessage("Kaydetme İşlemi Tamamlandı."); 
            this.ServicePresenter.CloseWindow(true);
        }

       

        #endregion


    }
}
