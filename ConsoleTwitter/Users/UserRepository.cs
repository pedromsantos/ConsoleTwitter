namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : IRepository<IUser>
    {
        private readonly IList<IUser> internalUsers;

        public UserRepository()
        {
            this.internalUsers = new List<IUser>();
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
            var user = this.internalUsers.FirstOrDefault(u => u.UserHandle == identifier);

            return user ?? new NullUser();
        }

        public IUser Create(string identifier)
        {
            var user = this.FindByIdentifier(identifier);

            if (user is NullUser)
            {
                user = new User(identifier, new UserWall());
                this.internalUsers.Add(user);
            }

            return user;
        }
    }
}