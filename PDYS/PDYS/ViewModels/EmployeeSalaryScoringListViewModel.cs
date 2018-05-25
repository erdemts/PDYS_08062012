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
using System.Globalization;

namespace PDYS.ViewModels
{
    public class EmployeeSalaryScoringListViewModel : ListViewModelBase<EmployeeSalaryScoring, EmployeeSalaryScoringViewModel>
    {

        public EmployeeSalaryScoringListViewModel(bool autoloaddata)
            : base(autoloaddata)
        {
            this.IsMultiSelect = true;
            this.IsDeleteCommand = true;
            this.OnDeleted += new Action<IEnumerable<EmployeeSalaryScoring>>(DeleteSalaryScoringsUIEvent);
            this.Loaded += new Action(PersonalInputOutputListViewModel_Loaded);
        }


        void PersonalInputOutputListViewModel_Loaded()
        {
            #region Month & Year

            this.ListYear.Clear();
            this.ListYear.Add(new ListItemViewModel(DateTime.Now.Year, DateTime.Now.Year.ToString()));
            this.ListYear.Add(new ListItemViewModel(DateTime.Now.Year - 1, (DateTime.Now.Year - 1).ToString()));
            this.ListYear.Insert(0, new ListItemViewModel(-1, "Tümü"));

            this.SelectYear = this.ListYear.First();

            this.ListMonth.Clear();

            string[] months = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames;

            for (int index = 0; index < months.Length - 1; index++)
            {
                this.ListMonth.Add(new ListItemViewModel(index + 1, months[index]));
            }

            this.ListMonth.Insert(0, new ListItemViewModel(-1, "Tümü"));

            this.SelectMonth = this.ListMonth.First();

            #endregion

            if (IsAutoLoadData)
                this.SearchCommand.Execute();
        }

        void DeleteSalaryScoringsUIEvent(IEnumerable<EmployeeSalaryScoring> deleteditems)
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
            ProcessManager.Execute("Hakediş Siliniyor...", new Action<IEnumerable<EmployeeSalaryScoring>>(DeleteSalaryScorings), deleteditems);
            //DeleteSalaryScorings(deleteditems);

            this.LoadData();

        }

