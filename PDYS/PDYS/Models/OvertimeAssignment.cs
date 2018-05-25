using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class OvertimeAssignment : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }

        // Navigation property
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        // Navigation property
        public int? WeeklyOvertimeID { get; set; }
        public virtual WeeklyOvertime WeeklyOvertime { get; set; }

        // Navigation property
        public int? OutSourceOvertimeID { get; set; }
        public virtual OutSourceOvertime OutSourceOvertime { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0}", this.Employee); }
        }

    }
}
