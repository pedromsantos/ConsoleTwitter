using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserRepository : IRepository
    {
        private static IList<User> users = new List<User>();

        public UserRepository()
        {
        }

        public User FindByIdentifier(string identifier)
        {
            return users.FirstOrDefault(u => u.UserHandle == identifier);
        }

        public User Create(string identifier)
        {
            var user = new User(identifier, new UserWall());
            users.Add(user);

            return user;
        }
    }
}

