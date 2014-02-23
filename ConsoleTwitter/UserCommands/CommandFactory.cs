namespace ConsoleTwitter
{
    using System.Linq;

    public class CommandFactory : ICommandFactory
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";
        private const string PostToken = "->";

        public virtual ICommand CreateCommand(string userName, string action, string argument)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new NullCommand();
            }

            action = action == null ? null : action.ToLower();

            return CreateActionCommand(userName, action, argument);
        }

        protected virtual ICommand CreateActionCommand(string userName, string action, string argument)
        {
            switch (action)
            {
                case null:
                case "":
                    return new ReadCommand(userName);
                case WallToken:
                    return new WallCommand(userName);
                case FollowToken:
                    return new FollowCommand(userName, argument);
                case PostToken:
                    return new PostCommand(userName, argument);
                default:
                    return new NullCommand();
            }
        }
    }
}