namespace ConsoleTwitter
{
    using ConsoleTwitter.Commands;

    public interface IInputParser
    {
        ICommand Parse(string inputString);
    }
}