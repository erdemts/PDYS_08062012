using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using Mvvm;
using PDYS.Models;
using PDYS.Services.ServiceParam;

namespace PDYS.ViewModels
{
    public class ApplicationDataModel : ViewModelBase
    {
        PropertyObserver<MenuViewDataModel> listenerMenuProp = null;

        public ApplicationDataModel()
        {
            this.Title = "Personel Yönetim Sistemi";
            this.Menu = new MenuViewDataModel();

            listenerMenuProp = new PropertyObserver<MenuViewDataModel>(this.Menu)
            .RegisterHandler(m => m.SelectedMenu, (m) => OnSelectedMenuChange(m.SelectedMenu));
            this.Loaded += new Action(ApplicationDataModel_Loaded);
        }

        void ApplicationDataModel_Loaded()
        {
            SetDefaultMenu();
        }

        private void SetDefaultMenu()
        {
            if (this.Menu.SelectedMenu == null)
                this.Menu.ExecuteMenuGroupSelection(this.Menu.MenuGroupList.FirstOrDefault());
        }


        void OnSelectedMenuChange(MenuItem menuItem)
        {
            this.SelectedMenuTitle = (menuItem != null) ? menuItem.DisplayName : string.Empty;
            //AsyncSetMenu(menuItem);
            if (menuItem != null)
            {
                App.Current.Dispatcher.BeginInvoke(new Action<MenuItem>(AsyncSetMenu), System.Windows.Threading.DispatcherPriority.Background, menuItem);
            }
            else
                this.MenuContent = null;

        }

        private void AsyncSetMenu(MenuItem menuItem)
        {
            if (menuItem.DataModelType != null)
            {
                if (!IsValidLicence)
                {
                    CheckLicence();
                }

                if (CurrentUser == null)
                {
                    Login();
                }


                if (menuItem is ReportMenuItem)
                    this.MenuContent = Activator.CreateInstance(menuItem.DataModelType) as ViewModelBase;
                else
                    this.MenuContent = Activator.CreateInstance(menuItem.DataModelType, new object[] { true }) as ViewModelBase;
            }
            else
                this.MenuContent = null;
        }

        private void CheckLicence()
        {
            LicenceViewModel licenceVM = new LicenceViewModel();

            IsValidLicence = licenceVM.IsValidLicence();

            if (!IsValidLicence)
            {
                DialogWindowParam windowParam = new DialogWindowParam();
                windowParam.ModelView = licenceVM;
                windowParam.OnClose = (result) =>
                {
                    IsValidLicence = result;
                };

                this.ServicePresenter.OpenBaseWindow(windowParam);


                if (!IsValidLicence)
                    this.ServicePresenter.CloseApplication(0);
            }
        }

        private void Login()
        {
            UserLoginViewModel userviewmodel = new UserLoginViewModel(null);

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = userviewmodel;
            windowParam.OnClose = (result) =>
            {
                if (result)
                {
                    CurrentUser = userviewmodel.DataModel;
                    this.UserName = userviewmodel.DataModel.FullName;
                }
                else
                    this.ServicePresenter.CloseApplication(0);
            };

            this.ServicePresenter.OpenWindow(windowParam);
        }

        #region Property Title

        private string _title;
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set
            {
                if (!object.Equals(this._title, value))
                {
                    this._title = value;
                    this.OnPropertyChanged(() => this.Title);
                }
            }
        }

        #endregion

        #region Property SelectedMenuTitle

        private string _selectedMenuTitle;

        public string SelectedMenuTitle
        {
            get { return this._selectedMenuTitle; }
            set
            {
                if (!object.Equals(this._selectedMenuTitle, value))
                {
                    this._selectedMenuTitle = value;
                    this.OnPropertyChanged(() => this.SelectedMenuTitle);
                    this.Validator.Validate(() => this.SelectedMenuTitle);
                }
            }
        }

        #endregion

        #region Property Menu

        private MenuViewDataModel _menu;
        /// <summary>
        /// 
        /// </summary>
        public MenuViewDataModel Menu
        {
            get { return this._menu; }
            set
            {
                if (!object.Equals(this._menu, value))
                {
                    this._menu = value;
                    this.OnPropertyChanged(() => this.Menu);
                }
            }
        }

        #endregion
        
        #region Property MenuContent

        private ViewModelBase _menucontent;
        /// <summary>
        /// Seçili Kayıt Tipi Ekranı
        /// </summary>
        public ViewModelBase MenuContent
        {
            get { return this._menucontent; }
            set
            {
                if (!object.Equals(this._menucontent, value))
                {
                    this._menucontent = value;
                    this.OnPropertyChanged(() => this.MenuContent);
                }
            }
        }

        #endregion

        #region Property CurrentUser

        private static User _CurrentUser;

        public static User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                if (!object.Equals(_CurrentUser, value))
                {
                    _CurrentUser = value;
                }
            }
        }

        #endregion

        #region Property IsValidLicence

        private static bool _IsValidLicence;

        public static bool IsValidLicence
        {
            get { return _IsValidLicence; }
            set
            {
                if (!object.Equals(_IsValidLicence, value))
                {
                    _IsValidLicence = value;
                }
            }
        }

        #endregion

        

        #region Property UserName

        private string _UserName;

        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (!object.Equals(this._UserName, value))
                {
                    this._UserName = value;
                    this.OnPropertyChanged(() => this.UserName);
                    this.Validator.Validate(() => this.UserName);
                }
            }
        }

        #endregion

        protected override void InitValidation()
        {

        }

    }
}
