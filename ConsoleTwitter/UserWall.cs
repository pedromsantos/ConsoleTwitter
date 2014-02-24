using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserWall : IWall
    {
        private readonly IList<string> internalMessages;

        public UserWall()
        {
            internalMessages = new List<string>();
        }
            
        public void Post(string message)
        {
            internalMessages.Add(message);
        }

        public IEnumerable<string> Wall
        {
            get 
            {
                return internalMessages.Skip(0);
            }
        }

        public IEnumerable<string> Posts
        {
            get
            {
                return internalMessages.Skip(0);
            }
        }
    }
}

