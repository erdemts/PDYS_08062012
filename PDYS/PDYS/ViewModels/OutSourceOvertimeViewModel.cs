using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using Mvvm.Validation;
using PDYS.Helper;
using System.Collections.ObjectModel;

namespace PDYS.ViewModels
{
    public class OutSourceOvertimeViewModel : EditViewModelBase<OutSourceOvertime>
    {
        public OutSourceOvertimeViewModel(OutSourceOvertime DataModel)
            : base(DataModel)
        {
            this.Title = "Kümulatif Mesai Tanımı";
            this.Loaded+=new Action(OnLoad);
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }

        protected override void InitValidation()
        {

            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Mesai Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50, ValidationMessage.MaxLengthText("Mesai Adı", 25));

            this.Validator.AddRule(() => this.HourlyPayment, () => RuleResult.Assert(this.HourlyPayment != 0, ValidationMessage.RequiredText("Saati Ücreti")));
            this.Validator.AddRule(() => this.MaximumCharge, () => RuleResult.Assert(this.MaximumCharge != DateTime.MinValue, ValidationMessage.RequiredText("Maksimum Çalışma Saati")));
            this.Validator.AddRule(() => this.DailyCheckPoint, () => RuleResult.Assert(this.DailyCheckPoint != DateTime.MinValue, ValidationMessage.RequiredText("Gün Sonu")));

        }

        #region Page Property

        #region Property Name

        private string _name;

        public string Name
        {
            get { return this._name; }
            set
            {
                if (!object.Equals(this._name, value))
                {
                    this._name = value;
                    this.OnPropertyChanged(() => this.Name);
                    this.Validator.Validate(() => this.Name);
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

        #region Property HourlyPayment

        private decimal _HourlyPaymenty;

        public decimal HourlyPayment
        {
            get { return this._HourlyPaymenty; }
            set
            {
                if (!object.Equals(this._HourlyPaymenty, value))
                {
                    this._HourlyPaymenty = value;
                    this.OnPropertyChanged(() => this.HourlyPayment);
                    this.Validator.Validate(() => this.HourlyPayment);
                }
            }
        }

        #endregion

        #region Property MaximumCharge

        private DateTime _maximumcharge;

        public DateTime MaximumCharge
        {
            get { return this._maximumcharge; }
            set
            {
                if (!object.Equals(this._maximumcharge, value))
                {
                    this._maximumcharge = value;
                    this.OnPropertyChanged(() => this.MaximumCharge);
                    this.Validator.Validate(() => this.MaximumCharge);
                }
            }
        }

        #endregion

        #region Property DailyCheckPoint

        private DateTime _DailyCheckPoint;

        public DateTime DailyCheckPoint
        {
            get { return this._DailyCheckPoint; }
            set
            {
                if (!object.Equals(this._DailyCheckPoint, value))
                {
                    this._DailyCheckPoint = value;
                    this.OnPropertyChanged(() => this.DailyCheckPoint);
                    this.Validator.Validate(() => this.DailyCheckPoint);
                }
            }
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(OutSourceOvertime outsourceovertime)
        {
            this.Name = outsourceovertime.Name;
            this.Description = outsourceovertime.Description;

            this.HourlyPayment = outsourceovertime.HourlyPayment;
            this.MaximumCharge = new DateTime(outsourceovertime.MaximumCharge);
            this.DailyCheckPoint = new DateTime(outsourceovertime.DailyCheckPoint);

        }

        private void SetDataModel(OutSourceOvertime outsourceovertime)
        {
            outsourceovertime.Name = this.Name;
            outsourceovertime.Description = this.Description;
            outsourceovertime.HourlyPayment = this.HourlyPayment;
            outsourceovertime.MaximumCharge = this.MaximumCharge.Ticks;
            outsourceovertime.DailyCheckPoint = this.DailyCheckPoint.Ticks;
        }

        

        #endregion

        #region Command

        protected override void ExcuteAcceptCommand()
        {
            this.Validator.ValidateAllAsync(OnComplateVAlidation);
        }

        void OnComplateVAlidation(ValidationResult result)
        {
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            if (this.DataModel.ID == 0)
            {
                PDYSEntities.DataContext.OutSourceOvertimeSet.Add(this.DataModel);
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
