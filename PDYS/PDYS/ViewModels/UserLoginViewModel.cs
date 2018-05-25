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
    public class UserLoginViewModel : EditViewModelBase<User>
    {
        public UserLoginViewModel(User DataModel)
            : base(DataModel)
        {
            this.Title = "Kullanici Giriş";
            this.AcceptButtonTitle = "Tamam";
            this.CancelCommandTitle = "İptal";
        }

        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.UserName, ValidationMessage.RequiredText("Kullanıcı Adı"));
            //this.Validator.AddRequiredRule(() => this.Password, ValidationMessage.RequiredText("Parola"));
        }

       

        #region Page Property
        
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

            User user = PDYSEntities.DataContext.UserSet.FirstOrDefault(item=> item.UserName == this.UserName);

            if (user == null)
            {
                this.CloseMessage();
                return;
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                string decryptoPwd = CyrptoTool.DeCryptoText(user.Password);
                if (!object.Equals(decryptoPwd, this.Password))
                {
                    CloseMessage();
                    return;
                }
            }

            if (!user.IsLogon)
            {
                this.ServicePresenter.ShowErrorMessage("Kullanıcı Giriş Yetkisi Bulunmamaktadır !..");
                return;
            }

            this.DataModel = user;
            this.ServicePresenter.CloseWindow(true);
        }

        void CloseMessage()
        {
            this.ServicePresenter.ShowErrorMessage("Hatalı Kullanıcı Adı yada Parola !..");
        }



        #endregion


    }
}
