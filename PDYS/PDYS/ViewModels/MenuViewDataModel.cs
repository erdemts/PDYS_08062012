using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mvvm;
using PDYS.Report.ViewModels;



namespace PDYS.ViewModels
{
    public class MenuViewDataModel : ObservableObject
    {

        public MenuViewDataModel()
        {
            
        }

        

        public void ExecuteMenuGroupSelection(MenuGroupItem menugroup)
        {
            if (this.SubMenuItems!=null)
                this.SubMenuItems.Clear();

            if (menugroup!=null && menugroup.SubMenuItems != null)
            {
                foreach (var submenu in menugroup.SubMenuItems)
	            {
                    this.SubMenuItems.Add(submenu);
	            }

                this.SelectedMenu = this.SubMenuItems.FirstOrDefault();
            }

        }

        #region Submenu

        #region Property SelectedMenu

        private MenuItem _selectedMenu;
        /// <summary>
        /// 
        /// </summary>
        public MenuItem SelectedMenu
        {
            get { return this._selectedMenu; }
            set
            {
                if (!object.Equals(this._selectedMenu, value))
                {
                    this._selectedMenu = value;
                    this.OnPropertyChanged(() => this.SelectedMenu);
                }
            }
        }

        #endregion

        #region Property SubMenuItems Collection

        private ObservableCollection<MenuItem> _subMenuItem;

        public ObservableCollection<MenuItem> SubMenuItems
        {
            get { return this._subMenuItem = this._subMenuItem ?? new ObservableCollection<MenuItem>(); }
        }

        #endregion

        #endregion

        private IEnumerable<MenuGroupItem> InitMenuItems()
        {
            #region Personel

            MenuGroupItem personel = new MenuGroupItem() { DisplayName = "Personel Bilgileri" };
            personel.SubMenuItems = new ObservableCollection<MenuItem>()
                    {
                        new MenuItem(){DisplayName = "Personel Bilgileri", DataModelType = typeof(EmployeeListViewModel) },
                        new MenuItem(){DisplayName = "Personel İzinleri", DataModelType = typeof(EmployeeHolidayListViewModel) },
                        new MenuItem(){DisplayName = "Departman Bilgileri", DataModelType = typeof(DepartmentListViewModel)},
                        new MenuItem(){DisplayName = "Servis Bilgileri", DataModelType = typeof(TransportListViewModel)},
                                                                                       
                    };

            personel.Command = new RelayCommand(() =>
                    {
                        ExecuteMenuGroupSelection(personel);
                    });

            #endregion

            #region Personel İşlemleri

            MenuGroupItem accounting = new MenuGroupItem() { DisplayName = "Personel İşlemleri" };
            accounting.SubMenuItems = new ObservableCollection<MenuItem>()
                    {
                        new MenuItem(){DisplayName = "Personel Giris/Çikis", DataModelType = typeof(EmployeeInputOutputListViewModel)},
                        new MenuItem(){DisplayName = "Puantaj İşlemleri", DataModelType = typeof(EmployeeInOutScoringListViewModel)},
                        new MenuItem(){DisplayName = "Hakediş İşlemleri", DataModelType = typeof(EmployeeSalaryScoringListViewModel)},
                        new MenuItem(){DisplayName = "Hesap Hareketleri", DataModelType=typeof(EmployeeAccountingListViewModel)},
                    };
            
            accounting.Command = new RelayCommand(() =>
            {
                ExecuteMenuGroupSelection(accounting);
            });

            #endregion

            #region Vardiya & Mesai

            MenuGroupItem overtime = new MenuGroupItem() { DisplayName = "Vardiya & Mesai" };
            overtime.SubMenuItems = new ObservableCollection<MenuItem>()
                    {
                        new MenuItem(){DisplayName = "Personel Mesai Atama", DataModelType = typeof(OvertimeAssignmentListViewModel)},
                        new MenuItem(){DisplayName = "Kümulatif Mesai Tanımı", DataModelType = typeof(OutSourceOvertimeListViewModel)},
                        new MenuItem(){DisplayName = "Haftalık Mesai Tanımı", DataModelType= typeof(WeeklyOvertimeListViewModel)},
                    };

            overtime.Command = new RelayCommand(() =>
            {
                ExecuteMenuGroupSelection(overtime);
            });

            #endregion

            #region Raporlar

            MenuGroupItem reports = new MenuGroupItem() { DisplayName = "Raporlar" };
            reports.SubMenuItems = new ObservableCollection<MenuItem>()
                    {
                        new ReportMenuItem(){DisplayName = "Personel Listesi Raporu", DataModelType=typeof(RptEmployeeListViewModel)},
                        new ReportMenuItem(){DisplayName = "Personel Hakediş Raporu", DataModelType=typeof(RptEmployeeSalaryScoringViewModel)},
                        new ReportMenuItem(){DisplayName = "Giriş & Çıkış Puantaj Raporu", DataModelType=typeof(RptEmployeeInOutScoringViewModel)},
                        new ReportMenuItem(){DisplayName = "Giriş & Çıkış Raporu", DataModelType=typeof(RptInputOutputViewModel)},
                        
                    };

            reports.Command = new RelayCommand(() =>
            {
                ExecuteMenuGroupSelection(reports);
            });

            #endregion

            #region Konfigürasyon

            MenuGroupItem config = new MenuGroupItem() { DisplayName = "Tanımlamalar" };
            config.SubMenuItems = new ObservableCollection<MenuItem>()
                    {
                        
                        //new MenuItem(){DisplayName = "Il & Ilçe Bilgileri"},
                        new MenuItem(){DisplayName = "Kart Okuyucu Cihazlar", DataModelType= typeof(ReaderDeviceListViewModel)},
                        new MenuItem(){DisplayName = "Resmi Tatiller", DataModelType = typeof(PublicHolidayListViewModel) },
                        new MenuItem(){DisplayName = "Hesap İşlem Tipleri", DataModelType=typeof(AccountingProcessTypeListViewModel)},
                        new MenuItem(){DisplayName = "Kullanici Bilgileri", DataModelType= typeof(UserListViewModel)},
                    };

            config.Command = new RelayCommand(() =>
            {
                ExecuteMenuGroupSelection(config);
            });

            #endregion


            return new[] { personel, accounting, overtime, reports, config };
        }


