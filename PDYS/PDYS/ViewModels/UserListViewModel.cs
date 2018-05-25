using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;
using PDYS.InfraStructure;
using PDYS.Interfaces;
using Mvvm;
using PDYS.Services;
using PDYS.Services.ServiceParam;

namespace PDYS.ViewModels
{
    public class UserListViewModel : ListViewModelBase<User, UserViewModel>
    {

        public UserListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.IsMultiSelect = false;
            this.IsDeleteCommand = false;
            
            this.Loaded += new Action(PersonalInputOutputListViewModel_Loaded);
        }

        

        void PersonalInputOutputListViewModel_Loaded()
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
            var query = PDYSEntities.DataContext.UserSet.AsQueryable();

            #region Query Expression

            if (!string.IsNullOrEmpty(this.SearchText))
                query = query.Where(item => item.FullName.Contains(this.SearchText) || item.UserName.Contains(this.SearchText) );

            query = query.OrderBy(item => item.FullName);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion
       
    }
}
