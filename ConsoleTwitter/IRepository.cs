using System;

namespace ConsoleTwitter
{
    public interface IRepository
    {
        User FindByIdentifier(string identifier);

        void Create(string identifier);
    }
}

