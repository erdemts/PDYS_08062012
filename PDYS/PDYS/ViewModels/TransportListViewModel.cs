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
    public class TransportListViewModel : ListViewModelBase<Transport,TransportViewModel>
    {

        public TransportListViewModel(bool autoload)
            : base(autoload)
        {
            this.Loaded += new Action(TransportListViewModel_Loaded);
        }

        void TransportListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

       

        #region Property TransportName

        private string transportname;

        public string TransportName
        {
            get { return this.transportname; }
            set
            {
                if (!object.Equals(this.transportname, value))
                {
                    this.transportname = value;
                    this.OnPropertyChanged(() => this.TransportName);
                    this.Validator.Validate(() => this.TransportName);
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
            var query = PDYSEntities.DataContext.TransportSet.AsQueryable();

            #region Query Expression

            if (!string.IsNullOrEmpty(this.TransportName))
                query = query.Where(item => item.Name.Contains(this.TransportName)
                    || item.DriverName.Contains(this.TransportName)
                    || item.DriverPhone.Contains(this.TransportName)
                    );

            query = query.OrderBy(item => item.Name);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion

       
    }
}
