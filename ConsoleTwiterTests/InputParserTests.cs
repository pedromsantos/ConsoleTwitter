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
            this.parser.Parse("user");
        
            this.factory.Received().CreateCommand("user", null, Arg.Is<IEnumerable<string>>(users => !users.Any()));
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
            const string UserName = "user";
        
            this.parser.Parse(UserName);
        
            this.factory.Received().CreateCommand("user", null, Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }

        [Test]
        public void GivenAPostUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameThePostActionAndTheMessage()
        {
            const string Input = "user -> message";
        
            this.parser.Parse(Input);
        
            this.factory.Received().CreateCommand("user", "->", Arg.Is<IEnumerable<string>>(messages => messages.All(message => message == "message")));
        }

        [Test]
        public void GivenAFollowsUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameTheFollowActionAndTheUserToFollow()
        {
            const string Input = "Alice follows Bob";
        
            this.parser.Parse(Input);
        
            this.factory.Received().CreateCommand("Alice", "follows", Arg.Is<IEnumerable<string>>(users => users.All(user => user == "Bob")));
        }

        [Test]
        public void GivenAWallUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameAndTheWallAction()
        {
            const string Input = "user wall";
        
            this.parser.Parse(Input);
        
            this.factory.Received().CreateCommand("user", "wall", Arg.Is<IEnumerable<string>>(users => !users.Any()));
        }
    }
}
