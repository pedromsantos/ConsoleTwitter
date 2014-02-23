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

            this.parser.Parse(input);
        }
            
        public static void Main()
        {
        }
    }
}
