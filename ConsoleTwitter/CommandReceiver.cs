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
            this.FindUserByHandle(userHandle);
        }

        public void Read(string userHandle)
        {
            this.FindUserByHandle(userHandle);
        }

        public void Wall(string userHandle)
        {
            this.FindUserByHandle(userHandle);
        }

        private void FindUserByHandle(string userHandle)
        {
            this.repository.FindByIdentifier(userHandle);
        }
    }
}

