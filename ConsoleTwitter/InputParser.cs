namespace ConsoleTwitter
{
    using System.Linq;

    public class InputParser
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";

        public ICommand Parse(string userAction)
        {
            if (string.IsNullOrEmpty(userAction))
            {
                return new NullCommand();
            }

            var tokenizedInput = userAction.Split(' ');

            var userName = tokenizedInput[0];

            if (tokenizedInput.Count() > 1)
            {
                var action = tokenizedInput[1];

                if (action.ToLower() == FollowToken)
                {
                    return new FollowCommand(userName);    
                }

                if (action.ToLower() == WallToken)
                {
                    return new WallCommand(userName);
                }

                if (tokenizedInput.Count() > 2)
                {
                    var message = tokenizedInput[2];

                    return new PostCommand(userName, message);
                }
            }

            return new ReadCommand(userName);
        }
    }
}