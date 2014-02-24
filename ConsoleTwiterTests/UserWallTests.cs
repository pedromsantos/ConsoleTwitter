using System;
using NUnit.Framework;
using ConsoleTwitter;
using FluentAssertions;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class UserWallTests
    {
        [Test]
        public void GivenAUserWallWhenPostIsCalledThenItSavesThePostMessage()
        {
            var userWall = new UserWall();

            userWall.Post("my message");

            userWall.Wall.Should().Contain("my message");
        }
    }
}

