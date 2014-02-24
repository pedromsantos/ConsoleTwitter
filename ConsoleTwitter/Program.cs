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

        public void Start()
        {
            var input = this.consoleWrapper.ConsoleRead();

            var comand = this.parser.Parse(input);

            comand.Execute();
        }
            
        public static void Main()
        {
        }
    }
}
