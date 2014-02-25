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
        private const string BobUserHandle = "Bob";
        private const string AliceUserHandle = "Alice";
        private const string PostMessageText = "message";

        private const string PostCommandToken = "->";
        private const string WallCommandToken = "wall";
        private const string FollowsCommandToken = "follows";

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
            var command = this.factory.CreateCommand(BobUserHandle);

            command.Should().BeAssignableTo<ICommand>();
        }

        [Test]
        public void GivenInvalidArgumentsWhenCreateCommandIsCalledThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var command = this.factory.CreateCommand(string.Empty);

            command.Should().BeAssignableTo<NullCommand>();
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandAssigningTheUsernameToTheCommand()
        {
            const string UserName = BobUserHandle;

            var command = this.factory.CreateCommand(UserName);

            command.User.Should().Be(UserName);
        }

        [Test]
        public void GivenAUsernameAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandForTheAction()
        {
            var command = this.factory.CreateCommand(BobUserHandle, PostCommandToken, new[] { PostMessageText });

            command.Should().BeAssignableTo<PostCommand>();
        }

        [Test]
        public void GivenAUsernameAPostActionAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandAssigningTheMessageToTheCommand()
        {
            var command = (PostCommand)this.factory.CreateCommand(BobUserHandle, PostCommandToken, new[] { PostMessageText });

            command.Message.Should().Be(PostMessageText);
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesAReadCommandForTheAction()
        {
            var command = this.factory.CreateCommand(BobUserHandle);

            command.Should().BeAssignableTo<ReadCommand>();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesAFollowCommandForTheAction()
        {
            var command = (FollowCommand)this.factory.CreateCommand(BobUserHandle, FollowsCommandToken, new[] { AliceUserHandle });

            command.Should().BeAssignableTo<FollowCommand>();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesFollowCommandAssigningTheserToFollowToTheCommand()
        {
            var command = (FollowCommand)this.factory.CreateCommand(AliceUserHandle, FollowsCommandToken, new[] { BobUserHandle });

            command.UserToFollow.Should().Be(BobUserHandle);
        }

        [Test]
        public void GivenAUsernameAndAWallActionWhenCreateCommandIsCalledThenItCreatesAWallCommandForTheAction()
        {
            var command = (WallCommand)this.factory.CreateCommand(AliceUserHandle, WallCommandToken);

            command.Should().BeAssignableTo<WallCommand>();
        }
    }
}
