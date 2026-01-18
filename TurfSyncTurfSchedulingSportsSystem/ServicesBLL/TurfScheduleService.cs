using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.ServicesBLL
{
    public class TurfScheduleService
    {
        private readonly string connectionString =
            @"Server=DESKTOP-K86QF27\SQLEXPRESS;Database=TurfSyncDB;Trusted_Connection=True;";

        // Add new schedule
        public void AddSchedule(TurfSchedule schedule)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TurfSchedule
                                (TurfId, ScheduleDate, ScheduleTime, TurfLocation, Duration, Price, Status)
                                VALUES (@TurfId, @Date, @Time, @Location, @Duration, @Price, @Status)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TurfId", schedule.TurfId);
                cmd.Parameters.AddWithValue("@Date", schedule.ScheduleDate);
                cmd.Parameters.AddWithValue("@Time", schedule.ScheduleTime);
                cmd.Parameters.AddWithValue("@Location", schedule.TurfLocation);
                cmd.Parameters.AddWithValue("@Duration", schedule.Duration);
                cmd.Parameters.AddWithValue("@Price", schedule.Price);
                cmd.Parameters.AddWithValue("@Status", schedule.Status);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update existing schedule
        public void UpdateSchedule(TurfSchedule schedule)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE TurfSchedule SET
                                ScheduleDate=@Date,
                                ScheduleTime=@Time,
                                TurfLocation=@Location,
                                Duration=@Duration,
                                Price=@Price,
                                Status=@Status
                                WHERE TurfId=@TurfId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TurfId", schedule.TurfId);
                cmd.Parameters.AddWithValue("@Date", schedule.ScheduleDate);
                cmd.Parameters.AddWithValue("@Time", schedule.ScheduleTime);
                cmd.Parameters.AddWithValue("@Location", schedule.TurfLocation);
                cmd.Parameters.AddWithValue("@Duration", schedule.Duration);
                cmd.Parameters.AddWithValue("@Price", schedule.Price);
                cmd.Parameters.AddWithValue("@Status", schedule.Status);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Get all schedules
        public List<TurfSchedule> GetAllSchedules()
        {
            List<TurfSchedule> list = new List<TurfSchedule>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TurfSchedule";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new TurfSchedule
                    {
                        TurfId = (int)dr["TurfId"],
                        ScheduleDate = (DateTime)dr["ScheduleDate"],
                        ScheduleTime = (TimeSpan)dr["ScheduleTime"],
                        TurfLocation = dr["TurfLocation"].ToString(),
                        Duration = (int)dr["Duration"],
                        Price = (decimal)dr["Price"],
                        Status = dr["Status"].ToString()
                    });
                }
            }
            return list;
        }
    }
}
