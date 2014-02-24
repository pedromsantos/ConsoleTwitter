using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleTwitter
{
    public class NullUser : IUser
    {
        public NullUser()
        {
        }

        public void Post(string message)
        {
        }

        public void Post(IUser user, string message)
        {
        }

        public IEnumerable<Message> Posts(IUser user = null)
        {
            return new Collection<Message> { };
        }

        public IEnumerable<Message> Wall 
        {
            get
            {
                return new Collection<Message> { };
            }
        }

        public void AddFollower(IUser user)
        {
        }

        public string UserHandle {
            get 
            {
                return string.Empty;
            }
        }

        public IEnumerable<IUser> Followers {
            get 
            {
                return new Collection<IUser> { };
            }
        }

        public void Post(User user, string message)
        {
        }
    }
}

