using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PDYS.Models
{
    public class EmployeeSalaryScoring : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }

        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }

        [NotMapped]
        public string PeriodPreview
        {
            get
            {
                return string.Format("{0} - {1}", PeriodYear, CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(PeriodMonth));
            }
        }

        public int MonthDays { get; set; }
        public int PaymentDays { get; set; }

        [NotMapped]
        public string DayCountPreview
        {
            get
            {
                return string.Format("{0:00} / {1:00}", PaymentDays, MonthDays);
            }
        }


        public decimal DefinedSalary { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal Deduction { get; set; }
        public decimal ExtPayment { get; set; }
        public decimal NetPayment { get; set; }

        public int ProcessState { get; set; }
        public string ProcessMessage { get; set; }

        // Navigation property
        public virtual ICollection<EmployeeInOutScoring> EmployeeInOutScorings { get; private set; }

        #region State
        [NotMapped]
        public bool IsError
        {
            get
            {
                return (ProcessState == 0);
            }
        }

        [NotMapped]
        public bool IsComplate
        {
            get
            {
                return (ProcessState == 1);
            }
        }

        [NotMapped]
        public bool IsEmpty
        {
            get
            {
                return (ProcessState == 2);
            }
        }

        #endregion

        public string DisplayName
        {
            get { return string.Format("{0} - {1:00}  {2}", PeriodYear, PeriodMonth, (Employee!=null) ? Employee.DisplayName : ""    ); }
        }
    }
}
