using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;
using static System.Collections.Specialized.BitVector32;


namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class CancelBooking : Form
    {
        User currentUser;
        public CancelBooking()
        {
            InitializeComponent();
        }
        public CancelBooking(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadUserBookings();

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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            TurnNameTextBox.Text = "Turf " + row.Cells["TurfId"].Value.ToString();

            dateTB.Text = Convert.ToDateTime(row.Cells["ScheduleDate"].Value)
                            .ToString("yyyy-MM-dd");

            timeslotTB.Text = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString())
                                .ToString(@"hh\:mm");

            amountPaidTB.Text = row.Cells["Price"].Value.ToString();

            remaingtimeLabel.Text = GetRemainingTimeText(row);

            // Refund logic (80%)
            decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
            refundTB.Text = (price * 0.8m).ToString("0.00");

            //  Cancel logic
            bool canCancel = CanCancelBooking(row);
            button1.Enabled = canCancel;

            if (!canCancel)
            {
                MessageBox.Show("Cancellation not allowed within 2 hours of booking time.");
            }
        }

        private bool CanCancelBooking(DataGridViewRow row)
        {
            DateTime date = Convert.ToDateTime(row.Cells["ScheduleDate"].Value);
            TimeSpan time = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString());

            DateTime bookingDateTime = date.Date + time;

            return (bookingDateTime - DateTime.Now).TotalHours >= 2;
        }


        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private string GetRemainingTimeText(DataGridViewRow row)
        {
            DateTime date = Convert.ToDateTime(row.Cells["ScheduleDate"].Value);
            TimeSpan time = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString());

            DateTime bookingTime = date.Date + time;
            TimeSpan remaining = bookingTime - DateTime.Now;

            if (remaining.TotalMinutes <= 0)
                return "Booking time passed";

            if (remaining.TotalHours < 1)
                return $"{remaining.Minutes} min remaining";

            return $"{(int)remaining.TotalHours} hr {remaining.Minutes} min remaining";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a booking first.");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            if (!CanCancelBooking(row))
            {
                MessageBox.Show("Cannot cancel within 2 hours.");
                return;
            }

            int requestId = Convert.ToInt32(row.Cells["RequestId"].Value);

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();

                try
                {
                    // Insert into history
                    SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO BookingHistory
                (RequestId, TurfId, ScheduleDate, ScheduleTime, Duration, Price, RequestedBy, RequestStatus)
                SELECT RequestId, TurfId, ScheduleDate, ScheduleTime, Duration, Price, RequestedBy, 'Cancelled'
                FROM PendingRequests
                WHERE RequestId = @id", con, tx);

                    insertCmd.Parameters.AddWithValue("@id", requestId);
                    insertCmd.ExecuteNonQuery();

                    // Delete from pending
                    SqlCommand deleteCmd = new SqlCommand(
                        "DELETE FROM PendingRequests WHERE RequestId = @id",
                        con, tx);

                    deleteCmd.Parameters.AddWithValue("@id", requestId);
                    deleteCmd.ExecuteNonQuery();

                    tx.Commit();

                    MessageBox.Show("Booking cancelled and moved to history.");
                    LoadUserBookings();
                }
                catch
                {
                    tx.Rollback();
                    MessageBox.Show("Cancellation failed.");
                }
            }
        }
    }
}
