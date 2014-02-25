namespace ConsoleTwiterTests.Commands
{
    using ConsoleTwitter;
    using ConsoleTwitter.Commands;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class CommandFactoryTests
    {
        private IMessageBroker receiver;
        private CommandFactory factory;

        [SetUp]
        public void Setup()
        {
            this.receiver = Substitute.For<IMessageBroker>();

            this.factory = new CommandFactory(this.receiver);
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandRepresentigTheUserAction()
        {
            var command = this.factory.CreateCommand("user", null, null);

            command.Should().BeAssignableTo<ICommand>();
        }

        [Test]
        public void GivenInvalidArgumentsWhenCreateCommandIsCalledThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var command = this.factory.CreateCommand(string.Empty, null, null);

            command.Should().BeAssignableTo<NullCommand>();
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandAssigningTheUsernameToTheCommand()
        {
            const string UserName = "user";

            var command = this.factory.CreateCommand(UserName, null, null);

            command.User.Should().Be(UserName);
        }

        [Test]
        public void GivenAUsernameAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandForTheAction()
        {
            var command = this.factory.CreateCommand("user", "->", new[] { "message" });

            command.Should().BeAssignableTo<PostCommand>();
        }

        [Test]
        public void GivenAUsernameAPostActionAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandAssigningTheMessageToTheCommand()
        {
            var command = (PostCommand)this.factory.CreateCommand("user", "->", new[] { "message" });

            command.Message.Should().Be("message");
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesAReadCommandForTheAction()
        {
            var command = this.factory.CreateCommand("user", null, null);

            command.Should().BeAssignableTo<ReadCommand>();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesAFollowCommandForTheAction()
        {
            var command = (FollowCommand)this.factory.CreateCommand("user", "follows", new[] { "user" });

            command.Should().BeAssignableTo<FollowCommand>();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesFollowCommandAssigningTheserToFollowToTheCommand()
        {
            var command = (FollowCommand)this.factory.CreateCommand("Alice", "follows", new[] { "Bob" });

            command.UserToFollow.Should().Be("Bob");
        }

        [Test]
        public void GivenAUsernameAndAWallActionWhenCreateCommandIsCalledThenItCreatesAWallCommandForTheAction()
        {
            var command = (WallCommand)this.factory.CreateCommand("User", "wall", null);

            command.Should().BeAssignableTo<WallCommand>();
        }
    }
}
