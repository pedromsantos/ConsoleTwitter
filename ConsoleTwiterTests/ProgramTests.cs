namespace ConsoleTwiterTests
{
    using System;
    using System.Linq;

    using ConsoleTwitter;
    using ConsoleTwitter.Commands;
    using ConsoleTwitter.Messages;
    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

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
            SystemTime.Now = () => new DateTime(2000, 1, 1);

            this.consoleMock = Substitute.For<IConsole>();
            this.repositoryMock = Substitute.For<IRepository<IUser>>(); 
            this.userWallMock = Substitute.For<IWall>();
            this.parserMock = Substitute.For<IInputParser>();
            this.brokerMock = Substitute.For<IMessageBroker>();
            this.formaterFactoryMock = Substitute.For<IMessageFormaterFactory>();

            this.bob = new User("Bob", this.userWallMock);
        }

        [Test]
        public void GivenTheUserTypesACommandWhenProcessUserInputIsCalledThenItInvokesParseOnInputParser()
        {
            this.consoleMock.ConsoleRead().Returns("user input");

            var program = new Program(this.consoleMock, this.parserMock, this.formaterFactoryMock);

            program.ProcessUserInput();

            this.parserMock.Received().Parse("user input");
        }

        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenPostOnMessageBrokerIsInvokedIndirectely()
        {
            var commandFactory = new CommandFactory(this.brokerMock);
            var parser = new InputParser(commandFactory);

            this.consoleMock.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(this.consoleMock, parser, this.formaterFactoryMock);

            program.ProcessUserInput();

            this.brokerMock.Received().Post("Bob", "my message");
        }
            
        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenPostOnUserWallIsInvokedIndirectely()
        {
            var broker = new MessageBroker(this.repositoryMock);

            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            this.consoleMock.ConsoleRead().Returns("Bob -> my message");

            this.repositoryMock.FindByIdentifier("Bob").Returns(this.bob);

            var program = new Program(this.consoleMock, parser, this.formaterFactoryMock);

            program.ProcessUserInput();

            this.userWallMock.Received().Post(this.bob, "my message");
        }
            
        [Test]
        [Category("Integration")]
        public void GivenTheUserTypesAPostCommandWhenProcessUserInputIsCalledThenTheUserWallShouldContainPostedMessage()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            this.consoleMock.ConsoleRead().Returns("Bob -> my message");

            var program = new Program(this.consoleMock, parser, this.formaterFactoryMock);

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
            charlie.Post("message from charlie");

            this.consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(this.consoleMock, parser, new MessageFormaterFactory());

            program.ProcessUserInput();

            this.consoleMock.Received().ConsoleWrite("message from charlie (0 seconds ago)");
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
            charlie.Post("message from charlie");

            this.consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(this.consoleMock, parser, new MessageFormaterFactory());

            program.ProcessUserInput();

            this.consoleMock.Received().ConsoleWrite("message from charlie (0 seconds ago)");
        }

        [Test]
        [Category("Integration")]
        public void GivenAliceWantsToReadCharliesWallButCharlieHasNoMessagesOnHisWallWhenProcessUserInputIsCalledThenTheProgramDisplaysNothing()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            this.consoleMock.ConsoleRead().Returns("charlie");

            var program = new Program(this.consoleMock, parser, this.formaterFactoryMock);

            program.ProcessUserInput();

            this.consoleMock.DidNotReceive().ConsoleWrite(string.Empty);
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

            this.consoleMock.ConsoleRead().Returns("charlie follows alice");

            var program = new Program(this.consoleMock, parser, this.formaterFactoryMock);

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

            var program = new Program(this.consoleMock, parser, new MessageFormaterFactory());

            this.consoleMock.ConsoleRead().Returns("charlie follows alice");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("charlie -> message from charlie");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("alice -> message from alice");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("charlie wall");
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

            var program = new Program(this.consoleMock, parser, new MessageFormaterFactory());

            this.consoleMock.ConsoleRead().Returns("charlie follows alice");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("charlie -> message from charlie");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("alice -> message from alice");
            program.ProcessUserInput();

            this.consoleMock.ConsoleRead().Returns("charlie wall");
            program.ProcessUserInput();

            this.consoleMock.Received().ConsoleWrite("charlie - message from charlie (0 seconds ago)");
            this.consoleMock.Received().ConsoleWrite("alice - message from alice (0 seconds ago)");
        }

        [Test]
        public void GivenTheUserTypesAnInvalidInputWhenProcessUserInputIsCalledThenItDoesNotExecuteCommand()
        {
            var program = new Program(this.consoleMock, this.parserMock, this.formaterFactoryMock);

            this.consoleMock.ConsoleRead().Returns(string.Empty);

            this.parserMock.Parse(Arg.Any<string>()).Returns(new NullCommand());

            Action action = () => program.ProcessUserInput();

            action.ShouldNotThrow();
        }
    }
}
