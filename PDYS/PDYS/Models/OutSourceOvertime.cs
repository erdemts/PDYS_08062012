using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class OutSourceOvertime : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0}", this.Name);
            }
        }

        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal HourlyPayment { get; set; }
        public long MaximumCharge { get; set; }
        public long DailyCheckPoint { get; set; }
    }
}
