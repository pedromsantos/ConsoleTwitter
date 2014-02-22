namespace ConsoleTwiterTests
{
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
    }
}
