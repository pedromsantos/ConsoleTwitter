using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public interface IUser : IWall
    {
        string UserHandle { get; }

        IEnumerable<IUser> Followers { get; }

        void AddFollower(IUser user);
    }
    
}
