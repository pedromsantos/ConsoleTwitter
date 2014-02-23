using System;
using System.Collections.Generic;

namespace ConsoleTwitter
{
    public class User : IUserWall
    {
        private IUserWall userWall;

        public User(string userHandle, IUserWall userWall)
        {
            this.userWall = userWall;
            this.UserHandle = userHandle;
        }

        public string UserHandle { get; private set; }

        public void AddMessage(string message)
        {
            this.userWall.AddMessage(message);
        }
    }
}

