using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly IList<IUser> internalUsers;

        public UserRepository()
        {
            internalUsers = new List<IUser>();
        }

        public IEnumerable<IUser> Users 
        {
            get 
            {
                return this.internalUsers.Skip(0);
            }
        }

        public IUser FindByIdentifier(string identifier)
        {
            var user = internalUsers.FirstOrDefault(u => u.UserHandle == identifier);

            return user != null ? (IUser)user : (IUser)new NullUser();
        }

        public IUser Create(string identifier)
        {
            var user = FindByIdentifier(identifier);

            if (user is NullUser)
            {
                user = new User(identifier, new UserWall());
                internalUsers.Add(user);
            }

            return user;
        }
    }
}

