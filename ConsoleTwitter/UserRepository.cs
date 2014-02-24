using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserRepository : IRepository
    {
        private IList<User> users = new List<User>();

        public UserRepository()
        {
        }

        public IEnumerable<User> Users 
        {
            get 
            {
                return this.users;
            }
        }

        public User FindByIdentifier(string identifier)
        {
            return users.FirstOrDefault(u => u.UserHandle == identifier);
        }

        public User Create(string identifier)
        {
            var user = FindByIdentifier(identifier);

            if (user == null)
            {
                user = new User(identifier, new UserWall());
                users.Add(user);
            }

            return user;
        }
    }
}

