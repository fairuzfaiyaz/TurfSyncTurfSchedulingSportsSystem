using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class RescheduleBooking : Form
    {
        User currentUser;

        public RescheduleBooking()
        {
            InitializeComponent();
        }
        public RescheduleBooking(User user)
        {
            InitializeComponent();  // Must be first
            this.currentUser = user;
            label1.Text = currentUser.FullName;
            label2.Text = currentUser.Username;
            button1.Enabled = false;
            
            LoadTurfSchedule();
            LoadUserBookings();
        }

        public void LoadTurfSchedule()
        {
            string query = "SELECT * FROM dbo.TurfSchedule";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView2.DataSource = dt;

                    // Optional: make Status column readonly so color shows properly
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.ReadOnly = true;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView2.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading turf locations: " + ex.Message);
            }
        }
        private void LoadUserBookings()
        {
            string query = @"
        SELECT 
            RequestId,
            TurfId,RequestedBy,
            ScheduleDate,
            ScheduleTime,
            Duration,
            Price,
            RequestStatus
        FROM PendingRequests
        WHERE RequestedBy = @username
          AND RequestStatus IN ('Pending', 'Booked')
        ORDER BY ScheduleDate, ScheduleTime";

            using (SqlConnection con = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", currentUser.Username);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["RequestStatus"].Value == null) continue;

                string status = row.Cells["RequestStatus"].Value.ToString();

                if (status == "Booked")
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else if (status == "Pending")
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
            }

        }
        private bool CanReschedule(DataGridViewRow bookingRow)
        {
            DateTime date = Convert.ToDateTime(bookingRow.Cells["ScheduleDate"].Value);
            TimeSpan time = TimeSpan.Parse(bookingRow.Cells["ScheduleTime"].Value.ToString());

            DateTime bookingDateTime = date.Date + time;
            return (bookingDateTime - DateTime.Now).TotalHours >= 2;
        }


        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // (optional) your existing label logic here

            UpdateRescheduleButton(); // ✅ CALL HERE
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            UpdateRescheduleButton(); // ✅ CALL HERE
        }

        private void UpdateRescheduleButton()
        {
            button1.Enabled =
                dataGridView1.SelectedRows.Count > 0 &&
                dataGridView2.SelectedRows.Count > 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count == 0 ||
                dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a booking and a new slot.");
                return;
            }

            DataGridViewRow oldBooking = dataGridView1.SelectedRows[0];
            DataGridViewRow newSlot = dataGridView2.SelectedRows[0];

            string bookingStatus = oldBooking.Cells["RequestStatus"].Value?.ToString();

            if (bookingStatus != "Pending" && bookingStatus != "Booked")
            {
                MessageBox.Show("Only Pending or Booked bookings can be rescheduled.");
                return;
            }
            // Time rule
            if (!CanReschedule(oldBooking))
            {
                MessageBox.Show("Rescheduling not allowed within 2 hours.");
                return;
            }



            string slotStatus = newSlot.Cells["Status"].Value?.ToString();

            if (slotStatus != "Available")
            {
                MessageBox.Show("Please select an available slot.");
                return;
            }


            // Slot availability
            //if (newSlot.Cells["Status"].Value.ToString() != "Approved")
            //{
            //    MessageBox.Show("Selected slot is not available.");
            //    return;
            //}

            int oldRequestId = Convert.ToInt32(oldBooking.Cells["RequestId"].Value);
            int newTurfId = Convert.ToInt32(newSlot.Cells["TurfId"].Value);

            DateTime newDate = Convert.ToDateTime(newSlot.Cells["ScheduleDate"].Value);
            TimeSpan newTime = TimeSpan.Parse(newSlot.Cells["ScheduleTime"].Value.ToString());
            int duration = Convert.ToInt32(newSlot.Cells["Duration"].Value);
            decimal price = Convert.ToDecimal(newSlot.Cells["Price"].Value);

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();

                try
                {
                    // 1️⃣ Mark old booking as Rescheduled
                    SqlCommand updateOld = new SqlCommand(@"
                UPDATE PendingRequests
                SET RequestStatus = 'Rescheduled'
                WHERE RequestId = @id", con, tx);

                    updateOld.Parameters.AddWithValue("@id", oldRequestId);
                    updateOld.ExecuteNonQuery();

                    // 2️⃣ Insert new booking
                    SqlCommand insertNew = new SqlCommand(@"
                INSERT INTO PendingRequests
                (TurfId, ScheduleDate, ScheduleTime, Duration, Price, RequestedBy, RequestStatus)
                VALUES
                (@turfId, @date, @time, @duration, @price, @user, 'Booked')",
                        con, tx);

                    insertNew.Parameters.AddWithValue("@turfId", newTurfId);
                    insertNew.Parameters.AddWithValue("@date", newDate);
                    insertNew.Parameters.AddWithValue("@time", newTime);
                    insertNew.Parameters.AddWithValue("@duration", duration);
                    insertNew.Parameters.AddWithValue("@price", price);
                    insertNew.Parameters.AddWithValue("@user", currentUser.Username);

                    insertNew.ExecuteNonQuery();

                    // 3️⃣ Update TurfSchedule
                    SqlCommand updateSchedule = new SqlCommand(@"
                UPDATE TurfSchedule
                SET Status = 'Booked'
                WHERE TurfId = @turfId
                  AND ScheduleDate = @date
                  AND ScheduleTime = @time",
                        con, tx);

                    updateSchedule.Parameters.AddWithValue("@turfId", newTurfId);
                    updateSchedule.Parameters.AddWithValue("@date", newDate);
                    updateSchedule.Parameters.AddWithValue("@time", newTime);

                    updateSchedule.ExecuteNonQuery();

                    tx.Commit();

                    MessageBox.Show("Booking rescheduled successfully.");
                    LoadUserBookings();
                    LoadTurfSchedule();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Reschedule failed: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile myProfile = new MyProfile(currentUser);
            myProfile.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeProfile changeProfile = new ChangeProfile(currentUser);
            changeProfile.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }
    }
}

