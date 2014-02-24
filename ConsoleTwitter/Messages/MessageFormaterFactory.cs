using System.Linq;

namespace ConsoleTwitter
{
    public class MessageFormaterFactory : IMessageFormaterFactory
    {
        public IMessageFormater CreateFormaterForCommand(IQueryCommand command)
        {
            if (command is WallCommand)
            {
                return new WallMessageFormater(new MessageFormater(new ElapsedTimeMessageFormater()));
            }

            return new MessageFormater(new ElapsedTimeMessageFormater());
        }
    }
}
