namespace ConsoleTwiterTests.Users
{
    using System;

    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class UserWallTests
    {
        private const string BobUserHandle = "Bob";
        private const string AliceUserHandle = "Alice";
        private const string PostMessageText = "message";

        [SetUp]
        public void Setup()
        {
            SystemTime.Now = () => new DateTime(2000, 1, 1);
        }

        [Test]
        public void GivenAUserWallWhenPostIsCalledThenItSavesThePostMessage()
        {
            var userWall = new UserWall();
            var bob = new User(BobUserHandle, userWall);

            userWall.Post(bob, PostMessageText);

            userWall.Posts(bob).Should().Contain(m => m.Body == PostMessageText);
        }

        [Test]
        public void GivenAUserWallWhenPostsIsCalledThenItFiltersPostsForSpecifiedUser()
        {
            var userWall = new UserWall();
            var bob = new User(BobUserHandle, userWall);
            var alice = new User(AliceUserHandle, userWall);

            userWall.Post(bob, string.Format("{0} {1}", BobUserHandle, PostMessageText));
            userWall.Post(alice, string.Format("{0} {1}", AliceUserHandle, PostMessageText));

            userWall.Posts(bob).Should().OnlyContain(m => m.Body == string.Format("{0} {1}", BobUserHandle, PostMessageText));
        }

        [Test]
        public void GivenAUserWallWhenPostIsCalledThenItTimestampsTheMessage()
        {
            var userWall = new UserWall();
            var bob = new User(BobUserHandle, userWall);

            userWall.Post(bob, PostMessageText);

            userWall.Posts(bob).Should().Contain(m => m.Timestamp == SystemTime.Now());
        }
    }
}
