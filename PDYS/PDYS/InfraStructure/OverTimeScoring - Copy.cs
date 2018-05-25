using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.Models;

namespace PDYS.InfraStructure
{
    public class OverTimeScoring
    {
        public OverTimeScoring(Employee employee, DateTime processdate)
        {
            this.ProcessedEmployee = employee;
            this.ProcessedDate = processdate;

            this.ListInOutActions = new List<InOutAction>();

            this.EmployeeStartTime = WorkingTime.GetEmptyWorking();
            this.EmployeeLunchOut = WorkingTime.GetEmptyWorking();
            this.EmployeeLunchIn = WorkingTime.GetEmptyWorking();
            this.EmployeeEndTime = WorkingTime.GetEmptyWorking();

            this.WorkTotalTime = this.GetEmptyTime();
            this.WorkLessTime = this.GetEmptyTime();

            this.WorkRegularTime = this.GetEmptyTime();
            this.WorkNetTime = this.GetEmptyTime();
            this.WorkDifference = this.GetEmptyTime();


        }

        TimeSpan GetEmptyTime()
        {
            return new TimeSpan(0);
        }

        //WorkingTime GetEmptyWorking()
        //{
        //    WorkingTime emptyWorking = new WorkingTime();
        //    emptyWorking.Difference = 0;
        //    emptyWorking.IsValid = false;
        //    emptyWorking.Time = null;

        //    return emptyWorking;
        //}



        public Employee ProcessedEmployee { get; private set; }
        public DateTime ProcessedDate { get; private set; }

        public virtual WeeklyOvertime WeeklyOvertime { get; set; }
        public virtual OutSourceOvertime OutSourceOvertime { get; set; }

        public int State { get; private set; }
        public string StateMessage { get; private set; }

        public bool IsWorkingHoliday { get; private set; }
        public bool IsEmployeeHoliday { get; private set; }
        public bool IsEmployeeNotPaymentHoliday { get; private set; }
        public bool IsPublicHoliday { get; private set; }

        public WorkingTime EmployeeStartTime { get; set; }
        public WorkingTime EmployeeLunchOut { get; set; }
        public WorkingTime EmployeeLunchIn { get; set; }
        public WorkingTime EmployeeEndTime { get; set; }

        public TimeSpan WorkRegularTime { get; set; }

        public TimeSpan WorkTotalTime { get; private set; }
        public TimeSpan WorkLessTime { get; private set; }
        //public TimeSpan WorkExtOverTime { get; private set; }
        public TimeSpan WorkNetTime { get; private set; }
        public TimeSpan WorkDifference { get; private set; }



        public List<InOutAction> ListInOutActions { get; private set; }



