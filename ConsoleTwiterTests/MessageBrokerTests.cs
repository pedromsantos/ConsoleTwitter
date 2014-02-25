namespace ConsoleTwiterTests
{
    using ConsoleTwitter;
    using ConsoleTwitter.Users;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class MessageBrokerTests
    {
        private IRepository<IUser> repository;
        private MessageBroker broker;
        private IWall userWall;
        private User bob;
        private User alice;

        [SetUp]
        public void SetUp()
        {
            this.repository = Substitute.For<IRepository<IUser>>();
            this.userWall = Substitute.For<IWall>();

            this.broker = new MessageBroker(this.repository);

            this.bob = new User("Bob", this.userWall);
            this.alice = new User("Alice", this.userWall);
        }

        [Test]
        public void GivenAMessageBrokerWhenReadIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);

            this.broker.Read("Bob");

            this.repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenReadIsExecutedThenItCallsPostsOnUserWall()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);

            this.broker.Read("Bob");

            this.userWall.Received().Posts(this.bob);
        }

        [Test]
        public void GivenAMessageBrokerAndANonExistingUserWhenReadIsExecutedThenItReturnsNoMessages()
        {
            var result = this.broker.Read("Bob");

            result.Should().BeEmpty();
        }

        [Test]
        public void GivenAMessageBrokerWhenFollowIsExecutedThenItCallsUserRepositoryTwiceToSearchForUserAndUserToFollow()
        {
            this.broker.Follow("Alice", "Bob");

            this.repository.Received().FindByIdentifier("Alice");
            this.repository.Received().FindByIdentifier("Bob");
        }

        public void GivenBobAndAliceAreUsersInTheSystemWhenBobFollowsAliceThenAliceFollowersShouldContainBob()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);
            this.repository.FindByIdentifier("Alice").Returns(this.alice);

            this.broker.Follow("Alice", "Bob");

            this.bob.Followers.Should().Contain(this.alice);
        }

        [Test]
        public void GivenAMessageBrokerWhenWallIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            this.broker.Wall("Bob");

            this.repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenWallIsExecutedThenReturnsTheUserPostsAndThePostsOfItsFollowwers()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);

            this.broker.Wall("Bob");

            var tmp = this.userWall.Received().Wall;
        }

        [Test]
        public void GivenAMessageBrokerWhenPostIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);

            this.broker.Post("Bob", "message");

            this.repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerAndThatTheUserPostingIsNotInTheSystemWhenPostIsExecutedThenItCallsRepositoryCreate()
        {
            this.repository.FindByIdentifier("Bob").Returns(new NullUser());
            this.repository.Create("Bob").Returns(new User("Bob", this.userWall));

            this.broker.Post("Bob", "message");

            this.repository.Received().Create("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenPostIsExecutedThenCallsPostOnUserWall()
        {
            this.repository.FindByIdentifier("Bob").Returns(this.bob);

            this.broker.Post("Bob", "message");

            this.userWall.Received().Post(this.bob, "message");
        }
    }
}
