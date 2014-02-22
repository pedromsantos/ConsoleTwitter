namespace ConsoleTwitter
{
    public class PostCommand : Command
    {
        public PostCommand(string userName, string message)
            : base(userName)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}