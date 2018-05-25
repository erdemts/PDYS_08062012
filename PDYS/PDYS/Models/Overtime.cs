using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    [ComplexType]
    public class Overtime
    {

        public int DayNumber { get; set; }

        public DayOfWeek Day
        {
            get { return (DayOfWeek)DayNumber; }
            set { DayNumber = (int)value; }
        }

        public long Start { get; set; }
        public long LunchOut { get; set; }
        public long LunchIn { get; set; }
        public long End { get; set; }
        //public int OffTime { get; set; }
        public bool IsHoliday { get; set; }
    }
}
