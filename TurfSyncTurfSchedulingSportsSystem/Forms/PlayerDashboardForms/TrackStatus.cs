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
    public partial class TrackStatus : Form
    {
        private User currentUser;
        public TrackStatus()
        {
            InitializeComponent();
        }
        public TrackStatus(User user) : this()
        {
            currentUser = user;
            label1.Text= currentUser.FullName;
            label2.Text= currentUser.Username;
            LoadUserBookings();
            dataGridView2.SelectionChanged += DataGridView2_SelectionChanged;

        }

        private void LoadUserBookings()
        {
            string query = @"
SELECT 
    RequestId,
    TurfId,
    RequestedBy,
    ScheduleDate,
    ScheduleTime,
    Duration,
    Price,
    RequestStatus,
    RequestedAt
FROM PendingRequests
WHERE RequestedBy = @username
  AND RequestStatus IN ('Pending', 'Booked','Rescheduled')
ORDER BY ScheduleDate, ScheduleTime";


            using (SqlConnection con = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", currentUser.Username);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.ReadOnly = true;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.MultiSelect = false;
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["RequestStatus"].Value == null) continue;

                string status = row.Cells["RequestStatus"].Value.ToString();

                if (status == "Booked")
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                else if (status == "Pending")
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                else if (status == "Rescheduled")
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }
        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
                return;

            var row = dataGridView2.SelectedRows[0];

            // Fill labels
            turfidlabel.Text = row.Cells["TurfId"].Value.ToString();
            dateTB.Text = Convert.ToDateTime(row.Cells["ScheduleDate"].Value).ToString("yyyy-MM-dd");
            timeslotTB.Text = row.Cells["ScheduleTime"].Value.ToString();
            durationlabel.Text = row.Cells["Duration"].Value.ToString() + " min";
            amountPaidLabel.Text = Convert.ToDecimal(row.Cells["Price"].Value).ToString("0.00");
            Requestedby.Text = row.Cells["RequestedBy"].Value.ToString();
            statusLabel.Text = row.Cells["RequestStatus"].Value.ToString();

            // Calculate remaining time
            DateTime scheduleDate = Convert.ToDateTime(row.Cells["ScheduleDate"].Value);
            TimeSpan scheduleTime = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString());
            DateTime bookingDateTime = scheduleDate.Date + scheduleTime;

            TimeSpan remaining = bookingDateTime - DateTime.Now;

            if (remaining.TotalSeconds > 0)
                remaingtimeLabel.Text = $"{(int)remaining.TotalHours}h {remaining.Minutes}m remaining";
            else
                remaingtimeLabel.Text = "Time passed";

            CreatedAtLabel.Text = row.Cells["RequestedAt"].Value != DBNull.Value ?
                Convert.ToDateTime(row.Cells["RequestedAt"].Value).ToString("yyyy-MM-dd HH:mm") : "N/A";

            // Change panel color based on status
            string status = row.Cells["RequestStatus"].Value?.ToString();

            if (status == "Booked")
                panel1.BackColor = Color.LightGreen;
            else if (status == "Pending")
                panel1.BackColor = Color.LightYellow;
            else if (status == "Rescheduled")
                panel1.BackColor = Color.LightBlue;
            else
                panel1.BackColor = SystemColors.Control; // default
        }


        private void TrackStatus_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show(); 
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile myProfile = new MyProfile(currentUser);
            myProfile.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
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
