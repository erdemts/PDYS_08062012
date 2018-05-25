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
    public class AccountingProcessTypeViewModel : EditViewModelBase<AccountingProcessType>
    {
        public AccountingProcessTypeViewModel(AccountingProcessType DataModel)
            : base(DataModel)
        {
            this.Title = "Hesap işlem Tipi";
            this.Loaded+=new Action(OnLoad);
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }

       

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Hesap Tipi İsmi"));
            this.Validator.AddMaxLengthRule(() => this.Name, 100, ValidationMessage.MaxLengthText("Hesap Tipi İsmi", 100));
            this.Validator.AddRequiredRule(() => this.Code, ValidationMessage.RequiredText("Hesap Tipi Kodu"));
            this.Validator.AddMaxLengthRule(() => this.Code, 10, ValidationMessage.MaxLengthText("Hesap Tipi Kodu", 10));

            this.Validator.AddRule(() => this.Code, () =>
            {
                if (string.IsNullOrEmpty(Code))
                    return RuleResult.Valid();
                else
                {
                    var query = PDYSEntities.DataContext.AccountingProcessTypeSet.Where(item => item.Code == this.Code);

                    if (this.DataModel.ID != 0)
                        query = query.Where(item => item.ID != this.DataModel.ID);

                    if (query.Any())
                    {
                        return RuleResult.Invalid(string.Format("({0}) Hesap Tipi Kodu kullanılmış. Lütfen başka bir kod değeri kullanınız.", this.Code));
                    }
                    else
                        return RuleResult.Valid();

                }
            });

            

            //this.Validator.AddRule(() => this.IsSystem, () =>
            //    {
            //        if (this.IsSystem)
            //            return RuleResult.Invalid("Sistem Tanımlamaları Değiştirilemez");
            //        else
            //            return RuleResult.Valid();
            //    }
            //);
        }

        #region Page Property

        #region Property Code

        private string _Code;

        public string Code
        {
            get { return this._Code; }
            set
            {
                if (!object.Equals(this._Code, value))
                {
                    this._Code = value;
                    this.OnPropertyChanged(() => this.Code);
                    this.Validator.Validate(() => this.Code);
                }
            }
        }

        #endregion

        #region Property Name

        private string _Name;

        public string Name
        {
            get { return this._Name; }
            set
            {
                if (!object.Equals(this._Name, value))
                {
                    this._Name = value;
                    this.OnPropertyChanged(() => this.Name);
                    this.Validator.Validate(() => this.Name);
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

        #region Property IsSystem

        private bool _IsSystem;

        public bool IsSystem
        {
            get { return this._IsSystem; }
            set
            {
                if (!object.Equals(this._IsSystem, value))
                {
                    this._IsSystem = value;
                    this.OnPropertyChanged(() => this.IsSystem);
                    this.Validator.Validate(() => this.IsSystem);
                }
            }
        }

        #endregion
        #endregion

        #region UI Property

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

        #endregion

        #region Property ListInputOutputType Collection

        private ObservableCollection<Parameter> _ListInputOutputType;

        public ObservableCollection<Parameter> ListInputOutputType
        {
            get { return this._ListInputOutputType = this._ListInputOutputType ?? new ObservableCollection<Parameter>(); }
            private set { this._ListInputOutputType = value; }
        }

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(AccountingProcessType accountingdefination)
        {
            this.Code = accountingdefination.Code;
            this.Name = accountingdefination.Name;
            this.IsDebit = accountingdefination.IsDebit;
            this.IsCredit = accountingdefination.IsCredit;
        
            this.IsSystem = accountingdefination.IsSystem;

            this.IsEditable = !this.IsSystem;
        }

        private void SetDataModel(AccountingProcessType accountingdefination)
        {
            accountingdefination.Code = this.Code;
            accountingdefination.Name = this.Name;
            accountingdefination.IsDebit = this.IsDebit;
            accountingdefination.IsCredit = this.IsCredit;
        
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
                PDYSEntities.DataContext.AccountingProcessTypeSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();
            this.ServicePresenter.CloseWindow(true);
        }

        #endregion


    }
}
