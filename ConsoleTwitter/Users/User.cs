using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class User : IUser
    {
        private IWall wall;
        private ICollection<IUser> internalFollowers;

        public User(string userHandle, IWall wall)
        {
            this.wall = wall;
            this.UserHandle = userHandle;
            this.internalFollowers = new List<IUser>();
        }

        public string UserHandle { get; private set; }

        public IEnumerable<IUser> Followers 
        {
            get 
            {
                return this.internalFollowers.Skip(0);
            }
        }

        public void AddFollower(IUser user)
        {
            this.internalFollowers.Add(user);
        }

        public void Post(string message)
        {
            this.Post(this, message);
        }

        public void Post(IUser user, string message)
        {
            this.wall.Post(user, message);

            Followers.ToList().ForEach(u => u.Post(user, message));
        }

        public IEnumerable<Message> Wall
        {
            get 
            {
                return this.wall.Wall;
            }
        }

        public IEnumerable<Message> Posts(IUser user = null)
        {
            return this.wall.Posts(this); 
        }
    }
}

