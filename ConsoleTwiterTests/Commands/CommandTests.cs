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
        private IMessageBroker receiver;

        [SetUp]
        public void Setup()
        {
            this.receiver = Substitute.For<IMessageBroker>();
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItCallsReadInTheCommandReceiver()
        {
            var command = new ReadCommand(this.receiver, "Bob");

            command.Execute();

            this.receiver.Received().Read("Bob");
        }

        [Test]
        public void GivenAReadCommandWhenExecuteMethodIsCalledThenItStoresTheExecutionResultInResults()
        {
            var command = new ReadCommand(this.receiver, "Bob");

            this.receiver.Read("Bob").Returns(new[] { new Message(null, "message") });

            command.Execute();

            ((IQueryCommand)command).Results.Should().Contain(m => m.Body == "message");
        }

        [Test]
        public void GivenAPostCommandWhenExecuteMethodIsCalledThenItCallsPostInTheCommandReceiver()
        {
            var command = new PostCommand(this.receiver, "Bob", "message");

            command.Execute();

            this.receiver.Received().Post("Bob", "message");
        }

        [Test]
        public void GivenAFollowCommandWhenExecuteMethodIsCalledThenItCallsFollowInTheCommandReceiver()
        {
            var command = new FollowCommand(this.receiver, "Alice", "Bob");

            command.Execute();

            this.receiver.Received().Follow("Alice", "Bob");
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItCallsWallInTheCommandReceiver()
        {
            var command = new WallCommand(this.receiver, "Bob");

            command.Execute();

            this.receiver.Received().Wall("Bob");
        }

        [Test]
        public void GivenAWallCommandWhenExecuteMethodIsCalledThenItStoresTheExecutionResultInResults()
        {
            var command = new WallCommand(this.receiver, "Bob");

            this.receiver.Wall("Bob").Returns(new[] { new Message(null, "message") });

            command.Execute();

            ((IQueryCommand)command).Results.Should().Contain(m => m.Body == "message");
        }
    }
}
