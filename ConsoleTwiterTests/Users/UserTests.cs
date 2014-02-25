namespace ConsoleTwiterTests.Users
{
    using System;
    using System.Linq;

    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class UserTests
    {
        private const string BobUserHandle = "Bob";
        private const string AliceUserHandle = "Alice";
        private const string PostMessageText = "message";

        private IWall userWall;
        private User bob;
        private User alice;

        [SetUp]
        public void Setup()
        {
            this.userWall = Substitute.For<IWall>();
            this.bob = new User(BobUserHandle, this.userWall);
            this.alice = new User(AliceUserHandle, this.userWall);

            SystemTime.Now = () => new DateTime(2000, 1, 1);
        }

        [Test]
        public void GivenAUserWhenPostIsCalledThenItCallsPostOnWall()
        {
            this.bob.Post(PostMessageText);

            this.userWall.Received().Post(this.bob, PostMessageText);
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

        [Test]
        public void GivenBobHasPostsAndAliceHasPostsAndAliceFollowsBobWhenWeCallAllicesWallThenItAggregatesBobsPosts()
        {
            this.bob = new User(BobUserHandle, new UserWall());
            this.alice = new User(AliceUserHandle, new UserWall());

            this.alice.Post(this.ComposePostMessage(AliceUserHandle, PostMessageText));
            this.bob.Post(this.ComposePostMessage(BobUserHandle, PostMessageText));

            this.bob.AddFollower(this.alice);

            this.alice.Wall.Should().Contain(m => m.Body == this.ComposePostMessage(BobUserHandle, PostMessageText));
        }

        [Test]
        public void GivenBobHasPostsAndAliceHasPostsAndAliceFollowsBobWhenWeCallAllicesWallThenTheFirstMessageShouldBeTheNewest()
        {
            this.bob = new User(BobUserHandle, new UserWall());
            this.alice = new User(AliceUserHandle, new UserWall());

            this.alice.Post(this.ComposePostMessage(AliceUserHandle, PostMessageText));

            SystemTime.Now = () => new DateTime(2000, 1, 1).AddSeconds(1);

            this.bob.Post(this.ComposePostMessage(BobUserHandle, PostMessageText));

            this.bob.AddFollower(this.alice);

            var firstPost = this.alice.Wall.First();

            firstPost.Body.Should().Be(this.ComposePostMessage(BobUserHandle, PostMessageText));
        }

        private string ComposePostMessage(string userHandle, string message)
        {
            return string.Format("{0} {1}", userHandle, message);
        }
    }
}
