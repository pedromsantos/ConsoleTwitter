namespace ConsoleTwitter.Commands
{
    public class NullCommand : ICommand
    {
        public string User { get; private set; }

        public void Execute()
        {
        }
    }
}