        public void Calculate()
        {
            OvertimeAssignment overtimeassignment = PDYSEntities.DataContext.OvertimeAssignmentSet.FirstOrDefault(item => item.StartDate <= this.ProcessedDate && this.ProcessedDate <= item.EndDate);

            if (overtimeassignment == null || (overtimeassignment.WeeklyOvertime == null && overtimeassignment.OutSourceOvertime == null))
            {
                error("Uygun vardia tanimi bulunamadi yada vardia tanimi hatali");
                return;
            }

            #region Read Input Output List

            bool isNotValidInOutActions = false;

            ReadInputOutputList(overtimeassignment, ref isNotValidInOutActions);

            if (isNotValidInOutActions)
            {
                error("Hatali giris çikis bilgisi. Giris ve Çikis hareketleri ardisik olamalidir.");
                return;
            }

            #endregion

            if (overtimeassignment.WeeklyOvertime != null)
            {
                #region Weekly Overtime

                CheckHoliday();

                this.WeeklyOvertime = overtimeassignment.WeeklyOvertime;

                Overtime regularOverTime = GetDailyOverTime(overtimeassignment.WeeklyOvertime);

                this.IsWorkingHoliday = regularOverTime.IsHoliday;


                if (this.IsWorkingHoliday || IsEmployeeHoliday || IsPublicHoliday)
                {
                    if (this.ListInOutActions.Any())
                    {
                        this.WorkTotalTime = CalcTime(DateTime.MinValue, DateTime.MaxValue);
                    }

                    this.WorkNetTime = this.WorkTotalTime;

                    complate();
                    return;
                }


                if (!ListInOutActions.Any())
                {
                    empty();
                    return;
                }

                #region Regular Time

                DateTime reqularStartTime = this.ProcessedDate.AddTicks(regularOverTime.Start);
                DateTime reqularLunchOut = this.ProcessedDate.AddTicks(regularOverTime.LunchOut);
                DateTime reqularLunchIn = this.ProcessedDate.AddTicks(regularOverTime.LunchIn);
                DateTime reqularEnd = this.ProcessedDate.AddTicks(regularOverTime.End);

                this.WorkRegularTime = (reqularLunchOut - reqularStartTime) + (reqularEnd - reqularLunchIn);

                #endregion

                #region Start

                this.EmployeeStartTime = FindInput(reqularStartTime, DateTime.MinValue);

                if (this.EmployeeStartTime == null)
                {
                    error("Giris kaydi bulunamadi.");
                    return;
                }

                #endregion

                #region Lunch

                this.EmployeeLunchOut = FindOutput(reqularLunchOut, this.EmployeeStartTime.Time.Value);
                this.EmployeeLunchIn = null;

                if (this.EmployeeLunchOut != null)
                {
                    this.EmployeeLunchIn = FindInput(reqularLunchIn, this.EmployeeLunchOut.Time.Value);

                    if (this.EmployeeLunchIn == null)
                        this.EmployeeLunchOut = null;
                }

                #endregion

                #region End

                DateTime minTimeforEnd = (this.EmployeeLunchIn.Time.HasValue) ? this.EmployeeLunchIn.Time.Value : this.EmployeeStartTime.Time.Value;

                this.EmployeeEndTime = FindOutput(reqularEnd, minTimeforEnd);

                if (this.EmployeeEndTime == null)
                {
                    error("Çikis kaydi bulunamadi.");
                    return;
                }

                #endregion


                #region Mesai Saati Sureleri

                AutoSetWorkTotalTime();

                #endregion

                complate();

                #endregion
            }
            else if (overtimeassignment.OutSourceOvertime != null)
            {
                #region Outsource

                this.IsWorkingHoliday = false;
                this.IsEmployeeHoliday = false;
                this.IsEmployeeNotPaymentHoliday = false;
                this.IsPublicHoliday = false;

                this.OutSourceOvertime = overtimeassignment.OutSourceOvertime;

                if (ListInOutActions.Any())
                {
                    this.WorkTotalTime = CalcTime(DateTime.MinValue, DateTime.MaxValue);
                    this.WorkNetTime = this.WorkTotalTime;

                    if (this.OutSourceOvertime.MaximumCharge != 0)
                    {
                        TimeSpan maxTime = new TimeSpan(this.OutSourceOvertime.MaximumCharge);
                        if (maxTime < this.WorkTotalTime)
                            this.WorkTotalTime = maxTime;
                    }

                    complate();
                }
                else
                {
                    empty();
                }

                #endregion
            }


        }

        public void AutoSetWorkTotalTime()
        {
            WorkingTime empty = WorkingTime.GetEmptyWorking();

            if (!this.EmployeeStartTime.Equals(empty)
                && !this.EmployeeLunchOut.Equals(empty)
                && !this.EmployeeLunchIn.Equals(empty)
                && !this.EmployeeEndTime.Equals(empty))
            {
                this.WorkTotalTime = (this.EmployeeLunchOut.Time.Value - this.EmployeeStartTime.Time.Value) + (this.EmployeeEndTime.Time.Value - this.EmployeeLunchIn.Time.Value);

                this.WorkLessTime += CalcTime(this.EmployeeStartTime.Time.Value, this.EmployeeLunchOut.Time.Value);
                this.WorkLessTime += CalcTime(this.EmployeeLunchIn.Time.Value, this.EmployeeEndTime.Time.Value);
            }
            else if (!this.EmployeeStartTime.Equals(empty)
                && !this.EmployeeEndTime.Equals(empty))
            {
                this.WorkTotalTime = (this.EmployeeEndTime.Time.Value - this.EmployeeStartTime.Time.Value);
                this.WorkLessTime += CalcTime(this.EmployeeStartTime.Time.Value, this.EmployeeEndTime.Time.Value);
            }
            else
            {
                this.WorkTotalTime = GetEmptyTime();
                this.WorkNetTime = GetEmptyTime();
                this.WorkDifference = GetEmptyTime();
                this.WorkLessTime = GetEmptyTime();
                return;
            }

            TimeSpan workExtOverTime = CalcTime(this.EmployeeEndTime.Time.Value, DateTime.MaxValue);
            this.WorkTotalTime = this.WorkTotalTime.Add(workExtOverTime);


            this.WorkNetTime = this.WorkTotalTime + this.WorkLessTime;
            this.WorkDifference = this.WorkNetTime - this.WorkRegularTime;
        }



