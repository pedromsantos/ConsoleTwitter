namespace ConsoleTwiterTests.Messages
{
    using ConsoleTwitter;
    using ConsoleTwitter.Commands;
    using ConsoleTwitter.Messages;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class MessageFormaterFactoryTests
    { 
        private IMessageBroker brokerMock;

        [SetUp]
        public void SetUp()
        {
            this.brokerMock = Substitute.For<IMessageBroker>();
        }

        [Test]
        public void GivenAReadCommandWhenCreateFormaterForCommandIsInvokedOnFactoryThenItReturnsAMessageFromater()
        {
            var command = new ReadCommand(this.brokerMock, "Bob");
            var factory = new MessageFormaterFactory();

            var formater = factory.CreateFormaterForCommand(command);

            formater.Should().BeAssignableTo<MessageFormater>();
        }

        [Test]
        public void GivenAWallCommandWhenCreateFormaterForCommandIsInvokedOnFactoryThenItReturnsAWallMessageFromater()
        {
            var command = new WallCommand(this.brokerMock, "Bob");
            var factory = new MessageFormaterFactory();

            var formater = factory.CreateFormaterForCommand(command);

            formater.Should().BeAssignableTo<WallMessageFormater>();
        }
    }
}