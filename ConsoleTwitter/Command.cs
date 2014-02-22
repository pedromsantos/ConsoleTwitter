namespace ConsoleTwitter
{
    public class Command : ICommand
    {
        public Command(string userName)
        {
            User = userName;
        }

        public string User { get; private set; }
    }
}