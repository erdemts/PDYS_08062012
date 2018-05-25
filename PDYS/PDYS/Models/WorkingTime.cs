using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    [ComplexType]
    public class WorkingTime
    {
        public WorkingTime()
        {
            this.IsValid = true;
        }

        public static WorkingTime Create(DateTime usertime, DateTime? regulartime, bool IsInput)
        {
            WorkingTime workingtime = new WorkingTime();
            workingtime.Time = usertime;
            if (regulartime.HasValue)
            {
                if (IsInput)
                    workingtime.Difference = (regulartime.Value - usertime).TotalMinutes;
                else
                    workingtime.Difference = (usertime - regulartime.Value).TotalMinutes;

                workingtime.IsValid = (workingtime.Difference == 0 || workingtime.Difference > 0);
            }

            return workingtime;
        }

        public static WorkingTime GetEmptyWorking()
        {
            WorkingTime emptyWorking = new WorkingTime();
            emptyWorking.Difference = 0;
            emptyWorking.IsValid = false;
            emptyWorking.Time = null;

            return emptyWorking;
        }

        public DateTime? Time { get; set; }
        public double Difference { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Time, this.Difference);
        }

        public override bool Equals(object obj)
        {
            if (obj!=null && obj is WorkingTime)
            {
                WorkingTime temp = (WorkingTime)obj;
                if (object.Equals(this.Time, temp.Time)
                    && object.Equals(this.IsValid, temp.IsValid)
                    && object.Equals(this.Difference, temp.Difference))
                    return true;
                else
                    return false;
            }
            else 
                return false;
        }

    }
}
