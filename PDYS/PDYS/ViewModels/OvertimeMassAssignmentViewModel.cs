using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using PDYS.Helper;
using Mvvm.Validation;
using Mvvm;
using PDYS.Services.ServiceParam;

namespace PDYS.ViewModels
{
    public class OvertimeMassAssignmentViewModel : ViewModelBase
    {
        public OvertimeMassAssignmentViewModel()
        {
            this.Title = "Toplu Mesai Atama";
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";

            this.Loaded += new Action(OnLoad);

            this.IsWeeklyOvertime = true;
            this.VisibleState = false;

            #region Employee List

            this.EmployeListVM.IsNewCommand = false;
            this.EmployeListVM.IsOpenCommand = false;
            this.EmployeListVM.IsAppendCommand = true;
            this.EmployeListVM.IsDeleteCommand = true;
            this.EmployeListVM.IsMultiSelect = true;

            this.EmployeListVM.QueryExpression = EmployeeLocalDataSource.AsQueryable().OrderBy(e => e.DisplayName);
            this.EmployeListVM.OnInserted += new Action<IEnumerable<Employee>>(EmployeListVM_OnInserted);
            this.EmployeListVM.OnDeleted += new Action<IEnumerable<Employee>>(EmployeListVM_OnDeleted);

            #endregion

        }




        void OnLoad()
        {
        }

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.StartDate, ValidationMessage.RequiredText("Başlangıç Tarihi"));
            //this.Validator.AddRequiredRule(() => this.EndDate, ValidationMessage.RequiredText("Bitiş Tarihi"));

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

            //this.Validator.AddRule(() =>
            //{
            //    if (this.StartDate.HasValue && this.EndDate.HasValue && this.EndDate > this.StartDate)
            //        return RuleResult.Valid();
            //    else
            //        return RuleResult.Invalid("Bitiş Tarihi, Başlangıç Tarihinden küçük yada eşit olamaz.");
            //});

