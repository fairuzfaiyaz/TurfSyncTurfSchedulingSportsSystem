using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Interfaces
{
    internal interface IAuthenticator
    {
        bool Login(string username, string password, out User user);

        bool Signup(string fullName, string username, string email, string password, string repeatPassword);
    }
}
