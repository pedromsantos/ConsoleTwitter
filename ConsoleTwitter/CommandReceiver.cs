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

        public void Follow(string userHandle, string userHandleToFollow)
        {
            this.FindUserByHandle(userHandle);
            this.FindUserByHandle(userHandleToFollow);
        }

        public void Post(string userHandle, string message)
        {
            var user = this.FindUserByHandle(userHandle);

            if (user == null)
            {
                user = this.repository.Create(userHandle);
            }

            user.Post(message);
        }

        public void Read(string userHandle)
        {
            this.FindUserByHandle(userHandle);
        }

        public void Wall(string userHandle)
        {
            this.FindUserByHandle(userHandle);
        }

        private User FindUserByHandle(string userHandle)
        {
            return this.repository.FindByIdentifier(userHandle);
        }
    }
}

