using System;

namespace ConsoleTwitter
{
    public interface ICommandReceiver
    {
        void Post(string user, string message);

        void Read(string user);
    }
}
