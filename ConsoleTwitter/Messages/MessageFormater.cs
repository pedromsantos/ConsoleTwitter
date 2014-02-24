using System;

namespace ConsoleTwitter
{
    public class MessageFormater : IMessageFormater
    {
        public string Format(Message message)
        {
            return string.Format("{0} ({1})", message.Body, ElapsedTime(message.Timestamp));
        }

        private string ElapsedTime(DateTime date) 
        {  
            int elapsedYears = SystemTime.Now().Year - date.Year; 

            if (elapsedYears > 0)
            {
                return FormatElapsedTime(elapsedYears, "year");
            } 

            int elapsedMonths = SystemTime.Now().Month - date.Month; 
             
            if (elapsedMonths > 0)
            {
                return FormatElapsedTime(elapsedMonths, "month");
            } 

            int elapsedDays = SystemTime.Now().Day - date.Day; 

            if (elapsedDays > 0)
            {
                return FormatElapsedTime(elapsedDays, "day");
            } 

            int elapsedHours = SystemTime.Now().Hour - date.Hour; 

            if (elapsedHours > 0)
            {
                return FormatElapsedTime(elapsedHours, "hour");
            } 

            int elapsedMinutes = SystemTime.Now().Minute - date.Minute; 

            if (elapsedMinutes > 0)
            {
                return FormatElapsedTime(elapsedMinutes, "minute");
            } 

            int elapsedSeconds = SystemTime.Now().Second - date.Second;

            if (elapsedSeconds > 0)
            {
                return FormatElapsedTime(elapsedSeconds, "second");
            }

            return "0 seconds ago";
        }

        private string FormatElapsedTime(int value, string singularUnitName)
        {
            return String.Format("{0} {1} ago", value, UnitNamePluralization(value, singularUnitName));
        }

        private string UnitNamePluralization(int value, string singularUnitName)
        {
            return (value == 1) ? singularUnitName : singularUnitName + "s";
        }
    }
}

