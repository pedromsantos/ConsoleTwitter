namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using ConsoleTwitter.Messages;

    public class UserWall : IWall
    {
        private readonly IList<Message> internalMessages;

        public UserWall()
        {
            this.internalMessages = new List<Message>();
        }

        public IEnumerable<Message> Wall
        {
            get
            {
                return this.internalMessages.Skip(0);
            }
        }

        public void Post(string message)
        {
            this.internalMessages.Add(new Message(new NullUser(), message));
        }

        public void Post(IUser user, string message)
        {
            this.internalMessages.Add(new Message(user, message));
        }

        public IEnumerable<Message> Posts(IUser user)
        {
            return this.internalMessages.Where(m => m.User.UserHandle == user.UserHandle).Skip(0);
        }
    }
}
