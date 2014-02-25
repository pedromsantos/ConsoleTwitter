namespace ConsoleTwitter.Commands
{
    using System.Collections.Generic;

    using ConsoleTwitter.Messages;

    public interface IQueryCommand : ICommand
    {
        IEnumerable<Message> Results { get; }
    }
}