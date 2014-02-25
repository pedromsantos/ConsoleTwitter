namespace ConsoleTwitter.Messages
{
    public class WallMessageFormater : IMessageFormater
    {
        private readonly MessageFormater messageFormater;

        public WallMessageFormater(MessageFormater messageFormater)
        {
            this.messageFormater = messageFormater;
        }

        public string Format(Message message)
        {
            return string.Format("{0} - {1}", message.User.UserHandle, this.messageFormater.Format(message));
        }
    }
}
