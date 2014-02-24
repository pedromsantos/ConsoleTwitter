namespace ConsoleTwitter
{
    public abstract class Command : ICommand
    {
        protected IMessageBroker receiver;

        protected Command(IMessageBroker receiver, string userName)
        {
            this.receiver = receiver;
            this.User = userName;
        }

        public string User { get; private set; }

        public abstract void Execute();
    }
}