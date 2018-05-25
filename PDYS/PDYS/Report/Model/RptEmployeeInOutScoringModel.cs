using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;

namespace PDYS.Report.Model
{
    public class RptEmployeeInOutScoringModel 
    {
        public string EmployeeName { get; set; }
        public DateTime ScoringDate { get; set; }
        
        public WorkingTime StartTime { get; set; }
        public WorkingTime LunchOut { get; set; }
        public WorkingTime LunchIn { get; set; }
        public WorkingTime EndTime { get; set; }
        public double WorkTime { get; set; }
        public double LessTime { get; set; }
        public double OverTime { get; set; }

        public string FormatedWorkTime { get; set; }
        public string FormatedLessTime { get; set; }
        public string FormatedOverTime { get; set; }



        public int ProcessState { get; set; }
        public string FormatedProcessState { get; set; }
        public string ProcessMessage { get; set; }

    }
}
