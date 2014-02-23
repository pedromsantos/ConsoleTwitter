using System;

namespace ConsoleTwitter
{
    public interface IRepository
    {
        void FindByIdentifier(string identifier);
    }
}