        #region Property MenuGroupList Collection

        private ObservableCollection<MenuGroupItem> _menugrouplist;

        public ObservableCollection<MenuGroupItem> MenuGroupList
        {
            get { return this._menugrouplist = this._menugrouplist ?? new ObservableCollection<MenuGroupItem>(InitMenuItems()); }
            //private set { _menugrouplist = value; }
        }

        #endregion
        
    }

    public class BaseMenu : ObservableObject
    {

        #region Property DisplayName

        private string _displayName;
        /// <summary>
        /// Menu Ekran Üzerinde Görünen İsim
        /// </summary>
        public string DisplayName
        {
            get { return this._displayName; }
            set
            {
                if (!object.Equals(this._displayName, value))
                {
                    this._displayName = value;
                    this.OnPropertyChanged(() => this.DisplayName);
                }
            }
        }

        #endregion

        #region Property Descriptiton

        private string description;
        /// <summary>
        /// Menu Tooltip üzerinde Görünen Açıklama
        /// </summary>
        public string Descriptiton
        {
            get { return this.description; }
            set
            {
                if (!object.Equals(this.description, value))
                {
                    this.description = value;
                    this.OnPropertyChanged(() => this.Descriptiton);
                }
            }
        }

        #endregion
    }

    public class ReportMenuItem : MenuItem
    {
 
    }


    public class MenuItem : BaseMenu
    {
        #region Property TargetDataModel

        private Type _targetDataModel;
       
        public Type DataModelType
        {
            get { return this._targetDataModel; }
            set
            {
                if (!object.Equals(this._targetDataModel, value))
                {
                    this._targetDataModel = value;
                    this.OnPropertyChanged(() => this.DataModelType);
                }
            }
        }

        #endregion
    }

    public class MenuGroupItem : BaseMenu
    {
        #region Property Command

        private RelayCommand _command;
        /// <summary>
        /// Menu seçiminde İşlenecek Komut
        /// </summary>
        public RelayCommand Command
        {
            get { return this._command; }
            set
            {
                if (!object.Equals(this._command, value))
                {
                    this._command = value;
                    this.OnPropertyChanged(() => this.Command);
                }
            }
        }

        #endregion

        #region Property SubMenuItems Collection

        private ObservableCollection<MenuItem> _submenuitem;

        public ObservableCollection<MenuItem> SubMenuItems
        {
            get { return this._submenuitem = this._submenuitem ?? new ObservableCollection<MenuItem>(); }
            set { this._submenuitem = value; }
        }

        #endregion
    }


   
}
