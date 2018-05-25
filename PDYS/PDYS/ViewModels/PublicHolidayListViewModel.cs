using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;
using PDYS.InfraStructure;
using PDYS.Interfaces;
using Mvvm;

namespace PDYS.ViewModels
{
    public class PublicHolidayListViewModel : ListViewModelBase<PublicHoliday,PublicHolidayViewModel>
    {

        public PublicHolidayListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.Loaded += new Action(PublicHolidayListViewModel_Loaded);
        }

        void PublicHolidayListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

        

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



        #region Property SearchCommand

        private RelayCommand _searchCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SearchCommand
        {
            get
            {
                if (this._searchCommand == null)
                    this._searchCommand = new RelayCommand(ExcuteSearchCommand);
                return this._searchCommand;
            }
        }

        void ExcuteSearchCommand()
        {
            var query = PDYSEntities.DataContext.PublicHolidaySet.AsQueryable();

            #region Query Expression

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate >= item.StartDate && this.StartDate <= item.EndDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => this.EndDate >= item.StartDate && this.EndDate <= item.EndDate);

            if (this.EndDate.HasValue && this.StartDate.HasValue)
                query = query.Where(item => (this.StartDate <= item.StartDate && item.StartDate <= this.EndDate)
                    || (this.StartDate <= item.EndDate && item.EndDate <= this.EndDate)
                    );

            query = query.OrderBy(item => item.ID);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion

       
    }
}
