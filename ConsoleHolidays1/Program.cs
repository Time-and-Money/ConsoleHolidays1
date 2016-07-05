using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHolidays1
{
    class Program
    // This code originated here: http://geekswithblogs.net/wpeck/archive/2011/12/27/us-holiday-list-in-c.aspx
    // I fixed issues with the solution/project in VS 2015 and added some sections for other holidays.
    // Jim Lewis.

    {
        static void Main(string[] args)
        {
            string dtToday = DateTime.Now.ToShortDateString();
            int dtCurrentYear = DateTime.Now.Year;

            List<DateTime> GH = GetHolidays(dtCurrentYear).ToList();

            foreach (DateTime gh in GH)
            {
                if (gh.ToShortDateString() == dtToday)
                {
                    System.Environment.Exit(0);
                }

            }
        }

        private static HashSet<DateTime> GetHolidays(int year)
        {
            //TODO: Consider using a Dictionary object instead of a HashSet.
            //      Then, we can store the holdiay name as well as the date.  
            HashSet<DateTime> holidays = new HashSet<DateTime>();
            //NEW YEARS 
            DateTime newYearsDate = AdjustForWeekendHoliday(new DateTime(year, 1, 1).Date);
            DayOfWeek dayOfWeek = newYearsDate.DayOfWeek;
            holidays.Add(newYearsDate);
            Console.WriteLine("New Years: " + newYearsDate.ToShortDateString());

            //Martin Luther King Day -- third monday in January
            DateTime MLKDay = new DateTime(year, 1, 21);
            dayOfWeek = MLKDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                MLKDay = MLKDay.AddDays(-1);
                dayOfWeek = MLKDay.DayOfWeek;
            }
            holidays.Add(MLKDay.Date);
            Console.WriteLine("MLK Day: " + MLKDay.ToShortDateString());

            //President's Day -- third monday in February
            DateTime PresDay = new DateTime(year, 2, 21);
            dayOfWeek = PresDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                PresDay = PresDay.AddDays(-1);
                dayOfWeek = PresDay.DayOfWeek;
            }
            holidays.Add(PresDay.Date);
            Console.WriteLine("President's Day: " + PresDay.ToShortDateString());

            // Good Friday
            DateTime goodFriday = EasterSunday(year).AddDays(-2);
            holidays.Add(goodFriday.Date);
            Console.WriteLine("Good Friday: " + goodFriday.ToShortDateString());

            // Easter Sunday
            DateTime easterSunday = EasterSunday(year);
            holidays.Add(easterSunday.Date);
            Console.WriteLine("Easter Sunday: " + easterSunday.ToShortDateString());

            //MEMORIAL DAY  -- last monday in May 
            DateTime memorialDay = new DateTime(year, 5, 31);
            dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            holidays.Add(memorialDay.Date);
            Console.WriteLine("Memorial Day: " + memorialDay.ToShortDateString());

            //INDEPENCENCE DAY 
            DateTime independenceDay = AdjustForWeekendHoliday(new DateTime(year, 7, 4).Date);
            holidays.Add(independenceDay);
            Console.WriteLine("July 4th: " + independenceDay.ToShortDateString());

            //LABOR DAY -- 1st Monday in September 
            DateTime laborDay = new DateTime(year, 9, 1);
            dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            holidays.Add(laborDay.Date);
            Console.WriteLine("Labor Day: " + laborDay.ToShortDateString());

            //THANKSGIVING DAY - 4th Thursday in November 
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
            holidays.Add(thanksgivingDay.Date);
            Console.WriteLine("Thanksgiving: " + thanksgivingDay.ToShortDateString());

            DateTime christmasDay = AdjustForWeekendHoliday(new DateTime(year, 12, 25).Date);
            holidays.Add(christmasDay);
            Console.WriteLine("Christmas Day: " + christmasDay.ToShortDateString());

            return holidays;
        }

        public static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        public static DateTime EasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }
    }
}