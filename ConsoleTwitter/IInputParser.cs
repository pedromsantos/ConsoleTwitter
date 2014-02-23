namespace ConsoleTwitter
{
    using System.Linq;

    public interface IInputParser
    {
        ICommand Parse (string inputString);
    }
    
}