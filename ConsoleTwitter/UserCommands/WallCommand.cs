namespace ConsoleTwitter
{
    public class WallCommand : Command
    {
        public WallCommand(ICommandReceiver receiver, string userName)
            : base(receiver, userName)
        {
        }

        public override void Execute ()
        {
            this.receiver.Wall(User);
        }
    }
}