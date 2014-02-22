namespace ConsoleTwitter
{
    using System.Linq;

    public class InputParser
    {
        public ICommand Parse(string userAction)
        {
            if (string.IsNullOrEmpty(userAction))
            {
                return new NullCommand();
            }

            var tokenizedInput = userAction.Split(' ');

            if (tokenizedInput.Count() > 1)
            {
                return new PostCommand(tokenizedInput[0]);
            }

            return new Command(tokenizedInput[0]);
        }
    }
}