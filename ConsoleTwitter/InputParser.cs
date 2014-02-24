namespace ConsoleTwitter
{
    using System.Linq;

    public class InputParser : IInputParser
    {
        private const int UserTokenPosition = 1;
        private const int ActionTokenPosition = 2;
        
        private const char SeparatorToken = ' ';

        private ICommandFactory commandFactory;

        public InputParser(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }
        
        public virtual ICommand Parse(string userInput)
        {
            var tokenizedInput = userInput.Split(SeparatorToken);

            var userName = tokenizedInput.First();
            var action = tokenizedInput.Skip(UserTokenPosition).FirstOrDefault();
            var argument = tokenizedInput.Skip(ActionTokenPosition);
            
            return this.commandFactory.CreateCommand(userName, action, argument);
        }
    }
}