using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using Mvvm;

namespace PDYS.ViewModels
{
    public class WeeklyOvertimeListViewModel: ListViewModelBase<WeeklyOvertime,WeeklyOvertimeViewModel>
    {

        public WeeklyOvertimeListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.Loaded += new Action(WeeklyOvertimeListViewModel_Loaded);
        }

        void WeeklyOvertimeListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

      

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
            var query = PDYSEntities.DataContext.WeeklyOvertimeSet.AsQueryable();

            #region Query Expression

            if (!string.IsNullOrEmpty(this.Name))
                query = query.Where(item => item.Name.Contains(this.Name));

            query = query.OrderBy(item => item.Name);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion

       
    }
}
