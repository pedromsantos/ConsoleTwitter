namespace ConsoleTwitter
{
    using System.Linq;

    public class InputParser
    {
        private const string FollowToken = "follows";

        public ICommand Parse(string userAction)
        {
            if (string.IsNullOrEmpty(userAction))
            {
                return new NullCommand();
            }

            var tokenizedInput = userAction.Split(' ');

            if (tokenizedInput.Count() > 1)
            {
                if (tokenizedInput[1].ToLower() == FollowToken)
                {
                    return new FollowCommand(tokenizedInput[0]);    
                }

                return new PostCommand(tokenizedInput[0]);
            }

            return new ReadCommand(tokenizedInput[0]);
        }
    }
}