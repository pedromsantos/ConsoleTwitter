using System;
using NUnit.Framework;
using FluentAssertions;

using ConsoleTwitter;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void GivenAnEmptyRepositoryWhenCreateIsCalledThenItCreatesANewUser()
        {
            var repository = new UserRepository();

            var user = repository.Create("Bob");

            user.UserHandle.Should().Be("Bob");
        }
    }
}

