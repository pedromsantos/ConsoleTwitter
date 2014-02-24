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
            int intYears = SystemTime.Now().Year - date.Year; 
            int intMonths = SystemTime.Now().Month - date.Month; 
            int intDays = SystemTime.Now().Day - date.Day; 
            int intHours = SystemTime.Now().Hour - date.Hour; 
            int intMinutes = SystemTime.Now().Minute - date.Minute; 
            int intSeconds = SystemTime.Now().Second - date.Second; 

            if (intYears > 0)
            {
                return String.Format("{0} {1} ago", intYears, (intYears == 1) ? "year" : "years");
            } 
            else if (intMonths > 0)
            {
                return String.Format("{0} {1} ago", intMonths, (intMonths == 1) ? "month" : "months");
            } 
            else if (intDays > 0)
            {
                return String.Format("{0} {1} ago", intDays, (intDays == 1) ? "day" : "days");
            } 
            else if (intHours > 0)
            {
                return String.Format("{0} {1} ago", intHours, (intHours == 1) ? "hour" : "hours");
            } 
            else if (intMinutes > 0)
            {
                return String.Format("{0} {1} ago", intMinutes, (intMinutes == 1) ? "minute" : "minutes");
            } 
            else if (intSeconds > 0)
            {
                return String.Format("{0} {1} ago", intSeconds, (intSeconds == 1) ? "second" : "seconds");
            }
            else 
            { 
                return "0 seconds ago";
            } 
        }
    }
}

