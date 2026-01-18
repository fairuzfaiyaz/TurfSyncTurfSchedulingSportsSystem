using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.DataDLL
{
    public class PendingRequestRepository
    {
        private readonly string connectionString =
            @"Server=DESKTOP-K86QF27\SQLEXPRESS;Database=TurfSyncDB;Trusted_Connection=True;";

        // 🔹 Get all pending requests
        public List<PendingRequest> GetPendingRequests()
        {
            List<PendingRequest> requests = new List<PendingRequest>();

            string query = "SELECT * FROM PendingRequests";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    requests.Add(new PendingRequest
                    {
                        RequestId = Convert.ToInt32(reader["RequestId"]),
                        TurfId = Convert.ToInt32(reader["TurfId"]),
                        ScheduleDate = Convert.ToDateTime(reader["ScheduleDate"]),
                        ScheduleTime = TimeSpan.Parse(reader["ScheduleTime"].ToString()),
                        Duration = Convert.ToInt32(reader["Duration"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        RequestedBy = reader["RequestedBy"].ToString(),
                        RequestStatus = reader["RequestStatus"].ToString()
                    });
                }
            }
            return requests;
        }

        // 🔹 APPROVE / CANCEL request
        public void UpdateRequestStatus(int requestId, string status)
        {
            string query =
                "UPDATE PendingRequests SET RequestStatus=@status WHERE RequestId=@id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", requestId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
