namespace ConsoleTwiterTests
{
    using ConsoleTwitter;
    using FluentAssertions;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class CommandFactoryTests
    {
        private IMessageBroker receiver;
        private CommandFactory factory;

        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<IMessageBroker>();

            factory = new CommandFactory(receiver);
        }
            
        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandRepresentigTheUserAction()
        {
            var command = factory.CreateCommand("user", null, null);
        
            command.Should ().BeAssignableTo<ICommand> ();
        }

        [Test]
        public void GivenInvalidArgumentsWhenCreateCommandIsCalledThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var command = factory.CreateCommand(string.Empty, null, null);
        
            command.Should ().BeAssignableTo<NullCommand> ();
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandAssigningTheUsernameToTheCommand()
        {
            const string UserName = "user";
        
            var command = factory.CreateCommand(UserName, null, null);
        
            command.User.Should().Be(UserName);
        }

        [Test]
        public void GivenAUsernameAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandForTheAction()
        {
            var command = factory.CreateCommand("user", "->", new[] { "message" });
        
            command.Should ().BeAssignableTo<PostCommand> ();
        }

        [Test]
        public void GivenAUsernameAPostActionAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandAssigningTheMessageToTheCommand()
        {
            var command = (PostCommand)factory.CreateCommand("user", "->" , new[] { "message" });
        
            command.Message.Should().Be ("message");
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesAReadCommandForTheAction()
        {
            const string Input = "user";
        
            var command = factory.CreateCommand("user", null, null);
        
            command.Should().BeAssignableTo<ReadCommand> ();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesAFollowCommandForTheAction()
        {
            var command = (FollowCommand)factory.CreateCommand("user", "follows", new [] { "user" });
        
            command.Should ().BeAssignableTo<FollowCommand> ();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesFollowCommandAssigningTheserToFollowToTheCommand()
        {
            var command = (FollowCommand)factory.CreateCommand("Alice", "follows", new [] { "Bob" });
        
            command.UserToFollow.Should ().Be ("Bob");
        }

        [Test]
        public void GivenAUsernameAndAWallActionWhenCreateCommandIsCalledThenItCreatesAWallCommandForTheAction()
        {
            var command = (WallCommand)factory.CreateCommand("User", "wall", null);
        
            command.Should ().BeAssignableTo<WallCommand> ();
        }
    }
}
