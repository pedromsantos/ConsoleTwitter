namespace ConsoleTwitter.Commands
{
    public abstract class Command : ICommand
    {
        protected IMessageBroker broker;

        protected Command(IMessageBroker broker, string userName)
        {
            this.broker = broker;
            this.User = userName;
        }

        public string User { get; private set; }

        public abstract void Execute();
    }
}