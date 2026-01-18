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
    public partial class BookingHistoryPlayer : Form
    {
        User currentUser;

        public BookingHistoryPlayer()
        {
            InitializeComponent();
        }
        public BookingHistoryPlayer(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadPendingRequests();
        }
        private void LoadPendingRequests()
        {
            try
            {
                string query = @"
            SELECT RequestId, TurfId, ScheduleDate, ScheduleTime, Duration, Price,
                   RequestedBy, RequestStatus, RequestedAt
            FROM PendingRequests WHERE RequestStatus='Arrived'
            ORDER BY ScheduleDate, ScheduleTime";

                using (SqlConnection con = DatabaseHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // GridView visual enhancements
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.AllowUserToOrderColumns = true;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.GridColor = Color.Gray;
                    dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    // Color code rows based on RequestStatus
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["RequestStatus"].Value == null) continue;
                        string status = row.Cells["RequestStatus"].Value.ToString();

                        switch (status)
                        {
                            case "Arrived":
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                                break;
                            case "Pending":
                                row.DefaultCellStyle.BackColor = Color.LightYellow;
                                break;
                            case "Booked":
                                row.DefaultCellStyle.BackColor = Color.LightSalmon;
                                break;
                            case "Rescheduled":
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                break;
                            default:
                                row.DefaultCellStyle.BackColor = Color.White;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending requests: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
