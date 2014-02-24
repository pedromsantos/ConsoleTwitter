using System;

namespace ConsoleTwitter
{
    public interface IConsole
    {
        string ConsoleRead();

        void ConsoleWrite(string output);
    }
}