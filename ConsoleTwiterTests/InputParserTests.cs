namespace ConsoleTwiterTests
{
    using ConsoleTwitter;
    using FluentAssertions;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class InputParserTests
    {
        ICommandFactory factory;
        InputParser parser;

        [SetUp]
        public void Setup()
        {
            factory = Substitute.For<ICommandFactory>();
            parser = new InputParser(factory);
        }

        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommand()
        {
            parser.Parse("user");
        
            factory.Received().CreateCommand("user", null, null);
        }

        [Test]
        public void GivenAnInvalidUserInputWhenParseIsCalledThenItCallsCreateCommandSettingAllArgumentsToNull()
        {
            parser.Parse(string.Empty);
        
            factory.Received().CreateCommand(string.Empty, null, null);
        }

        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsername()
        {
            const string UserName = "user";
        
            parser.Parse(UserName);
        
            factory.Received().CreateCommand("user", null, null);
        }

        [Test]
        public void GivenAPostUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameThePostActionAndTheMessage()
        {
            const string Input = "user -> message";
        
            parser.Parse(Input);
        
            factory.Received().CreateCommand("user", "->", "message");
        }

        [Test]
        public void GivenAFollowsUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameTheFollowActionAndTheUserToFollow()
        {
            const string Input = "Alice follows Bob";
        
            parser.Parse(Input);
        
            factory.Received().CreateCommand("Alice", "follows", "Bob");
        }

        [Test]
        public void GivenAWallUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameAndTheWallAction()
        {
            const string Input = "user wall";
        
            parser.Parse(Input);
        
            factory.Received().CreateCommand("user", "wall", null);
        }
    }
}
