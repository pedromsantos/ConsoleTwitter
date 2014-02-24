using System;
using NUnit.Framework;

using ConsoleTwitter;
using FluentAssertions;
using NSubstitute;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class MessageFormaterFactoryTests
    { 
        private IMessageBroker brokerMock;

        [SetUp]
        public void SetUp()
        {
            brokerMock = Substitute.For<IMessageBroker>();
        }

        [Test]
        public void GivenAReadCommandWhenCreateFormaterForCommandIsInvokedOnFactoryThenItReturnsAMessageFromater()
        {
            var command = new ReadCommand(brokerMock, "Bob");
            var factory = new MessageFormaterFactory();

            var formater = factory.CreateFormaterForCommand(command);

            formater.Should().BeAssignableTo<MessageFormater>();
        }

        [Test]
        public void GivenAWallCommandWhenCreateFormaterForCommandIsInvokedOnFactoryThenItReturnsAWallMessageFromater()
        {
            var command = new WallCommand(brokerMock, "Bob");
            var factory = new MessageFormaterFactory();

            var formater = factory.CreateFormaterForCommand(command);

            formater.Should().BeAssignableTo<WallMessageFormater>();
        }
    }
}

