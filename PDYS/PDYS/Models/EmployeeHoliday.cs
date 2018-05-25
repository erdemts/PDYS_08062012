using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class EmployeeHoliday : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsNotPayment { get; set; }
        // Foreign key
        public int? EmployeeID { get; set; }
        // Navigation property
        public virtual Employee Employee { get; set; }

        public int? Type { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0:dd.MM.yyyy} - {1:dd.MM.yyyy} {2}", this.StartDate, this.EndDate, this.Employee); }
        }
    }
}
