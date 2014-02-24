using System.Linq;

namespace ConsoleTwitter
{
    public interface IMessageFormaterFactory
    {
        IMessageFormater CreateFormaterForCommand(IQueryCommand command);
    }
    
}
