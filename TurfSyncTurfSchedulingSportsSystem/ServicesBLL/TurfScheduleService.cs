using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using TurfSyncTurfSchedulingSportsSystem.Models;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;

namespace TurfSyncTurfSchedulingSportsSystem.ServicesBLL
{
    public class TurfScheduleService
    {
        private readonly string connectionString =
            @"Server=DESKTOP-K86QF27\SQLEXPRESS;Database=TurfSyncDB;Trusted_Connection=True;";

        // Add new schedule
        public void AddSchedule(TurfSchedule schedule)
        {
            //using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlConnection con = DatabaseHelper.GetConnection())
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
            using (SqlConnection con = DatabaseHelper.GetConnection())
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
            using (SqlConnection con = DatabaseHelper.GetConnection())
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
        //newwwww
        public void UpdateStatusByRow(DateTime date, TimeSpan time, string location, string newStatus)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"UPDATE TurfSchedule
                         SET Status=@Status
                         WHERE ScheduleDate=@Date AND ScheduleTime=@Time AND TurfLocation=@Location";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Date", date.Date);
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.Parameters.AddWithValue("@Location", location);
                cmd.Parameters.AddWithValue("@Status", newStatus);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new Exception("No matching schedule found!");
            }
        }

        //delete row

        // DELETE SCHEDULE
        public void DeleteSchedule(int turfId, DateTime scheduleDate)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"DELETE FROM TurfSchedule
                         WHERE TurfId = @TurfId AND ScheduleDate = @Date";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TurfId", turfId);
                cmd.Parameters.AddWithValue("@Date", scheduleDate.Date);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new Exception("No matching schedule found to delete.");
                }
            }
        }

        //price control adding
        public void UpdatePriceByRow(DateTime date, TimeSpan time, string location, decimal newPrice)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"UPDATE TurfSchedule
                         SET Price = @Price
                         WHERE ScheduleDate = @Date
                           AND DATEPART(HOUR, ScheduleTime) = @Hour
                           AND DATEPART(MINUTE, ScheduleTime) = @Minute
                           AND LTRIM(RTRIM(TurfLocation)) = @Location";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Price", newPrice);
                cmd.Parameters.AddWithValue("@Date", date.Date);
                cmd.Parameters.AddWithValue("@Hour", time.Hours);
                cmd.Parameters.AddWithValue("@Minute", time.Minutes);
                cmd.Parameters.AddWithValue("@Location", location.Trim());

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    throw new Exception("No matching row found to update price. Check time and location.");
            }
        }

        //change to previous

        public void UpdatePriceForNightWeekend(decimal changeAmount)
        {
            using (SqlConnection con =DatabaseHelper.GetConnection())
            {
                string query = @"
            UPDATE TurfSchedule
            SET Price = Price + @Change
            WHERE 
                (DATEPART(HOUR, ScheduleTime) >= 18)       -- Night: after 6 PM
                OR (DATEPART(WEEKDAY, ScheduleDate) IN (6,7))  -- Weekend: Friday=6, Saturday=7 depending on SQL settings
        ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Change", changeAmount);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    throw new Exception("No matching slots found for night or weekend price update.");
            }
        }

        //change price manually by row maintain page
        // Update price manually for a selected schedule
        public void UpdatePriceSelectedSchedule(DateTime scheduleDate, TimeSpan scheduleTime, string location, decimal newPrice)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"UPDATE TurfSchedule
                         SET Price = @Price
                         WHERE ScheduleDate = @Date AND ScheduleTime = @Time AND TurfLocation = @Location";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Price", newPrice);
                cmd.Parameters.AddWithValue("@Date", scheduleDate.Date);
                cmd.Parameters.AddWithValue("@Time", scheduleTime);
                cmd.Parameters.AddWithValue("@Location", location);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new Exception("No matching schedule found.");
            }
        }






    }
}
