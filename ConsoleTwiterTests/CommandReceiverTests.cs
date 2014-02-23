using System;
using NUnit.Framework;
using NSubstitute;

using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class CommandReceiverTests
    {
        [Test]
        public void GivenACommandReceiverWhenReadIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            receiver.Read("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenFollowIsExecutedThenItCallsUserRepositoryTwiceToSearchForUserAndUserToFollow()
        {
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            receiver.Follow("Alice", "Bob");

            repository.Received().FindByIdentifier("Alice");
            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenWallIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            var repository = Substitute.For<IRepository>();
            var receiver = new CommandReceiver(repository);

            receiver.Wall("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenPostIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            var userWall = Substitute.For<IWall>();
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            repository.FindByIdentifier("Bob").Returns(new User("Bob", userWall));

            receiver.Post("Bob", "message");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenACommandReceiverAndThatTheUserPostingIsNotInTheSystemWhenPostIsExecutedThenItCallsRepositoryCreate()
        {
            var userWall = Substitute.For<IWall>();
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            repository.Create("Bob").Returns(new User("Bob", userWall));

            receiver.Post("Bob", "message");

            repository.Received().Create("Bob");
        }

        [Test]
        public void GivenACommandReceiverWhenPostIsExecutedThenCallsPostOnUserWall()
        {
            var userWall = Substitute.For<IWall>();
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            repository.FindByIdentifier("Bob").Returns(new User("Bob", userWall));

            receiver.Post("Bob", "message");

            userWall.Received().Post("message");
        }
    }
}

