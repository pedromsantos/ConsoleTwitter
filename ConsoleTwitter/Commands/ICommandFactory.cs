using System.Collections.Generic;

namespace ConsoleTwitter
{
    using System.Linq;

    public interface ICommandFactory
    {
        ICommand CreateCommand(string userName, string action, IEnumerable<string> arguments);
    }
    
}