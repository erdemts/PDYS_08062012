using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;

namespace PDYS.ViewModels
{
    public class WorkingTimeViewModel : ViewModelBase
    {
        public WorkingTimeViewModel(WorkingTime datamodel)
        {
            this.Time = datamodel.Time;
            this.Difference = datamodel.Difference;
            this.IsValid = datamodel.IsValid;

            this.Validator.ValidateAllAsync();
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(WorkingTimeViewModel_PropertyChanged);
        }

        protected override void InitValidation()
        {
            base.InitValidation();
        }

        void WorkingTimeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        #region Property Time

        private DateTime? _Time;

        public DateTime? Time
        {
            get { return this._Time; }
            set
            {
                if (!object.Equals(this._Time, value))
                {
                    this._Time = value;
                    this.OnPropertyChanged(() => this.Time);
                    this.Validator.Validate(() => this.Time);
                }
            }
        }

        #endregion

        #region Property Difference

        private double _Difference;

        public double Difference
        {
            get { return this._Difference; }
            set
            {
                if (!object.Equals(this._Difference, value))
                {
                    this._Difference = value;
                    this.OnPropertyChanged(() => this.Difference);
                    this.Validator.Validate(() => this.Difference);
                }
            }
        }

        #endregion

        #region Property IsValid

        private bool _IsValid;

        public bool IsValid
        {
            get { return this._IsValid; }
            set
            {
                if (!object.Equals(this._IsValid, value))
                {
                    this._IsValid = value;
                    this.OnPropertyChanged(() => this.IsValid);
                    this.Validator.Validate(() => this.IsValid);
                }
            }
        }

        #endregion

        public WorkingTime GetWorkingTime()
        {
            WorkingTime wt = new WorkingTime();
            wt.Time = this.Time;
            wt.Difference = this.Difference;
            wt.IsValid = this.IsValid;

            return wt;
        }

        
    }
}
