using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public User Create(string identifier)
        {
            var user = new User(identifier, new UserWall());
            users.Add(user);

            return user;
        }
    }
}

