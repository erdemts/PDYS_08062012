using System;
using System.Linq;
using PDYS.InfraStructure;
using System.Data.Entity;
using PDYS.Models;
using PDYS.Report.Design;
using System.Collections.Generic;
using PDYS.Report.Model;
using PDYS.ViewModels;
using System.Collections.ObjectModel;
using System.Collections;


namespace PDYS.Report.ViewModels
{
    public class RptEmployeeListViewModel : ReportViewModelBase<RptEmployeeList>
    {

        public RptEmployeeListViewModel()
        {
            
        }


        #region Parameter

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


        #region Property Department

        private Employee _manager;

        public Employee Manager
        {
            get { return this._manager; }
            set
            {
                if (!object.Equals(this._manager, value))
                {
                    this._manager = value;
                    this.OnPropertyChanged(() => this.Manager);
                    this.Validator.Validate(() => this.Manager);
                }
            }
        }

        #endregion

        #region Property LookupManager

        private LookupViewModel<EmployeeListViewModel> _lookupManager;

        public LookupViewModel<EmployeeListViewModel> LookupManager
        {
            get
            {
                if (this._lookupManager == null)
                    this._lookupManager = new LookupViewModel<EmployeeListViewModel>() { Title = "Yönetici Seçimi" };

                return this._lookupManager;
            }
        }

        #endregion


        #region Property Department

        private Department _Department;

        public Department Department
        {
            get { return this._Department; }
            set
            {
                if (!object.Equals(this._Department, value))
                {
                    this._Department = value;
                    this.OnPropertyChanged(() => this.Department);
                    this.Validator.Validate(() => this.Department);
                }
            }
        }

        #endregion

        #region Property LookupDepartment

        private LookupViewModel<DepartmentListViewModel> _lookupDepartment;

        public LookupViewModel<DepartmentListViewModel> LookupDepartment
        {
            get
            {
                if (this._lookupDepartment == null)
                    this._lookupDepartment = new LookupViewModel<DepartmentListViewModel>() { Title = "Departman Seçimi" };

                return this._lookupDepartment;
            }
        }

        #endregion

        #endregion 
        

        public override IEnumerable LoadReportDataSource()
        {

            var query = PDYSEntities.DataContext.EmployeeSet.AsQueryable();

             #region Query Expression

            

            if (!string.IsNullOrEmpty(this.SearchText))
                query = query.Where(item => (item.FirstName + " " + item.LastName).Contains(this.SearchText));

            if (this.Manager != null)
                query = query.Where(item => item.Manager.ID == this.Manager.ID);

            if (this.Department != null)
                query = query.Where(item => item.Department.ID == this.Department.ID);

            query = query.OrderBy(item => item.FirstName);
            query = query.OrderBy(item => item.LastName);

            #endregion

            var list = query.ToList();

            return list;
        }
    }
}
