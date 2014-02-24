using System;

namespace ConsoleTwitter
{
    public class WallMessageFormater : IMessageFormater
    {
        private MessageFormater messageFormater;

        public WallMessageFormater(MessageFormater messageFormater)
        {
            this.messageFormater = messageFormater;
        }

        public string Format(Message message)
        {
            return string.Format("{0} - {1}",message.User.UserHandle, messageFormater.Format(message));
        }
    }
}

