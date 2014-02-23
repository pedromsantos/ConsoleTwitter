using System;

namespace ConsoleTwitter
{
    public interface IRepository
    {
        User FindByIdentifier(string identifier);

        User Create(string identifier);
    }
}

