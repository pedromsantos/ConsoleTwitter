namespace ConsoleTwitter
{
    public class NullCommand : ICommand
    {
        public string User { get; private set; }

        public void Execute ()
        {
            throw new System.NotImplementedException ();
        }
    }
}