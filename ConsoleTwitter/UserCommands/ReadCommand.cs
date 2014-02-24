namespace ConsoleTwitter
{
    public class ReadCommand : Command
    {
        public ReadCommand(IMessageBroker receiver, string userName)
            : base(receiver, userName)
        {
        }

        public override void Execute ()
        {
            this.receiver.Read(User);
        }
    }
}