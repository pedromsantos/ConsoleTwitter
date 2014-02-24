using System.Linq;

namespace ConsoleTwitter
{
    public class Program
    {
        IConsole consoleWrapper;

        IInputParser parser;

        public Program(IConsole consoleWrapper, IInputParser parser)
        {
            this.parser = parser;
            this.consoleWrapper = consoleWrapper;
        }

        public bool ProcessUserInput()
        {
            var input = this.consoleWrapper.ConsoleRead();

            var command = this.parser.Parse(input);

            if (command is NullCommand)
            {
                return false;
            }

            command.Execute();

            if (command is IQueryCommand)
            {
                ((IQueryCommand)command).Results.ToList().ForEach(r => this.consoleWrapper.ConsoleWrite(r.Body));
            }

            return true;
        }
            
        public static void Main()
        {
        }
    }
}
