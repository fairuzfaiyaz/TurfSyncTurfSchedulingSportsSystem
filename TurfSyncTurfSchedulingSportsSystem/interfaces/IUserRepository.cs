using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        bool CreateUser(User user);
    }
}
