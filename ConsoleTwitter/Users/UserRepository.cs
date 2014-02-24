using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserRepository : IRepository
    {
        private readonly IList<User> internalUsers;

        public UserRepository()
        {
            internalUsers = new List<User>();
        }

        public IEnumerable<User> Users 
        {
            get 
            {
                return this.internalUsers.Skip(0);
            }
        }

        public User FindByIdentifier(string identifier)
        {
            return internalUsers.FirstOrDefault(u => u.UserHandle == identifier);
        }

        public User Create(string identifier)
        {
            var user = FindByIdentifier(identifier);

            if (user == null)
            {
                user = new User(identifier, new UserWall());
                internalUsers.Add(user);
            }

            return user;
        }
    }
}

