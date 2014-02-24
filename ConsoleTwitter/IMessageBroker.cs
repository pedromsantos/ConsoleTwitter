using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public interface IMessageBroker
    {
        void Follow(string user, string userToFollow);

        void Post(string user, string message);

        IEnumerable<Message> Read(string user);

        IEnumerable<Message> Wall(string bob);
    }
}
