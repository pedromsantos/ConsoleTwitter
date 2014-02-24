using System.Collections.Generic;

namespace ConsoleTwitter
{
    public class ReadCommand : Command, IQueryCommand
    {
        public ReadCommand(IMessageBroker receiver, string userName)
            : base(receiver, userName)
        {
        }

        public override void Execute ()
        {
            this.Results = this.broker.Read(User);
        }

        public IEnumerable<string> Results { get; private set; }
    }
}