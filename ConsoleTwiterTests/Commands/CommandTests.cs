using System;
using NUnit.Framework;
using NSubstitute;

using ConsoleTwitter;
using FluentAssertions;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class CommandTests
    {
        private IMessageBroker receiver;

        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<IMessageBroker>();
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItCallsReadInTheCommandReceiver()
        {
            var command = new ReadCommand(receiver, "Bob");

            command.Execute();

            receiver.Received().Read("Bob");
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItStoresTheExecutionResultInResults()
        {
            var command = new ReadCommand(receiver, "Bob");

            receiver.Read("Bob").Returns(new [] { new Message(null, "message") });

            command.Execute();

            ((IQueryCommand)command).Results.Should().Contain(m => m.Body == "message");
        }

        [Test]
        public void GivenAPostCommandWhenExecuteMethodIsCalledThenItCallsPostInTheCommandReceiver()
        {
            var command = new PostCommand(receiver, "Bob", "message");

            command.Execute();

            receiver.Received().Post("Bob", "message");
        }

        [Test]
        public void GivenAFollowCommandWhenExecuteMethodIsCalledThenItCallsFollowInTheCommandReceiver()
        {
            var command = new FollowCommand(receiver, "Alice", "Bob");

            command.Execute();

            receiver.Received().Follow("Alice", "Bob");
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItCallsWallInTheCommandReceiver()
        {
            var command = new WallCommand(receiver, "Bob");

            command.Execute();

            receiver.Received().Wall("Bob");
        }
    }
}

