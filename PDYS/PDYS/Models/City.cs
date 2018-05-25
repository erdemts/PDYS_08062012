using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PDYS.Interfaces;

namespace PDYS.Models
{
    public class City : Author, IDataItem
    {
        public City()
        {
            this.Counties = new HashSet<County>();
        }

        [Key]
        public int ID { get; set; }
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        
        // Navigation properties
        public virtual ICollection<County> Counties { get; private set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0}", this.Name);
            }
        }

        //public override string ToString()
        //{
        //    return (string.IsNullOrEmpty(Name)) ? string.Empty : this.Name;
        //}
    }
}
