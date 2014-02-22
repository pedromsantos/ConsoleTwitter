namespace ConsoleTwitter
{
    using ConsoleTwiterTests;

    public class InputParser
    {
        public ICommand Parse(string userAction)
        {
            if (string.IsNullOrEmpty(userAction))
            {
                return new NullCommand();
            }

            return new Command();
        }
    }
}