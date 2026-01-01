using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Interfaces;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.ServicesBLL
{
    internal class Authenticator:IAuthenticator
    {
        private IUserRepository repo = new UserRepository();

        public bool Login(string username, string password, out User user)
        {
            user = repo.GetUserByUsername(username);
            if (user == null) return false;

            string hash = HashPassword(password);
            return hash == user.PasswordHash;
        }

        public void Signup(string username, string email, string password)
        {
            User user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                Role = "Player"   // default role
            };

            repo.CreateUser(user);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
