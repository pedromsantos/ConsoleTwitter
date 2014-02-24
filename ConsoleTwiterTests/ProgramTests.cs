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
    }
}

