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
        IRepository<IUser> repository;
        IUser bob;

        [SetUp]
        public void Setup()
        {
            repository = new UserRepository();
            bob = repository.Create("Bob");
        }

        [Test]
        public void GivenAnEmptyRepositoryWhenCreateIsCalledThenItCreatesANewUser()
        {
            bob.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithAliceAndBobWhenFindByIdentifierIsCalledUsingBobsHandleThenBobsUserIsReturned()
        {
            var user = repository.FindByIdentifier("Bob");

            user.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithBobAsUserWhenCreateIsCalledForBobThenItDoesNotCreateANewUser()
        {
            repository.Create("Bob");

            ((UserRepository)repository).Users.Count().Should().Be(1);
        }
    }
}

