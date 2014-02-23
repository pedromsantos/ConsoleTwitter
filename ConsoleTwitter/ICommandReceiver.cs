using System;

namespace ConsoleTwitter
{
    public interface ICommandReceiver
    {
        void Read(string user);
    }
}
