namespace ConsoleTwitter
{
    using System.Linq;

    public class InputParser
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";
        private const string PostToken = "->";

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
                var action = tokenizedInput[1].ToLower();

                if (action == FollowToken)
                {
                    var userToFollow = tokenizedInput[2];

                    return new FollowCommand(userName, userToFollow);    
                }

                if (action == WallToken)
                {
                    return new WallCommand(userName);
                }

                if (action == PostToken)
                {
                    var message = tokenizedInput[2];

                    return new PostCommand(userName, message);
                }
            }

            return new ReadCommand(userName);
        }
    }
}