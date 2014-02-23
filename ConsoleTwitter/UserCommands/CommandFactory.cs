namespace ConsoleTwitter
{
    using System.Linq;

    public class CommandFactory : ICommandFactory
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";
        private const string PostToken = "->";

        private ICommandReceiver receiver;

        public CommandFactory(ICommandReceiver receiver)
        {
            this.receiver = receiver;
        }

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
                return new ReadCommand(this.receiver, userName);
                case WallToken:
                return new WallCommand(this.receiver, userName);
                case FollowToken:
                return new FollowCommand(this.receiver, userName, argument);
                case PostToken:
                return new PostCommand(this.receiver, userName, argument);
                default:
                    return new NullCommand();
            }
        }
    }
}