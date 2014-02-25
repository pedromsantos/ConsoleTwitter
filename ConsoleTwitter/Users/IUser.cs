namespace ConsoleTwitter.Users
{
    using System.Collections.Generic;

    public interface IUser : IWall
    {
        string UserHandle { get; }

        IEnumerable<IUser> Followers { get; }

        void AddFollower(IUser user);
    }
}
