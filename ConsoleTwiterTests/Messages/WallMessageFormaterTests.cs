using System;
using NUnit.Framework;
using ConsoleTwitter;
using FluentAssertions;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class WallMessageFormaterTests
    {
        private IMessageFormater formater;
        private Message message;

        [SetUp]
        public void Setup()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1);

            var userWall = new UserWall();
            var bob = new User("Bob", userWall);
            message = new Message(bob, "Bob's message");
            formater = new WallMessageFormater(new MessageFormater(new ElapsedTimeMessageFormater()));
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledThenItFormatsTheMessageToProgramOutput()
        {
            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob - Bob's message (0 seconds ago)");
        }
    }
}

