using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PDYS.Interfaces;

namespace PDYS.Models
{
    public class User: IDataItem
    {
        public const string SystemUser = "System";
        public const string AdminUser = "Admin";

        [Key]
        public int ID {get;set;}
        [MaxLength(25)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(25)]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsLogon { get; set; }

        public string DisplayName
        {
            get
            {
                return string.Format("{0}", this.FullName);
            }
        }
    }
}
