namespace ConsoleTwiterTests.Messages
{
    using System;

    using ConsoleTwitter;
    using ConsoleTwitter.Messages;
    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class MessageFormaterTests
    {
        private IMessageFormater formater;
        private Message message;

        [SetUp]
        public void Setup()
        {
            SystemTime.Now = () => new DateTime(2000, 1, 1);

            var userWall = new UserWall();
            var bob = new User("Bob", userWall);
            this.message = new Message(bob, "Bob's message");
            this.formater = new MessageFormater(new ElapsedTimeMessageFormater());
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledThenItFormatsTheMessageToProgramOutput()
        {
            var formatedMessage = this.formater.Format(this.message);

            formatedMessage.Should().Be("Bob's message (0 seconds ago)");
        }
    }
}
