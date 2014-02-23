namespace ConsoleTwitter
{
    public class PostCommand : Command
    {
        public PostCommand(ICommandReceiver receiver, string userName, string message)
            : base(receiver, userName)
        {
            this.Message = message;
        }

        public string Message { get; private set; }

        public override void Execute ()
        {
            throw new System.NotImplementedException ();
        }
    }
}