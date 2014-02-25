namespace ConsoleTwitter.Commands
{
    public interface ICommand
    {
        string User { get; }

        void Execute();
    }
}