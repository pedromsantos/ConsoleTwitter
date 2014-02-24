using System;
using NUnit.Framework;
using NSubstitute;
using FluentAssertions;

using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void GivenAUserWhenPostIsCalledThenItCallsPostOnWall()
        {
            var userWall = Substitute.For<IWall>();
            var bob = new User("Bob", userWall);

            bob.Post("message");

            userWall.Received().Post("message");
        }

        [Test]
        public void GivenAUserWhenAddFollowerIsCalledThenItAddsTheFollowerToItsListOfFollowers()
        {
            var userWall = Substitute.For<IWall>();
            var bob = new User("Bob", userWall);
            var alice = new User("Alice", userWall);

            bob.AddFollower(alice);

            bob.Followers.Should().Contain(alice);
        }
    }
}

