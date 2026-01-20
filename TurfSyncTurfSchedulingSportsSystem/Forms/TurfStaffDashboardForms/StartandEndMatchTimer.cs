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
    public partial class StartandEndMatchTimer : Form
    {
        User currentstaff;
        int remainingSeconds = 0;
        public StartandEndMatchTimer()
        {
            InitializeComponent();
        }
        public StartandEndMatchTimer(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            label4.Text = currentstaff.Username;
            LoadBookedRequests();
        }
        
        public void LoadBookedRequests()
        {
            string query = @"
        SELECT TOP (1000)
            [RequestId],
            [TurfId],
            [ScheduleDate],
            [ScheduleTime],
            [Duration],
            [Price],
            [RequestedBy],
            [RequestStatus],
            [RequestedAt]
        FROM [dbo].[PendingRequests]
        WHERE RequestStatus IN ('Arrived')";

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booked requests: " + ex.Message);
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard(currentstaff);
            turfStaffDashboard.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            int durationMinutes =
                Convert.ToInt32(dataGridView1.SelectedRows[0]
                .Cells["Duration"].Value);

            remainingSeconds = durationMinutes * 60;

            TimerLabel.Text =
                $"{remainingSeconds / 60:D2}:{remainingSeconds % 60:D2}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
                timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                TimerLabel.Text =
                    $"{remainingSeconds / 60:D2}:{remainingSeconds % 60:D2}";
            }
            else
            {
                timer1.Stop();
                TimerLabel.Text = "00:00";
            }
        }
    }
}
