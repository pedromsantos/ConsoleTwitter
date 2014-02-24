using System.Collections.Generic;

namespace ConsoleTwitter
{

    public interface IQueryCommand : ICommand
    {
        IEnumerable<Message> Results { get; }
    }
}