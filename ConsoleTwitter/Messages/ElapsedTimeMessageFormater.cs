namespace ConsoleTwitter.Messages
{
    using System;

    using ConsoleTwitter.Wrappers;

    public class ElapsedTimeMessageFormater : IMessageFormater
    {
        public string Format(Message message)
        {
            return this.ElapsedTime(message.Timestamp);
        }

        private string ElapsedTime(DateTime date) 
        {  
            var elapsedYears = SystemTime.Now().Year - date.Year; 

            if (elapsedYears > 0)
            {
                return this.FormatElapsedTime(elapsedYears, "year");
            } 

            var elapsedMonths = SystemTime.Now().Month - date.Month; 

            if (elapsedMonths > 0)
            {
                return this.FormatElapsedTime(elapsedMonths, "month");
            } 

            var elapsedDays = SystemTime.Now().Day - date.Day; 

            if (elapsedDays > 0)
            {
                return this.FormatElapsedTime(elapsedDays, "day");
            } 

            var elapsedHours = SystemTime.Now().Hour - date.Hour; 

            if (elapsedHours > 0)
            {
                return this.FormatElapsedTime(elapsedHours, "hour");
            } 

            var elapsedMinutes = SystemTime.Now().Minute - date.Minute; 

            if (elapsedMinutes > 0)
            {
                return this.FormatElapsedTime(elapsedMinutes, "minute");
            } 

            var elapsedSeconds = SystemTime.Now().Second - date.Second;

            return elapsedSeconds > 0 ? this.FormatElapsedTime(elapsedSeconds, "second") : "0 seconds ago";
        }

        private string FormatElapsedTime(int value, string singularUnitName)
        {
            return string.Format("{0} {1} ago", value, this.UnitNamePluralization(value, singularUnitName));
        }

        private string UnitNamePluralization(int value, string singularUnitName)
        {
            return (value == 1) ? singularUnitName : singularUnitName + "s";
        }
    }
}
