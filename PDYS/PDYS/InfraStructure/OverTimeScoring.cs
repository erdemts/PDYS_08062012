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

            this.WorkRegularTime = this.GetEmptyTime();
            this.WorkTime = this.GetEmptyTime();

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

        public TimeSpan WorkTime { get; private set; }
        public TimeSpan OverTime { get; private set; }
        public TimeSpan LessTime { get; private set; }

        public List<InOutAction> ListInOutActions { get; private set; }



        public void Calculate()
        {
            OvertimeAssignment overtimeassignment = PDYSEntities.DataContext.OvertimeAssignmentSet.FirstOrDefault(item => item.StartDate <= this.ProcessedDate && this.ProcessedDate <= item.EndDate);

            if (overtimeassignment == null || (overtimeassignment.WeeklyOvertime == null && overtimeassignment.OutSourceOvertime == null))
            {
                ErrorState("Uygun vardia tanimi bulunamadi yada vardia tanimi hatali");
                return;
            }

            if (overtimeassignment.WeeklyOvertime != null)
                this.WeeklyOvertime = overtimeassignment.WeeklyOvertime;
            else if (overtimeassignment.OutSourceOvertime != null)
                this.OutSourceOvertime = overtimeassignment.OutSourceOvertime;

            #region Read Input Output List

            bool isNotValidInOutActions = false;

            ReadInputOutputList(overtimeassignment, ref isNotValidInOutActions);

            if (isNotValidInOutActions)
            {
                ErrorState("Hatali giris çikis bilgisi. Giris ve Çikis hareketleri ardisik olamalidir.");
                return;
            }

            #endregion

            if (overtimeassignment.WeeklyOvertime != null)
            {
                #region Weekly Overtime

                CheckHoliday();

                Overtime regularOverTime = GetDailyOverTime(overtimeassignment.WeeklyOvertime);

                this.IsWorkingHoliday = regularOverTime.IsHoliday;


                if (this.IsWorkingHoliday || IsEmployeeHoliday || IsPublicHoliday)
                {
                    if (this.ListInOutActions.Any())
                    {
                        this.WorkTime = CalcTime(DateTime.MinValue, DateTime.MaxValue);
                    }

                    ComplateState();
                    return;
                }


                if (!ListInOutActions.Any())
                {
                    EmptyState();
                    return;
                }

                #region Regular Time

                SetRegularTime(regularOverTime);

                #endregion

                WorkingTime emptyTime = WorkingTime.GetEmptyWorking();

                #region Start

                this.EmployeeStartTime = FindInput(reqularStartTime, DateTime.MinValue);

                if (this.EmployeeStartTime.Equals(emptyTime))
                {
                    EmptyState();
                    return;
                }

                #endregion

                #region Lunch

                this.EmployeeLunchOut = FindOutput(reqularLunchOut, this.EmployeeStartTime.Time.Value);
                this.EmployeeLunchIn = WorkingTime.GetEmptyWorking();

                if (!this.EmployeeLunchOut.Equals(emptyTime))
                {
                    this.EmployeeLunchIn = FindInput(reqularLunchIn, this.EmployeeLunchOut.Time.Value);

                    if (this.EmployeeLunchIn.Equals(emptyTime))
                        this.EmployeeLunchOut = WorkingTime.GetEmptyWorking();
                }

                #endregion

                #region End

                DateTime minTimeforEnd = (this.EmployeeLunchIn.Time.HasValue) ? this.EmployeeLunchIn.Time.Value : reqularLunchIn;

                this.EmployeeEndTime = FindOutput(reqularEnd, minTimeforEnd);

                if (this.EmployeeEndTime.Equals(emptyTime))
                {
                    EmptyState();
                    return;
                }

                #endregion


                #region Mesai Saati Sureleri

                SetWorkTotalTime();

                #endregion



                ComplateState();

                #endregion
            }
            else if (overtimeassignment.OutSourceOvertime != null)
            {
                #region Outsource

                this.IsWorkingHoliday = false;
                this.IsEmployeeHoliday = false;
                this.IsEmployeeNotPaymentHoliday = false;
                this.IsPublicHoliday = false;



                if (ListInOutActions.Any())
                {
                    this.WorkTime = CalcTime(DateTime.MinValue, DateTime.MaxValue);

                    if (this.OutSourceOvertime.MaximumCharge != 0)
                    {
                        TimeSpan maxTime = new TimeSpan(this.OutSourceOvertime.MaximumCharge);
                        if (maxTime < this.WorkTime)
                            this.WorkTime = maxTime;
                    }

                    ComplateState();
                }
                else
                {
                    EmptyState();
                }

                #endregion
            }


        }

        public void SetRegularTime(Overtime regularOverTime)
        {
            reqularStartTime = this.ProcessedDate.AddTicks(regularOverTime.Start);
            reqularLunchOut = this.ProcessedDate.AddTicks(regularOverTime.LunchOut);
            reqularLunchIn = this.ProcessedDate.AddTicks(regularOverTime.LunchIn);
            reqularEnd = this.ProcessedDate.AddTicks(regularOverTime.End);

            this.WorkRegularTime = (reqularLunchOut - reqularStartTime) + (reqularEnd - reqularLunchIn);
        }

        public DateTime reqularStartTime = DateTime.MinValue;
        public DateTime reqularLunchOut = DateTime.MinValue;
        public DateTime reqularLunchIn = DateTime.MinValue;
        public DateTime reqularEnd = DateTime.MinValue;

        public void SetWorkTotalTime()
        {
            WorkingTime empty = WorkingTime.GetEmptyWorking();

            this.WorkTime = new TimeSpan();
            this.LessTime = new TimeSpan();
            this.OverTime = new TimeSpan();


            if (!this.EmployeeStartTime.Equals(empty) && !this.EmployeeEndTime.Equals(empty))
            {
                DateTime startTime = this.EmployeeStartTime.Time.Value;
                DateTime lunchOut = (!this.EmployeeLunchOut.Equals(empty)) ? this.EmployeeLunchOut.Time.Value : reqularLunchOut;
                DateTime lunchIn = (!this.EmployeeLunchIn.Equals(empty)) ? this.EmployeeLunchIn.Time.Value : reqularLunchIn;
                DateTime endTime = this.EmployeeEndTime.Time.Value;

                bool hasOverTime = (this.EmployeeEndTime.Time.Value > reqularEnd);



                this.WorkTime = (lunchOut - startTime) + (endTime - lunchIn);

                this.LessTime = (this.WorkTime < this.WorkRegularTime) ? (this.WorkRegularTime - this.WorkTime) : new TimeSpan();
                this.OverTime = (hasOverTime) ? endTime - reqularEnd : new TimeSpan();

                #region Gun Icindeki Eksik Calismalar
                
                TimeSpan extLessTime = CalcTime(startTime, lunchOut);
                extLessTime += CalcTime(lunchIn, endTime);

                if (extLessTime.Ticks!=0)
                    this.LessTime += new TimeSpan((-1 * extLessTime.Ticks));

                #endregion


                #region Calisma Saatinden sonraki Ekstra Mesai

                TimeSpan extOverTime = CalcTime(this.EmployeeEndTime.Time.Value, DateTime.MaxValue);
                this.OverTime += extOverTime;

                #endregion

                this.WorkTime += this.OverTime;
                this.WorkTime -= this.LessTime;
            }

        }



        private void ErrorState(string message)
        {
            this.State = 0;
            this.StateMessage = message;
        }

        private void ComplateState()
        {
            this.State = 1;
            this.StateMessage = "Islem Tamamlandi.";
        }

        private void EmptyState()
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

            TimeSpan timeTotal = new TimeSpan(0);

            if (this.ListInOutActions == null || !this.ListInOutActions.Any())
                return timeTotal;

            while (true)
            {
                var lessinputtime = this.ListInOutActions.FirstOrDefault(io => io.InOutType == 1 && lastInputMinTime < io.InOutDate && io.InOutDate < MaxTime);
                var lessoutputtime = this.ListInOutActions.FirstOrDefault(io => io.InOutType == 2 && lastOutputMinTime < io.InOutDate && io.InOutDate < MaxTime);

                if (lessinputtime != null && lessoutputtime != null)
                {
                    lastInputMinTime = lessinputtime.InOutDate;
                    lastOutputMinTime = lessoutputtime.InOutDate;

                    timeTotal += lastOutputMinTime - lastInputMinTime;
                }
                else
                    break;
            }

            return timeTotal;
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

                DateTime output = (outputaction == null) ? DateTime.MaxValue : outputaction.InOutDate;

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


            #region Giris & Cikis Sayisi Eslesiyor
            if (this.ProcessedDate != DateTime.Now.Date)
            {

                int inputCount = inputoutList.Where(item => item.InOutType == 1).Count();
                int outputCount = inputoutList.Where(item => item.InOutType == 2).Count();

                if (inputCount != outputCount)
                    isNotValidInOutActions = true;
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
