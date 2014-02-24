using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public interface IWall
    {
        void Post(string message);

        void Post(User user, string message);

        IEnumerable<Message> Wall { get; }

        IEnumerable<Message> Posts(User user);
    }
    
}
