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
using PDYS.Services.ServiceParam;

namespace PDYS.ViewModels
{
    public class TransportViewModel : EditViewModelBase<Transport>
    {
        public TransportViewModel(Transport DataModel)
            : base(DataModel)
        {
            

            this.Title = "Personel Servis Bilgisi";
            
            this.Loaded+=new Action(OnLoad);

            #region Employee List

            this.TransportEmployees.IsNewCommand = false;
            this.TransportEmployees.IsOpenCommand = false;
            this.TransportEmployees.IsAppendCommand = true;
            this.TransportEmployees.IsDeleteCommand = true;
            this.TransportEmployees.IsMultiSelect = true;

            this.TransportEmployees.QueryExpression = this.DataModel.TransportEmployees.AsQueryable().OrderBy(item=>item.DisplayName);
            this.TransportEmployees.OnInserted += new Action<IEnumerable<Employee>>(TransportEmployees_OnInserted);
            this.TransportEmployees.OnDeleted += new Action<IEnumerable<Employee>>(TransportEmployees_OnDeleted);
            this.TransportEmployees.OnOpening += new Action<Employee>(TransportEmployees_OnOpening);

            #endregion

        }

        

        

        void OnLoad()
        {
            SetViewModel(this.DataModel);

            this.IsNavigationEnabled = (this.DataModel.ID != 0);
            this.TransportEmployees.LoadData();
        }


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Tatil Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50,ValidationMessage.MaxLengthText("Tatil Adı", 50));
            
            this.Validator.AddMaxLengthRule(() => this.VhicleInformation, 50, ValidationMessage.MaxLengthText("Araç Bilgisi", 50));
            
            this.Validator.AddMaxLengthRule(() => this.VhicleInformation, 25, ValidationMessage.MaxLengthText("Araç Plaka", 25));

            this.Validator.AddRequiredRule(() => this.DriverName, ValidationMessage.RequiredText("Sürücü Adı"));
            this.Validator.AddMaxLengthRule(() => this.DriverName, 25, ValidationMessage.MaxLengthText("Sürücü Adı", 25));

            this.Validator.AddRequiredRule(() => this.DriverPhone, ValidationMessage.RequiredText("Sürücü Telefon"));
            this.Validator.AddMaxLengthRule(() => this.DriverPhone, 25, ValidationMessage.MaxLengthText("Sürücü Telefon", 25));
            
        }

        #region Page Property

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

        #region Property Description

        private string _description;

        public string Description
        {
            get { return this._description; }
            set
            {
                if (!object.Equals(this._description, value))
                {
                    this._description = value;
                    this.OnPropertyChanged(() => this.Description);
                    this.Validator.Validate(() => this.Description);
                }
            }
        }

        #endregion

        #region Property VhicleInformation

        private string _vhicleinformation;

        public string VhicleInformation
        {
            get { return this._vhicleinformation; }
            set
            {
                if (!object.Equals(this._vhicleinformation, value))
                {
                    this._vhicleinformation = value;
                    this.OnPropertyChanged(() => this.VhicleInformation);
                    this.Validator.Validate(() => this.VhicleInformation);
                }
            }
        }

        #endregion

        #region Property NumberPlate

        private string _numberplate;

        public string NumberPlate
        {
            get { return this._numberplate; }
            set
            {
                if (!object.Equals(this._numberplate, value))
                {
                    this._numberplate = value;
                    this.OnPropertyChanged(() => this.NumberPlate);
                    this.Validator.Validate(() => this.NumberPlate);
                }
            }
        }

        #endregion

        #region Property DriverName

        private string _drivername;

        public string DriverName
        {
            get { return this._drivername; }
            set
            {
                if (!object.Equals(this._drivername, value))
                {
                    this._drivername = value;
                    this.OnPropertyChanged(() => this.DriverName);
                    this.Validator.Validate(() => this.DriverName);
                }
            }
        }

        #endregion

        #region Property DriverPhone

        private string _driverphone;

        public string DriverPhone
        {
            get { return this._driverphone; }
            set
            {
                if (!object.Equals(this._driverphone, value))
                {
                    this._driverphone = value;
                    this.OnPropertyChanged(() => this.DriverPhone);
                    this.Validator.Validate(() => this.DriverPhone);
                }
            }
        }

        #endregion

        #endregion

        #region Navigation

        #region Property IsNavigationEnabled

        private bool isNavigationEnabled;

        public bool IsNavigationEnabled
        {
            get { return this.isNavigationEnabled; }
            set
            {
                if (!object.Equals(this.isNavigationEnabled, value))
                {
                    this.isNavigationEnabled = value;
                    this.OnPropertyChanged(() => this.IsNavigationEnabled);
                    this.Validator.Validate(() => this.IsNavigationEnabled);
                }
            }
        }

        #endregion

        #region Property TransportEmployees Collection

        private EmployeeListViewModel _transportemployees;

        public EmployeeListViewModel TransportEmployees
        {
            get {
                if (this._transportemployees == null)
                {
                    this._transportemployees = new EmployeeListViewModel(false);
                }

                return this._transportemployees;
            }
            
        }

        void TransportEmployees_OnDeleted(IEnumerable<Employee> deletedrecords)
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

            foreach (var obj in deletedrecords)
            {
                obj.Transport = null;
                this.DataModel.TransportEmployees.Remove(obj);
            }
            PDYSEntities.DataContext.SaveChanges();
            this.TransportEmployees.LoadData();
        }

        void TransportEmployees_OnInserted(IEnumerable<Employee> insertedrecords)
        {
            foreach (var obj in insertedrecords)
            {
                obj.Transport = this.DataModel;
                this.DataModel.TransportEmployees.Add(obj);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.TransportEmployees.LoadData();
        }

        void TransportEmployees_OnOpening(Employee obj)
        {
            obj.Transport = this.DataModel;
        }

        #endregion

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(Transport transport)
        {
            this.Name = transport.Name;
            this.Description = transport.Description;
            this.VhicleInformation = transport.VhicleInformation;
            this.NumberPlate = transport.NumberPlate;
            this.DriverName = transport.DriverName;
            this.DriverPhone = transport.DriverPhone;

            this.SelectedState = this.ListState.SingleOrDefault(p => transport.State == p.Value);
        }

        private void SetDataModel(Transport transport)
        {
            transport.Name = this.Name;
            transport.Description = this.Description;
            transport.VhicleInformation = this.VhicleInformation;
            transport.NumberPlate = this.NumberPlate;
            transport.DriverName = this.DriverName;
            transport.DriverPhone = this.DriverPhone;

            transport.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
        }

        #endregion

        #region  Command

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

                PDYSEntities.DataContext.TransportSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();
            
            this.IsNavigationEnabled = true;
            this.IsSavedData = true;
            this.ServicePresenter.ShowInformationMessage("Kaydetme İşlemi Tamamlandı.");
            
        }

        #endregion


    }
}
