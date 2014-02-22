namespace ConsoleTwiterTests
{
    using ConsoleTwitter;

    using FluentAssertions;

    using Xunit;

    public class CommandParserTests
    {
        [Fact]
        public void GivenAUserInputWhenParseIsCalledThenItCreatesACommandRepresentigTheUserAction()
        {
            var parser = new InputParser();

            var command = parser.Parse("user");

            command.Should().BeAssignableTo<ICommand>();
        }

        [Fact]
        public void GivenAnInvalidUserInputWhenParseIsCalledThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var parser = new InputParser();

            var command = parser.Parse(string.Empty);

            command.Should().BeAssignableTo<NullCommand>();
        }

        [Fact]
        public void GivenAUserInputWhenParseIsCalledThenItCreatesACommandAssigningTheUsernameToTheCommand()
        {
            const string UserName = "user";

            var parser = new InputParser();

            var command = parser.Parse(UserName);

            command.User.Should().Be(UserName);
        }
    }
}
