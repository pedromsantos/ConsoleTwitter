namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using ConsoleTwitter.Messages;

    public class User : IUser
    {
        private readonly IWall wall;
        private readonly ICollection<IUser> internalFollowers;
        private readonly ICollection<IUser> internalFollowees;

        public User(string userHandle, IWall wall)
        {
            this.wall = wall;
            this.UserHandle = userHandle;
            this.internalFollowers = new List<IUser>();
            this.internalFollowees = new List<IUser>();
        }

        public string UserHandle { get; private set; }

        public IEnumerable<IUser> Followers 
        {
            get 
            {
                return this.internalFollowers.Skip(0);
            }
        }

        public IEnumerable<IUser> Followees
        {
            get
            {
                return this.internalFollowees.Skip(0);
            }
        }

        public IEnumerable<Message> Wall
        {
            get
            {
                var followeesPosts = new List<Message>();

                foreach (var followee in this.Followees)
                {
                    followeesPosts.AddRange(followee.Posts());
                }

                return this.wall.Wall.Concat(followeesPosts).OrderByDescending(m => m.Timestamp);
            }
        }

        public void AddFollowee(IUser user)
        {
            this.internalFollowees.Add(user);
        }

        public void AddFollower(IUser user)
        {
            this.internalFollowers.Add(user);
            user.AddFollowee(this);
        }

        public void Post(string message)
        {
            this.Post(this, message);
        }

        public void Post(IUser user, string message)
        {
            this.wall.Post(user, message);
        }

        public IEnumerable<Message> Posts(IUser user = null)
        {
            return this.wall.Posts(this); 
        }
    }
}
