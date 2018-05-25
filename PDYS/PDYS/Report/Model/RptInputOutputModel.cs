using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDYS.Report.Model
{
    public class RptInputOutputModel
    {
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ActionType { get; set; }
        public string IsScoring { get; set; }
        public string Device { get; set; }
    }
}
