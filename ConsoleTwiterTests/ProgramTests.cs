using System;

using NUnit.Framework;
using NSubstitute;
using ConsoleTwitter;
using FluentAssertions;

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
        public void GivenTheUserTypesACommandWhenProcessUserInputIsCalledThenItInvokesParseOnInputParser()
        {
            consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(consoleMock, parserMock);

            program.ProcessUserInput();

            parserMock.Received().Parse("user input");
        }

        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenPostOnMessageBrokerIsInvokedIndirectely()
        {
            var commandFactory = new CommandFactory(brokerMock);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(consoleMock, parser);

            program.ProcessUserInput();

            brokerMock.Received().Post("Bob", "my message");
        }
            
        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenPostOnUserWallIsInvokedIndirectely()
        {
            var broker = new MessageBroker(repositoryMock);

            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("Bob -> my message");

            repositoryMock.FindByIdentifier("Bob").Returns(bob);

            var program = new Program(consoleMock, parser);

            program.ProcessUserInput();

            userWallMock.Received().Post("my message");
        }
            
        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenTheUserWallShouldContainPostedMessage()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(consoleMock, parser);

            program.ProcessUserInput();

            var user = repository.FindByIdentifier("Bob");

            user.Wall.Should().Contain("my message");
        }

        [Test]
        [Category("Integration")]
        public void GivenAliceWantsToReadCharliesWallAndCharlieHasMessagesOnHisWallWhenProcessUserInputIsCalledThenTheProgramDisplaysCharliesWallMessages()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            var charlie = repository.Create("charlie");
            ((IWall)charlie).Post("message from charlie");

            consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(consoleMock, parser);

            program.ProcessUserInput();

            consoleMock.Received().ConsoleWrite("message from charlie");
        }

        [Test]
        public void GivenTheUserTypesAnInvalidInputWhenProcessUserInputIsCalledThenItDoesNotExecuteCommand()
        {
            var program = new Program(consoleMock, parserMock);

            consoleMock.ConsoleRead().Returns("");

            var command = new NullCommand();
            parserMock.Parse("").Returns(command);

            Action action = () => program.ProcessUserInput();

            action.ShouldNotThrow();
        }
    }
}

