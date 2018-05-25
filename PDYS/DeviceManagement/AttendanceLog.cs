using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManagement
{
    

    public class AttendanceLog
    {
        public AttendanceLog()
        {
            VerifyMode = AttendanceVerifyMode.none;
        }

        public int UserID { get; set; }
        public AttendanceVerifyMode VerifyMode { get; set; }
        public AttendanceInOutMode InOutMode { get; set; }
        public DateTime Date { get; set; }
    }
}