        void DeleteSalaryScorings(IEnumerable<EmployeeSalaryScoring> deleteditems)
        {
            foreach (var item in deleteditems)
            {

                var inouts = PDYSEntities.DataContext.EmployeeInOutScoringSet.Where(ios => ios.EmployeeSalaryScoringID == item.ID);
                //var inouts = item.EmployeeInOutScorings;

                foreach (EmployeeInOutScoring ios in inouts)
                {
                    ios.DailyPayment = 0;
                    ios.DailyDeduction = 0;
                    ios.DailyExtPayment = 0;

                    ios.EmployeeSalaryScoringID = null;
                    ios.EmployeeSalaryScoring = null;
                }

                // Hesap Hareketi Sililiniyor
                var oldaccountings = PDYSEntities.DataContext.EmployeeAccountingSet.Where(accounting => accounting.EmployeeSalaryScoringID == item.ID);
                foreach (var oldaccounting in oldaccountings)
                {
                    PDYSEntities.DataContext.EmployeeAccountingSet.Remove(oldaccounting);
                }

                PDYSEntities.DataContext.EmployeeSalaryScoringSet.Remove(item);
            }

            PDYSEntities.DataContext.SaveChanges();
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

        #region Property SelectYear

        private ListItemViewModel _SelectYear;

        public ListItemViewModel SelectYear
        {
            get { return this._SelectYear; }
            set
            {
                if (!object.Equals(this._SelectYear, value))
                {
                    this._SelectYear = value;
                    this.OnPropertyChanged(() => this.SelectYear);
                    this.Validator.Validate(() => this.SelectYear);
                }
            }
        }

        #endregion

        #region Property ListYear Collection

        ObservableCollection<ListItemViewModel> _ListYear;
        public ObservableCollection<ListItemViewModel> ListYear
        {
            get { return this._ListYear = this._ListYear ?? new ObservableCollection<ListItemViewModel>(); }
            private set { this._ListYear = value; }
        }

        #endregion


        #region Property SelectMonth

        private ListItemViewModel _SelectMonth;

        public ListItemViewModel SelectMonth
        {
            get { return this._SelectMonth; }
            set
            {
                if (!object.Equals(this._SelectMonth, value))
                {
                    this._SelectMonth = value;
                    this.OnPropertyChanged(() => this.SelectMonth);
                    this.Validator.Validate(() => this.SelectMonth);
                }
            }
        }

        #endregion

        #region Property ListMonth Collection

        private ObservableCollection<ListItemViewModel> _ListMonth;

        public ObservableCollection<ListItemViewModel> ListMonth
        {
            get { return this._ListMonth = this._ListMonth ?? new ObservableCollection<ListItemViewModel>(); }
            private set { this._ListMonth = value; }
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
            var query = PDYSEntities.DataContext.EmployeeSalaryScoringSet.AsQueryable();

            #region Query Expression

            if (this.Personal != null)
                query = query.Where(item => item.EmployeeID == this.Personal.ID);

            if (this.SelectYear != null && this.SelectYear.ID != -1)
                query = query.Where(item => item.PeriodYear == this.SelectYear.ID);
            
            if (this.SelectMonth != null && this.SelectMonth.ID != -1 )
                query = query.Where(item => item.PeriodMonth == this.SelectMonth.ID);

            query = query.OrderByDescending(item => item.PeriodYear);
            query = query.OrderByDescending(item => item.PeriodMonth);

            #endregion

            this.QueryExpression = query;

            LoadData();
        }

        #endregion


        #region Property ScoringCommand

        private RelayCommand _ScoringCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ScoringCommand
        {
            get
            {
                if (this._ScoringCommand == null)
                    this._ScoringCommand = new RelayCommand(ExcuteScoringCommand);
                return this._ScoringCommand;
            }
        }

        void ExcuteScoringCommand()
        {
            Action<Employee,int, int, string> selectAction = (employee, year, month, monthname) =>
            {
                ProcessManager.Execute("Hakediş Hesaplanıyor", new Action<Employee, int, int, string>(CalculateScoring),employee, year, month, monthname);
            };

            SelectSalaryScoringDateViewModel.OpenSelectionWindow(selectAction);
        }



        void CalculateScoring(Employee SelectedEmployee,int ScoringYear, int ScoringMonth, string ScoringMonthName)
        {
            IEnumerable<Employee> employeList = null;

            if (SelectedEmployee != null)
                employeList = new Employee[] { SelectedEmployee };
            else
                employeList = PDYSEntities.DataContext.EmployeeSet.Where(e => e.State == 0);

            DateTime startDate = new DateTime(ScoringYear, ScoringMonth, 1);
            DateTime endDate = startDate.AddMonths(1);

            decimal totalDays = (decimal)(endDate - startDate).TotalDays;

            foreach (var employee in employeList)
            {

                #region  Onceki Hakedisi Siliniyor

                var oldSalaryScoring = from item in  PDYSEntities.DataContext.EmployeeSalaryScoringSet
                                     where item.EmployeeID == employee.ID
                                     && item.PeriodYear == ScoringYear
                                     && item.PeriodMonth == ScoringMonth
                                     select item;



                this.DeleteSalaryScorings(oldSalaryScoring);

                if (oldSalaryScoring.Any())
                    continue;

                #endregion 

                List<EmployeeInOutScoring> ListInOutScoring = PDYSEntities.DataContext.EmployeeInOutScoringSet.Where(ios => ios.EmployeeID == employee.ID && startDate <= ios.ScoringDate && ios.ScoringDate < endDate).OrderBy(ios => ios.ScoringDate).ToList();

                #region IsError
                bool IsError = ListInOutScoring.Any(ios => ios.ProcessState == 0);
                if (IsError)
                {
                    EmployeeSalaryScoring errorscroing = new EmployeeSalaryScoring();
                    errorscroing.Employee = employee;
                    errorscroing.PeriodYear = ScoringYear;
                    errorscroing.PeriodMonth = ScoringMonth;

                    errorscroing.ProcessState = 0; // Hatalı
                    errorscroing.ProcessMessage = "Hatalı puantaj bilgisi içeriyor.";

                    PDYSEntities.DataContext.EmployeeSalaryScoringSet.Add(errorscroing);
                    PDYSEntities.DataContext.SaveChanges();

                    // Puantaj ile Maaş Bilgisi ilişkilendiriliyor
                    ListInOutScoring.ForEach(ioutscoring => ioutscoring.EmployeeSalaryScoring = errorscroing);
                    PDYSEntities.DataContext.SaveChanges();

                    continue;
                }
                #endregion
                
                decimal salaryMontly = (employee.Salary.HasValue) ? employee.Salary.Value : 0;
                decimal slaryDaily = salaryMontly / 30;

                //DateTime processDate = startDate;
                endDate = (employee.WorkingEndDate.HasValue && employee.WorkingEndDate.Value < endDate) ? employee.WorkingEndDate.Value : endDate;

                int dailySalaryCount = 0;
                
                #region Gunluk Hesaplama

                foreach (EmployeeInOutScoring ioutscoring in ListInOutScoring)
                {
                    if (ioutscoring.ProcessState == 1 || ioutscoring.ProcessState == 2)
                    {
                        if (ioutscoring.WeeklyOvertime != null)
                        {
                            #region Haftalık Mesai
                            // ucretsiz izin icin gunluk para odemesi yapilmaz
                            if (ioutscoring.IsHoliday || ioutscoring.IsPublicHoliday || ioutscoring.WorkTime > 0)
                            {
                                ioutscoring.DailyPayment = slaryDaily;
                                dailySalaryCount++;
                            }


                            decimal slaryMinuteRate = 0;

                            double regularWorkingMinute = new TimeSpan(ioutscoring.WeeklyOvertime.RegularHrs).TotalMinutes;
                            if (regularWorkingMinute > 0)
                                slaryMinuteRate = slaryDaily / (decimal)regularWorkingMinute;


                            double deffenceMinute = new TimeSpan(ioutscoring.WeeklyOvertime.DefenceDuration).TotalMinutes;

                            #region Mesai & Ceza

                            if (ioutscoring.LessTime > 0 )
                            {

                                if (deffenceMinute < ioutscoring.LessTime)
                                {
                                    decimal wrkLessTimeMinute = (decimal)ioutscoring.LessTime;

                                    // Bunu veritabanina yaz
                                    //decimal calculatedDeduction = wrkLessMinute + (wrkLessMinute * ioutscoring.WeeklyOvertime.MissingFactor);
                                    decimal calculatedDeduction = (wrkLessTimeMinute * ioutscoring.WeeklyOvertime.MissingFactor);
                                    ioutscoring.DailyDeduction = calculatedDeduction * slaryMinuteRate;
                                }
                            }
                            
                            if (ioutscoring.OverTime > 0)
                            {
                                if (deffenceMinute < ioutscoring.OverTime)
                                {

                                    decimal wrkOverTimeMinute = (decimal)ioutscoring.OverTime;

                                    // Bunu veritabanina yaz
                                    //decimal calculatedOvertime = wrkOverTimeMinute + (wrkOverTimeMinute * ioutscoring.WeeklyOvertime.OvertimeFactor);
                                    decimal calculatedOvertime = (wrkOverTimeMinute * ioutscoring.WeeklyOvertime.OvertimeFactor);
                                    ioutscoring.DailyExtPayment = calculatedOvertime * slaryMinuteRate;
                                }
                            }

                            #endregion


                            // Tatil Gunu Calisma Hesabi
                            // Eger ucretsiz izinde calisirsa saat ucretinden  normal calistigi kadar para alir.
                            if (ioutscoring.IsAnyHoliday)
                            {
                                decimal overtimefactor = 0;

                                if (ioutscoring.IsHoliday)
                                    overtimefactor = ioutscoring.WeeklyOvertime.HolidayFactor;
                                else if (ioutscoring.IsPublicHoliday)
                                    overtimefactor = ioutscoring.WeeklyOvertime.PubHolidayFactor;

                                decimal wrkTotalTimeMinute = (decimal)ioutscoring.WorkTime;
                                // Bunu veritabanina yaz
                                decimal calculatedOvertime = wrkTotalTimeMinute + (wrkTotalTimeMinute * overtimefactor);
                                ioutscoring.DailyExtPayment = calculatedOvertime * slaryMinuteRate;
                            }

                            #endregion

                        }
                        else if (ioutscoring.OutSourceOvertime != null)
                        {
                            #region Outsource

                            decimal wrkTotalTimeMinute = (decimal) TimeSpan.FromMinutes(ioutscoring.WorkTime).TotalHours;
                            ioutscoring.DailyPayment = wrkTotalTimeMinute * (ioutscoring.OutSourceOvertime.HourlyPayment);
                            
                            #endregion
                        }
                    }

                }
                #endregion

                #region Hesaplanan Tutar Yuvarlaniyor
                foreach (EmployeeInOutScoring ioutscoring in ListInOutScoring)
                {
                    ioutscoring.DailyPayment = Math.Round(ioutscoring.DailyPayment, 2);
                    ioutscoring.DailyExtPayment = Math.Round(ioutscoring.DailyExtPayment, 2);
                    ioutscoring.DailyDeduction = Math.Round(ioutscoring.DailyDeduction, 2);

                    ioutscoring.DailyNetPayment = ioutscoring.DailyPayment + ioutscoring.DailyExtPayment - ioutscoring.DailyDeduction;
                }
                #endregion

                #region  Maas Toplamı

                EmployeeSalaryScoring salaryscroing = new EmployeeSalaryScoring();
                salaryscroing.Employee = employee;
                salaryscroing.PeriodYear = ScoringYear;
                salaryscroing.PeriodMonth = ScoringMonth;
                salaryscroing.MonthDays = (int)totalDays;
                salaryscroing.DefinedSalary = salaryMontly;
                salaryscroing.PaymentDays = ListInOutScoring.Count(ios => ios.DailyPayment!=0 || ios.DailyExtPayment != 0 || ios.DailyDeduction != 0);

                salaryscroing.ExtPayment = ListInOutScoring.Sum(ios => ios.DailyExtPayment);
                salaryscroing.Deduction = ListInOutScoring.Sum(ios => ios.DailyDeduction);


                #region Toplam Tutar
                //Aylik Maas Gun Sayisi Toplam Gune Esitse  Direkt Maas atanir
                if (totalDays == dailySalaryCount)
                    salaryscroing.TotalPayment = salaryMontly;
                else
                {
                    // Haftalık Mesai Maas Toplami
                    if (dailySalaryCount > 0)
                    {
                        //Eksik Gunler Maasdan cıkartılır.
                        decimal lessDays = totalDays - dailySalaryCount;
                        decimal temptotal =  salaryMontly - (lessDays * slaryDaily);

                        if (temptotal <= 0)
                            temptotal = dailySalaryCount * slaryDaily;

                        temptotal = (temptotal < 0) ? 0 : temptotal;

                        salaryscroing.TotalPayment += temptotal;

                    }

                    // Kumulatif Mesai Maas Toplami
                    if (ListInOutScoring.Any(item => item.OutSourceOvertimeID.HasValue))
                    {
                        salaryscroing.TotalPayment += ListInOutScoring.Where(item => item.OutSourceOvertimeID.HasValue).Sum(ios => ios.DailyPayment);
                    }
                }


                salaryscroing.TotalPayment =  Math.Round(salaryscroing.TotalPayment, 2);

                salaryscroing.NetPayment = salaryscroing.TotalPayment + salaryscroing.ExtPayment - salaryscroing.Deduction;
                salaryscroing.NetPayment = (salaryscroing.NetPayment < 0) ? 0 : salaryscroing.NetPayment;
                #endregion

                #region Hakedis Statu
                if (totalDays == ListInOutScoring.Count)
                {
                    salaryscroing.ProcessState = 1; // OK.
                    salaryscroing.ProcessMessage = "Islem Tamamlandi."; 
                }
                else
                {
                    salaryscroing.ProcessState = 2; // Eksik 
                    salaryscroing.ProcessMessage = "Eksik puantaj bilgisi.";
                }
                #endregion

                PDYSEntities.DataContext.EmployeeSalaryScoringSet.Add(salaryscroing);
                
                #endregion

                #region Personel Hesap Hareketine Maas Ekleniyoe
                EmployeeAccounting employeeAccounting = new EmployeeAccounting();
                employeeAccounting.Employee = employee;
                employeeAccounting.EmployeeSalaryScoring = salaryscroing;
                //Code="00101", Name="Maaş Hakediş"
                employeeAccounting.AccountingDefination = PDYSEntities.DataContext.AccountingProcessTypeSet.FirstOrDefault(at => at.Code == "00101");
                employeeAccounting.ProcessDate = DateTime.Now;
                employeeAccounting.Subject = string.Format("{2} ( {0} - {1} )", ScoringMonthName, ScoringYear, employeeAccounting.AccountingDefination.DisplayName);
                employeeAccounting.Debit = 0;
                employeeAccounting.Credit = salaryscroing.NetPayment;

                PDYSEntities.DataContext.EmployeeAccountingSet.Add(employeeAccounting);

                #endregion 


                #region Puantaj ile Maas Bilgisi iliskilendiriliyor

                ListInOutScoring.ForEach(ioutscoring => ioutscoring.EmployeeSalaryScoring = salaryscroing);

                #endregion

                PDYSEntities.DataContext.SaveChanges();

                this.LoadData();

            }
        }

        #endregion


    }
}
