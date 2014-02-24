using System;

namespace ConsoleTwitter
{
    public class MessageFormater : IMessageFormater
    {
        private IMessageFormater elapsedTimeFormater;

        public MessageFormater(ElapsedTimeMessageFormater messageFormater)
        {
            this.elapsedTimeFormater = messageFormater;
        }

        public string Format(Message message)
        {
            return string.Format("{0} ({1})", message.Body, elapsedTimeFormater.Format(message));
        }
    }
}

