namespace ConsoleTwiterTests.Users
{
    using System.Linq;

    using ConsoleTwitter;
    using ConsoleTwitter.Users;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class UserRepositoryTests
    {
        private IRepository<IUser> repository;
        private IUser bob;

        [SetUp]
        public void Setup()
        {
            this.repository = new UserRepository();
            this.bob = this.repository.Create("Bob");
        }

        [Test]
        public void GivenAnEmptyRepositoryWhenCreateIsCalledThenItCreatesANewUser()
        {
            this.bob.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithAliceAndBobWhenFindByIdentifierIsCalledUsingBobsHandleThenBobsUserIsReturned()
        {
            var user = this.repository.FindByIdentifier("Bob");

            user.UserHandle.Should().Be("Bob");
        }

        [Test]
        public void GivenARepositoryWithBobAsUserWhenCreateIsCalledForBobThenItDoesNotCreateANewUser()
        {
            this.repository.Create("Bob");

            ((UserRepository)this.repository).Users.Count().Should().Be(1);
        }
    }
}
