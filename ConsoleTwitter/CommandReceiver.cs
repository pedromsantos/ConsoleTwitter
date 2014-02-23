using System;

namespace ConsoleTwitter
{
    public class CommandReceiver : ICommandReceiver
    {
        IRepository repository;

        public CommandReceiver(IRepository repository)
        {
            this.repository = repository;
        }

        public void Follow(string user, string userToFollow)
        {
            this.repository.FindByIdentifier(user);
            this.repository.FindByIdentifier(userToFollow);
        }

        public void Post(string user, string message)
        {
            throw new NotImplementedException();
        }

        public void Read(string user)
        {
            this.repository.FindByIdentifier(user);
        }

        public void Wall(string bob)
        {
            throw new NotImplementedException();
        }
    }
}

