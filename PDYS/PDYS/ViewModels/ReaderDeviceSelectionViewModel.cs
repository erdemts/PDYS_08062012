using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Models;
using System.Collections.ObjectModel;
using PDYS.Interfaces;
using PDYS.Services.ServiceParam;
using DeviceManagement;
using System.Windows.Threading;
using System.Threading;

namespace PDYS.ViewModels
{
    public enum ReaderDeviceSelectionMode { SingleSelect, MultiSelect }

    class ReaderDeviceSelectionViewModel : EditViewModelBase<ReaderDeviceSelectionModel>
    {
        public ReaderDeviceSelectionViewModel()
            : base(null)
        {
            this.Title = "Okuyu Cihaz Seçimi";
            this.AcceptButtonTitle = "Tamam";


            //this.DataModel = null;
            this.Loaded += new Action(ReaderDeviceSelectionViewModel_Loaded);
        }

        private ReaderDeviceSelectionMode selectionmode { get; set; }

        void ReaderDeviceSelectionViewModel_Loaded()
        {
            List<ReaderDevice> list = PDYSEntities.DataContext.ReaderDeviceSet.ToList();
            list.ForEach(item => this.Items.Add(new ReaderDeviceSelectionModel() {  Device = item}));


               
                foreach (var deviceinfo in this.Items)
                {
                    deviceinfo.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                }

                App.Current.Dispatcher.BeginInvoke(new ParameterizedThreadStart(TestDevice), DispatcherPriority.Background,  new object[] { this.Items });
            
        }

        void TestDevice(object list)
        {
            ObservableCollection<ReaderDeviceSelectionModel> _items = (ObservableCollection<ReaderDeviceSelectionModel>)list;

            using (IDeviceManagement reader = new ZKDevice.ZKDeviceManagement())
            {
                foreach (var device in _items)
                {
                    try
                    {
                        bool isConnect = reader.ConnectDevice(device.Device.IP, device.Device.Port.Value, device.Device.ComKey.Value);

                        if (isConnect)
                        {
                            device.IsAway = false;
                            device.IsValid = true;
                            device.IsInValid = false;

                            reader.DisconnectDevice();
                        }
                        else
                        {
                            device.IsAway = false;
                            device.IsValid = false;
                            device.IsInValid = true;
                        }
                    }
                    catch (Exception e)
                    {
                        string xx = e.Message;
                    }
                }
            }
        }


        bool inprogress = false;  
        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (inprogress)
                return;

            if (this.selectionmode == ReaderDeviceSelectionMode.SingleSelect)
            {
                inprogress = true;

                var list = this.Items.Where(item => item.ID != ((ReaderDeviceSelectionModel)sender).ID);

                foreach (var unselectitem in list)
                {
                    unselectitem.Selected = false;
                }

                inprogress = false;
            }

            
        }

        public static void OpenSelectionWindow(Action<IEnumerable<ReaderDevice>> selectinprocess, ReaderDeviceSelectionMode selectionmode)
        {
            ReaderDeviceSelectionViewModel model = new ReaderDeviceSelectionViewModel();
            model.selectionmode = selectionmode;

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = model;
            windowParam.OnClose = (result) =>
            {
                if (result && selectinprocess != null)
                {
                    var selectionitems = model.Items.Where(item => item.Selected).Select(item => item.Device);
                    selectinprocess.Invoke(selectionitems);
                }
            };

            model.ServicePresenter.OpenWindow(windowParam);

        }


        #region Property SelectedItem

        private ReaderDeviceSelectionModel _SelectedItem;

        public ReaderDeviceSelectionModel SelectedItem
        {
            get { return this._SelectedItem; }
            set
            {
                if (!object.Equals(this._SelectedItem, value))
                {
                    this._SelectedItem = value;
                    this.OnPropertyChanged(() => this.SelectedItem);
                }
            }
        }

        #endregion

        #region Items

        ObservableCollection<ReaderDeviceSelectionModel> _items;
        public ObservableCollection<ReaderDeviceSelectionModel> Items
        {
            get { return this._items = this._items ?? new ObservableCollection<ReaderDeviceSelectionModel>(); }
            private set { this._items = value; }
        }

        #endregion


    }

    public class ReaderDeviceSelectionModel : ViewModelBase, IDataItem
    {
        public ReaderDeviceSelectionModel()
        {
            this.IsAway = true;
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ReaderDeviceSelectionModel_PropertyChanged);
        }

        void ReaderDeviceSelectionModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetTitle();
        }

        #region Property Selected

        private bool _Selected;

        public bool Selected
        {
            get { return this._Selected; }
            set
            {
                if (!object.Equals(this._Selected, value))
                {
                    this._Selected = value;
                    this.OnPropertyChanged(() => this.Selected);
                    this.Validator.Validate(() => this.Selected);
                }
            }
        }

        #endregion

        #region Property Device

        private ReaderDevice _Device;

        public ReaderDevice Device
        {
            get { return this._Device; }
            set
            {
                if (!object.Equals(this._Device, value))
                {
                    this._Device = value;
                    this.OnPropertyChanged(() => this.Device);
                    this.Validator.Validate(() => this.Device);
                }
            }
        }

        #endregion

        #region Property IsValid

        private bool _IsValid;

        public bool IsValid
        {
            get { return this._IsValid; }
            set
            {
                if (!object.Equals(this._IsValid, value))
                {
                    this._IsValid = value;
                    this.OnPropertyChanged(() => this.IsValid);
                    this.Validator.Validate(() => this.IsValid);
                }
            }
        }

        #endregion

        #region Property IsInValid

        private bool _IsInValid;

        public bool IsInValid
        {
            get { return this._IsInValid; }
            set
            {
                if (!object.Equals(this._IsInValid, value))
                {
                    this._IsInValid = value;
                    this.OnPropertyChanged(() => this.IsInValid);
                    this.Validator.Validate(() => this.IsInValid);
                }
            }
        }

        #endregion

        #region Property IsAway

        private bool _IsAway;

        public bool IsAway
        {
            get { return this._IsAway; }
            set
            {
                if (!object.Equals(this._IsAway, value))
                {
                    this._IsAway = value;
                    this.OnPropertyChanged(() => this.IsAway);
                    this.Validator.Validate(() => this.IsAway);
                }
            }
        }

        #endregion

        #region Property Title

        private string _title;

        public string Title
        {
            get { return this._title; }
            set
            {
                if (!object.Equals(this._title, value))
                {
                    this._title = value;
                    this.OnPropertyChanged(() => this.Title);
                    this.Validator.Validate(() => this.Title);
                }
            }
        }

        #endregion


        void SetTitle()
        {
            if (this.IsAway)
                this.Title = string.Format("{0}  [ Bağlantı Kuruluyor... ]", this.Device.DisplayName);
            else if (this.IsInValid)
                this.Title = string.Format("{0}  [ Bağlantı Hatası. ]", this.Device.DisplayName);
            else if (this.IsValid)
                this.Title = this.Device.DisplayName;
        }


        public int ID
        {
            get
            {
                return this.Device.ID;
            }
            set
            {
                this.Device.ID = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.Device.DisplayName;
            }
        }
    }
}
