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
        public void GivenAReadCommandWheExecuteMethodIsCalledThenItCallsReadInTheCommandReceiver()
        {
            var receiver = Substitute.For<ICommandReceiver>();

            var command = new ReadCommand(receiver, "user");

            command.Execute();

            receiver.Received().Read("user");
        }
    }
}

