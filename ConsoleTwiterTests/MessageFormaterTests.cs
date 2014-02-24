using System;
using NUnit.Framework;

using ConsoleTwitter;
using FluentAssertions;


namespace ConsoleTwiterTests
{
    [TestFixture]
    public class MessageFormaterTests
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
            formater = new OutputMessageFormater();
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledThenItFormatsTheMessageToProgramOutput()
        {
            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (0 seconds ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd5SecondsPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddSeconds(5);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (5 seconds ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd1SecondPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddSeconds(1);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (1 second ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd1MinutePassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddSeconds(60);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (1 minute ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd5MinutesPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddMinutes(5);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (5 minutes ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd1HourPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddMinutes(60);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (1 hour ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd5hoursPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddHours(5);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (5 hours ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd1MonthPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddMonths(1);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (1 month ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd5MonthsPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddMonths(5);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (5 months ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd1YearPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddMonths(12);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (1 year ago)");
        }

        [Test]
        public void GivenAMessageWhenFormatIsCalledAnd5YearsPassedThenItFormatsTheMessageToProgramOutput()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1).AddYears(5);

            var formatedMessage = formater.Format(message);

            formatedMessage.Should().Be("Bob's message (5 years ago)");
        }
    }
}

