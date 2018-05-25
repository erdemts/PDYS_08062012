using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using Mvvm.Validation;
using PDYS.Helper;
using System.Collections.ObjectModel;
using System.Globalization;
using Mvvm;

namespace PDYS.ViewModels
{
    [Serializable]
    public class OvertimeViewModel : ViewModelBase
    {

        public Action<OvertimeViewModel> CopyCmd { get; set; }
        public Action<OvertimeViewModel> PasteCmd { get; set; }

        public OvertimeViewModel(Overtime datamodel, Action<OvertimeViewModel> copycmd, Action<OvertimeViewModel> pastecmd)
        {
            this.CopyCmd = copycmd;
            this.PasteCmd = pastecmd;


            CultureInfo uiCul = System.Threading.Thread.CurrentThread.CurrentUICulture;
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OvertimeViewModel_PropertyChanged);

            this.DayNumber = datamodel.Day;
            this.DayName = uiCul.DateTimeFormat.DayNames[(int)datamodel.Day];
            this.IsHoliday = datamodel.IsHoliday;

            this.Start = new DateTime(datamodel.Start);
            this.LunchOut = new DateTime(datamodel.LunchOut);
            this.LunchIn = new DateTime(datamodel.LunchIn);
            this.End = new DateTime(datamodel.End);

        }

        void OvertimeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => this.IsHoliday))
            {
                this.IsEditable = !this.IsHoliday;

                if (this.IsHoliday)
                {
                    // Clear Values
                    this.Start = DateTime.MinValue;
                    this.LunchOut = DateTime.MinValue;
                    this.LunchIn = DateTime.MinValue;
                    this.End = DateTime.MinValue;
                }
            }

            this.Validator.ValidateAllAsync();
        }

        protected override void InitValidation()
        {
            this.Validator.AddRule(() => this.Start, () => RuleResult.Assert((!this.IsEditable || (this.IsEditable && this.Start != DateTime.MinValue)), ValidationMessage.RequiredText(this.DayName +" Mesai Başlagıç Saati")));
            this.Validator.AddRule(() => this.LunchOut, () => RuleResult.Assert((!this.IsEditable || (this.IsEditable && this.LunchOut != DateTime.MinValue)), ValidationMessage.RequiredText(this.DayName + " Öğlen Yemek Saati")));
            this.Validator.AddRule(() => this.LunchIn, () => RuleResult.Assert((!this.IsEditable || (this.IsEditable && this.LunchIn != DateTime.MinValue)), ValidationMessage.RequiredText(this.DayName + " Öğlen Bitiş Saati")));
            this.Validator.AddRule(() => this.End, () => RuleResult.Assert((!this.IsEditable || (this.IsEditable && this.End != DateTime.MinValue)), ValidationMessage.RequiredText(this.DayName + " Mesai Bitiş Saati")));
        }

        #region Property DayNumber

        private System.DayOfWeek _dayNumber;

        public System.DayOfWeek DayNumber
        {
            get { return this._dayNumber; }
            set
            {
                this._dayNumber = value;
                this.OnPropertyChanged(() => this.DayNumber);
                this.Validator.Validate(() => this.DayNumber);
            }
        }

        #endregion

        #region Property DayName

        private string dayName;

        public string DayName
        {
            get { return this.dayName; }
            set
            {
                this.dayName = value;
                this.OnPropertyChanged(() => this.DayName);
                this.Validator.Validate(() => this.DayName);
            }
        }

        #endregion

        #region Property Start

        private DateTime _start;

        public DateTime Start
        {
            get { return this._start; }
            set
            {
                this._start = value;
                this.OnPropertyChanged(() => this.Start);
                this.Validator.Validate(() => this.Start);
            }
        }

        #endregion

        #region Property LunchOut

        private DateTime _lunchout;

        public DateTime LunchOut
        {
            get { return this._lunchout; }
            set
            {
                this._lunchout = value;
                this.OnPropertyChanged(() => this.LunchOut);
                this.Validator.Validate(() => this.LunchOut);
            }
        }

        #endregion

        #region Property LunchIn

        private DateTime _lunchin;

        public DateTime LunchIn
        {
            get { return this._lunchin; }
            set
            {
                this._lunchin = value;
                this.OnPropertyChanged(() => this.LunchIn);
                this.Validator.Validate(() => this.LunchIn);
            }
        }

        #endregion

        #region Property End

        private DateTime _end;

        public DateTime End
        {
            get { return this._end; }
            set
            {
                this._end = value;
                this.OnPropertyChanged(() => this.End);
                this.Validator.Validate(() => this.End);
            }
        }

        #endregion

        #region Property IsHoliday

        private bool _isholiday;

        public bool IsHoliday
        {
            get { return this._isholiday; }
            set
            {
                this._isholiday = value;
                this.OnPropertyChanged(() => this.IsHoliday);
                this.Validator.Validate(() => this.IsHoliday);
            }
        }

        #endregion

        #region Property IsEditable

        private bool _iseditable;

        public bool IsEditable
        {
            get { return this._iseditable; }
            set
            {
                this._iseditable = value;
                this.OnPropertyChanged(() => this.IsEditable);
                this.Validator.Validate(() => this.IsEditable);
            }
        }

        #endregion

        #region Command

        #region Property CopyCommand
        [NonSerialized]
        private RelayCommand _copycommand;
        /// <summary>
        /// Copy Command
        /// </summary>
        public RelayCommand CopyCommand
        {
            get
            {
                if (this._copycommand == null)
                    this._copycommand = new RelayCommand(ExcuteCopyCommand);
                return this._copycommand;
            }
        }

        void ExcuteCopyCommand()
        {
            this.CopyCmd(this);
        }

        #endregion

        #region Property PasteCommand
        [NonSerialized]
        private RelayCommand _pastecommand;
        /// <summary>
        /// Paste Command
        /// </summary>

        public RelayCommand PasteCommand
        {
            get
            {
                if (this._pastecommand == null)
                    this._pastecommand = new RelayCommand(ExcutePasteCommand);
                return this._pastecommand;
            }
        }

        void ExcutePasteCommand()
        {
            this.PasteCmd(this);
            //Validation için
            //this.PasteCmd(this);
        }

        #endregion

        #endregion


        public Overtime GetOvertime()
        {
            Overtime overtime = new Overtime();

            overtime.Day = this.DayNumber;
            overtime.Start = this.Start.Ticks;
            overtime.LunchOut = this.LunchOut.Ticks;
            overtime.LunchIn = this.LunchIn.Ticks;
            overtime.End = this.End.Ticks;
            overtime.IsHoliday = this.IsHoliday;

            return overtime;

        }
    }
}
