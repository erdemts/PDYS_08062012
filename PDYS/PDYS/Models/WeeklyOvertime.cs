using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PDYS.Models
{
    public class WeeklyOvertime : Author, IDataItem
    {
        public WeeklyOvertime()
        {
            this.Monday = new Overtime() { Day = DayOfWeek.Monday };
            this.Tuesday = new Overtime() { Day = DayOfWeek.Tuesday };
            this.Wednesday = new Overtime() { Day = DayOfWeek.Wednesday };
            this.Thursday = new Overtime() { Day = DayOfWeek.Thursday };
            this.Friday = new Overtime() { Day = DayOfWeek.Friday };
            this.Saturday = new Overtime() { Day = DayOfWeek.Saturday };
            this.Sunday = new Overtime() { Day = DayOfWeek.Sunday };
        }

        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        public long RegularHrs { get; set; }
        public decimal OvertimeFactor { get; set; }
        public decimal MissingFactor { get; set; }
        public decimal PubHolidayFactor { get; set; }
        public decimal HolidayFactor { get; set; }
        public long DefenceDuration { get; set; }
        public long DailyCheckPoint { get; set; }

        public Overtime Monday { get; set; }
        public Overtime Tuesday { get; set; }
        public Overtime Wednesday { get; set; }
        public Overtime Thursday { get; set; }
        public Overtime Friday { get; set; }
        public Overtime Saturday { get; set; }
        public Overtime Sunday { get; set; }


        public string DisplayName
        {
            get { return this.Name; }
        }
        
    }

    
}
