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
    public class DepartmentListViewModel : ListViewModelBase<Department,DepartmentViewModel>
    {

        public DepartmentListViewModel(bool autoload)
            : base(autoload)
        {
            this.Loaded += new Action(DepartmentListViewModel_Loaded);
        }

        void DepartmentListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

        

        #region Property SearchText

        private string _searchText;

        public string SearchText
        {
            get { return this._searchText; }
            set
            {
                if (!object.Equals(this._searchText, value))
                {
                    this._searchText = value;
                    this.OnPropertyChanged(() => this.SearchText);
                    this.Validator.Validate(() => this.SearchText);
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
            #region Query Expression

            bool isNotCriteria = string.IsNullOrEmpty(this.SearchText);

            var query = from item in PDYSEntities.DataContext.DepartmentSet
                        where isNotCriteria || (!isNotCriteria && item.Name.Contains(this.SearchText))
                        select item;

            this.QueryExpression = query.OrderBy(item => item.Name);

            #endregion

            LoadData();
        }

        #endregion

       
    }
}
