using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Mvvm;
using Mvvm.Validation;
using PDYS.Helper;
using PDYS.InfraStructure;
using PDYS.Models;
using PDYS.Services;
using PDYS.Services.ServiceParam;
using System.Collections.Generic;
using DeviceManagement;
using System.Text;



namespace PDYS.ViewModels
{
    public class EmployeeViewModel : EditViewModelBase<Employee>
    {
        public EmployeeViewModel(Employee DataModel)
            : base(DataModel)
        {
            this.Title = "Personel Bilgisi";

            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChange);
            this.Loaded += new Action(OnLoad);

            #region Employee List

            this.EmployeeHolidays.IsNewCommand = true;
            this.EmployeeHolidays.IsOpenCommand = true;
            this.EmployeeHolidays.IsDeleteCommand = true;

            this.EmployeeHolidays.IsAppendCommand = false;
            this.EmployeeHolidays.IsMultiSelect = false;

            this.EmployeeHolidays.OnOpening += new Action<EmployeeHoliday>(EmployeeHolidays_OnOpening);
            this.EmployeeHolidays.OnDeleted += new Action<IEnumerable<EmployeeHoliday>>(EmployeeHolidays_OnDeleted);

            this.EmployeeHolidays.QueryExpression = this.DataModel.EmployeeHolidays.AsQueryable().OrderBy(item => item.StartDate);
            this.EmployeeAccountings.Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Accounting_Items_CollectionChanged);

            #endregion


            #region EmployeeAccountings List

            this.EmployeeAccountings.IsNewCommand = true;
            this.EmployeeAccountings.IsOpenCommand = true;
            this.EmployeeAccountings.IsDeleteCommand = true;

            this.EmployeeAccountings.IsAppendCommand = false;
            this.EmployeeAccountings.IsMultiSelect = false;

