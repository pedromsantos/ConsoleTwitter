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
        public void GivenACommandReceiverWhenPostIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            receiver.Post("Bob", "message");

            repository.Received().FindByIdentifier("Bob");
        }
    }
}

