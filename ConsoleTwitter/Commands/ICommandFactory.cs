namespace ConsoleTwitter.Commands
{
    using System.Collections.Generic;

    public interface ICommandFactory
    {
        ICommand CreateCommand(string userName, string action = null, IEnumerable<string> arguments = null);
    }
}