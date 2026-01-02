using System;
using System.Security.Cryptography;
using System.Text;
using TurfSyncTurfSchedulingSportsSystem.Interfaces;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.ServicesBLL
{
    internal class Authenticator : IAuthenticator
    {
        private readonly IUserRepository turfSyncUser;

        // Dependency Injection (GOOD OOP)
        public Authenticator(IUserRepository dll_User)
        {
            turfSyncUser = dll_User;
        }

        public bool Login(string username, string password, out User user)
        {
            user = turfSyncUser.GetUserByUsername(username);
            if (user == null)
                return false;

            string hash = HashPassword(password);
            return hash == user.PasswordHash;
        }

        public bool Signup(string fullName, string username, string email,
                           string password, string repeatPassword)
        {
            if (password != repeatPassword)
                throw new Exception("Passwords do not match");

            User user = new User
            {
                FullName = fullName,
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                Role = "Player", // default role
                IsActive = true
            };

            return turfSyncUser.CreateUser(user);
        }
        public bool adminSignup(string fullName, string username, string email, string password, string repeatPassword, string role)
        {
            if (password != repeatPassword)
                throw new Exception("Passwords do not match");

            User user = new User
            {
                FullName = fullName,
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                Role = role, // default role
                IsActive = true
            };

            return turfSyncUser.CreateUser(user);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
