using System;
using NUnit.Framework;
using NSubstitute;

using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class MessageBrokerTests
    {
        private IRepository repository; 
        private MessageBroker broker;
        private IWall userWall;
        private User bob;
        private User alice;

        [SetUp]
        public void SetUp()
        {
            repository = Substitute.For<IRepository>(); 
            userWall = Substitute.For<IWall>();

            broker = new MessageBroker(repository);

            bob = new User("Bob", userWall);
            alice = new User("Alice", userWall);
        }

        [Test]
        public void GivenACommandReceiverWhenReadIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            broker.Read("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenFollowIsExecutedThenItCallsUserRepositoryTwiceToSearchForUserAndUserToFollow()
        {
            broker.Follow("Alice", "Bob");

            repository.Received().FindByIdentifier("Alice");
            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenWallIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            broker.Wall("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenPostIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Post("Bob", "message");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverAndThatTheUserPostingIsNotInTheSystemWhenPostIsExecutedThenItCallsRepositoryCreate()
        {
            repository.Create("Bob").Returns(new User("Bob", userWall));

            broker.Post("Bob", "message");

            repository.Received().Create("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenPostIsExecutedThenCallsPostOnUserWall()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Post("Bob", "message");

            userWall.Received().Post("message");
        }

        [Test]
        public void GivenACommandReceiverWhenPostIsExecutedThenCallsPostOnUserFollowers()
        {
            bob.AddFollower(alice);

            repository.FindByIdentifier("Bob").Returns(bob);
            repository.FindByIdentifier("Alice").Returns(alice);

            broker.Post("Bob", "message");

            userWall.Received(2).Post("message");
        }
    }
}

