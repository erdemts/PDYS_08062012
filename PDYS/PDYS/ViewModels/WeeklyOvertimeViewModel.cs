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
    public class WeeklyOvertimeViewModel: EditViewModelBase<WeeklyOvertime>
    {
        public WeeklyOvertimeViewModel(WeeklyOvertime DataModel)
            : base(DataModel)
        {
            this.Title = "Haftalık Mesai Tanımı";
            this.Loaded+=new Action(OnLoad);
        }

        void OnLoad()
        {
            SetViewModel(this.DataModel);
        }

        protected override void InitValidation()
        {

            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Mesai Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50, ValidationMessage.MaxLengthText("Mesai Adı", 50));

            this.Validator.AddRule(() => this.RegularHrs
                , () => RuleResult.Assert(this.RegularHrs != DateTime.MinValue, ValidationMessage.RequiredText("Günlük Çalışma Saati")));

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

        #region Property RegularHrs

        private DateTime _regularhrs;

        public DateTime RegularHrs
        {
            get { return this._regularhrs; }
            set
            {
                if (!object.Equals(this._regularhrs, value))
                {
                    this._regularhrs = value;
                    this.OnPropertyChanged(() => this.RegularHrs);
                    this.Validator.Validate(() => this.RegularHrs);
                }
            }
        }

        #endregion

        #region Property OvertimeFactor

        private decimal _overtimeFactor;

        public decimal OvertimeFactor
        {
            get { return this._overtimeFactor; }
            set
            {
                if (!object.Equals(this._overtimeFactor, value))
                {
                    this._overtimeFactor = value;
                    this.OnPropertyChanged(() => this.OvertimeFactor);
                    this.Validator.Validate(() => this.OvertimeFactor);
                }
            }
        }

        #endregion

        #region Property MissingFactor

        private decimal _missingfactor;

        public decimal MissingFactor
        {
            get { return this._missingfactor; }
            set
            {
                if (!object.Equals(this._missingfactor, value))
                {
                    this._missingfactor = value;
                    this.OnPropertyChanged(() => this.MissingFactor);
                    this.Validator.Validate(() => this.MissingFactor);
                }
            }
        }

        #endregion

        #region Property PubHolidayFactor

        private decimal _pubHolidayFactor;

        public decimal PubHolidayFactor
        {
            get { return this._pubHolidayFactor; }
            set
            {
                if (!object.Equals(this._pubHolidayFactor, value))
                {
                    this._pubHolidayFactor = value;
                    this.OnPropertyChanged(() => this.PubHolidayFactor);
                    this.Validator.Validate(() => this.PubHolidayFactor);
                }
            }
        }

        #endregion

        #region Property HolidayFactor

        private decimal _holidayFactor;

        public decimal HolidayFactor
        {
            get { return this._holidayFactor; }
            set
            {
                if (!object.Equals(this._holidayFactor, value))
                {
                    this._holidayFactor = value;
                    this.OnPropertyChanged(() => this.HolidayFactor);
                    this.Validator.Validate(() => this.HolidayFactor);
                }
            }
        }

        #endregion

        #region Property DefenceDuration

        private DateTime _defenceduration;

        public DateTime DefenceDuration
        {
            get { return this._defenceduration; }
            set
            {
                if (!object.Equals(this._defenceduration, value))
                {
                    this._defenceduration = value;
                    this.OnPropertyChanged(() => this.DefenceDuration);
                    this.Validator.Validate(() => this.DefenceDuration);
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


        

        #region Property OvertimeList Collection

        private ObservableCollection<OvertimeViewModel> _overtimelist;

        public ObservableCollection<OvertimeViewModel> OvertimeList
        {
            get { return this._overtimelist = this._overtimelist ?? new ObservableCollection<OvertimeViewModel>(); }
            private set { this._overtimelist = value; }
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(WeeklyOvertime weeklyovertime)
        {
            this.Name = weeklyovertime.Name;
            this.Description = weeklyovertime.Description;
            this.RegularHrs = new DateTime(weeklyovertime.RegularHrs);
            this.OvertimeFactor = weeklyovertime.OvertimeFactor;
            this.MissingFactor = weeklyovertime.MissingFactor;
            this.PubHolidayFactor = weeklyovertime.PubHolidayFactor;
            this.HolidayFactor = weeklyovertime.HolidayFactor;
            this.DefenceDuration = new DateTime(weeklyovertime.DefenceDuration);
            this.DailyCheckPoint = new DateTime(weeklyovertime.DailyCheckPoint);


            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Monday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Tuesday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Wednesday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Thursday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Friday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Saturday, CopyOvertime, PasteOvertime));
            this.OvertimeList.Add(new OvertimeViewModel(weeklyovertime.Sunday, CopyOvertime, PasteOvertime));

          
            Validator.AddChildValidatableCollection(() => this.OvertimeList);

        }

        private void SetDataModel(WeeklyOvertime weeklyovertime)
        {
            weeklyovertime.Name = this.Name;
            weeklyovertime.Description = this.Description;
            weeklyovertime.OvertimeFactor = this.OvertimeFactor;
            weeklyovertime.MissingFactor = this.MissingFactor;
            weeklyovertime.PubHolidayFactor = this.PubHolidayFactor;
            weeklyovertime.HolidayFactor = this.HolidayFactor;

            weeklyovertime.RegularHrs = this.RegularHrs.Ticks;
            weeklyovertime.DefenceDuration = this.DefenceDuration.Ticks;
            weeklyovertime.DailyCheckPoint = this.DailyCheckPoint.Ticks;


            weeklyovertime.Monday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Monday.Day).GetOvertime();
            weeklyovertime.Tuesday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Tuesday.Day).GetOvertime();
            weeklyovertime.Wednesday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Wednesday.Day).GetOvertime();
            weeklyovertime.Thursday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Thursday.Day).GetOvertime();
            weeklyovertime.Friday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Friday.Day).GetOvertime();
            weeklyovertime.Saturday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Saturday.Day).GetOvertime();
            weeklyovertime.Sunday = this.OvertimeList.First(item => item.DayNumber == this.DataModel.Sunday.Day).GetOvertime();
               
        }

        #region Copy & Paste

        private OvertimeViewModel coypiedovertime = null;

        private void CopyOvertime(OvertimeViewModel overtime)
        {
            coypiedovertime = overtime;
        }

        private void PasteOvertime(OvertimeViewModel overtime)
        {
            if (coypiedovertime == null)
                return;

            overtime.IsHoliday = coypiedovertime.IsHoliday;
            overtime.Start = coypiedovertime.Start;
            overtime.LunchOut = coypiedovertime.LunchOut;
            overtime.LunchIn = coypiedovertime.LunchIn;
            overtime.End = coypiedovertime.End;
        }

        #endregion

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

                PDYSEntities.DataContext.WeeklyOvertimeSet.Add(this.DataModel);
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
