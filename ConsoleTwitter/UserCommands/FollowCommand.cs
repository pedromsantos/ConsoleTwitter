namespace ConsoleTwitter
{
    public class FollowCommand : Command
    {
        public FollowCommand(IMessageBroker receiver, string userName, string userToFollow)
            : base(receiver, userName)
        {
            this.UserToFollow = userToFollow;
        }

        public string UserToFollow { get; private set; }

        public override void Execute()
        {
            this.broker.Follow(User, UserToFollow);
        }
    }
}