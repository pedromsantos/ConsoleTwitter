namespace ConsoleTwitter.Messages
{
    using System;

    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    public class Message
    {
        private readonly IUser user;

        private readonly string message;

        private readonly DateTime timestamp;

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
                return this.user;
            }
        }

        public string Body
        {
            get 
            {
                return this.message;
            }
        }

        public DateTime Timestamp 
        {
            get 
            {
                return this.timestamp;
            }
        }
    }
}