        private void error(string message)
        {
            this.State = 0;
            this.StateMessage = message;
        }

        private void complate()
        {
            this.State = 1;
            this.StateMessage = "Islem Tamamlandi.";
        }

        private void empty()
        {
            this.State = 2;
            this.StateMessage = "Giris & Çikis kaydi bulunamadi.";
        }


        #region Calc Method

        void CheckHoliday()
        {
            #region Check Holiday


            this.IsPublicHoliday = PDYSEntities.DataContext.PublicHolidaySet.Any(item => item.StartDate <= this.ProcessedDate && this.ProcessedDate < item.EndDate);
            var holiday = PDYSEntities.DataContext.EmployeeHolidaySet.FirstOrDefault(item => item.EmployeeID == this.ProcessedEmployee.ID && item.StartDate <= this.ProcessedDate && this.ProcessedDate < item.EndDate);
            if (!this.IsPublicHoliday && holiday != null)
            {
                if (holiday.IsNotPayment)
                    this.IsEmployeeNotPaymentHoliday = true;
                else
                    this.IsEmployeeHoliday = true;
            }


            #endregion
        }

        TimeSpan CalcTime(DateTime MinTime, DateTime MaxTime)
        {

            DateTime lastInputMinTime = MinTime;
            DateTime lastOutputMinTime = MinTime;

            TimeSpan lessTimeTotal = new TimeSpan(0);

            while (true)
            {
                var lessinputtime = this.ListInOutActions.FirstOrDefault(io => io.InOutType == 1 && lastInputMinTime < io.InOutDate && io.InOutDate < MaxTime);
                var lessoutputtime = this.ListInOutActions.FirstOrDefault(io => io.InOutType == 2 && lastOutputMinTime < io.InOutDate && io.InOutDate < MaxTime);

                if (lessinputtime != null && lessoutputtime != null)
                {
                    lastInputMinTime = lessinputtime.InOutDate;
                    lastOutputMinTime = lessoutputtime.InOutDate;

                    lessTimeTotal += lastOutputMinTime - lastInputMinTime;
                }
                else
                    break;
            }

            return lessTimeTotal;
        }

        WorkingTime FindOutput(DateTime regularTime, DateTime MinTime)
        {
            var outputList = this.ListInOutActions.Where(io => io.InOutType == 2 && io.InOutDate > MinTime).OrderByDescending(item => item.InOutDate);

            long lastdifference = long.MaxValue;

            WorkingTime workingtime = WorkingTime.GetEmptyWorking();

            foreach (var useroutput in outputList)
            {
                long difference = regularTime.Ticks - useroutput.InOutDate.Ticks;

                if (difference < 0)
                    difference = difference * -1;

                if (lastdifference > difference)
                {
                    lastdifference = difference;

                    workingtime = WorkingTime.Create(useroutput.InOutDate, regularTime, false);
                    //workingtime = new WorkingTime();
                    //workingtime.Time = output.InOutDate;
                    //workingtime.Difference = (output.InOutDate - processTime).TotalMinutes;
                    //workingtime.IsValid = (workingtime.Difference == 0 || workingtime.Difference > 0);
                }
            }

            return workingtime;
        }

        WorkingTime FindInput(DateTime regularTime, DateTime MinTime)
        {
            DateTime lastinput = MinTime;

            while (true)
            {
                var inputaction = this.ListInOutActions.Where(io => io.InOutDate > lastinput && io.InOutType == 1).FirstOrDefault();
                if (inputaction == null)
                    return WorkingTime.GetEmptyWorking();

                DateTime userinput = inputaction.InOutDate;
                lastinput = userinput;

                var outputaction = this.ListInOutActions.Where(io => io.InOutDate > userinput && io.InOutType == 2).FirstOrDefault();
                if (outputaction == null)
                    return WorkingTime.GetEmptyWorking();

                DateTime output = outputaction.InOutDate;

                //Bulunan saatin çikisi vardiya taniminda ki MG den büyükse bu saat O günün MG saati olarak belirlenir.
                if (regularTime < output)
                {

                    WorkingTime workingtime = WorkingTime.Create(userinput, regularTime, true);

                    //WorkingTime workingtime = new WorkingTime();
                    //workingtime.Time = input;
                    //workingtime.Difference = (processTime - input).TotalMinutes;
                    //workingtime.IsValid = (workingtime.Difference == 0 || workingtime.Difference > 0);

                    return workingtime;
                }
            }

        }

