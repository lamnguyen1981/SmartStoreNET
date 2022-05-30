//================================================================================
// Copyright <Complete Coders>
// All Rights Reserved
// Created by	: Kevin
// Create Date	: 08/02/2016
// Description	: <Description of the file>
//================================================================================

namespace CC.Plugins.Core.Utilities
{
    
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class WeeksOfYearHelper
    {
        /// <summary>
        /// According to the business logic week enable equal current week add 2
        /// </summary>
        public const int AddWeekValue = 3;

        public const int GET_WEDNESDAY_BASE_ON_FRIDAY = -2;

        public const int LOCKOUT_DAYS_PLASTIC_CARDS = 14;//28;

        public const int LOCKOUT_DAYS_P2N = 14;

        public const int NEW_SALON_LOCKOUT_DAYS = 17;


        public const int NEW_SALON_LOCKOUT_DAYS_FIX = 42;
        public const int REOPEN_SALON_LOCKOUT_DAYS = 44;

        /// <summary>
        /// Get list weeks of the year
        /// </summary>
        /// <param name="YYYY">Year value</param>
        /// <returns></returns>
        public static IEnumerable<WeeksOfYear> GetWeeksOfYear(int YYYY)
        {
            List<WeeksOfYear> listWeeks = new List<WeeksOfYear>();
            if (YYYY >= DateTime.MinValue.Year && YYYY <= DateTime.MaxValue.Year)
            {
                int seedweek = 1;
                int numWeekOfYear = 53;
                int numDayOfWeek = 7;
                int startDayOfWeekIsFriday = 5;
                DateTime beginDayOfYear = new DateTime(YYYY, 1, 1);
                int seedday = startDayOfWeekIsFriday - (int)beginDayOfYear.DayOfWeek % numDayOfWeek;
                seedday = seedday < 0 ? (seedday + numDayOfWeek) : seedday;
                DateTime startDate = beginDayOfYear.AddDays(seedday);
                DateTime friDayDate = beginDayOfYear.AddDays(seedday);
                DateTime endDate = startDate.AddDays(numDayOfWeek - 1);
                int yearWeek = 0;
                for (seedweek = 1; seedweek < numWeekOfYear; seedweek++)
                {
                    yearWeek = int.Parse(string.Format("{0}{1}", YYYY, seedweek > 9 ? seedweek.ToString() : string.Format("0{0}", seedweek)));
                    listWeeks.Add(new WeeksOfYear { Week = seedweek, Month = friDayDate.Month, Year = YYYY, YearWeek = yearWeek, StartDate = startDate, FridayDate = friDayDate, EndDate = endDate });
                    startDate = startDate.AddDays(numDayOfWeek);
                    friDayDate = startDate;
                    endDate = startDate.AddDays(numDayOfWeek - 1);
                }
                if (startDate.Year == YYYY)
                {
                    yearWeek = int.Parse(string.Format("{0}{1}", YYYY, seedweek > 9 ? seedweek.ToString() : string.Format("0{0}", seedweek)));
                    listWeeks.Add(new WeeksOfYear { Week = seedweek, Month = friDayDate.Month, Year = YYYY, YearWeek = yearWeek, StartDate = startDate, FridayDate = friDayDate, EndDate = endDate });
                }
            }

            /*var listWeeksPC = GetWeeksOfYearForPC(YYYY).ToDictionary(n => n.YearWeek);

            foreach (var yw  in listWeeks)
            {
                if (listWeeksPC.ContainsKey(yw.YearWeek))
                {
                    yw.IsFirstYearWeekOfMonth = true;
                }
            }*/

            return listWeeks;
        }

        public static IEnumerable<WeeksOfYear> GetWeeksOfYearForPC(int YYYY)
        {
            List<WeeksOfYear> listWeeks = new List<WeeksOfYear>();
            if (YYYY >= DateTime.MinValue.Year && YYYY <= DateTime.MaxValue.Year)
            {

                for (int i = 1; i <= 12; i++)
                {
                    var date = new DateTime(YYYY, i, 1);

                    DateTime firstWenesdayInMonth;

                    bool valid = WeeksOfYearHelper.TryGetDayOfMonth(date, DayOfWeek.Wednesday, 1, out firstWenesdayInMonth);//WeeksOfYearHelper.GetFridayDateByYearWeek(currentYW.ToString());

                    if (valid)
                    {
                        var friday = firstWenesdayInMonth.AddDays(2);
                        var yw = new WeeksOfYear()
                        {
                            YearWeek = (GetWeekOfYearByDateTime(friday)),// WeeksOfYearHelper.GetWeekOfYearByDateTime(firstFridayInMonth),
                            Year = date.Year,
                            Month = i,
                            FridayDate = friday,
                            WednesdayDate = firstWenesdayInMonth//(firstFridayInMonth.Day == 1 || firstFridayInMonth.Day == 2) ? firstFridayInMonth.AddDays(5) : firstFridayInMonth.AddDays(-2)
                        };

                        listWeeks.Add(yw);
                    }
                }
            }
            return listWeeks;
        }

        public static IEnumerable<WeeksOfYear> GetWeeksOfYear(int startYW, int endYW)
        {
            int[] tmp = new int[] { startYW / 100, endYW / 100 };

            List<int> listOfYears = new List<int>();

            int year = tmp[0];
            while(year <= tmp[1])
            {
                listOfYears.Add(year);

                year++;
            }
                       
            int[] arrOfYears = listOfYears.Distinct().OrderBy(n => n).ToArray();

            //int[] arrOfYears = tmp.Distinct().OrderBy(n => n).ToArray();

            List<WeeksOfYear> weeksOfYear = new List<WeeksOfYear>();

            /*foreach (int year in arrOfYears)
            {
                var weeks = GetWeeksOfYear(year);

                weeksOfYear.AddRange(weeks);
            }*/

            for(int start = 0; start < arrOfYears.Length; start++)
            {
                var weeks = GetWeeksOfYear(arrOfYears[start]);

                weeksOfYear.AddRange(weeks);
            }


            return weeksOfYear.Where(n => n.YearWeek >= startYW && n.YearWeek <= endYW);
        }

        public static IEnumerable<WeeksOfYear> GetWeeksOfYearForPC(int startYW, int endYW)
        {
            int[] tmp = new int[] { startYW / 100, endYW / 100 };


            List<int> listOfYears = new List<int>();
            int year = tmp[0];
            while (year <= tmp[1])
            {
                listOfYears.Add(year);

                year++;
            }


            int[] arrOfYears = listOfYears.Distinct().OrderBy(n => n).ToArray();

            List<WeeksOfYear> weeksOfYear = new List<WeeksOfYear>();

            /*foreach (int year in arrOfYears)
            {
                var weeks = GetWeeksOfYearForPC(year);

                weeksOfYear.AddRange(weeks);
            }*/

            for (int start = 0; start < arrOfYears.Length; start++)
            {
                var weeks = GetWeeksOfYearForPC(arrOfYears[start]);

                weeksOfYear.AddRange(weeks);
            }


            return weeksOfYear.Where(n => n.YearWeek >= startYW && n.YearWeek <= endYW); ;

        }

        /// <summary>
        /// Get 52 weeks
        /// </summary>
        /// <param name="YYYY"></param>
        /// <returns></returns>
        public static IEnumerable<WeeksOfYear> GetWeeksOfYearForDropDown()
        {
            DateTime currentDate = DateTime.Now;
            int YYYY = currentDate.Year;
            int YYYYWW = GetWeekOfYearByDateTime(currentDate);
            int YYYYWWAdd = YYYYWW + 2;
            var weeksCurrentYear = GetWeeksOfYear(YYYY).Where(wks => wks.YearWeek >= YYYYWWAdd);
            var weeksCurrentAdd = GetWeeksOfYear(YYYY + 1);
            var results = weeksCurrentYear.Union(weeksCurrentAdd).Take(52);
            return results;
        }

        /// <summary>
        /// Get FridayDate by YYYYWWW
        /// </summary>
        /// <param name="YYYYWW">Week of the year</param>
        /// <param string length></param>
        /// <returns></returns>
        public static DateTime GetFridayDateByYearWeek(string YYYYWW)
        {
            var YYYY = int.Parse(YYYYWW.Substring(0, 4));
            var WW = int.Parse(YYYYWW.Substring(4, 2));
            int numDayOfWeek = 7;
            int startDayOfWeekIsFriday = 5;
            DateTime beginDayOfYear = new DateTime(YYYY, 1, 1);
            int seedday = startDayOfWeekIsFriday - (int)beginDayOfYear.DayOfWeek % numDayOfWeek;
            seedday = seedday < 0 ? (seedday + numDayOfWeek) : seedday;
            DateTime startDateFirst = beginDayOfYear.AddDays(seedday);
            var days = startDateFirst.Day + (WW - 1) * numDayOfWeek - 1;
            return beginDayOfYear.AddDays(days);
        }

        /// <summary>
        /// Wednesday before the yearweek by YYYYWWW base on GetFridayDateByYearWeek
        /// </summary>
        /// <param name="YYYYWW">Week of the year</param>
        /// <param string length></param>
        /// <returns></returns>
        public static DateTime GetWednesdayDateBeforeYearWeek(string YYYYWW)
        {
            var friday = GetFridayDateByYearWeek(YYYYWW);
            return friday.AddDays(GET_WEDNESDAY_BASE_ON_FRIDAY);
        }

        /// <summary>
        /// Get the closest YearWeek editable for user
        /// </summary>
        /// <returns></returns>
        public static int GetClosestEditableWeek()
        {
            /*var closestEditableDate = DateTime.Now.AddDays(AddWeekValue * 7);
            var closestEditableWeek = GetWeekOfYearByDateTime(closestEditableDate);
            return closestEditableWeek;*/

            return GetClosestEditableWeek(DateTime.Now);
        }

        public static int GetClosestEditableWeek(DateTime fromDate)
        {
            var closestEditableDate = fromDate.AddDays(AddWeekValue * 7);
            var closestEditableWeek = GetWeekOfYearByDateTime(closestEditableDate);
            return closestEditableWeek;
        }

        public static int GetClosestEditableWeekPC()
        {
            return GetClosestEditableWeekPC(DateTime.Now);
        }

        public static int GetClosestEditableWeek_P2N()
        {
            return GetClosestEditableWeek_P2N(DateTime.Now);
        }

        public static int GetClosestEditableWeekPC(DateTime currentdate)
        {

            var currentMonth = new DateTime(currentdate.Year, currentdate.Month, 1);//.AddDays(13);
            var nextMonth = currentdate.AddMonths(1);

            //var mailDateOfCurrentMonth = GetWednesdayDateForPC(currentMonth);
            var mailDateOfNextMonth = GetWednesdayDateForPC(nextMonth);

            var fridayDate = mailDateOfNextMonth.AddDays(2);

            //var lockedOutDate = mailDateOfNextMonth.AddDays(-LOCKOUT_DAYS_PLASTIC_CARDS);

            int closestEditableWeek = 0;

            if (currentdate.AddDays(LOCKOUT_DAYS_PLASTIC_CARDS).CompareTo(fridayDate) < 0)
                //if (HasReachedTo3WeeksBeforeYearWeek(currentdate) == false)
                closestEditableWeek = GetWeekOfYearByDateTime(fridayDate);
            else
            {
                nextMonth = nextMonth.AddMonths(1);
                mailDateOfNextMonth = GetWednesdayDateForPC(nextMonth);
                fridayDate = mailDateOfNextMonth.AddDays(2);
                closestEditableWeek = GetWeekOfYearByDateTime(fridayDate);
            }

            return closestEditableWeek;

        }

        public static int GetClosestEditableWeek_P2N(DateTime currentdate)
        {

            var currentMonth = new DateTime(currentdate.Year, currentdate.Month, 1);//.AddDays(13);
            var nextMonth = currentdate.AddMonths(1);

            //var mailDateOfCurrentMonth = GetWednesdayDateForPC(currentMonth);
            var mailDateOfNextMonth = GetWednesdayDateForPC(nextMonth);

            var fridayDate = mailDateOfNextMonth.AddDays(2);

            //var lockedOutDate = mailDateOfNextMonth.AddDays(-LOCKOUT_DAYS_P2N);

            int closestEditableWeek = 0;

            if (currentdate.AddDays(LOCKOUT_DAYS_P2N).CompareTo(fridayDate) < 0)
                closestEditableWeek = GetWeekOfYearByDateTime(fridayDate);
            else
            {
                nextMonth = nextMonth.AddMonths(1);
                mailDateOfNextMonth = GetWednesdayDateForPC(nextMonth);
                fridayDate = mailDateOfNextMonth.AddDays(2);
                closestEditableWeek = GetWeekOfYearByDateTime(fridayDate);
            }

            return closestEditableWeek;

        }


        public static bool HasReachedTo3WeeksBeforeYearWeek()
        {
            var currentMonth = DateTime.Now.Date;

            return HasReachedTo3WeeksBeforeYearWeek(currentMonth);

        }

        public static bool HasReachedTo3WeeksBeforeYearWeek(DateTime currentdate)
        {
            //var currentMonth = DateTime.Now.Date;
            var nextMonth = currentdate.AddMonths(1);

            nextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);

            //var mailDateOfNextMonth = GetWednesdayDateForPC(nextMonth);
            var fridayDate = GetWednesdayDateForPC(nextMonth).AddDays(2);

            if (currentdate.AddDays(LOCKOUT_DAYS_PLASTIC_CARDS).CompareTo(fridayDate) < 0)
                return false;
            else
                return true;

        }

