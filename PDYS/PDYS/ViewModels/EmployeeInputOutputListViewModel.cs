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
using DeviceManagement;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace PDYS.ViewModels
{
    public class EmployeeInputOutputListViewModel : ListViewModelBase<EmployeeInputOutput, EmployeeInputOutputViewModel>
    {

        public EmployeeInputOutputListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.IsMultiSelect = true;
            this.IsDeleteCommand = true;
            this.OnDeleted += new Action<IEnumerable<EmployeeInputOutput>>(UIDeleteInOut);
            this.Loaded += new Action(PersonalInputOutputListViewModel_Loaded);
        }



        void PersonalInputOutputListViewModel_Loaded()
        {
            #region InputOutput Type List

            var queryState = from item in PDYSEntities.DataContext.ParameterSet
                             where item.Name == "InputOutputType"
                             orderby item.Text
                             select item;

            queryState.ToList().ForEach(item => this.ListInputOutputType.Add(item));

            Parameter allParam = new Parameter() { Name = "InputOutputType", Text = "Tümü", Value = -1 };
            this.ListInputOutputType.Insert(0, allParam);
            this.SelectedInputOutput = allParam;

            #endregion

            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

        void UIDeleteInOut(IEnumerable<EmployeeInputOutput> deleteditems)
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

            ProcessManager.Execute("Giriş & Çıkış Siliniyor", new Action<IEnumerable<EmployeeInputOutput>>(DeleteInOut), deleteditems);

        }

        void DeleteInOut(IEnumerable<EmployeeInputOutput> deleteditems)
        {
            foreach (var item in deleteditems)
            {
                if (!item.IsProcessed)
                    PDYSEntities.DataContext.EmployeeInputOutputSet.Remove(item);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.LoadData();
        }

        #region Parameter

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

        #region Property SelectedInputOutput

        private Parameter _SelectedInputOutput;

        public Parameter SelectedInputOutput
        {
            get { return this._SelectedInputOutput; }
            set
            {
                if (!object.Equals(this._SelectedInputOutput, value))
                {
                    this._SelectedInputOutput = value;
                    this.OnPropertyChanged(() => this.SelectedInputOutput);
                    this.Validator.Validate(() => this.SelectedInputOutput);
                }
            }
        }

        #endregion

        #region Property ListInputOutputType Collection

        private ObservableCollection<Parameter> _ListInputOutputType;

        public ObservableCollection<Parameter> ListInputOutputType
        {
            get { return this._ListInputOutputType = this._ListInputOutputType ?? new ObservableCollection<Parameter>(); }
            private set { this._ListInputOutputType = value; }
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
            var query = PDYSEntities.DataContext.EmployeeInputOutputSet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.StartDate.HasValue && !this.EndDate.HasValue)
                query = query.Where(item => this.StartDate <= item.InOutDate);

            if (this.EndDate.HasValue && !this.StartDate.HasValue)
                query = query.Where(item => item.InOutDate <= this.EndDate);

            if (this.SelectedInputOutput != null && this.SelectedInputOutput.Value != -1)
                query = query.Where(item => item.InOutType == this.SelectedInputOutput.Value);


            if (this.EndDate.HasValue && this.StartDate.HasValue)
                query = query.Where(item => this.StartDate <= item.InOutDate && item.InOutDate <= this.EndDate);

            query = query.OrderByDescending(item => item.InOutDate);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion

        

        #region Property ReadDeviceCommand

        private RelayCommand _ReadDeviceCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ReadDeviceCommand
        {
            get
            {
                if (this._ReadDeviceCommand == null)
                    this._ReadDeviceCommand = new RelayCommand(ExcuteReadDeviceCommand);
                return this._ReadDeviceCommand;
            }
        }

        void ExcuteReadDeviceCommand()
        {
            ReaderDeviceSelectionViewModel.OpenSelectionWindow(ReadDeviceInputOput, ReaderDeviceSelectionMode.MultiSelect);
        }

        void ReadDeviceInputOput(IEnumerable<ReaderDevice> selectedDevice)
        {
            ProcessManager.Execute("Chiazdan Okunuyor...", new Action<IEnumerable<ReaderDevice>>(internalReadDeviceInputOput), new object[] { selectedDevice });
        }

        void internalReadDeviceInputOput(IEnumerable<ReaderDevice> selectedDevice)
        {
            IDeviceManagement reader = new ZKDevice.ZKDeviceManagement();

            foreach (ReaderDevice device in selectedDevice)
            {
                try
                {
                    bool isConnect = reader.ConnectDevice(device.IP, device.Port.Value, device.ComKey.Value);

                    if (!isConnect)
                    {
                        this.ServicePresenter.ShowErrorMessage(string.Format("\"{0}\" isimli cihaza  bağlantı kurulamadı.", device.DisplayName));
                        continue;
                    }


                    // Read Log
                    IEnumerable<AttendanceLog> logs = reader.GetAttendanceLogs();

                    var groups = logs.GroupBy(item => item.UserID);
                    foreach (var groupitem in groups)
                    {
                        Employee employee = PDYSEntities.DataContext.EmployeeSet.Find(groupitem.Key);
                        if (employee == null)
                            continue;

                        foreach (AttendanceLog attendancelog in groupitem)
                        {
                            EmployeeInputOutput InOutDBItem = new EmployeeInputOutput();
                            InOutDBItem.Employee = employee;
                            InOutDBItem.ReaderDevice = device;
                            InOutDBItem.InOutDate = attendancelog.Date;
                            InOutDBItem.InOutType = device.InputOutputType;

                            #region Auto Set Input Output Type

                            //if (device.InputOutputType.HasValue && device.InputOutputType.Value == 0) // Bilinmiyor
                            //{
                            //    InOutDBItem.InOutType = (++counter % 2 == 0) ? 2 : 1; // 1:Giriş,  2:Çıkış
                            //}
                            //else
                            //{
                            //    switch (attendancelog.InOutMode)
                            //    {
                            //        case AttendanceInOutMode.CheckIn:
                            //        case AttendanceInOutMode.BreakIn:
                            //        case AttendanceInOutMode.OTIn:
                            //            InOutDBItem.InOutType = 1; //Giriş
                            //            break;
                            //        case AttendanceInOutMode.CheckOut:
                            //        case AttendanceInOutMode.BreakOut:
                            //        case AttendanceInOutMode.OTOut:
                            //            InOutDBItem.InOutType = 2; //Çıkış
                            //            break;
                            //        default:
                            //            InOutDBItem.InOutType = 0; //Bilinmiyor
                            //            break;
                            //    }
                            //}

                            #endregion

                            PDYSEntities.DataContext.EmployeeInputOutputSet.Add(InOutDBItem);

                        }

                        PDYSEntities.DataContext.SaveChanges();
                    }


                    // Clear Log
                    reader.ClearAttendanceLogs();


                }
                catch (Exception e)
                {
                    this.ServicePresenter.ShowErrorMessage(string.Format("\"{0}\" isimli cihaza  bilgi yazılamadı.\n Hata Mesajı : {1}", device.DisplayName, e.Message));
                }
                finally
                {
                    reader.DisconnectDevice();
                }
            }


            //this.ServicePresenter.ShowInformationMessage("Giriş/Çıkış bilgileri cihazdan okunmuştur.");
            this.LoadData();
        }

        #endregion


        #region Property ReadFileCommand

        private RelayCommand _ReadFileCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ReadFileCommand
        {
            get
            {
                if (this._ReadFileCommand == null)
                    this._ReadFileCommand = new RelayCommand(ExcuteReadFileCommand);
                return this._ReadFileCommand;
            }
        }

        void ExcuteReadFileCommand()
        {
            DialogFileParam fileparam = new DialogFileParam();
            fileparam.FileFilter = DialogFileParam.TextFileFilter;
            fileparam.FileLoadedAction = (buffer) =>
                {
                    ProcessManager.Execute("Dosyadan Okunuyor...",new Action<byte[]>(ReadFile), new object[] { buffer });
                };

            this.ServicePresenter.OpenFileDialog(fileparam);
        }

        private void ReadFile(byte[] buffer)
        {
            try
            {
                internalReadFileData(buffer);
            }
            catch (Exception e)
            {
                this.ServicePresenter.ShowErrorMessage("Dosya okuma işlemi sırasında hata oluştu.");
            }
        }

        private void internalReadFileData(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                this.ServicePresenter.ShowAlertMessage("Dosya Boş.");
                return;
            }

            int counter = 0;
            List<string> headers = null;
            StringBuilder inputlog = new StringBuilder();

            using (MemoryStream membuffer = new MemoryStream(buffer))
            {
                StreamReader reader = new StreamReader(membuffer);

                while (true)
                {
                    counter++;

                    string linedata = reader.ReadLine();

                    if (string.IsNullOrEmpty(linedata))
                        break;

                    string[] seperatedata = linedata.Split(';');

                    if (headers == null)
                    {
                        headers = seperatedata.ToList();
                        continue;
                    }

                    inputlog.AppendFormat("{0:000}. Satır : ", counter);

                    int employeeid = -1;
                    Employee employee = null;
                    int readerdeviceid = -1;
                    ReaderDevice readerdevice = null;
                    int inouttype = -1;
                    DateTime inoutdate = DateTime.MinValue;

                    #region Validate & Init Line Data

                    #region Employee
                    int index = headers.IndexOf("EmployeeID");

                    if (index == -1)
                    {
                        inputlog.AppendLine("EmployeeID kolonu bulunamadı.");
                        continue;
                    }
                    else if (seperatedata.Length <= index || !int.TryParse(seperatedata[index], out employeeid))
                    {
                        inputlog.AppendLine("EmployeeID değeri hatalı.");
                        continue;
                    }
                    employee =   PDYSEntities.DataContext.EmployeeSet.Find(employeeid);
                    if (employee == null)
                    {
                        inputlog.AppendLine("EmployeeID için tanımlı personel kaydı bulunamadı.");
                        continue;
                    }
                    #endregion

                    #region Reader Device
                    index = headers.IndexOf("ReaderDeviceID");
                    
                    if (index == -1)
                    {
                        inputlog.AppendLine("ReaderDeviceID kolonu bulunamadı.");
                        continue;
                    }
                    else if (seperatedata.Length <= index || !int.TryParse(seperatedata[index], out readerdeviceid))
                    {
                        inputlog.AppendLine("ReaderDeviceID değeri hatalı.");
                        continue;
                    }
                    readerdevice = PDYSEntities.DataContext.ReaderDeviceSet.Find(readerdeviceid);
                    if (readerdevice == null)
                    {
                        inputlog.AppendLine("ReaderDeviceID için tanımlı Cihaz kaydı bulunamadı.");
                        continue;
                    }
                    #endregion

                    #region Input Type
                    index = headers.IndexOf("InOutType");

                    if (index == -1)
                    {
                        inputlog.AppendLine("InOutType kolonu bulunamadı.");
                        continue;
                    }
                    else if (seperatedata.Length <= index || !int.TryParse(seperatedata[index], out inouttype))
                    {
                        inputlog.AppendLine("InOutType değeri hatalı.");
                        continue;
                    }
                    // 0:Giriş & Çıkış, 1:Giriş, 2:Çıkış
                    else if (!(inouttype == 0 || inouttype == 1 || inouttype == 2))
                    {
                        inputlog.AppendLine("InOutType değeri \"0,1,2\" olabilir. (0:Giriş & Çıkış, 1:Giriş, 2:Çıkış) .");
                        continue;
                    }
                    #endregion

                    #region Input Date
                    index = headers.IndexOf("InOutDate");

                    if (index == -1)
                    {
                        inputlog.AppendLine("InOutDate kolonu bulunamadı.");
                        continue;
                    }
                    else if (seperatedata.Length <= index || !DateTime.TryParse(seperatedata[index], out inoutdate))
                    {
                        inputlog.AppendLine("InOutDate değeri hatalı.");
                        continue;
                    }
                    #endregion

                    #endregion

                    EmployeeInputOutput InOutDBItem = new EmployeeInputOutput();

                    InOutDBItem.Employee = employee;
                    InOutDBItem.ReaderDevice = readerdevice;
                    InOutDBItem.InOutDate = inoutdate;
                    InOutDBItem.InOutType = inouttype;

                    PDYSEntities.DataContext.EmployeeInputOutputSet.Add(InOutDBItem);
                    PDYSEntities.DataContext.SaveChanges();

                    inputlog.AppendLine(string.Format("{0} için işlem başarılı.",employee.DisplayName));
                }


                reader.Close();
                membuffer.Close();

                inputlog.Insert(0,"İşlem Tamamlandı.\n");

                LogViewModel.OpenWindow("Giriş & Çıkış Bilgisi Dosyadan Okundu...", inputlog.ToString());
                 // this.ServicePresenter.ShowInformationMessage(inputlog.ToString());

                this.SearchCommand.Execute();

            }
        }

        #endregion
    }
}


