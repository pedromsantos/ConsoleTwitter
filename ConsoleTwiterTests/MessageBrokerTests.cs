using System;
using NUnit.Framework;
using NSubstitute;

using ConsoleTwitter;
using FluentAssertions;

namespace ConsoleTwiterTests
{
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
            repository = Substitute.For<IRepository<IUser>>(); 
            userWall = Substitute.For<IWall>();

            broker = new MessageBroker(repository);

            bob = new User("Bob", userWall);
            alice = new User("Alice", userWall);
        }

        [Test]
        public void GivenAMessageBrokerWhenReadIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Read("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenReadIsExecutedThenItCallsPostsOnUserWall()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Read("Bob");

            userWall.Received().Posts(bob);
        }

        [Test]
        public void GivenAMessageBrokerAndANonExistingUserWhenReadIsExecutedThenItReturnsNoMessages()
        {
            var result = broker.Read("Bob");

            result.Should().BeEmpty();
        }

        [Test]
        public void GivenAMessageBrokerWhenFollowIsExecutedThenItCallsUserRepositoryTwiceToSearchForUserAndUserToFollow()
        {
            broker.Follow("Alice", "Bob");

            repository.Received().FindByIdentifier("Alice");
            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenBobAndAliceAreUsersInTheSystemWhenBobFollowsAliceThenAliceFollowersShouldContainBob()
        {
            repository.FindByIdentifier("Bob").Returns(bob);
            repository.FindByIdentifier("Alice").Returns(alice);

            broker.Follow("Alice", "Bob");

            bob.Followers.Should().Contain(alice);
        }

        [Test]
        public void GivenAMessageBrokerWhenWallIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            broker.Wall("Bob");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenWallIsExecutedThenReturnsTheUserPostsAndThePostsOfItsFollowwers()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Wall("Bob");

            var tmp = userWall.Received().Wall;
        }

        [Test]
        public void GivenAMessageBrokerWhenPostIsExecutedThenItCallsUserRepositoryToSearchForUser()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Post("Bob", "message");

            repository.Received().FindByIdentifier("Bob");
        }

        [Test]
        public void GivenAMessageBrokerAndThatTheUserPostingIsNotInTheSystemWhenPostIsExecutedThenItCallsRepositoryCreate()
        {
            repository.FindByIdentifier("Bob").Returns(new NullUser());
            repository.Create("Bob").Returns(new User("Bob", userWall));

            broker.Post("Bob", "message");

            repository.Received().Create("Bob");
        }

        [Test]
        public void GivenAMessageBrokerWhenPostIsExecutedThenCallsPostOnUserWall()
        {
            repository.FindByIdentifier("Bob").Returns(bob);

            broker.Post("Bob", "message");

            userWall.Received().Post(bob, "message");
        }

        [Test]
        public void GivenAMessageBrokerWhenPostIsExecutedThenCallsPostOnUserFollowers()
        {
            bob.AddFollower(alice);

            repository.FindByIdentifier("Bob").Returns(bob);
            repository.FindByIdentifier("Alice").Returns(alice);

            broker.Post("Bob", "message");

            userWall.Received().Post(bob, "message");
            userWall.Received().Post(alice, "message");
        }
    }
}

