namespace ConsoleTwiterTests.Users
{
    using ConsoleTwitter.Users;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class UserTests
    {
        private IWall userWall;
        private User bob;
        private User alice;

        [SetUp]
        public void Setup()
        {
            this.userWall = Substitute.For<IWall>();
            this.bob = new User("Bob", this.userWall);
            this.alice = new User("Alice", this.userWall);
        }

        [Test]
        public void GivenAUserWhenPostIsCalledThenItCallsPostOnWall()
        {
            this.bob.Post("message");

            this.userWall.Received().Post(this.bob, "message");
        }

        [Test]
        public void GivenAUserWhenPostsIsCalledThenItCallsPostsOnWall()
        {
            this.bob.Posts();

            this.userWall.Received().Posts(this.bob);
        }

        [Test]
        public void GivenAUserWhenAddFollowerIsCalledThenItAddsTheFollowerToItsListOfFollowers()
        {
            this.bob.AddFollower(this.alice);

            this.bob.Followers.Should().Contain(this.alice);
        }

        [Test]
        public void GivenAUserWhenAddFolloweeIsCalledThenItAddsTheFolloweeToItsListOfFollowees()
        {
            this.bob.AddFollowee(this.alice);

            this.bob.Followees.Should().Contain(this.alice);
        }

        [Test]
        public void GivenAUserWhenAddFollowerIsCalledThenItAddsTheUserAsAFolloweeOfFollower()
        {
            this.bob.AddFollower(this.alice);

            this.alice.Followees.Should().Contain(this.bob);
        }
    }
}
