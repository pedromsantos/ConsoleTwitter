namespace ConsoleTwitter
{
    public class FollowCommand : Command
    {
        public FollowCommand(ICommandReceiver receiver, string userName, string userToFollow)
            : base(receiver, userName)
        {
            this.UserToFollow = userToFollow;
        }

        public string UserToFollow { get; private set; }

        public override void Execute ()
        {
            throw new System.NotImplementedException ();
        }
    }
}