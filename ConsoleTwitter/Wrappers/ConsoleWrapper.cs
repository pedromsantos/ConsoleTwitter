namespace ConsoleTwitter.Wrappers
{
    using System;

    public class ConsoleWrapper : IConsole
    {
        public string ConsoleRead()
        {
            return Console.ReadLine();
        }

        public void ConsoleWrite(string output)
        {
            Console.WriteLine(output);
        }
    }
}