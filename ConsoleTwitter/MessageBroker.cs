using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleTwitter
{
    public class MessageBroker : IMessageBroker
    {
        IRepository<IUser> repository;

        public MessageBroker(IRepository<IUser> repository)
        {
            this.repository = repository;
        }

        public void Follow(string followerUserHandle, string followedUserHandle)
        {
            var follower = this.FindUserByHandle(followerUserHandle);
            var followed =this.FindUserByHandle(followedUserHandle);

            followed.AddFollower(follower);
        }

        public void Post(string userHandle, string message)
        {
            var user = this.FindUserByHandle(userHandle);

            if (user is NullUser)
            {
                user = this.repository.Create(userHandle);
            }

            user.Post(message);
        }

        public IEnumerable<Message> Read(string userHandle)
        {
            var user = this.FindUserByHandle(userHandle);

            return user.Posts();
        }

        public IEnumerable<Message> Wall(string userHandle)
        {
            var user = this.FindUserByHandle(userHandle);

            return user.Wall;
        }

        private IUser FindUserByHandle(string userHandle)
        {
            return this.repository.FindByIdentifier(userHandle);
        }
    }
}

