using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PDYS.InfraStructure;
using PDYS.Interfaces;

namespace PDYS.Models
{
    public enum ParameterNames { Gender, MaritalStatus }

    public class Parameter : IDataItem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Text { get; set; }
        public int Value { get; set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0}", this.Text);
            }
        }

        //public override string ToString()
        //{
        //    return (string.IsNullOrEmpty(Text)) ? string.Empty : this.Text;
        //}
    }
}
