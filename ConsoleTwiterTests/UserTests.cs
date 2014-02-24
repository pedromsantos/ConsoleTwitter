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
        private IWall userWall;
        private User bob;
        private User alice;

        [SetUp]
        public void Setup()
        {
            userWall = Substitute.For<IWall>();
            bob = new User("Bob", userWall);
            alice = new User("Alice", userWall);
        }

        [Test]
        public void GivenAUserWhenPostIsCalledThenItCallsPostOnWall()
        {
            bob.Post("message");

            userWall.Received().Post("message");
        }
            
        [Test]
        public void GivenAUserWhenAddFollowerIsCalledThenItAddsTheFollowerToItsListOfFollowers()
        {
            bob.AddFollower(alice);

            bob.Followers.Should().Contain(alice);
        }
    }
}