        /*public static int GetClosestEditableWeekPC(DateTime date)
        {
            //var nextMonth = date.AddMonths(1);

            //var firstYearWeekOnNextMonth = GetWeekOfYearByDateTime(new DateTime(nextMonth.Year, nextMonth.Month, 1));

            var firstMailDate = GetWednesdayDateForPC(date);//GetWednesdayDateBeforeYearWeek(firstYearWeekOnNextMonth.ToString());

            var closestEditableDate = firstMailDate.AddDays(21);
            var closestEditableWeek = GetWeekOfYearByDateTime_v2(closestEditableDate);
            return closestEditableWeek;
        }*/

        public static DateTime GetWednesdayDateForPC()
        {
            return GetWednesdayDateForPC(DateTime.Now);
        }

        public static DateTime GetFirstFridayDateForPC(DateTime date)
        {
            /*DateTime firstFridayInMonth;

            bool valid = WeeksOfYearHelper.TryGetDayOfMonth(date, DayOfWeek.Friday, 1, out firstFridayInMonth);//WeeksOfYearHelper.GetFridayDateByYearWeek(currentYW.ToString());

            return firstFridayInMonth;*/

            var maildate = GetWednesdayDateForPC(date);

            return maildate.AddDays(2);
        }

        public static DateTime GetWednesdayDateForPC(DateTime date)
        {
            /*
            DateTime firstFridayInMonth = GetFirstFridayDateForPC(date);

            return (firstFridayInMonth.Day == 1 || firstFridayInMonth.Day == 2) ? firstFridayInMonth.AddDays(5) : firstFridayInMonth.AddDays(-2);
            */

            DateTime firstWednesdayInMonth;

            bool valid = WeeksOfYearHelper.TryGetDayOfMonth(date, DayOfWeek.Wednesday, 1, out firstWednesdayInMonth);

            return firstWednesdayInMonth;

        }

