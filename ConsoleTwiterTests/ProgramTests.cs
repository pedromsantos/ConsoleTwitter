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
        private IMessageFormater formaterMock;

        private User bob;
        private IMessageFormater formater;

        [SetUp]
        public void SetUp()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1);

            consoleMock = Substitute.For<IConsole>();
            repositoryMock = Substitute.For<IRepository>(); 
            userWallMock = Substitute.For<IWall>();
            parserMock = Substitute.For<IInputParser>();
            brokerMock = Substitute.For<IMessageBroker>();
            formaterMock = Substitute.For<IMessageFormater>();

            bob = new User("Bob", userWallMock);

            formater = new OutputMessageFormater();
        }

        [Test]
        public void GivenTheUserTypesACommandWhenProcessUserInputIsCalledThenItInvokesParseOnInputParser()
        {
            consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(consoleMock, parserMock, formaterMock);

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

            var program = new Program(consoleMock, parser, formaterMock);

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

            var program = new Program(consoleMock, parser, formaterMock);

            program.ProcessUserInput();

            userWallMock.Received().Post(bob, "my message");
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

            var program = new Program(consoleMock, parser, formaterMock);

            program.ProcessUserInput();

            var user = repository.FindByIdentifier("Bob");

            user.Wall.Should().Contain(m => m.Body == "my message");
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

            var program = new Program(consoleMock, parser, formater);

            program.ProcessUserInput();

            consoleMock.Received().ConsoleWrite("message from charlie (0 seconds ago)");
        }

        [Test]
        [Category("Integration")]
        public void GivenAliceWantsToReadCharliesWallAndCharlieHasMessagesOnHisWallWhenProcessUserInputIsCalledThenTheProgramDisplaysCharliesWallMessagesAndHowLongHasCharliePostedEachMessage()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            var charlie = repository.Create("charlie");
            ((IWall)charlie).Post("message from charlie");

            consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(consoleMock, parser, formater);

            program.ProcessUserInput();

            consoleMock.Received().ConsoleWrite("message from charlie (0 seconds ago)");
        }

        [Test]
        [Category("Integration")]
        public void GivenAliceWantsToReadCharliesWallButCharlieHasNoMessagesOnHisWallWhenProcessUserInputIsCalledThenTheProgramDisplaysNothing()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(consoleMock, parser, formater);

            program.ProcessUserInput();

            consoleMock.DidNotReceive().ConsoleWrite("");
        }
            
        [Test]
        public void GivenTheUserTypesAnInvalidInputWhenProcessUserInputIsCalledThenItDoesNotExecuteCommand()
        {
            var program = new Program(consoleMock, parserMock, formaterMock);

            consoleMock.ConsoleRead().Returns("");

            parserMock.Parse(Arg.Any<string>()).Returns(new NullCommand());

            Action action = () => program.ProcessUserInput();

            action.ShouldNotThrow();
        }
    }
}

