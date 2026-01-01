using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Interfaces
{
    internal interface IUserRepository
    {
        User GetUserByUsername(string username);
        void CreateUser(User user);
    }
}
