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
            var consoleMock = Substitute.For<IConsole>();
            var parserMock = Substitute.For<IInputParser>();

            consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(consoleMock, parserMock);

            program.Start();

            parserMock.Received().Parse("user input");
        }
    }
}

