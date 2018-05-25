using System.Linq;
using PDYS.InfraStructure;
using Mvvm;
using PDYS.Models;
using PDYS.Services;
using PDYS.Helper;
using System.Collections.Generic;
using PDYS.Interfaces;

namespace PDYS.ViewModels
{
	public class ReaderDeviceListViewModel : ListViewModelBase<ReaderDevice,ReaderDeviceViewModel>
	{

        public ReaderDeviceListViewModel(bool autoload)
            : base(autoload)
        {
            this.Loaded += new System.Action(ReaderDeviceListViewModel_Loaded);
        }

        void ReaderDeviceListViewModel_Loaded()
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

            var query = PDYSEntities.DataContext.ReaderDeviceSet.AsQueryable();

            if(!string.IsNullOrEmpty(this.SearchText))
                query= query.Where(item=>item.Name.Contains(this.SearchText));

            this.QueryExpression = query.OrderBy(item => item.Name);

            #endregion

            LoadData();
        }

        #endregion

    }
}