        public static DateTime GetWednesdayDateForPC(int yearweek)
        {
            var firstFridayInMonth = WeeksOfYearHelper.GetFridayDateByYearWeek(yearweek.ToString());

            return (firstFridayInMonth.Day == 1 || firstFridayInMonth.Day == 2) ? firstFridayInMonth.AddDays(5) : firstFridayInMonth.AddDays(-2);
        }

        /// <summary>
        /// Get week of year by datetime value
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>YYYYWW</returns>
        public static int GetWeekOfYearByDateTime(DateTime dateTime)
        {
            int year = dateTime.Year;
            int month = dateTime.Month;
            int WW = 0;
            WW = (from week in GetWeeksOfYear(year)
                  where DateHelper.CompareBetweenDateTimes(week.StartDate, week.EndDate, dateTime)
                  select week.Week).FirstOrDefault();
            if (WW == 0)
            {
                if (month == 12) { year++; }
                if (month == 1) { year--; }
                WW = (from week in GetWeeksOfYear(year)
                      where DateHelper.CompareBetweenDateTimes(week.StartDate, week.EndDate, dateTime)
                      select week.Week).FirstOrDefault();
            }
            return int.Parse(string.Format("{0}{1}", year, WW > 9 ? WW.ToString() : string.Format("0{0}", WW)));
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            var weeknum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Friday); 

