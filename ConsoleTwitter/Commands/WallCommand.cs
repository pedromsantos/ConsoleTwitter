using System.Collections.Generic;

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
            this.Results = this.broker.Wall(User);
        }

        public IEnumerable<Message> Results { get; private set; }
    }
}