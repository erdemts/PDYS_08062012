using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using PDYS.Interfaces;

namespace PDYS.Models
{
    public class Department : Author, IDataItem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name {get;set;}
        
        
        // Foreign key
        public int? ParentDepartmentID { get; set; }
        // Navigation property
        public virtual Department ParentDepartment { get; set; }

        public virtual  ICollection<Department> ChildDepartments { get; private set; }

        public Department()
        {
            this.ChildDepartments = new HashSet<Department>();
        }

        public string DisplayName
        {
            get
            {
                return string.Format("{0}", this.Name);
            }
        }

    }
}
