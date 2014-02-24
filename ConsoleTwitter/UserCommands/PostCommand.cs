namespace ConsoleTwitter
{
    public class PostCommand : Command
    {
        public PostCommand(IMessageBroker receiver, string userName, string message)
            : base(receiver, userName)
        {
            this.Message = message;
        }

        public string Message { get; private set; }

        public override void Execute ()
        {
            this.receiver.Post(User, Message);
        }
    }
}