using System;

using NUnit.Framework;
using NSubstitute;
using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void GivenTheUserTypesACommandWhenTheProgramReadsTheCommandThenItInvokesParseOnInputParser()
        {
            var console = Substitute.For<IConsole>();
            var parser = Substitute.For<IInputParser>();

            console.ConsoleRead().Returns("user input");

            var program = new Program(console, parser);

            program.Start();

            parser.Received().Parse("user input");
        }

        [Test]
        public void GivenTheUserTypesAPostCommandWhenTheProgramExecutesThenItCallsPostOnMessageBroker()
        {
            var console = Substitute.For<IConsole>();
            var broker = Substitute.For<IMessageBroker>();

            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            console.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(console, parser);

            program.Start();

            broker.Received().Post("Bob", "my message");
        }
    }
}

