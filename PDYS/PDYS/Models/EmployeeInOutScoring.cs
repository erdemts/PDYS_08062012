using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class EmployeeInOutScoring : Author, IDataItem
    {

        public EmployeeInOutScoring()
        {
            this.EmployeeInputOutputs = new HashSet<EmployeeInputOutput>();

            this.StartTime = new WorkingTime();
            this.LunchOut = new WorkingTime();
            this.LunchIn = new WorkingTime();
            this.EndTime = new WorkingTime();
        }

        [Key]
        public int ID { get; set; }

        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int? WeeklyOvertimeID { get; set; }
        public virtual WeeklyOvertime WeeklyOvertime { get; set; }

        public int? OutSourceOvertimeID { get; set; }
        public virtual OutSourceOvertime OutSourceOvertime { get; set; }

        public bool IsManualEdit { get; set; }

        public DateTime ScoringDate { get; set; }

        public WorkingTime StartTime { get; set; }
        public WorkingTime LunchOut { get; set; }
        public WorkingTime LunchIn { get; set; }
        public WorkingTime EndTime { get; set; }

       
        public double WorkRegularTime { get; set; }
        public double WorkTime { get; set; }
        public double LessTime { get; set; }
        public double OverTime { get; set; }

        public bool IsHoliday { get; set; }
        public bool IsNotPaymentHoliday { get; set; }
        public bool IsPublicHoliday { get; set; }

        [NotMapped]
        public bool IsAnyHoliday
        {
            get
            {
                return (IsHoliday || IsNotPaymentHoliday || IsPublicHoliday);
            }
        }

        public int ProcessState { get; set; }
        public string ProcessMessage { get; set; }

        public string Description { get; set; }


        #region SalaryScoring

        public decimal DailyPayment { get; set; }
        public decimal DailyDeduction { get; set; }
        public decimal DailyExtPayment { get; set; }
        public decimal DailyNetPayment { get; set; }

        public int? EmployeeSalaryScoringID { get; set; }
        public virtual EmployeeSalaryScoring EmployeeSalaryScoring { get; set; }
        [NotMapped]
        public bool IsSalaryScoring
        {
            get
            {
                return (EmployeeSalaryScoring != null);
            }
        }

        #endregion

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

        // Navigation property
        public virtual ICollection<EmployeeInputOutput> EmployeeInputOutputs { get; private set; }

        public string DisplayName
        {
            get { return this.ScoringDate.ToString(); }
        }
    }
}
