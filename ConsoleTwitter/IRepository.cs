namespace ConsoleTwitter
{
    public interface IRepository<T>
    {
        T FindByIdentifier(string identifier);

        T Create(string identifier);
    }
}
