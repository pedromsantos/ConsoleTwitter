using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserWall : IWall
    {
        private readonly IList<Message> internalMessages;

        public UserWall()
        {
            internalMessages = new List<Message>();
        }

        public void Post(string message)
        {
            internalMessages.Add(new Message(new NullUser(), message));
        }

        public void Post(IUser user, string message)
        {
            internalMessages.Add(new Message(user, message));
        }

        public IEnumerable<Message> Wall
        {
            get 
            {
                return internalMessages.Skip(0);
            }
        }

        public IEnumerable<Message> Posts(IUser user)
        {
            return internalMessages.Where(m => m.User.UserHandle == user.UserHandle).Skip(0);
        }
    }
}

