namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;

    using ConsoleTwitter.Messages;

    public interface IWall
    {
        IEnumerable<Message> Wall { get; }

        IEnumerable<Message> Posts(IUser user = null);

        void Post(string message);

        void Post(IUser user, string message);
    }
}