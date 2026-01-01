using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurfSyncTurfSchedulingSportsSystem.Models;
using TurfSyncTurfSchedulingSportsSystem.Interfaces;

namespace TurfSyncTurfSchedulingSportsSystem.DataDLL
{
    internal class UserRepository:IUserRepository
    {
        public User GetUserByUsername(string username)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                string query = @"SELECT * FROM Users WHERE Username=@username AND IsActive=1";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        UserId = (int)reader["UserId"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString(),
                        IsActive = (bool)reader["IsActive"],
                        CreatedAt = (System.DateTime)reader["CreatedAt"]
                    };
                }
            }
            return null;
        }

        public void CreateUser(User user)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                string query = @"INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@u, @e, @p, @r)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@r", user.Role);

                cmd.ExecuteNonQuery();
            }
        }
    
    }
}
