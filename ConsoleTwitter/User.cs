using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public class User : IWall
    {
        private IWall wall;

        public User(string userHandle, IWall wall)
        {
            this.wall = wall;
            this.UserHandle = userHandle;
        }

        public string UserHandle { get; private set; }

        public void Post(string message)
        {
            this.wall.Post(message);
        }
    }
}

