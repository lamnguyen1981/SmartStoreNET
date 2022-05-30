//================================================================================
// Copyright <Complete Coders>
// All Rights Reserved
// Created by	: Kevin
// Create Date	: 07/06/2016
// Description	: <Description of the file>
//================================================================================

namespace CC.Plugins.Core.Utilities
{
    using System;

    public class WeeksOfYear
    {
        public int Week { get; set; }
        public int Month { get; set; }

        public int YearMonth
        {
            get
            {
                return int.Parse($"{Year}{Month.ToString().PadLeft(2, '0')}");
            }
        }

        public string MonthName
        {
            get
            {

                switch (Month)
                {
                    case 1: return "Jan";
                    case 2: return "Feb";
                    case 3: return "Mar";
                    case 4: return "Apr";
                    case 5: return "May";
                    case 6: return "Jun";
                    case 7: return "Jul";
                    case 8: return "Aug";
                    case 9: return "Sep";
                    case 10: return "Oct";
                    case 11: return "Nov";
                    case 12: return "Dec";
                }

                return string.Empty;
            }
        }

        public int Year { get; set; }
        public int YearWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FridayDate { get; set; }
        public DateTime WednesdayDate { get; set; }
        public DateTime MailingDate {
            get
            {
                return FridayDate.AddDays(-2);
            }
        }
        public DateTime EndDate { get; set; }

        public bool IsFirstYearWeekOfMonth { get; set; }
    }
}