            // Return the week of our adjusted day
            return Convert.ToInt32($"{time.Year}{weeknum.ToString().PadLeft(2, '0')}"); 
        }

        /// <summary>
        /// validation between two year week
        /// </summary>
        /// <param name="firstYearWeek">YYYYWW</param>
        /// <param name="secondYearWeek">YYYYWW</param>
        /// <returns>bool</returns>
        ///
        public static bool ValidationTwoYearWeeks(int firstYearWeek, int secondYearWeek)
        {
            var firstYear = (firstYearWeek - (firstYearWeek % 100)) / 100;
            var secondYear = (secondYearWeek - (secondYearWeek % 100)) / 100;

            var firstWeekValidate = GetWeeksOfYear(firstYear).Count() >= (firstYearWeek % 100);
            var secondWeekValidate = GetWeeksOfYear(secondYear).Count() >= (secondYearWeek % 100);
            var yearWeekValidate = secondYearWeek - (firstYearWeek + AddWeekValue) >= 0;

            return firstWeekValidate && secondWeekValidate && yearWeekValidate;
        }

        /// <summary>
        /// Get # Of Editable Weeks Left in a year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetNumberOfEditableWeeksLeft(int year)
        {
            var weeks = GetWeeksOfYear(year);
            var currentWeekEnabled = GetClosestEditableWeek();
            return weeks.Count(x => x.YearWeek >= currentWeekEnabled);
        }

     

        public static bool TryGetDayOfMonth(DateTime instance,
                                 DayOfWeek dayOfWeek,
                                 int occurance,
                                 out DateTime dateOfMonth)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (occurance <= 0 || occurance > 5)
            {
                throw new ArgumentOutOfRangeException("occurance", "Occurance must be greater than zero and less than 6.");
            }

            bool result;
            dateOfMonth = new DateTime();

            // Change to first day of the month
            DateTime dayOfMonth = instance.AddDays(1 - instance.Day);

            // Find first dayOfWeek of this month;
            if (dayOfMonth.DayOfWeek > dayOfWeek)
            {
                dayOfMonth = dayOfMonth.AddDays(7 - (int)dayOfMonth.DayOfWeek + (int)dayOfWeek);
            }
            else
            {
                dayOfMonth = dayOfMonth.AddDays((int)dayOfWeek - (int)dayOfMonth.DayOfWeek);
            }

            // add 7 days per occurance
            dayOfMonth = dayOfMonth.AddDays(7 * (occurance - 1));

            // make sure this occurance is within the original month
            result = dayOfMonth.Month == instance.Month;


            if (result)
            {
                dateOfMonth = dayOfMonth;
            }

            return result;
        }

    }


}