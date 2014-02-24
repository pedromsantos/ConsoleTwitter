using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class User : IWall
    {
        private IWall wall;
        private ICollection<User> internalFollowers;

        public User(string userHandle, IWall wall)
        {
            this.wall = wall;
            this.UserHandle = userHandle;
            this.internalFollowers = new List<User>();
        }

        public string UserHandle { get; private set; }

        public IEnumerable<User> Followers 
        {
            get 
            {
                return this.internalFollowers.Skip(0);
            }
        }

        public void AddFollower(User user)
        {
            this.internalFollowers.Add(user);
        }

        public void Post(string message)
        {
            this.wall.Post(message);
        }
    }
}

