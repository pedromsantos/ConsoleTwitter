using System;
using NUnit.Framework;
using ConsoleTwitter;
using FluentAssertions;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class UserWallTests
    {
        [SetUp]
        public void Setup()
        {
            SystemTime.Now = () => new DateTime(2000,1, 1);
        }

        [Test]
        public void GivenAUserWallWhenPostIsCalledThenItSavesThePostMessage()
        {
            var userWall = new UserWall();
            var bob = new User("Bob", userWall);

            userWall.Post(bob, "my message");

            userWall.Posts(bob).Should().Contain(m => m.Body == "my message");
        }

        [Test]
        public void GivenAUserWallWhenPostsIsCalledThenItFiltersPostsForSpecifiedUser()
        {
            var userWall = new UserWall();
            var bob = new User("Bob", userWall);
            var alice = new User("Alice", userWall);

            userWall.Post(bob, "Bobs message");
            userWall.Post(alice, "Alices message");

            userWall.Posts(bob).Should().OnlyContain(m => m.Body == "Bobs message");
        }

        [Test]
        public void GivenAUserWallWhenPostIsCalledThenItTimestampsTheMessage()
        {
            var userWall = new UserWall();
            var bob = new User("Bob", userWall);

            userWall.Post(bob, "my message");

            userWall.Posts(bob).Should().Contain(m => m.Timestamp == SystemTime.Now());
        }
    }
}

