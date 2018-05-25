using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class EmployeeFingerPrint
    {
        [Key]
        public int ID { get; set; }
        public int FingerIndex { get; set; }
        public string TemplateData { get; set; }
        public int Flag { get; set; }

        // Foreign key
        public int? EmployeeID { get; set; }
        // Navigation property
        public virtual Employee Employee { get; set; }
    }
}
