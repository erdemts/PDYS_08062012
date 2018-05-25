using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using System.Windows.Input;
using Mvvm;
using Mvvm.Validation;
using PDYS.Models;
using PDYS.Helper;
using System.Data.Entity;
using System.Collections.ObjectModel;
using PDYS.Services;

namespace PDYS.ViewModels
{
    public class DepartmentViewModel : EditViewModelBase<Department>
    {
        public DepartmentViewModel(Department DataModel)
            : base(DataModel)
        {
            this.Title = "Departman Bilgisi";
            this.AcceptButtonTitle = "Kaydet";
            this.CancelCommandTitle = "Kapat";

            this.Loaded += new Action(OnLoad);

        }

       
        void OnLoad()
        {
            SetViewModel(this.DataModel);
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

        #region Property ParentDepartment

        private Department _parentDepartment;

        public Department ParentDepartment
        {
            get { return this._parentDepartment; }
            set
            {
                
                    this._parentDepartment = value;
                    this.OnPropertyChanged(() => this.ParentDepartment);
                    this.Validator.Validate(() => this.ParentDepartment);
                
            }
        }

        #endregion


        #region Property LookupParentDepartment

        private LookupViewModel<DepartmentListViewModel> _lookupParentDepartment;

        public LookupViewModel<DepartmentListViewModel> LookupParentDepartment
        {
            get
            {
                if (_lookupParentDepartment == null)
                    this._lookupParentDepartment = new LookupViewModel<DepartmentListViewModel>() 
                    { 
                        Title = "Ana Departman Seçimi",
                    };

                return _lookupParentDepartment;
            }
        }

        

        #endregion



        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Departman Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50, ValidationMessage.MaxLengthText("Departman Adı", 50));

            this.Validator.AddRule(()=>this.ParentDepartment,    () =>
            {
                if (this.ParentDepartment != null && this.ParentDepartment.ID == this.DataModel.ID)
                {
                    return RuleResult.Invalid("Bir departmanın ana departmanı kendisi olamaz.");
                }
                else
                    return RuleResult.Valid();

            });
        }

      

        private void SetViewModel(Department department)
        {
            this.Name = department.Name;
            this.ParentDepartment = department.ParentDepartment;
            this.SelectedState = this.ListState.SingleOrDefault(p => department.State == p.Value);
        }

        private void SetDataModel(Department department)
        {
            department.Name = this.Name;
            department.ParentDepartment = this.ParentDepartment;
            department.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
        }

        protected override void ExcuteAcceptCommand()
        {
            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            if (this.DataModel.ID == 0)
            {

                PDYSEntities.DataContext.DepartmentSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);

        }

       

    }
}
