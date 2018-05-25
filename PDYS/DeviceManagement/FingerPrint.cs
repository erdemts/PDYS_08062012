using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManagement
{
    public class FingerPrint
    {
        public FingerPrint()
        {
            this.Flag = FingerPrintFlag.Valid;
        }
        public int UserID { get; set; }
        public int FingerIndex { get; set; }
        public FingerPrintFlag Flag { get; set; }
        public string TemplateData { get; set; }
    }
}