            this.Validator.AddRule(()=>this.EmployeeLocalDataSource, () =>
            {
                if (!this.EmployeeLocalDataSource.Any())
                    return RuleResult.Invalid("En bir personel seçimi yapılmalıdır.");
                else
                    return RuleResult.Valid();
            });




        }

        #region Root Property

        #region Property Title

        private string _title;

        public string Title
        {
            get { return this._title; }
            set
            {
                if (!object.Equals(this._title, value))
                {
                    this._title = value;
                    this.OnPropertyChanged(() => this.Title);
                    this.Validator.Validate(() => this.Title);
                }
            }
        }

        #endregion

        #region Property AcceptButtonTitle

        private string _acceptButtonTitle;

        public string AcceptButtonTitle
        {
            get { return this._acceptButtonTitle; }
            set
            {
                if (!object.Equals(this._acceptButtonTitle, value))
                {
                    this._acceptButtonTitle = value;
                    this.OnPropertyChanged(() => this.AcceptButtonTitle);
                    this.Validator.Validate(() => this.AcceptButtonTitle);
                }
            }
        }

        #endregion

        #region Property CancelCommandTitle

        private string _cancelCommandTitle;

        public string CancelCommandTitle
        {
            get { return this._cancelCommandTitle; }
            set
            {
                if (!object.Equals(this._cancelCommandTitle, value))
                {
                    this._cancelCommandTitle = value;
                    this.OnPropertyChanged(() => this.CancelCommandTitle);
                    this.Validator.Validate(() => this.CancelCommandTitle);
                }
            }
        }

        #endregion

        #endregion

        #region Page Property

        #region Property VisibleState

        public bool VisibleState { get; set; }

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


        #region EmployeeList

        private List<Employee> _EmployeeLocalDataSource;
        public List<Employee> EmployeeLocalDataSource
        {
            get
            {
                if (_EmployeeLocalDataSource == null)
                {
                    _EmployeeLocalDataSource = new List<Employee>();
                }

                return _EmployeeLocalDataSource;
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

        #region Property OvertimeEmployees Collection

        private EmployeeListViewModel _EmployeListVM;

        public EmployeeListViewModel EmployeListVM
        {
            get
            {
                if (this._EmployeListVM == null)
                {
                    this._EmployeListVM = new EmployeeListViewModel(false);
                }

                return this._EmployeListVM;
            }

        }

        void EmployeListVM_OnDeleted(IEnumerable<Employee> selecteditems)
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

            foreach (var item in selecteditems)
            {
                this.EmployeeLocalDataSource.Remove(item);
            }

            this.EmployeListVM.LoadData();
        }

        void EmployeListVM_OnInserted(IEnumerable<Employee> objlist)
        {
            foreach (var item in objlist)
            {
                this.EmployeeLocalDataSource.Add(item);
            }

            this.EmployeListVM.LoadData();
        }

        #endregion

        #region Command

        #region Accept Command

        private RelayCommand _acceptCommand;
        /// <summary>
        /// Save & Close Window Command
        /// </summary>
        public RelayCommand AcceptCommand
        {
            get
            {
                if (this._acceptCommand == null)
                    this._acceptCommand = new RelayCommand(ExcuteAcceptCommand);
                return this._acceptCommand;
            }
        }

        protected virtual void ExcuteAcceptCommand()
        {
            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            var validAssignments = new List<OvertimeAssignment>();

            foreach (var employee in this.EmployeeLocalDataSource)
            {
                OvertimeAssignment overtimeassignment = GetDataModel(employee);

                RuleResult validatation = IsValidOveritimeAssignment(overtimeassignment);

                if (validatation.IsValid)
                    validAssignments.Add(overtimeassignment);
                else
                    this.ServicePresenter.ShowErrorMessage(validatation.Errors.ElementAt(0));
            }

            foreach (var overtimeassignment in validAssignments)
            {
                PDYSEntities.DataContext.OvertimeAssignmentSet.Add(overtimeassignment);
                PDYSEntities.DataContext.SaveChanges();
            }

            if (validAssignments.Any())
            {
                this.ServicePresenter.ShowInformationMessage(string.Format("[ {0}/{1} ] için Kaydetme İşlemi Tamamlandı.", validAssignments.Count, this.EmployeeLocalDataSource.Count));
                this.ServicePresenter.CloseWindow(true);
            }
        }

        #endregion

        #region Cancel Command

        private RelayCommand _cancelCommand;
        /// <summary>
        /// Close Window Command
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                if (this._cancelCommand == null)
                    this._cancelCommand = new RelayCommand(ExcuteCancelCommand);
                return this._cancelCommand;
            }
        }

        protected virtual void ExcuteCancelCommand()
        {
            this.ServicePresenter.CloseWindow(false);
        }

        #endregion

        #endregion



        private RuleResult IsValidOveritimeAssignment(OvertimeAssignment overtimeassignment)
        {
            bool preValidation = this.Validator.Validate(() => this.EmployeeLocalDataSource).IsValid
                                        && this.Validator.Validate(() => this.StartDate).IsValid
                                        && this.Validator.Validate(() => this.EndDate).IsValid;

                if (preValidation)
                {
                    var queryCheckAssigntment = from item in PDYSEntities.DataContext.OvertimeAssignmentSet.AsQueryable()
                                                where item.EmployeeID == overtimeassignment.Employee.ID
                                                && (
                                                    (this.StartDate <= item.StartDate && item.StartDate <= this.EndDate)
                                                    || (this.StartDate <= item.EndDate && item.EndDate <= this.EndDate)
                                                   )
                                                select item;

                    bool exists = queryCheckAssigntment.Any();

                    return (exists) ? RuleResult.Invalid(string.Format("Tarih çakışması olmuştur. {0} için bu tarihler arasında atama yapılamaz.", overtimeassignment.Employee.DisplayName)) : RuleResult.Valid();
                }
                else
                    return RuleResult.Valid();
        }

        private OvertimeAssignment GetDataModel(Employee employee)
        {
            OvertimeAssignment overtimerelation = new OvertimeAssignment();

            overtimerelation.Employee = employee;
            overtimerelation.StartDate = this.StartDate.Value;
            overtimerelation.EndDate = (this.EndDate.HasValue) ? this.EndDate.Value : this.StartDate.Value.AddYears(10);

            overtimerelation.WeeklyOvertime = null;
            overtimerelation.OutSourceOvertime = null;

            if (IsWeeklyOvertime)
            {
                overtimerelation.WeeklyOvertime = this.WeeklyOvertime;
            }
            else if (IsOutSourceOvertime)
            {
                overtimerelation.OutSourceOvertime = this.OutSourceOvertime;
            }

            overtimerelation.Description = this.Description;
            overtimerelation.State = 0;

            return overtimerelation;
        }

    }
}
