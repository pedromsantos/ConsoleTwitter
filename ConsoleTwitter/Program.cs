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

        public void ProcessUserInput()
        {
            var input = this.consoleWrapper.ConsoleRead();

            var command = this.parser.Parse(input);

            command.Execute();

            if (command is IQueryCommand)
            {
                ((IQueryCommand)command).Results.ToList().ForEach(r => this.consoleWrapper.ConsoleWrite(r));
            }
        }
            
        public static void Main()
        {
        }
    }
}
