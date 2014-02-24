namespace ConsoleTwitter
{
    public class WallCommand : Command
    {
        public WallCommand(IMessageBroker receiver, string userName)
            : base(receiver, userName)
        {
        }

        public override void Execute ()
        {
            this.broker.Wall(User);
        }
    }
}