using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTwitter
{
    public interface IMessageFormater
    {
        string Format(Message message);
    }
    
}
