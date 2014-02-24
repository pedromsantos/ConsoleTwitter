using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleTwitter
{
    public class MessageBroker : IMessageBroker
    {
        IRepository repository;

        public MessageBroker(IRepository repository)
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

            user.Followers.ToList().ForEach(u => u.Post(message));
        }

        public IEnumerable<Message> Read(string userHandle)
        {
            var user = this.FindUserByHandle(userHandle);

            if (user == null)
            {
                return new Collection<Message> { };
            }

            return user.Posts();
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

