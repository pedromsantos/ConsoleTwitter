namespace ConsoleTwitter
{
    using System.Linq;

    using ConsoleTwitter.Commands;
    using ConsoleTwitter.Messages;
    using ConsoleTwitter.Users;
    using ConsoleTwitter.Wrappers;

    public class Program
    {
        private readonly IConsole consoleWrapper;

        private readonly IInputParser parser;

        private readonly IMessageFormaterFactory formaterFactory;

        public Program(IConsole consoleWrapper, IInputParser parser, IMessageFormaterFactory formaterFactory)
        {
            this.formaterFactory = formaterFactory;
            this.parser = parser;
            this.consoleWrapper = consoleWrapper;
        }

        public static void Main()
        {
            var repository = new UserRepository();
            var broker = new MessageBroker(repository);
            var commandFactory = new CommandFactory(broker);
            var parser = new InputParser(commandFactory);

            var formaterFactory = new MessageFormaterFactory();
            var consoleWrapper = new ConsoleWrapper();

            var program = new Program(consoleWrapper, parser, formaterFactory);

            while (program.ProcessUserInput())
            {
            }
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

            var queryCommand = command as IQueryCommand;
            if (queryCommand != null)
            {
                var formater = this.formaterFactory.CreateFormaterForCommand(queryCommand);

                queryCommand.Results.ToList().ForEach(r => 
                    this.consoleWrapper.ConsoleWrite(formater.Format(r)));
            }

            return true;
        }
    }
}
