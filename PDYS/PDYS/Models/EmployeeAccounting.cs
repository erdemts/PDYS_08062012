using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{


    public class EmployeeAccounting : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Subject { get; set; }
        
        public DateTime ProcessDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Description { get; set; }


        public int? AccountingDefinationID { get; set; }
        public virtual AccountingProcessType AccountingDefination { get; set; }
        
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int? EmployeeSalaryScoringID { get; set; }
        public virtual EmployeeSalaryScoring EmployeeSalaryScoring { get; set; }

        public string DisplayName
        {
            get { return Subject; }
        }
    }
}
