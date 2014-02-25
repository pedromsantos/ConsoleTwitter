namespace ConsoleTwitter.Commands
{
    using System.Collections.Generic;

    public interface ICommandFactory
    {
        ICommand CreateCommand(string userName, string action, IEnumerable<string> arguments);
    }
}