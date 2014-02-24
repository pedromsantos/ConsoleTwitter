using System;

namespace ConsoleTwitter
{
    public interface IMessageBroker
    {
        void Follow(string user, string userToFollow);

        void Post(string user, string message);

        void Read(string user);

        void Wall(string bob);
    }
}
