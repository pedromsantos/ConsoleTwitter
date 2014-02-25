namespace ConsoleTwitter.Commands
{
    using System.Collections.Generic;

    using ConsoleTwitter.Messages;

    public class ReadCommand : Command, IQueryCommand
    {
        public ReadCommand(IMessageBroker receiver, string userName)
            : base(receiver, userName)
        {
        }

        public IEnumerable<Message> Results { get; private set; }

        public override void Execute()
        {
            this.Results = this.broker.Read(this.User);
        }
    }
}