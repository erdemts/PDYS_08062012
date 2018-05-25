using System.Linq;
using PDYS.InfraStructure;
using Mvvm;
using PDYS.Models;
using PDYS.Services;
using PDYS.Helper;
using System.Collections.Generic;
using PDYS.Interfaces;
using PDYS.Services.ServiceParam;
using DeviceManagement;
using System;
using System.Text;

namespace PDYS.ViewModels
{
    public class EmployeeListViewModel : ListViewModelBase<Employee, EmployeeViewModel>
    {

        public EmployeeListViewModel(bool autoload)
            : base(autoload)
        {
            this.Loaded += new System.Action(EmployeeListViewModel_Loaded);
        }

        void EmployeeListViewModel_Loaded()
        {
            if (IsAutoLoadData)
                this.SearchCommand.Execute();
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

            var query = PDYSEntities.DataContext.EmployeeSet.AsQueryable();

            if (!string.IsNullOrEmpty(this.SearchText))
                query = query.Where(item => (item.FirstName + " " + item.LastName).Contains(this.SearchText));

            if (this.Manager != null)
                query = query.Where(item => item.Manager.ID == this.Manager.ID);

            if (this.Department != null)
                query = query.Where(item => item.Department.ID == this.Department.ID);

            query = query.OrderBy(item => item.FirstName);
            query = query.OrderBy(item => item.LastName);

            this.QueryExpression = query;

            #endregion

            LoadData();
        }

        #endregion

        #region Property SyncDeviceCommand

        private RelayCommand _SyncDeviceCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SyncDeviceCommand
        {
            get
            {
                if (this._SyncDeviceCommand == null)
                    this._SyncDeviceCommand = new RelayCommand(ExcuteSyncDeviceCommand);
                return this._SyncDeviceCommand;
            }
        }

        void ExcuteSyncDeviceCommand()
        {

            if (!this.SelectedItems.Any())
            {
                this.ServicePresenter.ShowAlertMessage("Lütfen Kayıt Seçiniz.");
                return;
            }


            Action<IEnumerable<ReaderDevice>> AsyncWriteDevice = (selectedDevices) =>
                {
                    if (!selectedDevices.Any())
                    {
                        this.ServicePresenter.ShowAlertMessage("Cihaz Seçimi Yapılmadı.");
                        return;
                    }

                    ProcessManager.Execute("Kullanıcı Bilgileri Okuyucu Cihaza Yazılıyor...", new Action<IEnumerable<ReaderDevice>>(WriteDevice), new object[] { selectedDevices });
                };

            ConfirmParam param = new ConfirmParam();
            param.Message = "Seçili Kullanıcı Bilgileri Okuyucu Cihaza Yazilsinmi ?";
            param.OnConfirmResult = (confirm_result) =>
                {
                    if (confirm_result == ConfirmParam.ConfirmResult.Yes)
                    {
                        ReaderDeviceSelectionViewModel.OpenSelectionWindow(AsyncWriteDevice, ReaderDeviceSelectionMode.MultiSelect);
                    }
                };


            this.ServicePresenter.ConfirmMessage(param);


        }

        void WriteDevice(IEnumerable<ReaderDevice> selectedDevices)
        {

            StringBuilder strLog = new StringBuilder();

            foreach (var personal in this.SelectedItems)
            {
                strLog.AppendLine(string.Format(">>> Personel Bilgisi \"{0}\" Cihaza Yazılıyor...",personal.DisplayName));
                strLog.AppendLine(string.Empty);

                IDeviceManagement reader = new ZKDevice.ZKDeviceManagement();

                bool isSyncDevice = false;

                foreach (ReaderDevice device in selectedDevices)
                {
                    #region Write Device
                    try
                    {
                        bool isConnect = reader.ConnectDevice(device.IP, device.Port.Value, device.ComKey.Value);

                        if (!isConnect)
                        {
                            strLog.AppendLine(string.Format("\"{0}\" isimli cihaza  baglanti kurulamadi.", device.DisplayName));
                            continue;
                        }

                        DeviceUser user = new DeviceUser();
                        user.UserID = personal.ID;
                        user.UserName = string.Format("{0} {1}", personal.FirstName, personal.LastName);
                        user.CardNumber = personal.AccessCardNo;
                        user.Password = personal.AccessPassword;
                        user.Privilege = UserPrivilege.commonuser;
                        user.Enabled = (personal.State == 0);

                        reader.CreateUser(user);

                        strLog.AppendLine(string.Format("\"{0}\" isimli cihaza  bilgiler yazılmıştır.", device.DisplayName));

                        isSyncDevice = true;

                    }
                    catch (Exception e)
                    {
                        isSyncDevice = false;

                        strLog.AppendLine(string.Format("\"{0}\" isimli cihaza  bilgi yazilamadi.\n Hata Mesaji : {1}", device.DisplayName, e.Message));
                    }
                    finally
                    {
                        reader.DisconnectDevice();
                    }
                    #endregion
                }

                strLog.AppendLine(string.Empty);


                if (!isSyncDevice)
                    strLog.AppendLine("Bilgiler tüm cihazlara yazılamadığından senkronizasyon tamamlanamadı !...");
                else
                    strLog.AppendLine("Senkronizasyon Tamamlandı !...");

                strLog.AppendLine(string.Empty);

                personal.IsSyncDevice = isSyncDevice;

                PDYSEntities.DataContext.SaveChanges();

            }

            LogViewModel.OpenWindow("Personel Bilgileri Cihaz Senkronizasyonu...", strLog.ToString());

            this.LoadData();
        }

        #endregion

    }
}
