using System;
using NUnit.Framework;
using NSubstitute;

using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItCallsReadInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new ReadCommand(receiver, "Bob");

            command.Execute();

            receiver.Received().Read("Bob");
        }

        [Test]
        public void GivenAPostCommandWhenExecuteMethodIsCalledThenItCallsPostInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new PostCommand(receiver, "Bob", "message");

            command.Execute();

            receiver.Received().Post("Bob", "message");
        }

        [Test]
        public void GivenAFollowCommandWhenExecuteMethodIsCalledThenItCallsFollowInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new FollowCommand(receiver, "Alice", "Bob");

            command.Execute();

            receiver.Received().Follow("Alice", "Bob");
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItCallsWallInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new WallCommand(receiver, "Bob");

            command.Execute();

            receiver.Received().Wall("Bob");
        }
    }
}

