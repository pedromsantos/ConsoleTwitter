namespace ConsoleTwiterTests.Messages
{
    using System;

    using ConsoleTwitter.Messages;
    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WallMessageFormaterTests
    {
        private const string BobUserHandle = "Bob";
        private const string PostMessageText = "message";

        private IMessageFormater formater;
        private Message message;

        [SetUp]
        public void Setup()
        {
            SystemTime.Now = () => new DateTime(2000, 1, 1);

            var userWall = new UserWall();
            var bob = new User(BobUserHandle, userWall);
            this.message = new Message(bob, PostMessageText);
            this.formater = new WallMessageFormater(new MessageFormater(new ElapsedTimeMessageFormater()));
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledThenItFormatsTheMessageToProgramOutput()
        {
            var formatedMessage = this.formater.Format(this.message);

            formatedMessage.Should().Be(string.Format("{0} - {1} (0 seconds ago)", BobUserHandle, PostMessageText));
        }
    }
}
