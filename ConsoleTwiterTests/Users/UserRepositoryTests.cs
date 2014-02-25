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
        private const string BobUserHandle = "Bob";

        private IRepository<IUser> repository;
        private IUser bob;

        [SetUp]
        public void Setup()
        {
            this.repository = new UserRepository();
            this.bob = this.repository.Create(BobUserHandle);
        }

        [Test]
        public void GivenAnEmptyRepositoryWhenCreateIsCalledThenItCreatesANewUser()
        {
            this.bob.UserHandle.Should().Be(BobUserHandle);
        }

        [Test]
        public void GivenARepositoryWithAliceAndBobWhenFindByIdentifierIsCalledUsingBobsHandleThenBobsUserIsReturned()
        {
            var user = this.repository.FindByIdentifier(BobUserHandle);

            user.UserHandle.Should().Be(BobUserHandle);
        }

        [Test]
        public void GivenARepositoryWithBobAsUserWhenCreateIsCalledForBobThenItDoesNotCreateANewUser()
        {
            this.repository.Create(BobUserHandle);

            ((UserRepository)this.repository).Users.Count().Should().Be(1);
        }
    }
}
