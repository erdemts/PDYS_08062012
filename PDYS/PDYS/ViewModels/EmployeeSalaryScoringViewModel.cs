using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using Mvvm;
using Mvvm.Validation;
using PDYS.Helper;
using PDYS.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DeviceManagement;
using System.Globalization;

namespace PDYS.ViewModels
{
    public class EmployeeSalaryScoringViewModel : EditViewModelBase<EmployeeSalaryScoring>
    {
        public EmployeeSalaryScoringViewModel(EmployeeSalaryScoring DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Puantaj Bilgisi";
            this.Loaded += new Action(InternalLoaded);

            #region Input Output List
            

            this.EmployeeInOutScoringListVM.IsNewCommand = false;
            this.EmployeeInOutScoringListVM.IsOpenCommand = false;
            this.EmployeeInOutScoringListVM.IsAppendCommand = false;
            this.EmployeeInOutScoringListVM.IsDeleteCommand = false;
            this.EmployeeInOutScoringListVM.IsMultiSelect = false;

            this.EmployeeInOutScoringListVM.QueryExpression = PDYSEntities.DataContext.EmployeeInOutScoringSet.Where(item => item.EmployeeSalaryScoringID == this.DataModel.ID).OrderBy(item => item.ScoringDate);
                //this.DataModel.EmployeeInOutScorings.AsQueryable().OrderBy(item => item.ScoringDate);

            #endregion
        }


        void InternalLoaded()
        {
            #region Load ProcessState Parameter

            this.ListState.Clear();

            var queryState = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "ProcessState"
                             orderby item.Text
                             select item;

            queryState.ToList().ForEach(item => this.ListState.Add(item));

            #endregion

            this.SetViewModel(this.DataModel);

            this.EmployeeInOutScoringListVM.LoadData();
        }

     


        protected override void InitValidation()
        {
            
        }

        #region Page Property

        #region Property Employee

        private Employee _employee;

        public Employee Employee
        {
            get { return this._employee; }
            set
            {
                if (!object.Equals(this._employee, value))
                {
                    this._employee = value;
                    this.OnPropertyChanged(() => this.Employee);
                    this.Validator.Validate(() => this.Employee);
                }
            }
        }

        #endregion

        #region Property LookupEmployee

        private LookupViewModel<EmployeeListViewModel> _lookupemployee;

        public LookupViewModel<EmployeeListViewModel> LookupEmployee
        {
            get
            {
                if (this._lookupemployee == null)
                    this._lookupemployee = new LookupViewModel<EmployeeListViewModel>() { Title = "Personel Seçimi" };

                return this._lookupemployee;
            }
        }

        #endregion

        
     
        #endregion

        #region EmployeeInputOutputListVM

        private EmployeeInOutScoringListViewModel _EmployeeInOutScoringListVM;

        public EmployeeInOutScoringListViewModel EmployeeInOutScoringListVM
        {
            get
            {
                if (this._EmployeeInOutScoringListVM == null)
                {
                    this._EmployeeInOutScoringListVM = new EmployeeInOutScoringListViewModel(false);
                }

                return this._EmployeeInOutScoringListVM;
            }

        }

        #endregion

        

        #region Model & ViewModel Transformation

        private void SetViewModel(EmployeeSalaryScoring inoutscoring)
        {
            this.Employee = inoutscoring.Employee;
            this.SelectedState = this.ListState.SingleOrDefault(p => inoutscoring.ProcessState == p.Value);

            for (int i = this.ListState.Count - 1; i > -1; i--)
            {
                var item = this.ListState[i];

                if (!object.Equals(item, this.SelectedState))
                    this.ListState.Remove(item);
            }
        }

        private void SetDataModel(EmployeeSalaryScoring inoutscoring)
        {
            //readerdevice.Name = this.Name;
            //readerdevice.SerialNumber = this.SerialNumber;
            //readerdevice.Description = this.Description;
            //readerdevice.IP = this.IP;
            //readerdevice.Port = this.Port;
            //if (this.SelectedInputOutputType != null)
            //    readerdevice.InputOutputType = this.SelectedInputOutputType.Value;
            //else
            //    readerdevice.InputOutputType = null;

            //readerdevice.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;

        }

        #endregion

        #region  Command

      

        protected override void ExcuteAcceptCommand()
        {
            this.ServicePresenter.ShowAlertMessage("Hakediş hesaplamaları değiştirilemez.");
            return;
        }

       

        #endregion


    }
}
