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
    public class ReaderDeviceViewModel : EditViewModelBase<ReaderDevice>
    {
        public ReaderDeviceViewModel(ReaderDevice DataModel)
            : base(DataModel)
        {
            this.Title = "Okuyucu Cihaz Bilgisi";

            this.Loaded += new Action(ReaderDeviceViewModel_Loaded);
        }

        void ReaderDeviceViewModel_Loaded()
        {
            #region Load InputOutputType Parameter

            var inputOutputType = from item in PDYSEntities.DataContext.ParameterSet
                                  where item.Name == "InputOutputType"
                                  orderby item.Value
                                  select item;

            inputOutputType.ToList().ForEach(item => this.ListInputOutputType.Add(item));

            this.SelectedInputOutputType = this.ListInputOutputType.FirstOrDefault();

            #endregion

            this.Port = 4370;
            this.ComKey = 0;
            SetViewModel(this.DataModel);
        }



        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.Name, ValidationMessage.RequiredText("Cihaz Adı"));
            this.Validator.AddMaxLengthRule(() => this.Name, 50, ValidationMessage.MaxLengthText("Cihaz Adı", 50));

            this.Validator.AddRequiredRule(() => this.SerialNumber, ValidationMessage.RequiredText("Cihaz Seri Numarası"));
            this.Validator.AddMaxLengthRule(() => this.SerialNumber, 50, ValidationMessage.MaxLengthText("Cihaz Seri Numarası", 50));

            //this.Validator.AddRequiredRule(() => this.IP, ValidationMessage.RequiredText("IP Adres"));
            //this.Validator.AddMaxLengthRule(() => this.IP, 15, ValidationMessage.MaxLengthText("IP Adres", 15));
            this.Validator.AddRule(() => this.IP, () =>
            {

                string IPADDRESS_PATTERN = @"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$";
                //"^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                //"([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                //"([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                //"([01]?\\d\\d?|2[0-4]\\d|25[0-5])$";

                if (string.IsNullOrEmpty(this.IP) || !Regex.IsMatch(this.IP, IPADDRESS_PATTERN))
                    return RuleResult.Invalid("Hatalı IP Adresi.");
                else
                    return RuleResult.Valid();
            });

            this.Validator.AddRule(() => this.Port, () =>
            {
                if (this.Port > 0 && this.Port < 10000)
                    return RuleResult.Valid();
                else
                    return RuleResult.Invalid("Port Numrası 0 dan büyük olmalıdır.");
            });




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

        #region Property IP

        private string _IP;

        public string IP
        {
            get { return this._IP; }
            set
            {
                if (!object.Equals(this._IP, value))
                {
                    this._IP = value;
                    this.OnPropertyChanged(() => this.IP);
                    this.Validator.Validate(() => this.IP);
                }
            }
        }

        #endregion

        #region Property Port

        private int? _Port;

        public int? Port
        {
            get { return this._Port; }
            set
            {
                if (!object.Equals(this._Port, value))
                {
                    this._Port = value;
                    this.OnPropertyChanged(() => this.Port);
                    this.Validator.Validate(() => this.Port);
                }
            }
        }

        #endregion

     

        #region Property ComKey

        private int? _ComKey;

        public int? ComKey
        {
            get { return this._ComKey; }
            set
            {
                if (!object.Equals(this._ComKey, value))
                {
                    this._ComKey = value;
                    this.OnPropertyChanged(() => this.ComKey);
                    this.Validator.Validate(() => this.ComKey);
                }
            }
        }

        #endregion

        #region Property SerialNumber

        private string _SerialNumber;

        public string SerialNumber
        {
            get { return this._SerialNumber; }
            set
            {
                if (!object.Equals(this._SerialNumber, value))
                {
                    this._SerialNumber = value;
                    this.OnPropertyChanged(() => this.SerialNumber);
                    this.Validator.Validate(() => this.SerialNumber);
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

        private void SetViewModel(ReaderDevice readerdevice)
        {
            this.Name = readerdevice.Name;
            this.SerialNumber = readerdevice.SerialNumber;
            this.Description = readerdevice.Description;
            this.IP = readerdevice.IP;
            
            
            if (readerdevice.Port.HasValue)
                this.Port = readerdevice.Port;

            if (readerdevice.ComKey.HasValue)
                this.ComKey = readerdevice.ComKey.Value;
            
            if (readerdevice.InputOutputType.HasValue)
                this.SelectedInputOutputType = this.ListInputOutputType.SingleOrDefault(io => readerdevice.InputOutputType == io.Value);

            this.SelectedState = this.ListState.SingleOrDefault(p => readerdevice.State == p.Value);
        }

        private void SetDataModel(ReaderDevice readerdevice)
        {
            readerdevice.Name = this.Name;
            readerdevice.SerialNumber = this.SerialNumber;
            readerdevice.Description = this.Description;
            readerdevice.IP = this.IP;
            readerdevice.Port = this.Port;
            readerdevice.ComKey = this.ComKey;

            if (this.SelectedInputOutputType != null)
                readerdevice.InputOutputType = this.SelectedInputOutputType.Value;
            else
                readerdevice.InputOutputType = null;

            readerdevice.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;

        }

        #endregion

        #region  Command

        #region Property ReadDeviceSerialCommand

        private RelayCommand _ReadDeviceSerialCommand;
        
        public RelayCommand ReadDeviceSerialCommand
        {
            get
            {
                if (this._ReadDeviceSerialCommand == null)
                    this._ReadDeviceSerialCommand = new RelayCommand(ExcuteReadDeviceSerialCommand);
                return this._ReadDeviceSerialCommand;
            }
        }

        void ExcuteReadDeviceSerialCommand()
        {
            ValidationResult ipval =  this.Validator.Validate(() => this.IP);
            ValidationResult portval = this.Validator.Validate(() => this.Port);

            if (!ipval.IsValid || !portval.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                ipval.ErrorList.Select(e => e.ErrorText).ToList().ForEach(e => sb.AppendLine(e));
                portval.ErrorList.Select(e => e.ErrorText).ToList().ForEach(e => sb.AppendLine(e));
                this.ServicePresenter.ShowErrorMessage(sb.ToString());
                return;
            }


            try
            {
                using (IDeviceManagement reader = new ZKDevice.ZKDeviceManagement())
                {
                    bool isConnect = reader.ConnectDevice(this.IP, this.Port.Value, this.ComKey.Value);

                    if (!isConnect)
                    {
                        this.ServicePresenter.ShowErrorMessage("Bağlantı Kurulamadı.");
                        return;
                    }

                    this.SerialNumber = reader.GetSerialNumber();
                }
            }
            catch (Exception e)
            {
                this.ServicePresenter.ShowErrorMessage(e.Message);
            }
        }

        #endregion


        #region Property SetDeviceTimeCommand

        private RelayCommand _SetDeviceTimeCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SetDeviceTimeCommand
        {
            get
            {
                if (this._SetDeviceTimeCommand == null)
                    this._SetDeviceTimeCommand = new RelayCommand(ExcuteSetDeviceTimeCommand);
                return this._SetDeviceTimeCommand;
            }
        }

        void ExcuteSetDeviceTimeCommand()
        {
            try
            {
                using (IDeviceManagement reader = new ZKDevice.ZKDeviceManagement())
                {
                    bool isConnect = reader.ConnectDevice(this.IP, this.Port.Value, this.ComKey.Value);

                    if (!isConnect)
                    {
                        this.ServicePresenter.ShowErrorMessage("Bağlantı Kurulamadı.");
                        return;
                    }

                    DateTime dt = reader.RefreshDeviceTime();
                    this.ServicePresenter.ShowInformationMessage(string.Format("\"{0}\" Olarak Güncellenmiştir.", dt.ToString(CultureInfo.CurrentUICulture.DateTimeFormat)));
                }
            }
            catch (Exception e)
            {
                this.ServicePresenter.ShowErrorMessage(e.Message);
            }
        }

        #endregion

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

                PDYSEntities.DataContext.ReaderDeviceSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);

            PDYSEntities.DataContext.SaveChanges();

            this.ServicePresenter.CloseWindow(true);
        }

        #endregion


    }
}
