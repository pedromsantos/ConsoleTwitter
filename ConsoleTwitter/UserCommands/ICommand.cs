namespace ConsoleTwitter
{
    public interface ICommand
    {
        string User { get; }

        void Execute();
    }
}