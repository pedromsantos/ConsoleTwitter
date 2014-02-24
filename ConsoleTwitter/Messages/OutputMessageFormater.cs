using System;

namespace ConsoleTwitter
{
    public class OutputMessageFormater : IMessageFormater
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
                return String.Format("{0} {1} ago", elapsedYears, (elapsedYears == 1) ? "year" : "years");
            } 

            int elapsedMonths = SystemTime.Now().Month - date.Month; 
             
            if (elapsedMonths > 0)
            {
                return String.Format("{0} {1} ago", elapsedMonths, (elapsedMonths == 1) ? "month" : "months");
            } 

            int elapsedDays = SystemTime.Now().Day - date.Day; 

            if (elapsedDays > 0)
            {
                return String.Format("{0} {1} ago", elapsedDays, (elapsedDays == 1) ? "day" : "days");
            } 

            int elapsedHours = SystemTime.Now().Hour - date.Hour; 

            if (elapsedHours > 0)
            {
                return String.Format("{0} {1} ago", elapsedHours, (elapsedHours == 1) ? "hour" : "hours");
            } 

            int elapsedMinutes = SystemTime.Now().Minute - date.Minute; 

            if (elapsedMinutes > 0)
            {
                return String.Format("{0} {1} ago", elapsedMinutes, (elapsedMinutes == 1) ? "minute" : "minutes");
            } 

            int elapsedSeconds = SystemTime.Now().Second - date.Second;

            if (elapsedSeconds > 0)
            {
                return String.Format("{0} {1} ago", elapsedSeconds, (elapsedSeconds == 1) ? "second" : "seconds");
            }

            return "0 seconds ago";
        }
    }
}

