using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class Message
    {
        private IUser user;

        private string message;

        private DateTime timestamp;

        public Message(IUser user, string message)
        {
            this.message = message;
            this.user = user;
            this.timestamp = SystemTime.Now();
        }

        public IUser User 
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
