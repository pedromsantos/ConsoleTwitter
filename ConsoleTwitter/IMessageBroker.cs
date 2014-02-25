namespace ConsoleTwitter
{
    using System.Collections.Generic;

    using ConsoleTwitter.Messages;

    public interface IMessageBroker
    {
        void Follow(string user, string userToFollow);

        void Post(string user, string message);

        IEnumerable<Message> Read(string user);

        IEnumerable<Message> Wall(string bob);
    }
}
