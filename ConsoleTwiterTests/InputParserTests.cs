namespace ConsoleTwiterTests
{
    using ConsoleTwitter;
    using FluentAssertions;
    using NUnit.Framework;
    using NSubstitute;

    [TestFixture]
    public class InputParserTests
    {
        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommand()
        {
            var mokedFactory = Substitute.For<ICommandFactory>();
            
            var parser = new InputParser(mokedFactory);
        
            parser.Parse("user");
        
            mokedFactory.Received().CreateCommand("user", null, null);
        }

        [Test]
        public void GivenAnInvalidUserInputWhenParseIsCalledThenItCallsCreateCommandSettingAllArgumentsToNull()
        {
            var mokedFactory = Substitute.For<ICommandFactory>();
            
            var parser = new InputParser(mokedFactory);
        
            parser.Parse(string.Empty);
        
            mokedFactory.Received().CreateCommand(string.Empty, null, null);
        }

        [Test]
        public void GivenAUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsername()
        {
            const string UserName = "user";
        
            var mokedFactory = Substitute.For<ICommandFactory>();
            
            var parser = new InputParser(mokedFactory);
        
            parser.Parse(UserName);
        
            mokedFactory.Received().CreateCommand("user", null, null);
        }

        [Test]
        public void GivenAPostUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameThePostActionAndTheMessage()
        {
            const string Input = "user -> message";
        
            var mokedFactory = Substitute.For<ICommandFactory>();
            
            var parser = new InputParser(mokedFactory);
        
            parser.Parse(Input);
        
            mokedFactory.Received().CreateCommand("user", "->", "message");
        }

        [Test]
        public void GivenAFollowsUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameTheFollowActionAndTheUserToFollow()
        {
            const string Input = "Alice follows Bob";
        
            var mokedFactory = Substitute.For<ICommandFactory>();
        
            var parser = new InputParser(mokedFactory);
            
            parser.Parse(Input);
        
            mokedFactory.Received().CreateCommand("Alice", "follows", "Bob");
        }

        [Test]
        public void GivenAWallUserInputWhenParseIsCalledThenItCallsCreateCommandPassingTheParsedUsernameAndTheWallAction()
        {
            const string Input = "user wall";
        
            var mokedFactory = Substitute.For<ICommandFactory>();
        
            var parser = new InputParser(mokedFactory);
            
            parser.Parse(Input);
        
            mokedFactory.Received().CreateCommand("user", "wall", null);
        }
    }
}
