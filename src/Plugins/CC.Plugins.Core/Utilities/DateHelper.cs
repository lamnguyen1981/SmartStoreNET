namespace CC.Plugins.Core.Utilities
{
    using System;

    // support functions about DateTime converting
    public static class DateHelper
    {
        /// <summary>
        /// Convert DateTime to DateString for frontend use.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Compare datetime between from startdate to enddate
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="compareDate"></param>
        /// <returns></returns>
        public static bool CompareBetweenDateTimes(DateTime startDate, DateTime endDate, DateTime compareDate)
        {
            return (DateTime.Compare(GetStartDate(startDate), GetStartDate(compareDate)) <= 0 && DateTime.Compare(GetEndDate(compareDate), GetEndDate(endDate)) <= 0);
        }

        /// <summary>
        /// Get datetime default min value is 2000-01-01
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTimeMinValue()
        {
            return new DateTime(2000, 01, 01, 0, 0, 1);
        }

        /// <summary>
        /// Get datetime default max value is 2050-01-01
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTimeMaxValue()
        {
            return new DateTime(2050, 12, 31, 23, 59, 59);
        }

        /// <summary>
        /// Get start date
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static DateTime GetStartDate(DateTime startDate)
        {
            return new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 1);
        }

        /// <summary>
        /// Get end date
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DateTime GetEndDate(DateTime endDate)
        {
            return new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
        }
    }
}