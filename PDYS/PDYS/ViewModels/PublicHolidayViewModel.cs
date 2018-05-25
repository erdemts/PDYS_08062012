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
    public class PublicHolidayViewModel : EditViewModelBase<PublicHoliday>
    {
        public PublicHolidayViewModel(PublicHoliday DataModel)
            : base(DataModel)
        {
            this.Title = "Resmi Tatil Bilgisi";
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";

            this.Loaded+=new Action(OnLoad);
            
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Tatil Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50,ValidationMessage.MaxLengthText("Tatil Adı", 50));

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

        #region Property OvertimeFactor

        private decimal _overtimefactor;

        public decimal OvertimeFactor
        {
            get { return this._overtimefactor; }
            set
            {
                if (!object.Equals(this._overtimefactor, value))
                {
                    this._overtimefactor = value;
                    this.OnPropertyChanged(() => this.OvertimeFactor);
                    this.Validator.Validate(() => this.OvertimeFactor);
                }
            }
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(PublicHoliday publicholiday)
        {
            this.Name = publicholiday.Name;
            this.Description = publicholiday.Description;

            if (publicholiday.StartDate != DateTime.MinValue)
                this.StartDate = publicholiday.StartDate;

            if (publicholiday.EndDate != DateTime.MinValue)
                this.EndDate = publicholiday.EndDate;

            this.OvertimeFactor = publicholiday.OvertimeFactor;

            this.SelectedState = this.ListState.SingleOrDefault(p => publicholiday.State == p.Value);
        }

        private void SetDataModel(PublicHoliday publicholiday)
        {
            publicholiday.Name = this.Name; 
            publicholiday.Description = this.Description;

            publicholiday.StartDate = this.StartDate.Value;
            publicholiday.EndDate = this.EndDate.Value;
            publicholiday.OvertimeFactor = this.OvertimeFactor;

            publicholiday.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
            
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

                PDYSEntities.DataContext.PublicHolidaySet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }

      

        #endregion


    }
}
