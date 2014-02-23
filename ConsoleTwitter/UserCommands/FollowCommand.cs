namespace ConsoleTwitter
{
    public class FollowCommand : Command
    {
        public FollowCommand(string userName, string userToFollow)
            : base(userName)
        {
            this.UserToFollow = userToFollow;
        }

        public string UserToFollow { get; private set; }
    }
}