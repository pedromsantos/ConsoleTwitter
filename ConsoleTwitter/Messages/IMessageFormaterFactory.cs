namespace ConsoleTwitter.Messages
{
    using ConsoleTwitter.Commands;

    public interface IMessageFormaterFactory
    {
        IMessageFormater CreateFormaterForCommand(IQueryCommand command);
    }
}
