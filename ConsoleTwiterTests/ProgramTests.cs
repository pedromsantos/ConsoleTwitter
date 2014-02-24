using System;

using NUnit.Framework;
using NSubstitute;
using ConsoleTwitter;
using FluentAssertions;
using System.Linq;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class ProgramTests
    {
        private IConsole consoleMock;
        private IInputParser parserMock;
        private IRepository<IUser> repositoryMock; 
        private IMessageBroker brokerMock;
        private IWall userWallMock;
        private IMessageFormaterFactory formaterFactoryMock;

        private User bob;

        [SetUp]
        public void SetUp()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1);

            consoleMock = Substitute.For<IConsole>();
            repositoryMock = Substitute.For<IRepository<IUser>>(); 
            userWallMock = Substitute.For<IWall>();
            parserMock = Substitute.For<IInputParser>();
            brokerMock = Substitute.For<IMessageBroker>();
            formaterFactoryMock = Substitute.For<IMessageFormaterFactory>();

            bob = new User("Bob", userWallMock);
        }

        [Test]
        public void GivenTheUserTypesACommandWhenProcessUserInputIsCalledThenItInvokesParseOnInputParser()
        {
            consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(consoleMock, parserMock, formaterFactoryMock);

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

            var program = new Program(consoleMock, parser, formaterFactoryMock);

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

            var program = new Program(consoleMock, parser, formaterFactoryMock);

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

            var program = new Program(consoleMock, parser, formaterFactoryMock);

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

            var program = new Program(consoleMock, parser, new MessageFormaterFactory());

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

            var program = new Program(consoleMock, parser, new MessageFormaterFactory());

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

            var program = new Program(consoleMock, parser, formaterFactoryMock);

            program.ProcessUserInput();

            consoleMock.DidNotReceive().ConsoleWrite("");
        }

        [Test]
        [Category("Integration")]
        public void GivenCharlieAndAliceAreUsersInTheSystemWhenCharlieFollowsAliceThenAliceFollowersShouldContainCharlie()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            var charlie = repository.Create("charlie");
            var alice = repository.Create("alice");

            consoleMock.ConsoleRead().Returns("charlie follows alice");

            var program = new Program(consoleMock, parser, formaterFactoryMock);

            program.ProcessUserInput();

            alice.Followers.Should().Contain(charlie);
        }

        [Test]
        [Category("Integration")]
        public void GivenCharlieAndAliceAreUsersInTheSystemAndCharlieFollowsAliceWhenAWallCommandOnCharlieIsRequestedThenCharliesWallShouldContainPostsFromAliceAndCharlie()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            var charlie = repository.Create("charlie");
            repository.Create("alice");

            var program = new Program(consoleMock, parser, new MessageFormaterFactory());

            consoleMock.ConsoleRead().Returns("charlie follows alice");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("charlie -> message from charlie");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("alice -> message from alice");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("charlie wall");
            program.ProcessUserInput();

            charlie.Wall.Count().Should().Be(2);
        }

        [Test]
        [Category("Integration")]
        public void GivenCharlieAndAliceAreUsersInTheSystemAndCharlieFollowsAliceWhenAWallCommandOnCharlieIsRequestedThenItDisplayCharliesWallStatingTheAuthorOfThePosts()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            repository.Create("charlie");
            repository.Create("alice");

            var program = new Program(consoleMock, parser, new MessageFormaterFactory());

            consoleMock.ConsoleRead().Returns("charlie follows alice");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("charlie -> message from charlie");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("alice -> message from alice");
            program.ProcessUserInput();

            consoleMock.ConsoleRead().Returns("charlie wall");
            program.ProcessUserInput();

            consoleMock.Received().ConsoleWrite("charlie - message from charlie (0 seconds ago)");
            consoleMock.Received().ConsoleWrite("alice - message from alice (0 seconds ago)");
        }

        [Test]
        public void GivenTheUserTypesAnInvalidInputWhenProcessUserInputIsCalledThenItDoesNotExecuteCommand()
        {
            var program = new Program(consoleMock, parserMock, formaterFactoryMock);

            consoleMock.ConsoleRead().Returns("");

            parserMock.Parse(Arg.Any<string>()).Returns(new NullCommand());

            Action action = () => program.ProcessUserInput();

            action.ShouldNotThrow();
        }
    }
}

