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
        public void GivenACommandReceiverWhenReadIsExecutedThenItCallsUserRepositoryToSerchForUser()
        {
            var repository = Substitute.For<IRepository>(); 
            var receiver = new CommandReceiver(repository);

            receiver.Read("Bob");

            repository.Received().FindByIdentifier("Bob");
        }
    }
}

