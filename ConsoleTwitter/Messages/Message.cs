using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class Message
    {
        private User user;

        private string message;

        private DateTime timestamp;

        public Message(User user, string message)
        {
            this.message = message;
            this.user = user;
            this.timestamp = SystemTime.Now();
        }

        public User User 
        {
            get 
            {
                return user;
            }
        }

        public string Body
        {
            get 
            {
                return message;
            }
        }

        public DateTime Timestamp 
        {
            get 
            {
                return timestamp;
            }
        }
    }
    
}
