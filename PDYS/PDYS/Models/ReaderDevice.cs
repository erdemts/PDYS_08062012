using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class ReaderDevice : Author,IDataItem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string SerialNumber { get; set; }
        public int? InputOutputType { get; set; }
        [MaxLength(15)]
        public string IP { get; set; }
        public int? Port { get; set; }
        public int? ComKey { get; set; }
        

        public string Description { get; set; }

        public string DisplayName
        {
            get { return Name; }
        }
    }
}
