namespace ConsoleTwitter
{
    using System.Linq;

    public class CommandFactory : ICommandFactory
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";
        private const string PostToken = "->";
        
        public ICommand CreateCommand (string userName, string action, string argument)
        {
            if (string.IsNullOrEmpty (userName))
            {
                return new NullCommand();
            }
            
            if (action == null) 
            {
                return new ReadCommand(userName);
            }

            if (action == WallToken)
            {
                return new WallCommand(userName);
            }
            
            if (action == FollowToken) 
            {
                var userToFollow = argument;

                return new FollowCommand(userName, userToFollow);    
            }

            if (action == PostToken) 
            {
                var message = argument;

                return new PostCommand(userName, message);
            }
            
            return new NullCommand();
        }
    }
}