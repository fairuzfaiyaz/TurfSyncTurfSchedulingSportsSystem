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
    public partial class BookTurfSchedule : Form
    {
        private Turf selectedTurf;
        private User currentUser;
        public BookTurfSchedule()
        {
            InitializeComponent();
            turfLocationCombo.Items.Clear();
            durationCombo.Items.Clear();
            timeSlotCombo.Items.Clear();
            BookTurfSchedule_Load(this, EventArgs.Empty);
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }
        public BookTurfSchedule(User user)
        {
            InitializeComponent();
            currentUser = user;
            label1.Text = currentUser.FullName;
            label2.Text = currentUser.Username;

            // Populate controls directly
            LoadTurfSchedule();
            LoadTurfLocations();
            LoadDurations();
            LoadTimeSlots();

            // Subscribe to DataGridView events
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            dataGridView1.ReadOnly = true;


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Map row to Turf object
                selectedTurf = new Turf()
                {
                    TurfId = Convert.ToInt32(row.Cells["TurfId"].Value),
                    TurfLocation = row.Cells["TurfLocation"].Value.ToString(),
                    ScheduleDate = Convert.ToDateTime(row.Cells["ScheduleDate"].Value),
                    ScheduleTime = (TimeSpan)row.Cells["ScheduleTime"].Value,
                    Duration = Convert.ToInt32(row.Cells["Duration"].Value),
                    Price = Convert.ToDecimal(row.Cells["Price"].Value),
                    Status = row.Cells["Status"].Value.ToString()
                };

            }
        }

        private void LoadTurfSchedule()
        {
            string query = "SELECT * FROM dbo.TurfSchedule";
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Optional: make Status column readonly so color shows properly
                    dataGridView1.Columns["Status"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading turf locations: " + ex.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                switch (status)
                {
                    case "Available":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        e.CellStyle.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "Booked":
                        e.CellStyle.BackColor = System.Drawing.Color.Orange;
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        break;
                    case "Arrived":
                        e.CellStyle.BackColor = System.Drawing.Color.LightBlue;
                        e.CellStyle.ForeColor = System.Drawing.Color.Black;
                        break;
                    default:
                        e.CellStyle.BackColor = System.Drawing.Color.White;
                        e.CellStyle.ForeColor = System.Drawing.Color.Black;
                        break;
                }
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchTurfSlots();
        }
        private void SearchTurfSlots()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.TurfSchedule WHERE 1=1";

                    // Turf Location filter
                    if (turfLocationCombo.SelectedIndex != -1 && turfLocationCombo.Text != "All")
                    {
                        query += " AND TurfLocation = @TurfLocation";
                    }

                    // Date filter
                    if (dateTimePicker1.Checked)
                    {
                        query += " AND ScheduleDate = @ScheduleDate";
                    }

                    // Time Slot filter
                    if (timeSlotCombo.SelectedIndex != -1 && timeSlotCombo.Text != "All")
                    {
                        query += " AND ScheduleTime = @ScheduleTime";
                    }

                    // Duration filter
                    if (durationCombo.SelectedIndex != -1 && durationCombo.Text != "All")
                    {
                        query += " AND Duration = @Duration";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Add parameters only if not "All"
                    if (turfLocationCombo.SelectedIndex != -1 && turfLocationCombo.Text != "All")
                        cmd.Parameters.AddWithValue("@TurfLocation", turfLocationCombo.Text);

                    if (dateTimePicker1.Checked)
                        cmd.Parameters.AddWithValue("@ScheduleDate", dateTimePicker1.Value.Date);

                    if (timeSlotCombo.SelectedIndex != -1 && timeSlotCombo.Text != "All")
                        cmd.Parameters.AddWithValue("@ScheduleTime", TimeSpan.Parse(timeSlotCombo.Text));

                    if (durationCombo.SelectedIndex != -1 && durationCombo.Text != "All")
                        cmd.Parameters.AddWithValue("@Duration", int.Parse(durationCombo.Text));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching turf slots: " + ex.Message);
            }
        }

        private void BookTurfSchedule_Load(object sender, EventArgs e)
        {
            LoadTurfSchedule(); // loads all slots initially
            LoadTurfLocations(); // populate turf dropdown
            LoadDurations();     // populate duration dropdown
            LoadTimeSlots();     // optional: populate times dynamically
        }

        private void LoadTurfLocations()
        {
            try
            {
                turfLocationCombo.Items.Clear();
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT TurfLocation FROM dbo.TurfSchedule ORDER BY TurfLocation", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        turfLocationCombo.Items.Add(reader["TurfLocation"].ToString());
                    }
                }

                // Add "All" as first option
                turfLocationCombo.Items.Insert(0, "All");
                turfLocationCombo.SelectedIndex = 0; // default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading turf locations: " + ex.Message);
            }
        }



        private void LoadDurations()
        {
            try
            {
                durationCombo.Items.Clear();
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Duration FROM dbo.TurfSchedule ORDER BY Duration", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        durationCombo.Items.Add(reader["Duration"].ToString());
                    }
                }

                // Add "All" as first option
                durationCombo.Items.Insert(0, "All");
                durationCombo.SelectedIndex = 0; // default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading durations: " + ex.Message);
            }
        }

        private void LoadTimeSlots()
        {
            try
            {
                timeSlotCombo.Items.Clear();
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT ScheduleTime FROM dbo.TurfSchedule ORDER BY ScheduleTime", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TimeSpan time = (TimeSpan)reader["ScheduleTime"];
                        timeSlotCombo.Items.Add(time.ToString(@"hh\:mm\:ss"));
                    }
                }

                // Add "All" as first option
                timeSlotCombo.Items.Insert(0, "All");
                timeSlotCombo.SelectedIndex = 0; // default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading time slots: " + ex.Message);
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            LoadTurfSchedule();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (selectedTurf == null)
            {
                MessageBox.Show("Please select a turf slot to book.");
                return;
            }

            if (selectedTurf.Status != "Available")
            {
                MessageBox.Show("This slot is not available for booking.");
                return;
            }

            // Replace with actual logged-in user's username
            string requestedBy = currentUser.Username;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Insert into PendingRequests
                    string insertQuery = @"
                INSERT INTO dbo.PendingRequests
                (TurfId, ScheduleDate, ScheduleTime, Duration, Price, RequestedBy, RequestStatus)
                VALUES
                (@TurfId, @ScheduleDate, @ScheduleTime, @Duration, @Price, @RequestedBy, 'Pending')";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@TurfId", selectedTurf.TurfId);
                    cmd.Parameters.AddWithValue("@ScheduleDate", selectedTurf.ScheduleDate);
                    cmd.Parameters.AddWithValue("@ScheduleTime", selectedTurf.ScheduleTime);
                    cmd.Parameters.AddWithValue("@Duration", selectedTurf.Duration);
                    cmd.Parameters.AddWithValue("@Price", selectedTurf.Price);
                    cmd.Parameters.AddWithValue("@RequestedBy", requestedBy);
                    cmd.ExecuteNonQuery();

                    // Update TurfSchedule status
                    string updateQuery = "UPDATE dbo.TurfSchedule SET Status = 'Pending' WHERE TurfId = @TurfId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@TurfId", selectedTurf.TurfId);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Turf slot booked! Request is now pending.");

                    // Refresh grid
                    LoadTurfSchedule();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error booking turf: " + ex.Message);
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