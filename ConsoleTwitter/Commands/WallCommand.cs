namespace ConsoleTwitter
{
    public class WallCommand : Command, IQueryCommand
    {
        public WallCommand(IMessageBroker receiver, string userName)
            : base(receiver, userName)
        {
        }

        public override void Execute ()
        {
            this.broker.Wall(User);
        }

        public System.Collections.Generic.IEnumerable<Message> Results 
        {
            get 
            {
                throw new System.NotImplementedException();
            }
        }
    }
}