        public Overtime GetDailyOverTime(WeeklyOvertime weeklyovertime)
        {
            Overtime processOverTime = null;

            #region  Select Process Day Overtime
            switch (this.ProcessedDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    processOverTime = weeklyovertime.Friday;
                    break;

                case DayOfWeek.Monday:
                    processOverTime = weeklyovertime.Monday;
                    break;

                case DayOfWeek.Saturday:
                    processOverTime = weeklyovertime.Saturday;
                    break;

                case DayOfWeek.Sunday:
                    processOverTime = weeklyovertime.Sunday;
                    break;

                case DayOfWeek.Thursday:
                    processOverTime = weeklyovertime.Thursday;
                    break;

                case DayOfWeek.Tuesday:
                    processOverTime = weeklyovertime.Tuesday;
                    break;

                case DayOfWeek.Wednesday:
                    processOverTime = weeklyovertime.Wednesday;
                    break;
                default:
                    break;
            }

            #endregion

            return processOverTime;
        }

        void ReadInputOutputList(OvertimeAssignment overtimeassignment, ref bool isNotValidInOutActions)
        {

            DateTime dailystart = this.ProcessedDate;

            if (overtimeassignment.WeeklyOvertime != null)
                dailystart = this.ProcessedDate.AddTicks(overtimeassignment.WeeklyOvertime.DailyCheckPoint);

            else if (overtimeassignment.OutSourceOvertime != null)
                dailystart = this.ProcessedDate.AddTicks(overtimeassignment.OutSourceOvertime.DailyCheckPoint);

            DateTime dailyend = dailystart.AddDays(1);

            var inputoutputQuery = from io in PDYSEntities.DataContext.EmployeeInputOutputSet
                                   where dailystart <= io.InOutDate && io.InOutDate < dailyend && !io.IsProcessed
                                   orderby io.InOutDate
                                   select new InOutAction()
                                   {
                                       RefID = io.ID,
                                       InOutDate = io.InOutDate,
                                       InOutType = (io.InOutType.HasValue) ? io.InOutType.Value : 0,
                                   };


            List<InOutAction> inputoutList = inputoutputQuery.ToList();

            #region Giris & Çikis Bilgileri Düzenleniyor

            // 0:Giris & Çikis, 1:Giris, 2:Çikis
            for (int index = 0; index < inputoutList.Count; index++)
            {
                InOutAction itemIO = inputoutList[index];

                InOutAction previousitem = null;
                InOutAction nextitem = null;

                if ((index - 1) >= 0)
                    previousitem = inputoutList[index - 1];

                if ((index + 1) < inputoutList.Count)
                    nextitem = inputoutList[index + 1];


                if (itemIO.InOutType != 0) //0:Giris & Çikis Degilse
                    itemIO.InOutType = itemIO.InOutType;
                else
                {
                    #region Giris & Çikis Oto. Set Ediliyor
                    //Bir önceki yada sonraki hareket NULL ise bu hareket Giris hareketidir.
                    if (previousitem == null && nextitem == null)
                    {
                        itemIO.InOutType = 1; //Giris
                    }
                    //Bir önceki hareket "Null" degil ise zittini uygula
                    if (previousitem != null)
                    {
                        itemIO.InOutType = reverseInOut(previousitem.InOutType);
                    }
                    //Bir önceki hareket "Null" ise bir sonraki harekete bak "Giris & Çikis" ise bu hareket "Giris" hareketidir
                    else if (nextitem != null && nextitem.InOutType == 0)
                    {
                        itemIO.InOutType = 1; //Giris
                    }
                    //Bir önceki hareket "Null" ise bir sonraki hareketin tersini uygula
                    else if (nextitem != null)
                    {
                        itemIO.InOutType = reverseInOut(nextitem.InOutType);
                    }
                    #endregion
                }

                if (nextitem != null && itemIO.InOutType == nextitem.InOutType)
                {
                    isNotValidInOutActions = true;
                    break;
                }

            }

            #endregion

            this.ListInOutActions.AddRange(inputoutList);
        }

        int reverseInOut(int? state)
        {
            if (!state.HasValue)
                return 0;
            else if (state == 1)
                return 2; //Çkis
            else if (state == 2)
                return 1; //Giris
            else
                return 0;
        }

        #endregion



        public class InOutAction
        {
            public int RefID { get; set; }
            public int InOutType { get; set; }
            public DateTime InOutDate { get; set; }
            public bool IsProcessed { get; set; }

            public override string ToString()
            {
                return string.Format("{0} : {1}", this.InOutType, this.InOutDate);
            }
        }

    }
}
