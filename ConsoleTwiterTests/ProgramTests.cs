using System;

using NUnit.Framework;
using NSubstitute;
using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class ProgramTests
    {
        private IConsole consoleMock;
        private IInputParser parserMock;
        private IRepository repositoryMock; 
        private IMessageBroker brokerMock;
        private IWall userWallMock;

        private User bob;

        [SetUp]
        public void SetUp()
        {
            consoleMock = Substitute.For<IConsole>();
            repositoryMock = Substitute.For<IRepository>(); 
            userWallMock = Substitute.For<IWall>();
            parserMock = Substitute.For<IInputParser>();
            brokerMock = Substitute.For<IMessageBroker>();

            bob = new User("Bob", userWallMock);
        }

        [Test]
        public void GivenTheUserTypesACommandWhenTheProgramReadsTheCommandThenItInvokesParseOnInputParser()
        {
            consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(consoleMock, parserMock);

            program.Start();

            parserMock.Received().Parse("user input");
        }

        [Test]
        public void GivenTheUserTypesAPostCommandWhenTheProgramExecutesThenItCallsPostOnMessageBroker()
        {
            var commandFactory = new CommandFactory(brokerMock);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(consoleMock, parser);

            program.Start();

            brokerMock.Received().Post("Bob", "my message");
        }
            
        [Test]
        public void GivenTheUserTypesAPostCommandWhenTheProgramExecutesThenItCallsPostOnUserWall()
        {
            var broker = new MessageBroker(repositoryMock);

            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("Bob -> my message");

            repositoryMock.FindByIdentifier("Bob").Returns(bob);

            var program = new Program(consoleMock, parser);

            program.Start();

            userWallMock.Received().Post("my message");
        }
    }
}

