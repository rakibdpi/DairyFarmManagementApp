using System;
using System.Globalization;

namespace BusinessManagementSystemApp.Core.Models.Base
{
    public static class DateTimeFormatter
    {
        public static DateTime StringToDate(string text)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                return DateTime.ParseExact(text, "MM/dd/yyyy", provider);
            }
            catch (Exception)
            {
                throw new ApplicationException("Date Not Valid");
            }
        }

        public static string DateToString(DateTime date)
        {
            try
            {

                return date.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            {
                throw new ApplicationException("Date Not Valid");
            }
        }

        public static TimeSpan StringToTime(string text)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                return DateTime.ParseExact(text, "h:mm tt", provider).TimeOfDay;
            }
            catch (Exception)
            {
                throw new ApplicationException("Time Not Valid");
            }
        }

        public static string TimeToString(TimeSpan time)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var date = DateTime.ParseExact("01-01-2000", "dd-MM-yyyy", provider);
                date += time;
                return date.ToString("h:mm tt");
            }
            catch (Exception)
            {
                throw new ApplicationException("Time Not Valid");
            }
        }
    }
}