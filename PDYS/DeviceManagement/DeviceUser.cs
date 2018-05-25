using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManagement
{

    
    public class DeviceUser
    {
        public DeviceUser()
        {
            this.Privilege = UserPrivilege.commonuser;
            this.Enabled = true;
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public UserPrivilege Privilege { get; set; }
        public bool Enabled { get; set; }
    }
}
