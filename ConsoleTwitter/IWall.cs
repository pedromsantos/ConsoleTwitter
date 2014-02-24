using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public interface IWall
    {
        void Post(string message);

        IEnumerable<string> Wall { get; }

        IEnumerable<string> Posts { get; }
    }
    
}
