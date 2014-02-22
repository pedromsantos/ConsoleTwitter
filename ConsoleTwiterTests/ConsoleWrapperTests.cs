namespace ConsoleTwiterTests
{
    using ConsoleTwitter;

    using FluentAssertions;

    using Xunit;

    public class CommandParserTests
    {
        [Fact]
        public void GivenAUserInputWhenParseIsCalleThenItCreatesACommandRepresentigTheUserAction()
        {
            var parser = new InputParser();

            var command = parser.Parse("user");

            command.Should().BeAssignableTo<ICommand>();
        }

        [Fact]
        public void GivenAnInvalidUserInputWhenParseIsCalleThenItCreatesANullCommandRepresentigTheUserAction()
        {
            var parser = new InputParser();

            var command = parser.Parse(string.Empty);

            command.Should().BeAssignableTo<NullCommand>();
        }
    }
}
