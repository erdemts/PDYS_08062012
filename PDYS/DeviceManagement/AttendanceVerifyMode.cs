using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManagement
{
    public enum AttendanceVerifyMode 
    { 
        none = -1,
        passwordverification = 0, 
        fingerprintverification = 1, 
        cardverification = 2 
    }
}
