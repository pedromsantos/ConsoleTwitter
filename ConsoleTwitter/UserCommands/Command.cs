namespace ConsoleTwitter
{
    public abstract class Command : ICommand
    {
        protected Command(string userName)
        {
            this.User = userName;
        }

        public string User { get; private set; }
    }
}