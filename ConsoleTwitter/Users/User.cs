namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using ConsoleTwitter.Messages;

    public class User : IUser
    {
        private readonly IWall wall;
        private readonly ICollection<IUser> internalFollowers;

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

        public IEnumerable<Message> Wall
        {
            get
            {
                return this.wall.Wall;
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

            this.Followers.ToList().ForEach(u => u.Post(user, message));
        }

        public IEnumerable<Message> Posts(IUser user = null)
        {
            return this.wall.Posts(this); 
        }
    }
}
