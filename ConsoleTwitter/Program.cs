﻿using System.Linq;

namespace ConsoleTwitter
{
    public class Program
    {
        private IConsole consoleWrapper;

        private IInputParser parser;

        private IMessageFormaterFactory formaterFactory;

        public Program(IConsole consoleWrapper, IInputParser parser, IMessageFormaterFactory formaterFactory)
        {
            this.formaterFactory = formaterFactory;
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
                var formater = formaterFactory.CreateFormaterForCommand((IQueryCommand)command);

                ((IQueryCommand)command).Results.ToList().ForEach(r => 
                    this.consoleWrapper.ConsoleWrite(formater.Format(r)));
            }

            return true;
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

            while (program.ProcessUserInput() != false)
                ;
        }
    }
}
