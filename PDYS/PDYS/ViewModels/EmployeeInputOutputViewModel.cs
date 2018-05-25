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

namespace PDYS.ViewModels
{
    public class EmployeeInputOutputViewModel : EditViewModelBase<EmployeeInputOutput>
    {
        public EmployeeInputOutputViewModel(EmployeeInputOutput DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Giriş/çıkış Bilgileri";

            this.Loaded+=new Action(OnLoad);
        }

        void OnLoad()
        {
            #region Load InputOutputType Parameter

            var inputOutputType = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "InputOutputType"
                             orderby item.Value 
                             select item;

            inputOutputType.ToList().ForEach(item => this.ListInputOutputType.Add(item));

            var removeditem = this.ListInputOutputType.FirstOrDefault(i => i.Value == 0); // Bilinmiyor 
            this.ListInputOutputType.Remove(removeditem);

            this.SelectedInputOutputType = this.ListInputOutputType.FirstOrDefault();

            #endregion

            SetViewModel(this.DataModel);
        }

      


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.InputOutputDate, ValidationMessage.RequiredText("Giriş/Çıkış Tarihi"));
            this.Validator.AddRequiredRule(() => this.SelectedInputOutputType, ValidationMessage.RequiredText("Hareket Tipi"));
            this.Validator.AddRequiredRule(() => this.Employee, ValidationMessage.RequiredText("Personel"));
            //this.Validator.AddRequiredRule(() => this.ReaderDevice, ValidationMessage.RequiredText("Okuyucu Cihaz"));
        }

        #region Page Property

        #region Property InputOutputDate

        private DateTime? _InputOutputDate;

        public DateTime? InputOutputDate    
        {
            get { return this._InputOutputDate; }
            set
            {
                if (!object.Equals(this._InputOutputDate, value))
                {
                    this._InputOutputDate = value;
                    this.OnPropertyChanged(() => this.InputOutputDate);
                    this.Validator.Validate(() => this.InputOutputDate);
                }
            }
        }

        #endregion

        #region Property SelectedInputOutputType

        private Parameter _SelectedInputOutputType;

        public Parameter SelectedInputOutputType
        {
            get { return this._SelectedInputOutputType; }
            set
            {
                if (!object.Equals(this._SelectedInputOutputType, value))
                {
                    this._SelectedInputOutputType = value;
                    this.OnPropertyChanged(() => this.SelectedInputOutputType);
                    this.Validator.Validate(() => this.SelectedInputOutputType);
                }
            }
        }

        #endregion

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

        #region Property ReaderDevice

        private ReaderDevice _ReaderDevice;

        public ReaderDevice ReaderDevice
        {
            get { return this._ReaderDevice; }
            set
            {
                if (!object.Equals(this._ReaderDevice, value))
                {
                    this._ReaderDevice = value;
                    this.OnPropertyChanged(() => this.ReaderDevice);
                    this.Validator.Validate(() => this.ReaderDevice);
                }
            }
        }

        #endregion

        #region Property LookupReaderDevice

        private LookupViewModel<ReaderDeviceListViewModel> _LookupReaderDevice;

        public LookupViewModel<ReaderDeviceListViewModel> LookupReaderDevice
        {
            get
            {
                if (this._LookupReaderDevice == null)
                    this._LookupReaderDevice = new LookupViewModel<ReaderDeviceListViewModel>() { Title = "Okuyucu Cihaz Seçimi" };

                return this._LookupReaderDevice;
            }
        }

        #endregion

        #region Property IsEditable

        private bool _IsEditable;

        public bool IsEditable
        {
            get { return this._IsEditable; }
            set
            {
                if (!object.Equals(this._IsEditable, value))
                {
                    this._IsEditable = value;
                    this.OnPropertyChanged(() => this.IsEditable);
                    this.Validator.Validate(() => this.IsEditable);
                }
            }
        }

        #endregion

        #endregion


        #region Property ListInputOutputType Collection

        private ObservableCollection<Parameter> _ListInputOutputType;

        public ObservableCollection<Parameter> ListInputOutputType
        {
            get { return this._ListInputOutputType = this._ListInputOutputType ?? new ObservableCollection<Parameter>(); }
            private set { this._ListInputOutputType = value; }
        }

        #endregion

        #region Model & ViewModel Transformation

        private void SetViewModel(EmployeeInputOutput personalinputoutput)
        {
            this.IsEditable = !personalinputoutput.IsProcessed;

            this.Employee = personalinputoutput.Employee;
            this.ReaderDevice = personalinputoutput.ReaderDevice;

            if (personalinputoutput.InOutDate != DateTime.MinValue)
                this.InputOutputDate = personalinputoutput.InOutDate;

            if (personalinputoutput.InOutType.HasValue)
                this.SelectedInputOutputType = this.ListInputOutputType.SingleOrDefault(io => personalinputoutput.InOutType == io.Value);

            this.SelectedState = this.ListState.SingleOrDefault(p => personalinputoutput.State == p.Value);
        }

        private void SetDataModel(EmployeeInputOutput personalinputoutput)
        {
            personalinputoutput.Employee = this.Employee;
            personalinputoutput.ReaderDevice = this.ReaderDevice;

            if (this.InputOutputDate.HasValue)
                personalinputoutput.InOutDate = this.InputOutputDate.Value;

            personalinputoutput.InOutType = (this.SelectedInputOutputType != null) ? this.SelectedInputOutputType.Value : 0;
            personalinputoutput.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;
        }

        #endregion

        #region  Command

        protected override void ExcuteAcceptCommand()
        {
            if (!IsEditable)
            {
                this.ServicePresenter.ShowAlertMessage("Pauntaj hesaplaması yapılan giriş/çıkış bilgisinde değişiklik yapılamaz.");
                return;
            }

            ValidationResult result = this.Validator.ValidateAll();
            if (!result.IsValid)
            {
                this.ServicePresenter.ShowErrorMessage(result.ToString());
                return;
            }

            if (this.DataModel.ID == 0)
            {
                PDYSEntities.DataContext.EmployeeInputOutputSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            //this.IsSavedData = true;
            //this.ServicePresenter.ShowInformationMessage("Kaydetme İşlemi Tamamlandı."); 
            this.ServicePresenter.CloseWindow(true);
        }

       

        #endregion


    }
}
