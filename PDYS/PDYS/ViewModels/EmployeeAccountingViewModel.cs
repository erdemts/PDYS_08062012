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
    public class EmployeeAccountingViewModel : EditViewModelBase<EmployeeAccounting>
    {
        public EmployeeAccountingViewModel(EmployeeAccounting DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Hesap Hareketi";

            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(EmployeeAccountingViewModel_PropertyChanged);
            this.Loaded+=new Action(OnLoad);
        }

        void EmployeeAccountingViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => this.AccountingDefination))
            {
                if (this.AccountingDefination == null)
                {
                    this.IsEditableSubect = false;
                    this.IsDebit = false;
                    this.IsCredit = false;

                    this.Debit = 0;
                    this.Credit = 0;
                }
                else
                {
                    this.IsEditableSubect = !this.AccountingDefination.IsSystem;
                    this.Subject = this.AccountingDefination.DisplayName;
                    if (this.AccountingDefination.IsDebit)
                    {
                        this.IsDebit = true;
                        this.Credit = 0;
                    }

                    if (this.AccountingDefination.IsCredit)
                    {
                        this.IsCredit = true;
                        this.Debit = 0;
                    }
                }
            }
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Employee, ValidationMessage.RequiredText("Personel"));
            this.Validator.AddRequiredRule(() => this.AccountingDefination, ValidationMessage.RequiredText("HesapTipi"));
            this.Validator.AddMaxLengthRule(() => this.Subject, 100, ValidationMessage.MaxLengthText("işlem Tanımı",100));
            this.Validator.AddRequiredRule(() => this.ProcessDate, ValidationMessage.RequiredText("İşlem Tarihi"));

        }

        #region Page Property

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
                    this.Validator.Validate(() => this.IsEditable);
                }
            }
        }

        #endregion

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

        #region Property AccountingDefination

        private AccountingProcessType _AccountingDefination;

        public AccountingProcessType AccountingDefination
        {
            get { return this._AccountingDefination; }
            set
            {
                if (!object.Equals(this._AccountingDefination, value))
                {
                    this._AccountingDefination = value;
                    this.OnPropertyChanged(() => this.AccountingDefination);
                    this.Validator.Validate(() => this.AccountingDefination);
                }
            }
        }

        #endregion

        #region Property LookupAccountingDefination

        private LookupViewModel<AccountingProcessTypeListViewModel> _LookupAccountingDefination;

        public LookupViewModel<AccountingProcessTypeListViewModel> LookupAccountingDefination
        {
            get
            {
                if (this._LookupAccountingDefination == null)
                    this._LookupAccountingDefination = new LookupViewModel<AccountingProcessTypeListViewModel>() { Title = "Hesap Tipi Seçimi" };

                return this._LookupAccountingDefination;
            }
        }

        #endregion

        #region Property Subject

        private string _Subject;

        public string Subject
        {
            get { return this._Subject; }
            set
            {
                if (!object.Equals(this._Subject, value))
                {
                    this._Subject = value;
                    this.OnPropertyChanged(() => this.Subject);
                    this.Validator.Validate(() => this.Subject);
                }
            }
        }

        #endregion

        #region Property Debit

        private decimal _Debit;

        public decimal Debit
        {
            get { return this._Debit; }
            set
            {
                if (!object.Equals(this._Debit, value))
                {
                    this._Debit = value;
                    this.OnPropertyChanged(() => this.Debit);
                    this.Validator.Validate(() => this.Debit);
                }
            }
        }

        #endregion

        #region Property Credit

        private decimal _Credit;

        public decimal Credit
        {
            get { return this._Credit; }
            set
            {
                if (!object.Equals(this._Credit, value))
                {
                    this._Credit = value;
                    this.OnPropertyChanged(() => this.Credit);
                    this.Validator.Validate(() => this.Credit);
                }
            }
        }

        #endregion

        #region Property ProcessDate

        private DateTime? _ProcessDate;

        public DateTime? ProcessDate
        {
            get { return this._ProcessDate; }
            set
            {
                if (!object.Equals(this._ProcessDate, value))
                {
                    this._ProcessDate = value;
                    this.OnPropertyChanged(() => this.ProcessDate);
                    this.Validator.Validate(() => this.ProcessDate);
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


        #endregion

        #region UI Property

        #region Property IsEditableSubect

        private bool _IsEditableSubkect;

        public bool IsEditableSubect
        {
            get { return this._IsEditableSubkect; }
            set
            {
                if (!object.Equals(this._IsEditableSubkect, value))
                {
                    this._IsEditableSubkect = value;
                    this.OnPropertyChanged(() => this.IsEditableSubect);
                    this.Validator.Validate(() => this.IsEditableSubect);
                }
            }
        }

        #endregion

        #region Property IsDebit

        private bool _IsDebit;

        public bool IsDebit
        {
            get { return this._IsDebit; }
            set
            {
                if (!object.Equals(this._IsDebit, value))
                {
                    this._IsDebit = value;
                    this.OnPropertyChanged(() => this.IsDebit);
                    this.Validator.Validate(() => this.IsDebit);
                }
            }
        }

        #endregion

        #region Property IsCredit

        private bool _IsCredit;

        public bool IsCredit
        {
            get { return this._IsCredit; }
            set
            {
                if (!object.Equals(this._IsCredit, value))
                {
                    this._IsCredit = value;
                    this.OnPropertyChanged(() => this.IsCredit);
                    this.Validator.Validate(() => this.IsCredit);
                }
            }
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(EmployeeAccounting employeeaccounting)
        {
            this.IsEditable = !employeeaccounting.EmployeeSalaryScoringID.HasValue;

            this.Employee = employeeaccounting.Employee;
            this.AccountingDefination = employeeaccounting.AccountingDefination;

            this.Subject = employeeaccounting.Subject;
            this.Debit = employeeaccounting.Debit;
            this.Credit = employeeaccounting.Credit;

            if (employeeaccounting.ProcessDate!= DateTime.MinValue)
                this.ProcessDate = employeeaccounting.ProcessDate;

            this.Description = employeeaccounting.Description;

            this.SelectedState = this.ListState.SingleOrDefault(p => employeeaccounting.State == p.Value);
        }

        private void SetDataModel(EmployeeAccounting employeeaccounting)
        {
            employeeaccounting.Employee = this.Employee;
            employeeaccounting.AccountingDefination = this.AccountingDefination;
            employeeaccounting.Subject = this.Subject;
            employeeaccounting.Debit = this.Debit;
            employeeaccounting.Credit = this.Credit;

            employeeaccounting.Description = this.Description;

            if (this.ProcessDate.HasValue)
                employeeaccounting.ProcessDate = this.ProcessDate.Value;

            
            employeeaccounting.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
        }

        #endregion

        #region  Command

        protected override void ExcuteAcceptCommand()
        {
            if (!IsEditable)
            {
                this.ServicePresenter.ShowAlertMessage("Hakediş ilişkili hesap hareketi değiştirilemez.");
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
                PDYSEntities.DataContext.EmployeeAccountingSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }

       

        #endregion


    }
}
