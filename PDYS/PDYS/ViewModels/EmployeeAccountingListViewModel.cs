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
    public class EmployeeAccountingListViewModel : ListViewModelBase<EmployeeAccounting, EmployeeAccountingViewModel>
    {

        public EmployeeAccountingListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.IsMultiSelect = true;
            this.IsDeleteCommand = true;
            this.OnDeleted += new Action<IEnumerable<EmployeeAccounting>>(PersonalInputOutputListViewModel_OnDeleted);
            this.Loaded += new Action(PersonalInputOutputListViewModel_Loaded);
        }

        

        void PersonalInputOutputListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

        void PersonalInputOutputListViewModel_OnDeleted(IEnumerable<EmployeeAccounting> deleteditems)
        {
            bool iscontinue = true;

            ConfirmParam param = new ConfirmParam();
            param.Message = "Silme işlemini onaylıyormusunuz ?";
            param.OnConfirmResult = (result) =>
            {
                if (result == ConfirmParam.ConfirmResult.No)
                    iscontinue = false;
            };

            this.ServicePresenter.ConfirmMessage(param);

            if (!iscontinue)
                return;

            foreach (var item in deleteditems)
            {
                if(!item.EmployeeSalaryScoringID.HasValue)
                    PDYSEntities.DataContext.EmployeeAccountingSet.Remove(item);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.LoadData();

        }

        #region Property Personal

        private Employee _personal;

        public Employee Personal
        {
            get { return this._personal; }
            set
            {
                if (!object.Equals(this._personal, value))
                {
                    this._personal = value;
                    this.OnPropertyChanged(() => this.Personal);
                    this.Validator.Validate(() => this.Personal);
                }
            }
        }

        #endregion

        #region Property LookupPersonal

        private LookupViewModel<EmployeeListViewModel> _lookupPersonal;

        public LookupViewModel<EmployeeListViewModel> LookupPersonal
        {
            get
            {
                if (this._lookupPersonal == null)
                    this._lookupPersonal = new LookupViewModel<EmployeeListViewModel>() { Title = "Personel Seçimi" };

                return this._lookupPersonal;
            }
        }

        #endregion

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
            var query = PDYSEntities.DataContext.EmployeeAccountingSet.AsQueryable();

            #region Query Expression

            //if (this.Personal != null)
            //    query = query.Where(item => item.EmployeeID == this.Personal.ID);

            //if (this.StartDate.HasValue && !this.EndDate.HasValue)
            //    query = query.Where(item => this.StartDate >= item.InputOutputDate);

            //if (this.EndDate.HasValue && !this.StartDate.HasValue)
            //    query = query.Where(item => this.EndDate <= item.InputOutputDate);

            //if (this.EndDate.HasValue && this.StartDate.HasValue)
            //    query = query.Where(item => this.StartDate <= item.InputOutputDate && item.InputOutputDate <= this.EndDate);

            query = query.OrderByDescending(item => item.Employee.FirstName);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion
       
    }
}
