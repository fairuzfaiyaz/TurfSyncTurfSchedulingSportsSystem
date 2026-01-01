using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Models
{
    internal class User
    {
        public int UserId { get; set; }          // Primary key
        public string Username { get; set; }     // Login name
        public string Email { get; set; }        // For contact / recovery
        public string PasswordHash { get; set; } // Hashed password
        public string Role { get; set; }          // Admin, Manager, Staff, Player
        public bool IsActive { get; set; }        // Soft delete / block user
        public DateTime CreatedAt { get; set; }   // Audit info
    }
}
