namespace ConsoleTwitter.Messages
{
    public class MessageFormater : IMessageFormater
    {
        private readonly IMessageFormater elapsedTimeFormater;

        public MessageFormater(ElapsedTimeMessageFormater messageFormater)
        {
            this.elapsedTimeFormater = messageFormater;
        }

        public string Format(Message message)
        {
            return string.Format("{0} ({1})", message.Body, this.elapsedTimeFormater.Format(message));
        }
    }
}
