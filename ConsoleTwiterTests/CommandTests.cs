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

            var command = new ReadCommand(receiver, "user");

            command.Execute();

            receiver.Received().Read("user");
        }

        [Test]
        public void GivenAPostCommandWhenExecuteMethodIsCalledThenItCallsPostInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new PostCommand(receiver, "user", "message");

            command.Execute();

            receiver.Received().Post("user", "message");
        }
    }
}

