using System;
using NUnit.Framework;
using FluentAssertions;

using ConsoleTwitter;
using System.Linq;

namespace ConsoleTwiterTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void GivenAnEmptyRepositoryWhenCreateIsCalledThenItCreatesANewUser()
        {
            var repository = new UserRepository();

            var bob = repository.Create("Bob");

            bob.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithAliceAndBobWhenFindByIdentifierIsCalledUsingBobsHandleThenBobsUserIsReturned()
        {
            var repository = new UserRepository();

            repository.Create("Bob");

            var bob = repository.FindByIdentifier("Bob");

            bob.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithBobAsUserWhenCreateIsCalledForBobThenItDoesNotCreateANewUser()
        {
            var repository = new UserRepository();

            repository.Create("Bob");

            repository.Create("Bob");

            repository.Users.Count().Should().Be(1);
        }
    }
}

