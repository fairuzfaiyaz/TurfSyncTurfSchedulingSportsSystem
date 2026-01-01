using System;
using System.Data.SqlClient;
using TurfSyncTurfSchedulingSportsSystem.Models;
using TurfSyncTurfSchedulingSportsSystem.Interfaces;

namespace TurfSyncTurfSchedulingSportsSystem.DataDLL
{
    internal class UserRepository : IUserRepository
    {
        public User GetUserByUsername(string username)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                string query = @"SELECT * FROM Users WHERE Username = @Username AND IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FullName = reader["FullName"].ToString(),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        };
                    }
                }
            }
        }

        public bool CreateUser(User user)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                //Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@Username", user.Username);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Username already exists, cannot create user
                        return false;
                    }
                }

                //Insert new user
                string query = @"INSERT INTO Users (FullName, Username, Email, PasswordHash, Role) VALUES (@FullName, @Username, @Email, @PasswordHash, @Role)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
