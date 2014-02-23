namespace ConsoleTwiterTests
{
    using ConsoleTwitter;
    using FluentAssertions;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class CommandFactoryTests
    {
        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandRepresentigTheUserAction()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = factory.CreateCommand("user", null, null);
        
            command.Should ().BeAssignableTo<ICommand> ();
        }

        [Test]
        public void GivenInvalidArgumentsWhenCreateCommandIsCalledThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = factory.CreateCommand(string.Empty, null, null);
        
            command.Should ().BeAssignableTo<NullCommand> ();
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesACommandAssigningTheUsernameToTheCommand()
        {
            const string UserName = "user";
        
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = factory.CreateCommand(UserName, null, null);
        
            command.User.Should().Be(UserName);
        }

        [Test]
        public void GivenAUsernameAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandForTheAction()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = factory.CreateCommand("user", "->", "message");
        
            command.Should ().BeAssignableTo<PostCommand> ();
        }

        [Test]
        public void GivenAUsernameAPostActionAndAMessageWhenCreateCommandIsCalledThenItCreatesAPostCommandAssigningTheMessageToTheCommand()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = (PostCommand)factory.CreateCommand("user", "->" , "message");
        
            command.Message.Should().Be ("message");
        }

        [Test]
        public void GivenAUsernameWhenCreateCommandIsCalledThenItCreatesAReadCommandForTheAction()
        {
            const string Input = "user";
        
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = factory.CreateCommand("user", null, null);
        
            command.Should().BeAssignableTo<ReadCommand> ();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesAFollowCommandForTheAction()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = (FollowCommand)factory.CreateCommand("user", "follows", "user");
        
            command.Should ().BeAssignableTo<FollowCommand> ();
        }

        [Test]
        public void GivenAUsernameAFollowActionAndAUserToFollowWhenCreateCommandIsCalledThenItCreatesFollowCommandAssigningTheserToFollowToTheCommand()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = (FollowCommand)factory.CreateCommand("Alice", "follows", "Bob");
        
            command.UserToFollow.Should ().Be ("Bob");
        }

        [Test]
        public void GivenAUsernameAndAWallActionWhenCreateCommandIsCalledThenItCreatesAWallCommandForTheAction()
        {
            var receiverStub = Substitute.For<ICommandReceiver>();

            var factory = new CommandFactory(receiverStub);
        
            var command = (WallCommand)factory.CreateCommand("User", "wall", null);
        
            command.Should ().BeAssignableTo<WallCommand> ();
        }
    }
}
