using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PDYS.InfraStructure;
using PDYS.Interfaces;

namespace PDYS.Models
{
    public class Employee : Author, IDataItem
    {
        public Employee()
        {
            this.EmployeeHolidays = new HashSet<EmployeeHoliday>();
        }

        [Key]
        public int ID { get; set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        

        [MaxLength(50)]
        public string JopTitle { get; set; } //Eklenmedi
        // Foreign key
        public int? DepartmentID { get; set; } //Eklenmedi
        // Navigation property
        public virtual Department Department { get; set; }//Eklenmedi
        [MaxLength(15)]
        public string CompanyRegisterNo { get; set; }
        [MaxLength(25)]
        public string AccessCardNo { get; set; }
        [MaxLength(10)]
        public string AccessPassword { get; set; }
        public bool IsSyncDevice { get; set; }


        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        

        [MaxLength(11)]
        public string GovernmentNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Gender { get; set; } 
        public int? MaritalStatus { get; set; } 
        [MaxLength(50)]
        public string FatherName { get; set; }
        [MaxLength(50)]
        public string MotherName { get; set; }
        // Foreign key
        public int? RegistryCityID { get; set; }
        // Navigation property
        public virtual City RegistryCity { get; set; }
        // Foreign key
        public int? RegistryCountyID { get; set; }
        // Navigation property
        public virtual County RegistryCounty { get; set; }

        
        public decimal? Salary { get; set; }
        public DateTime? WorkingStartDate { get; set; }
        public DateTime? WorkingEndDate { get; set; }
        // Foreign key
        public int? ManagerID { get; set; }
        // Navigation property
        public virtual Employee Manager { get; set; }
        
        
        public string Address { get; set; }
        // Foreign key
        public int? AddressCityID { get; set; }
        // Navigation property
        public virtual City AddressCity { get; set; }
        // Foreign key
        public int? AddressCountyID { get; set; }
        // Navigation property
        public virtual County AddressCounty { get; set; }

        [MaxLength(25)]
        public string Email { get; set; }
        [MaxLength(25)]
        public string HomePhone { get; set; }
        [MaxLength(25)]
        public string MobilePhone { get; set; }
        [MaxLength(50)]
        public string ContactName { get; set; }
        [MaxLength(25)]
        public string ContactPhone { get; set; }

        // Foreign key
        public int? TransportID { get; set; }
        // Navigation property
        public virtual Transport Transport { get; set; }


        // Navigation property
        public virtual ICollection<EmployeeHoliday> EmployeeHolidays { get; private set; }

        // Navigation property
        public virtual ICollection<EmployeeFingerPrint> FingerPrints { get; private set; }


        

       
    }
}
