using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{

    
    public class EmployeeInputOutput : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }
        
        public DateTime InOutDate { get; set; }
        public int? InOutType { get; set; }
        public bool IsProcessed { get; set; }

        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int? ReaderDeviceID { get; set; }
        public virtual ReaderDevice ReaderDevice { get; set; }

        public int? EmployeeInOutScoringID { get; set; }
        public virtual EmployeeInOutScoring EmployeeInOutScoring { get; set; }

        public string DisplayName
        {
            get { return InOutDate.ToLongDateString(); }
        }
    }
}
