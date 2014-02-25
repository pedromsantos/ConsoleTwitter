namespace ConsoleTwiterTests
{
    using System.Collections.Generic;
    using System.Linq;

    using ConsoleTwitter;
    using ConsoleTwitter.Commands;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class InputParserTests
    {
        private const string BobUserHandle = "Bob";
        private const string AliceUserHandle = "Alice";
        private const string PostMessageText = "message";
        private const string PostCommandToken = "->";
        private const string WallCommandToken = "wall";
        private const string FollowsCommandToken = "follows";

        private ICommandFactory factory;
        private InputParser parser;

        [SetUp]
        public void Setup()
        {
            this.factory = Substitute.For<ICommandFactory>();
            this.parser = new InputParser(this.factory);
        }

        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommand()
        {
            this.parser.Parse(BobUserHandle);
        
            this.factory.Received().CreateCommand(BobUserHandle, null, Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }

        [Test]
        public void GivenAnInvalidUserInputWhenParseIsCalledThenItCallsCreateCommandSettingAllArgumentsToNull()
        {
            this.parser.Parse(string.Empty);
        
            this.factory.Received().CreateCommand(string.Empty, null, Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }

        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsername()
        {
            const string UserName = BobUserHandle;
        
            this.parser.Parse(UserName);
        
            this.factory.Received().CreateCommand(BobUserHandle, null, Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }

        [Test]
        public void GivenAPostUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameThePostActionAndTheMessage()
        {
            var input = string.Format("{0} {1} {2}", BobUserHandle, PostCommandToken, PostMessageText);
        
            this.parser.Parse(input);
        
            this.factory.Received().CreateCommand(BobUserHandle, "->", Arg.Is<IEnumerable<string>>(messages => messages.All(message => message == PostMessageText)));
        }

        [Test]
        public void GivenAFollowsUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameTheFollowActionAndTheUserToFollow()
        {
            var input = string.Format("{0} {1} {2}", AliceUserHandle, FollowsCommandToken, BobUserHandle);
        
            this.parser.Parse(input);
        
            this.factory.Received().CreateCommand(AliceUserHandle, FollowsCommandToken, Arg.Is<IEnumerable<string>>(users => users.All(user => user == BobUserHandle)));
        }

        [Test]
        public void GivenAWallUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameAndTheWallAction()
        {
            var input = string.Format("{0} {1}", BobUserHandle, WallCommandToken);
        
            this.parser.Parse(input);
        
            this.factory.Received().CreateCommand(BobUserHandle, WallCommandToken, Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }
    }
}
