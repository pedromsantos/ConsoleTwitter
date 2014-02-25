namespace ConsoleTwiterTests.Commands
{
    using ConsoleTwitter;
    using ConsoleTwitter.Commands;
    using ConsoleTwitter.Messages;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class CommandTests
    {
        private const string BobUserHandle = "Bob";
        private const string AliceUserHandle = "Alice";
        private const string PostMessageText = "message";

        private IMessageBroker receiver;

        [SetUp]
        public void Setup()
        {
            this.receiver = Substitute.For<IMessageBroker>();
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItCallsReadInTheCommandReceiver()
        {
            var command = new ReadCommand(this.receiver, BobUserHandle);

            command.Execute();

            this.receiver.Received().Read(BobUserHandle);
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItStoresTheExecutionResultInResults()
        {
            var command = new ReadCommand(this.receiver, BobUserHandle);

            this.receiver.Read(BobUserHandle).Returns(new[] { new Message(null, PostMessageText) });

            command.Execute();

            ((IQueryCommand)command).Results.Should().Contain(m => m.Body == PostMessageText);
        }

        [Test]
        public void GivenAPostCommandWhenExecuteMethodIsCalledThenItCallsPostInTheCommandReceiver()
        {
            var command = new PostCommand(this.receiver, BobUserHandle, PostMessageText);

            command.Execute();

            this.receiver.Received().Post(BobUserHandle, PostMessageText);
        }

        [Test]
        public void GivenAFollowCommandWhenExecuteMethodIsCalledThenItCallsFollowInTheCommandReceiver()
        {
            var command = new FollowCommand(this.receiver, AliceUserHandle, BobUserHandle);

            command.Execute();

            this.receiver.Received().Follow(AliceUserHandle, BobUserHandle);
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItCallsWallInTheCommandReceiver()
        {
            var command = new WallCommand(this.receiver, BobUserHandle);

            command.Execute();

            this.receiver.Received().Wall(BobUserHandle);
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItStoresTheExecutionResultInResults()
        {
            var command = new WallCommand(this.receiver, BobUserHandle);

            this.receiver.Wall(BobUserHandle).Returns(new[] { new Message(null, PostMessageText) });

            command.Execute();

            ((IQueryCommand)command).Results.Should().Contain(m => m.Body == PostMessageText);
        }
    }
}
