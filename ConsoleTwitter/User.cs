using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public class User : IWall
    {
        private IWall wall;

        public User(string userHandle, IWall wall)
        {
            this.wall = wall;
            this.UserHandle = userHandle;
            this.InternalFollowers = new LinkedList<User>();
        }

        public string UserHandle { get; private set; }

        public IEnumerable<User> Followers 
        {
            get 
            {
                return this.InternalFollowers;
            }
        }

        private ICollection<User> InternalFollowers { get; set; }

        public void AddFollower(User user)
        {
            this.InternalFollowers.Add(user);
        }

        public void Post(string message)
        {
            this.wall.Post(message);
        }
    }
}

