namespace ConsoleTwitter.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    public class CommandFactory : ICommandFactory
    {
        private const string FollowToken = "follows";
        private const string WallToken = "wall";
        private const string PostToken = "->";

        private readonly IMessageBroker receiver;

        public CommandFactory(IMessageBroker receiver)
        {
            this.receiver = receiver;
        }

        public virtual ICommand CreateCommand(string userName, string action, IEnumerable<string> arguments)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new NullCommand();
            }

            action = action == null ? null : action.ToLower();

            return this.CreateActionCommand(userName, action, arguments);
        }

        protected virtual ICommand CreateActionCommand(string userName, string action, IEnumerable<string> arguments)
        {
            switch (action)
            {
                case null:
                case "":
                    return new ReadCommand(this.receiver, userName);
                case WallToken:
                    return new WallCommand(this.receiver, userName);
                case FollowToken:
                    return new FollowCommand(this.receiver, userName, arguments.First());
                case PostToken:
                    return new PostCommand(this.receiver, userName, string.Join(" ", arguments));
                default:
                    return new NullCommand();
            }
        }
    }
}