            this.EmployeeAccountings.QueryExpression = PDYSEntities.DataContext.EmployeeAccountingSet.Where(ea => ea.EmployeeID == this.DataModel.ID).OrderByDescending(ea => ea.ProcessDate);
            this.EmployeeAccountings.Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Accounting_Items_CollectionChanged);
            this.EmployeeAccountings.OnOpening += new Action<EmployeeAccounting>(EmployeeAccountings_OnOpening);
            this.EmployeeAccountings.OnInserted += new Action<IEnumerable<EmployeeAccounting>>(EmployeeAccountings_OnInserted);

            #endregion

        }


        public bool IsChangeFingerPrint { get; set; }

        void OnLoad()
        {
            #region Load Parameter

            #region Gender Parameter

            var queryGender = from item in PDYSEntities.DataContext.ParameterSet
                              where item.Name == "Gender"
                              orderby item.Text
                              select item;

            queryGender.ToList().ForEach(item => this.ListGender.Add(item));

            #endregion

            #region MaritalStatus Parameter

            var queryMaritalStatus = from item in PDYSEntities.DataContext.ParameterSet
                                     where item.Name == "MaritalStatus"
                                     orderby item.Text
                                     select item;

            queryMaritalStatus.ToList().ForEach(item => this.ListMaritalStatus.Add(item));

            #endregion

            #region City

            //.Include(e => e.Counties)
            PDYSEntities.DataContext.CitySet.ToList().ForEach(item => this.ListCity.Add(item));

            #endregion

            #endregion

            SetViewModel(this.DataModel);

            this.IsNavigationEnabled = (this.DataModel.ID != 0);
            this.EmployeeHolidays.LoadData();


            this.EmployeeAccountings.LoadData();
        }



        void OnPropertyChange(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => this.SelectedAddressCity))
            {
                this.ListAddressCounty.Clear();
                if (this.SelectedAddressCity != null)
                {
                    this.SelectedAddressCity.Counties.ToList().ForEach(county => this.ListAddressCounty.Add(county));
                }
            }
            if (e.PropertyName == this.GetPropertyName(() => this.SelectedRegistryCity))
            {
                this.ListRegistryCounty.Clear();
                if (this.SelectedRegistryCity != null)
                {
                    this.SelectedRegistryCity.Counties.ToList().ForEach(county => this.ListRegistryCounty.Add(county));
                }
            }
        }


        protected override void InitValidation()
        {
            this.Validator.AddRequiredRule(() => this.FirstName, ValidationMessage.RequiredText("Adı"));
            this.Validator.AddMaxLengthRule(() => this.FirstName, 25, ValidationMessage.MaxLengthText("Adı", 25));
            this.Validator.AddRequiredRule(() => this.LastName, ValidationMessage.RequiredText("Soyadı"));
            this.Validator.AddMaxLengthRule(() => this.LastName, 25, ValidationMessage.MaxLengthText("Soyadı", 25));

            this.Validator.AddRequiredRule(() => this.AccessCardNo, ValidationMessage.RequiredText("Kapı Kart Numarası"));
            this.Validator.AddMaxLengthRule(() => this.AccessCardNo, 25, ValidationMessage.MaxLengthText("Kapı Kart Numarası", 25));

            //this.Validator.AddRequiredRule(() => this.AccessPassword, ValidationMessage.RequiredText("Kapı Giriş Şifresi"));
            this.Validator.AddMaxLengthRule(() => this.AccessPassword, 10, ValidationMessage.MaxLengthText("Kapı Giriş Şifresi", 10));

            this.Validator.AddMaxLengthRule(() => this.CompanyRegisterNo, 15, ValidationMessage.MaxLengthText("Sicil Numarası", 15));


            this.Validator.AddMaxLengthRule(() => this.MobilePhone, 25, ValidationMessage.MaxLengthText("Cep Telefonu", 25));
            this.Validator.AddMaxLengthRule(() => this.HomePhone, 25, ValidationMessage.MaxLengthText("Ev Telefonu", 25));

            this.Validator.AddMaxLengthRule(() => this.Email, 25, ValidationMessage.MaxLengthText("E-Posta", 25));
            this.Validator.AddRule(() => this.Email,
                              () =>
                              {
                                  if (string.IsNullOrEmpty(this.Email))
                                      return RuleResult.Valid();

                                  const string regexPattern = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
                                  return RuleResult.Assert(Regex.IsMatch(this.Email, regexPattern), "E-Posta formatı Hatalı");
                              });

            this.Validator.AddMaxLengthRule(() => this.ContactName, 50, ValidationMessage.MaxLengthText("Kontak Adı", 50));
            this.Validator.AddMaxLengthRule(() => this.ContactPhone, 25, ValidationMessage.MaxLengthText("Kontak Telefon", 50));

            this.Validator.AddMaxLengthRule(() => this.GovernmentNo, 11, ValidationMessage.MaxLengthText("TCKN", 11));

            this.Validator.AddRequiredRule(() => this.WorkingStartDate, ValidationMessage.RequiredText("İşe Başlama Tarihi"));
            this.Validator.AddRequiredRule(() => this.Salary, ValidationMessage.RequiredText("Aylık Maaş Tutarı"));

            this.Validator.AddMaxLengthRule(() => this.FatherName, 50, ValidationMessage.MaxLengthText("Baba Adı", 50));
            this.Validator.AddMaxLengthRule(() => this.MotherName, 50, ValidationMessage.MaxLengthText("Anne Adı", 50));
            this.Validator.AddMaxLengthRule(() => this.JopTitle, 50, ValidationMessage.MaxLengthText("Görevi", 50));
        }

        #region Model & ViewModel Transformation

        private void SetViewModel(Employee employee)
        {
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Department = employee.Department;
            this.JopTitle = employee.JopTitle;
            this.Photo = employee.Photo;
            this.IsSyncDevice = employee.IsSyncDevice;

            this.Address = employee.Address;
            this.SelectedAddressCity = employee.AddressCity;//this.ListCity.SingleOrDefault(p => employee.AddressCityID == p.ID);
            this.SelectedAddressCounty = employee.AddressCounty; // this.ListAddressCounty.SingleOrDefault(p => employee.AddressCountyID == p.ID);
            this.MobilePhone = employee.MobilePhone;
            this.HomePhone = employee.HomePhone;
            this.Email = employee.Email;
            this.ContactName = employee.ContactName;
            this.ContactPhone = employee.ContactPhone;

            this.Manager = employee.Manager;
            this.Transport = employee.Transport;
            this.GovernmentNo = employee.GovernmentNo;
            this.BirthDate = employee.BirthDate;
            this.FatherName = employee.FatherName;
            this.MotherName = employee.MotherName;
            this.SelectedRegistryCity = this.ListCity.SingleOrDefault(p => employee.RegistryCityID == p.ID);
            this.SelectedRegistryCounty = this.ListRegistryCounty.SingleOrDefault(p => employee.RegistryCountyID == p.ID);
            this.SelectedGender = this.ListGender.SingleOrDefault(p => employee.Gender == p.Value);
            this.SelectedMaritalStatus = this.ListMaritalStatus.SingleOrDefault(p => employee.MaritalStatus == p.Value);
            this.Description = employee.Description;

            this.WorkingStartDate = employee.WorkingStartDate;
            this.WorkingEndDate = employee.WorkingEndDate;
            this.Salary = employee.Salary;
            this.AccessCardNo = employee.AccessCardNo;
            this.AccessPassword = employee.AccessPassword;
            this.CompanyRegisterNo = employee.CompanyRegisterNo;

            this.SelectedState = this.ListState.SingleOrDefault(p => employee.State == p.Value);

            if (employee.FingerPrints != null)
            {
                foreach (var item in employee.FingerPrints)
                {
                    this.ListFigerPrint.Add(item);
                }
            }

            this.ListFigerPrint.CollectionChanged += (sender, e) =>
            {
                this.IsChangeFingerPrint = true;
            };

        }


        private void SetDataModel(Employee employee)
        {
            employee.FirstName = this.FirstName;
            employee.LastName = this.LastName;
            employee.Department = this.Department;
            employee.JopTitle = this.JopTitle;
            employee.Photo = this.Photo;
            employee.IsSyncDevice = this.IsSyncDevice;

            employee.Address = this.Address;
            employee.AddressCity = this.SelectedAddressCity;
            employee.AddressCounty = this.SelectedAddressCounty;
            employee.MobilePhone = this.MobilePhone;
            employee.HomePhone = this.HomePhone;
            employee.Email = this.Email;
            employee.ContactName = this.ContactName;
            employee.ContactPhone = this.ContactPhone;

            employee.GovernmentNo = this.GovernmentNo;
            employee.BirthDate = this.BirthDate;
            employee.FatherName = this.FatherName;
            employee.MotherName = this.MotherName;
            employee.RegistryCity = this.SelectedRegistryCity;
            employee.RegistryCounty = this.SelectedRegistryCounty;
            employee.Gender = (this.SelectedGender != null) ? this.SelectedGender.Value : default(int?);
            employee.MaritalStatus = (this.SelectedMaritalStatus != null) ? this.SelectedMaritalStatus.Value : default(int?);
            employee.Description = this.Description;


            employee.Manager = this.Manager;
            employee.Transport = this.Transport;
            employee.WorkingStartDate = this.WorkingStartDate;
            employee.WorkingEndDate = this.WorkingEndDate;
            employee.Salary = this.Salary;
            employee.AccessCardNo = this.AccessCardNo;
            employee.AccessPassword = this.AccessPassword;
            employee.CompanyRegisterNo = this.CompanyRegisterNo;

            employee.State = (this.SelectedState != null) ? this.SelectedState.Value : 0;

        }

        #endregion

        #region ViewModel Property

        #region Property FirstName

        private string _firstName;

        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                if (!object.Equals(this._firstName, value))
                {
                    this._firstName = value;
                    this.OnPropertyChanged(() => this.FirstName);
                    this.Validator.Validate(() => this.FirstName);
                }
            }
        }

        #endregion

        #region Property LastName

        private string _lastName;

        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (!object.Equals(this._lastName, value))
                {
                    this._lastName = value;
                    this.OnPropertyChanged(() => this.LastName);
                    this.Validator.Validate(() => this.LastName);
                }
            }
        }

        #endregion

        #region Property JopTitle

        private string _jopTitle;

        public string JopTitle
        {
            get { return this._jopTitle; }
            set
            {
                if (!object.Equals(this._jopTitle, value))
                {
                    this._jopTitle = value;
                    this.OnPropertyChanged(() => this.JopTitle);
                    this.Validator.Validate(() => this.JopTitle);
                }
            }
        }

        #endregion

        #region Property AccessCardNo

        private string _accessCardNo;

        public string AccessCardNo
        {
            get { return this._accessCardNo; }
            set
            {
                if (!object.Equals(this._accessCardNo, value))
                {
                    this._accessCardNo = value;
                    this.OnPropertyChanged(() => this.AccessCardNo);
                    this.Validator.Validate(() => this.AccessCardNo);
                }
            }
        }

        #endregion

        #region Property AccessPassword

        private string _AccessPassword;

        public string AccessPassword
        {
            get { return this._AccessPassword; }
            set
            {
                if (!object.Equals(this._AccessPassword, value))
                {
                    this._AccessPassword = value;
                    this.OnPropertyChanged(() => this.AccessPassword);
                    this.Validator.Validate(() => this.AccessPassword);
                }
            }
        }

        #endregion

        #region Property CompanyRegisterNo

        private string myField;

        public string CompanyRegisterNo
        {
            get { return this.myField; }
            set
            {
                if (!object.Equals(this.myField, value))
                {
                    this.myField = value;
                    this.OnPropertyChanged(() => this.CompanyRegisterNo);
                    this.Validator.Validate(() => this.CompanyRegisterNo);
                }
            }
        }

        #endregion

        #region Property Address

        private string _address;

        public string Address
        {
            get { return this._address; }
            set
            {
                if (!object.Equals(this._address, value))
                {
                    this._address = value;
                    this.OnPropertyChanged(() => this.Address);
                    this.Validator.Validate(() => this.Address);
                }
            }
        }

        #endregion

        #region Property MobilePhone

        private string _mobilePhone;

        public string MobilePhone
        {
            get { return this._mobilePhone; }
            set
            {
                if (!object.Equals(this._mobilePhone, value))
                {
                    this._mobilePhone = value;
                    this.OnPropertyChanged(() => this.MobilePhone);
                    this.Validator.Validate(() => this.MobilePhone);
                }
            }
        }

        #endregion

        #region Property HomePhone

        private string _homePhone;

        public string HomePhone
        {
            get { return this._homePhone; }
            set
            {
                if (!object.Equals(this._homePhone, value))
                {
                    this._homePhone = value;
                    this.OnPropertyChanged(() => this.HomePhone);
                    this.Validator.Validate(() => this.HomePhone);
                }
            }
        }

        #endregion

        #region Property Email

        private string _email;

        public string Email
        {
            get { return this._email; }
            set
            {
                if (!object.Equals(this._email, value))
                {
                    this._email = value;
                    this.OnPropertyChanged(() => this.Email);
                    this.Validator.Validate(() => this.Email);
                }
            }
        }

        #endregion

        #region Property ContactName

        private string _contactName;

        public string ContactName
        {
            get { return this._contactName; }
            set
            {
                if (!object.Equals(this._contactName, value))
                {
                    this._contactName = value;
                    this.OnPropertyChanged(() => this.ContactName);
                    this.Validator.Validate(() => this.ContactName);
                }
            }
        }

        #endregion

        #region Property ContactPhone

        private string _contactPhone;

        public string ContactPhone
        {
            get { return this._contactPhone; }
            set
            {
                if (!object.Equals(this._contactPhone, value))
                {
                    this._contactPhone = value;
                    this.OnPropertyChanged(() => this.ContactPhone);
                    this.Validator.Validate(() => this.ContactPhone);
                }
            }
        }

        #endregion

        #region Property GovernmentNo

        private string _governmentNo;

        public string GovernmentNo
        {
            get { return this._governmentNo; }
            set
            {
                if (!object.Equals(this._governmentNo, value))
                {
                    this._governmentNo = value;
                    this.OnPropertyChanged(() => this.GovernmentNo);
                    this.Validator.Validate(() => this.GovernmentNo);
                }
            }
        }

        #endregion

        #region Property BirthDate

        private DateTime? _birthDate;

        public DateTime? BirthDate
        {
            get { return this._birthDate; }
            set
            {
                if (!object.Equals(this._birthDate, value))
                {
                    this._birthDate = value;
                    this.OnPropertyChanged(() => this.BirthDate);
                    this.Validator.Validate(() => this.BirthDate);
                }
            }
        }

        #endregion

        #region Property FatherName

        private string _fatherName;

        public string FatherName
        {
            get { return this._fatherName; }
            set
            {
                if (!object.Equals(this._fatherName, value))
                {
                    this._fatherName = value;
                    this.OnPropertyChanged(() => this.FatherName);
                    this.Validator.Validate(() => this.FatherName);
                }
            }
        }

        #endregion

        #region Property MotherName

        private string _motherName;

        public string MotherName
        {
            get { return this._motherName; }
            set
            {
                if (!object.Equals(this._motherName, value))
                {
                    this._motherName = value;
                    this.OnPropertyChanged(() => this.MotherName);
                    this.Validator.Validate(() => this.MotherName);
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

        #region Property Photo

        private byte[] _photo;

        public byte[] Photo
        {
            get { return this._photo; }
            set
            {
                if (!object.Equals(this._photo, value))
                {
                    this._photo = value;
                    this.OnPropertyChanged(() => this.Photo);
                    this.Validator.Validate(() => this.Photo);
                }
            }
        }

        #endregion

        #region Property IsSyncDevice

        private bool _IsSyncDevice;

        public bool IsSyncDevice
        {
            get { return this._IsSyncDevice; }
            set
            {
                if (!object.Equals(this._IsSyncDevice, value))
                {
                    this._IsSyncDevice = value;
                    this.OnPropertyChanged(() => this.IsSyncDevice);
                    this.Validator.Validate(() => this.IsSyncDevice);
                }
            }
        }

        #endregion

        #region Property WorkingStartDate

        private DateTime? _workingStartDate;

        public DateTime? WorkingStartDate
        {
            get { return this._workingStartDate; }
            set
            {
                if (!object.Equals(this._workingStartDate, value))
                {
                    this._workingStartDate = value;
                    this.OnPropertyChanged(() => this.WorkingStartDate);
                    this.Validator.Validate(() => this.WorkingStartDate);
                }
            }
        }

        #endregion

        #region Property WorkingEndDate

        private DateTime? _workingEndDate;

        public DateTime? WorkingEndDate
        {
            get { return this._workingEndDate; }
            set
            {
                if (!object.Equals(this._workingEndDate, value))
                {
                    this._workingEndDate = value;
                    this.OnPropertyChanged(() => this.WorkingEndDate);
                    this.Validator.Validate(() => this.WorkingEndDate);
                }
            }
        }

        #endregion

        #region Property Salary

        private decimal? _salary;

        public decimal? Salary
        {
            get { return this._salary; }
            set
            {
                if (!object.Equals(this._salary, value))
                {
                    this._salary = value;
                    this.OnPropertyChanged(() => this.Salary);
                    this.Validator.Validate(() => this.Salary);
                }
            }
        }

        #endregion

        #region Property TotalDebit

        private decimal _TotalDebit;

        public decimal TotalDebit
        {
            get { return this._TotalDebit; }
            set
            {
                if (!object.Equals(this._TotalDebit, value))
                {
                    this._TotalDebit = value;
                    this.OnPropertyChanged(() => this.TotalDebit);
                    this.Validator.Validate(() => this.TotalDebit);
                }
            }
        }

        #endregion

        #region Property TotalCredit

        private decimal _ToptalCredit;

        public decimal TotalCredit
        {
            get { return this._ToptalCredit; }
            set
            {
                if (!object.Equals(this._ToptalCredit, value))
                {
                    this._ToptalCredit = value;
                    this.OnPropertyChanged(() => this.TotalCredit);
                    this.Validator.Validate(() => this.TotalCredit);
                }
            }
        }

        #endregion

        #region Property TotalNet

        private decimal _TotalNet;

        public decimal TotalNet
        {
            get { return this._TotalNet; }
            set
            {
                if (!object.Equals(this._TotalNet, value))
                {
                    this._TotalNet = value;
                    this.OnPropertyChanged(() => this.TotalNet);
                    this.Validator.Validate(() => this.TotalNet);
                }
            }
        }

        #endregion

        #endregion

        #region Property Manager

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

        #region Property Transport

        private Transport _transport;

        public Transport Transport
        {
            get { return this._transport; }
            set
            {
                if (!object.Equals(this._transport, value))
                {
                    this._transport = value;
                    this.OnPropertyChanged(() => this.Transport);
                    this.Validator.Validate(() => this.Transport);
                }
            }
        }

        #endregion

        #region Property Department

        private Department _department;

        public Department Department
        {
            get { return this._department; }
            set
            {
                if (!object.Equals(this._department, value))
                {
                    this._department = value;
                    this.OnPropertyChanged(() => this.Department);
                    this.Validator.Validate(() => this.Department);
                }
            }
        }

        #endregion

        #region Property SelectedRegistryCity

        private City _selectedRegistryCity;

        public City SelectedRegistryCity
        {
            get { return this._selectedRegistryCity; }
            set
            {
                if (!object.Equals(this._selectedRegistryCity, value))
                {
                    this._selectedRegistryCity = value;
                    this.OnPropertyChanged(() => this.SelectedRegistryCity);
                    this.Validator.Validate(() => this.SelectedRegistryCity);
                }
            }
        }

        #endregion

        #region Property SelectedAddressCity

        private City _selectedAddressCity;

        public City SelectedAddressCity
        {
            get { return this._selectedAddressCity; }
            set
            {
                if (!object.Equals(this._selectedAddressCity, value))
                {
                    this._selectedAddressCity = value;
                    this.OnPropertyChanged(() => this.SelectedAddressCity);
                    this.Validator.Validate(() => this.SelectedAddressCity);
                }
            }
        }

        #endregion

        #region Property ListCity Collection

        private ObservableCollection<City> _listCity;

        public ObservableCollection<City> ListCity
        {
            get { return this._listCity = this._listCity ?? new ObservableCollection<City>(); }
            private set { this._listCity = value; }
        }

        #endregion

        #region Property SelectedRegistryCounty

        private County _selectedRegistryCounty;

        public County SelectedRegistryCounty
        {
            get { return this._selectedRegistryCounty; }
            set
            {
                if (!object.Equals(this._selectedRegistryCounty, value))
                {
                    this._selectedRegistryCounty = value;
                    this.OnPropertyChanged(() => this.SelectedRegistryCounty);
                    this.Validator.Validate(() => this.SelectedRegistryCounty);
                }
            }
        }

        #endregion

        #region Property ListRegistryCounty Collection

        private ObservableCollection<County> _listRegistryCounty;

        public ObservableCollection<County> ListRegistryCounty
        {
            get { return this._listRegistryCounty = this._listRegistryCounty ?? new ObservableCollection<County>(); }
            private set { this._listRegistryCounty = value; }
        }

        #endregion

        #region Property SelectedAddressCounty

        private County _selectedAddressCounty;

        public County SelectedAddressCounty
        {
            get { return this._selectedAddressCounty; }
            set
            {
                if (!object.Equals(this._selectedAddressCounty, value))
                {
                    this._selectedAddressCounty = value;
                    this.OnPropertyChanged(() => this.SelectedAddressCounty);
                    this.Validator.Validate(() => this.SelectedAddressCounty);
                }
            }
        }

        #endregion

        #region Property ListAddressCounty Collection

        private ObservableCollection<County> _listAddressCounty;

        public ObservableCollection<County> ListAddressCounty
        {
            get { return this._listAddressCounty = this._listAddressCounty ?? new ObservableCollection<County>(); }
            private set { this._listAddressCounty = value; }
        }

        #endregion

        #region Property SelectedGender

        private Parameter selectedGender;

        public Parameter SelectedGender
        {
            get { return this.selectedGender; }
            set
            {
                if (!object.Equals(this.selectedGender, value))
                {
                    this.selectedGender = value;
                    this.OnPropertyChanged(() => this.SelectedGender);
                    this.Validator.Validate(() => this.SelectedGender);
                }
            }
        }

        #endregion

        #region Property SelectedMaritialStatus

        private Parameter _selectedMaritalStatus;

        public Parameter SelectedMaritalStatus
        {
            get { return this._selectedMaritalStatus; }
            set
            {
                if (!object.Equals(this._selectedMaritalStatus, value))
                {
                    this._selectedMaritalStatus = value;
                    this.OnPropertyChanged(() => this.SelectedMaritalStatus);
                    this.Validator.Validate(() => this.SelectedMaritalStatus);
                }
            }
        }

        #endregion

        #region Property ListGender Collection

        private ObservableCollection<Parameter> _listGender;

        public ObservableCollection<Parameter> ListGender
        {
            get { return this._listGender = this._listGender ?? new ObservableCollection<Parameter>(); }
            private set { this._listGender = value; }
        }

        #endregion

        #region Property ListMaritalStatus Collection

        private ObservableCollection<Parameter> _listMaritalStatus;

        public ObservableCollection<Parameter> ListMaritalStatus
        {
            get { return this._listMaritalStatus = this._listMaritalStatus ?? new ObservableCollection<Parameter>(); }
            private set { this._listMaritalStatus = value; }
        }

        #endregion

        #region Lookup

        #region Property LookupDepartment

        private LookupViewModel<DepartmentListViewModel> _lookupDepartment;

        public LookupViewModel<DepartmentListViewModel> LookupDepartment
        {

            get
            {
                if (this._lookupDepartment == null)
                    this._lookupDepartment = new LookupViewModel<DepartmentListViewModel>() { Title = "Departman Seçimi" };

                return _lookupDepartment;
            }
        }

        #endregion

        #region Property LookupManager

        private LookupViewModel<EmployeeListViewModel> _lookupmanager;

        public LookupViewModel<EmployeeListViewModel> LookupManager
        {
            get
            {
                if (this._lookupmanager == null)
                    this._lookupmanager = new LookupViewModel<EmployeeListViewModel>() { Title = "Yönetici Seçimi" };

                return this._lookupmanager;
            }
        }

        #endregion

        #region Property LookupTransport

        private LookupViewModel<TransportListViewModel> _lookuptransport;

        public LookupViewModel<TransportListViewModel> LookupTransport
        {
            get
            {
                if (this._lookuptransport == null)
                    this._lookuptransport = new LookupViewModel<TransportListViewModel>() { Title = "Servis Seçimi" };

                return this._lookuptransport;
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


        #region Property EmployeeAccountings Collection

        private EmployeeAccountingListViewModel _EmployeeAccountings;

        public EmployeeAccountingListViewModel EmployeeAccountings
        {
            get
            {
                if (this._EmployeeAccountings == null)
                {
                    this._EmployeeAccountings = new EmployeeAccountingListViewModel(false);
                }

                return this._EmployeeAccountings;
            }
        }

        #endregion

        #region EmployeeAccountings Method

        void Accounting_Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.TotalDebit = this.EmployeeAccountings.Items.Sum(item => item.Debit);
            this.TotalCredit = this.EmployeeAccountings.Items.Sum(item => item.Credit);

            this.TotalNet = this.TotalCredit - this.TotalDebit;
        }

        void EmployeeAccountings_OnOpening(EmployeeAccounting obj)
        {
            obj.Employee = this.DataModel;
        }

        void EmployeeAccountings_OnInserted(IEnumerable<EmployeeAccounting> insertedrecords)
        {
            foreach (var item in insertedrecords)
            {
                item.Employee = this.DataModel;
                PDYSEntities.DataContext.EmployeeAccountingSet.Add(item);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.EmployeeAccountings.LoadData();
        }



        #endregion

        #region Property EmployeeHolidays Collection

        private EmployeeHolidayListViewModel _employeeholidays;

        public EmployeeHolidayListViewModel EmployeeHolidays
        {
            get
            {
                if (this._employeeholidays == null)
                {
                    this._employeeholidays = new EmployeeHolidayListViewModel(false);
                }

                return this._employeeholidays;
            }

        }

        void EmployeeHolidays_OnOpening(EmployeeHoliday obj)
        {
            obj.Employee = this.DataModel;
        }

        void EmployeeHolidays_OnDeleted(IEnumerable<EmployeeHoliday> deletedrecords)
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

            foreach (var item in deletedrecords)
            {
                PDYSEntities.DataContext.EmployeeHolidaySet.Remove(item);
            }

            PDYSEntities.DataContext.SaveChanges();
            this.EmployeeHolidays.LoadData();
        }

        //void EmployeeHolidays_OnInserted(System.Collections.Generic.IEnumerable<EmployeeHoliday> insertedrecords)
        //{
        //    foreach (var item in insertedrecords)
        //    {
        //        item.Employee = this.DataModel;
        //        this.DataModel.EmployeeHolidays.Add(item);
        //    }

        //    PDYSEntities.DataContext.SaveChanges();
        //    this.EmployeeHolidays.LoadData();
        //}

        #endregion

        #region Property ListFigerPrint Collection

        private ObservableCollection<EmployeeFingerPrint> _ListFigerPrint;

        public ObservableCollection<EmployeeFingerPrint> ListFigerPrint
        {
            get { return this._ListFigerPrint = this._ListFigerPrint ?? new ObservableCollection<EmployeeFingerPrint>(); }
            private set { this._ListFigerPrint = value; }
        }

        #endregion

        #endregion


        #region Command

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
                PDYSEntities.DataContext.EmployeeSet.Add(this.DataModel);
            }

            SetDataModel(this.DataModel);


            if (this.IsChangeFingerPrint && this.IsNavigationEnabled)
            {
                var oldDataList = PDYSEntities.DataContext.EmployeeFingerPrints.Where(item => item.EmployeeID == this.DataModel.ID);

                foreach (var item in oldDataList)
                {
                    PDYSEntities.DataContext.EmployeeFingerPrints.Remove(item);
                }

                foreach (var item in this.ListFigerPrint)
                {
                    PDYSEntities.DataContext.EmployeeFingerPrints.Add(item);
                }

            }

            PDYSEntities.DataContext.SaveChanges();


            this.IsNavigationEnabled = true;
            this.IsSavedData = true;

            this.ServicePresenter.ShowInformationMessage("Kaydetme İşlemi Tamamlandı.");
        }

        #region Property ReadDeviceCardCommand

        private RelayCommand _ReadDeviceCardCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ReadDeviceCardCommand
        {
            get
            {
                if (this._ReadDeviceCardCommand == null)
                    this._ReadDeviceCardCommand = new RelayCommand(ExcuteReadDeviceCardCommand);
                return this._ReadDeviceCardCommand;
            }
        }

        void ExcuteReadDeviceCardCommand()
        {
            ReaderDeviceSelectionViewModel.OpenSelectionWindow(ReadCardNumber, ReaderDeviceSelectionMode.SingleSelect);
        }

        void ReadCardNumber(IEnumerable<ReaderDevice> selectedDevice)
        {
            ReaderDevice device = selectedDevice.FirstOrDefault();

            if (device == null)
                return;

            using (IDeviceManagement reader = new ZKDevice.ZKDeviceManagement())
            {
                bool isConnect = reader.ConnectDevice(device.IP, device.Port.Value, device.ComKey.Value);

                if (!isConnect)
                {
                    this.ServicePresenter.ShowErrorMessage("Bağlantı Kurulamadı.");
                    return;
                }

                ConfirmParam param = new ConfirmParam();
                param.Message = "Lütfen Kartı Cihaza Okutunuz ve Tamam tuşuna basınız.";
                param.OnConfirmResult = (result) =>
                {
                    if (result == ConfirmParam.ConfirmResult.Yes)
                    {
                        this.AccessCardNo = reader.GetLastCardNumber();
                    }
                };

                this.ServicePresenter.ConfirmMessage(param);
                //this.ServicePresenter.ShowMessage(reader.LOG.ToString());
            }
        }

        #endregion

        #region Property WriteFingerPrintCommand

        private RelayCommand _WriteFingerPrintCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand WriteFingerPrintCommand
        {
            get
            {
                if (this._WriteFingerPrintCommand == null)
                    this._WriteFingerPrintCommand = new RelayCommand(ExcuteWriteFingerPrintCommand);
                return this._WriteFingerPrintCommand;
            }
        }

        void ExcuteWriteFingerPrintCommand()
        {
            Action<IEnumerable<ReaderDevice>> AsynWriteFingerPrint = (selectedDevices) =>
            {
                if (!selectedDevices.Any())
                {
                    this.ServicePresenter.ShowAlertMessage("Cihaz Seçimi Yapılmadı.");
                    return;
                }

                ProcessManager.Execute("Parmak İzi Bilgileri Yazılıyor...", new Action<IEnumerable<ReaderDevice>>(WriteFingerPrint), new object[] { selectedDevices });
            };



            ReaderDeviceSelectionViewModel.OpenSelectionWindow(AsynWriteFingerPrint, ReaderDeviceSelectionMode.MultiSelect);
        }

        void WriteFingerPrint(IEnumerable<ReaderDevice> selectedDevices)
        {
            IDeviceManagement reader = new ZKDevice.ZKDeviceManagement();

            StringBuilder strLog = new StringBuilder();

            foreach (var deviceInfo in selectedDevices)
            {
                bool isConnect = reader.ConnectDevice(deviceInfo.IP, deviceInfo.Port.Value, deviceInfo.ComKey.Value);

                if (!isConnect)
                {
                    strLog.AppendLine(string.Format("\"{0}\" isimli cihaza  baglanti kurulamadi.", deviceInfo.DisplayName));
                    continue;
                }

                try
                {
                    var oldFingerPrints = reader.GetFingerPrintData(this.DataModel.ID);

                    foreach (var fingerprint in oldFingerPrints)
                    {
                        reader.DeleteFingerPrintData(fingerprint);

                        strLog.AppendLine(string.Format("{0}. Eski parmak izi bilgisi silindi.", fingerprint.FingerIndex));
                    }

                    strLog.AppendLine(string.Empty);

                    foreach (var dbfingerprint in this.ListFigerPrint)
                    {
                        FingerPrint fingerprint = new FingerPrint();
                        fingerprint.FingerIndex = dbfingerprint.FingerIndex;
                        fingerprint.TemplateData = dbfingerprint.TemplateData;
                        fingerprint.Flag = (FingerPrintFlag)dbfingerprint.Flag;
                        fingerprint.UserID = this.DataModel.ID;

                        reader.CrateFingerPrintData(fingerprint);

                        strLog.AppendLine(string.Format("{0}. Parmak izi bilgisi yazıldı.", dbfingerprint.FingerIndex));
                    }

                    strLog.AppendLine(string.Empty);

                    strLog.AppendLine("İşlem Tamamlandı.");
                }
                catch (Exception e)
                {
                    strLog.AppendLine("Hata Oluştu. Hata Mesajı : " + e.Message);
                }
                finally
                {
                    reader.DisconnectDevice();
                }
            }

            LogViewModel.OpenWindow("Parmak izi bilgileri yazma işlemi...", strLog.ToString());
        }

        #endregion

        #region Property ReadFingerPrintCommand

        private RelayCommand _ReadFingerPrintCommand;

        public RelayCommand ReadFingerPrintCommand
        {
            get
            {
                if (this._ReadFingerPrintCommand == null)
                    this._ReadFingerPrintCommand = new RelayCommand(ExcuteReadFingerPrintCommand);
                return this._ReadFingerPrintCommand;
            }
        }

        void ExcuteReadFingerPrintCommand()
        {
            Action<IEnumerable<ReaderDevice>> AsynReadFingerPrint = (selectedDevices) =>
            {
                if (!selectedDevices.Any())
                {
                    this.ServicePresenter.ShowAlertMessage("Cihaz Seçimi Yapılmadı.");
                    return;
                }

                ProcessManager.Execute("Parmak İzi Bilgileri Okunuyor...", new Action<IEnumerable<ReaderDevice>>(ReadFingerPrint), new object[] { selectedDevices });
            };



            ReaderDeviceSelectionViewModel.OpenSelectionWindow(AsynReadFingerPrint, ReaderDeviceSelectionMode.SingleSelect);
        }

        void ReadFingerPrint(IEnumerable<ReaderDevice> selectedDevices)
        {

            ReaderDevice deviceInfo = selectedDevices.First();

            IDeviceManagement reader = new ZKDevice.ZKDeviceManagement();

            bool isConnect = reader.ConnectDevice(deviceInfo.IP, deviceInfo.Port.Value, deviceInfo.ComKey.Value);

            if (!isConnect)
            {
                this.ServicePresenter.ShowErrorMessage(string.Format("\"{0}\" isimli cihaza  baglanti kurulamadi.", deviceInfo.DisplayName));
                return;
            }


            this.ListFigerPrint.Clear();

            var items = reader.GetFingerPrintData(this.DataModel.ID);

            foreach (var item in items)
            {
                EmployeeFingerPrint efp = new EmployeeFingerPrint();
                efp.FingerIndex = item.FingerIndex;
                efp.TemplateData = item.TemplateData;
                efp.Flag = (int)item.Flag;

                efp.EmployeeID = this.DataModel.ID;
                efp.Employee = this.DataModel;

                this.ListFigerPrint.Add(efp);
            }

            reader.DisconnectDevice();

        }

        #endregion


        #region LoadImageCommand

        private RelayCommand _loadImageCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand LoadImageCommand
        {
            get
            {
                if (this._loadImageCommand == null)
                    this._loadImageCommand = new RelayCommand(ExcuteLoadImageCommand);
                return this._loadImageCommand;
            }
        }

        void ExcuteLoadImageCommand()
        {
            DialogFileParam fileparam = new DialogFileParam();
            fileparam.FileFilter = DialogFileParam.ImageFileFilter;
            fileparam.FileLoadedAction = (buffer) =>
                {
                    this.Photo = ImageHelper.ResizeImage(buffer, 120, 100);
                };

            this.ServicePresenter.OpenFileDialog(fileparam);
        }

        #endregion

        #endregion


    }
}
