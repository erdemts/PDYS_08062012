using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class Transport : Author, IDataItem
    {
        public Transport()
        {
            TransportEmployees = new HashSet<Employee>(); 
        }

        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxLength(50)]
        public string VhicleInformation { get; set; }
        [MaxLength(25)]
        public string NumberPlate { get; set; }
        [MaxLength(25)]
        public string DriverName { get; set; }
        [MaxLength(25)]
        public string DriverPhone { get; set; }

        // Navigation property
        public virtual ICollection<Employee> TransportEmployees { get; private set; }

        public string DisplayName
        {
            get { return Name; }
        }
    }
}
