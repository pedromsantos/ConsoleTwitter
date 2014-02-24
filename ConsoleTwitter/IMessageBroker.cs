using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public interface IMessageBroker
    {
        void Follow(string user, string userToFollow);

        void Post(string user, string message);

        IEnumerable<string> Read(string user);

        void Wall(string bob);
    }
}
