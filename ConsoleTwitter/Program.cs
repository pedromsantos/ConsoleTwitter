using System.Linq;

namespace ConsoleTwitter
{
    public class Program
    {
        private IConsole consoleWrapper;

        private IInputParser parser;

        private IMessageFormater formater;

        public Program(IConsole consoleWrapper, IInputParser parser, IMessageFormater formater)
        {
            this.formater = formater;
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
                ((IQueryCommand)command).Results.ToList().ForEach(r => 
                    this.consoleWrapper.ConsoleWrite(formater.Format(r)));
            }

            return true;
        }
            
        public static void Main()
        {
        }
    }
}
