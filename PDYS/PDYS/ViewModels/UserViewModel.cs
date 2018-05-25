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
using Common;
using PDYS.Properties;

namespace PDYS.ViewModels
{
    public class UserViewModel : EditViewModelBase<User>
    {
        public UserViewModel(User DataModel)
            : base(DataModel)
        {
            this.Title = "Kullanici Bilgileri";

            this.IsEditableLogon = true;
            this.IsEditableUserName = true;
            this.IsEditablePassword = true;

            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(EmployeeAccountingViewModel_PropertyChanged);
            this.Loaded += new Action(OnLoad);


        }

        void EmployeeAccountingViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => this.UserName))
            {
                if (object.Equals(this.UserName, User.AdminUser))
                {
                    this.IsEditableLogon = false;
                    this.IsEditableUserName = false;
                    this.IsEditablePassword = true;
                }
                else if (object.Equals(this.UserName, User.SystemUser))
                {
                    this.IsEditableLogon = false;
                    this.IsEditableUserName = false;
                    this.IsEditablePassword = false;
                }


                this.IsEditable = object.Equals(ApplicationDataModel.CurrentUser.UserName, User.AdminUser) || object.Equals(this.UserName, ApplicationDataModel.CurrentUser.UserName);

                this.Validator.ValidateAll();
            }
            else if (e.PropertyName == GetPropertyName(() => this.Password))
            {
                this.Validator.Validate(() => this.PasswordConfirm);
            }
            else if (e.PropertyName == GetPropertyName(() => this.PasswordConfirm))
            {
                this.Validator.Validate(() => this.Password);
            }
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.FullName, ValidationMessage.RequiredText("Tam Adı"));
            this.Validator.AddMaxLengthRule(() => this.FullName, 100, ValidationMessage.MaxLengthText("Tam Adı", 100));


            this.Validator.AddRule(() => this.UserName, () =>
            {
                if (this.IsEditableUserName && string.IsNullOrEmpty(this.UserName))
                    return RuleResult.Invalid(ValidationMessage.RequiredText("Kullanıcı Adı"));
                else
                    return RuleResult.Valid();
            }
            );


            this.Validator.AddRule(() => this.Password, () =>
                {
                    if (this.IsEditablePassword && string.IsNullOrEmpty(this.Password))
                        return RuleResult.Invalid(ValidationMessage.RequiredText("Parola"));
                    else if (!object.Equals(this.Password, this.PasswordConfirm))
                        return RuleResult.Invalid("Parola ve Parola Tekrar eşit değil.");
                    else
                        return RuleResult.Valid();
                }
            );

            this.Validator.AddRule(() => this.PasswordConfirm, () =>
            {
                if (this.IsEditablePassword && string.IsNullOrEmpty(this.Password))
                    return RuleResult.Invalid(ValidationMessage.RequiredText("Parola Tekrar"));
                else if (!object.Equals(this.Password, this.PasswordConfirm))
                    return RuleResult.Invalid("Parola ve Parola Tekrar eşit değil.");
                else
                    return RuleResult.Valid();
            }
            );

            this.Validator.AddMaxLengthRule(() => this.Email, 25, ValidationMessage.MaxLengthText("E-Posta", 25));
            this.Validator.AddRule(() => this.Email,
                              () =>
                              {
                                  if (string.IsNullOrEmpty(this.Email))
                                      return RuleResult.Valid();

                                  const string regexPattern = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
                                  return RuleResult.Assert(Regex.IsMatch(this.Email, regexPattern), "E-Posta formatı Hatalı");
                              });
        }

        #region Page Property

        #region Property FullName

        private string _FullName;

        public string FullName
        {
            get { return this._FullName; }
            set
            {
                if (!object.Equals(this._FullName, value))
                {
                    this._FullName = value;
                    this.OnPropertyChanged(() => this.FullName);
                    this.Validator.Validate(() => this.FullName);
                }
            }
        }

        #endregion

        #region Property UserName

        private string _UserName;

        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (!object.Equals(this._UserName, value))
                {
                    this._UserName = value;
                    this.OnPropertyChanged(() => this.UserName);
                    this.Validator.Validate(() => this.UserName);
                }
            }
        }

        #endregion

        #region Property Password

        private string _Password;

        public string Password
        {
            get { return this._Password; }
            set
            {
                if (!object.Equals(this._Password, value))
                {
                    this._Password = value;
                    this.OnPropertyChanged(() => this.Password);
                    this.Validator.Validate(() => this.Password);
                }
            }
        }

        #endregion

        #region Property PasswordConfirm

        private string _PasswordConfirm;

        public string PasswordConfirm
        {
            get { return this._PasswordConfirm; }
            set
            {
                if (!object.Equals(this._PasswordConfirm, value))
                {
                    this._PasswordConfirm = value;
                    this.OnPropertyChanged(() => this.PasswordConfirm);
                    this.Validator.Validate(() => this.PasswordConfirm);
                }
            }
        }

        #endregion

        #region Property IsLogon

        private bool _IsLogon;

        public bool IsLogon
        {
            get { return this._IsLogon; }
            set
            {
                if (!object.Equals(this._IsLogon, value))
                {
                    this._IsLogon = value;
                    this.OnPropertyChanged(() => this.IsLogon);
                    this.Validator.Validate(() => this.IsLogon);
                }
            }
        }

        #endregion

        #region Property Email

        private string _Email;

        public string Email
        {
            get { return this._Email; }
            set
            {
                if (!object.Equals(this._Email, value))
                {
                    this._Email = value;
                    this.OnPropertyChanged(() => this.Email);
                    this.Validator.Validate(() => this.Email);
                }
            }
        }

        #endregion

        #endregion

        #region UI Property

        #region Property IsEditableUserName

        private bool _IsEditableUserName;

        public bool IsEditableUserName
        {
            get { return this._IsEditableUserName; }
            set
            {
                if (!object.Equals(this._IsEditableUserName, value))
                {
                    this._IsEditableUserName = value;
                    this.OnPropertyChanged(() => this.IsEditableUserName);
                    this.Validator.Validate(() => this.IsEditableUserName);
                }
            }
        }

        #endregion

        #region Property IsEditableLogon

        private bool _IsEditableLogon;

        public bool IsEditableLogon
        {
            get { return this._IsEditableLogon; }
            set
            {
                if (!object.Equals(this._IsEditableLogon, value))
                {
                    this._IsEditableLogon = value;
                    this.OnPropertyChanged(() => this.IsEditableLogon);
                    this.Validator.Validate(() => this.IsEditableLogon);
                }
            }
        }

        #endregion

        #region Property IsEditablePassword

        private bool _IsEditablePassword;

        public bool IsEditablePassword
        {
            get { return this._IsEditablePassword; }
            set
            {
                if (!object.Equals(this._IsEditablePassword, value))
                {
                    this._IsEditablePassword = value;
                    this.OnPropertyChanged(() => this.IsEditablePassword);
                    this.Validator.Validate(() => this.IsEditablePassword);
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
                    this.Validator.Validate(() => this.IsEditable);
                }
            }
        }

        #endregion

        #endregion



        #region Model & ViewModel Transformation

        private void SetViewModel(User user)
        {
            this.FullName = user.FullName;
            this.UserName = user.UserName;
            if (this.DataModel.ID == 0)
                this.IsLogon = true;
            else
                this.IsLogon = user.IsLogon;

            this.Email = user.Email;


        }

        private void SetDataModel(User user)
        {
            user.FullName = this.FullName;
            user.UserName = this.UserName;
            user.Password = CyrptoTool.CyrptoText(this.Password);
            user.IsLogon = this.IsLogon;
            user.Email = this.Email;
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
                PDYSEntities.DataContext.UserSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }



        #endregion


    }
}
