using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class AccountingProcessType : IDataItem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public bool IsDebit { get; set; }
        public bool IsCredit { get; set; }
        
        public bool IsSystem { get; set; }

        public string DisplayName
        {
            get { return string.Format("[{0}] - {1}",this.Code,this.Name); }
        }
    }
}
