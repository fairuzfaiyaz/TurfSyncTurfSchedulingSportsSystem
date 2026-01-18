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
    public partial class EquipmentHandling : Form
    {
        User currentstaff;
        public EquipmentHandling()
        {
            InitializeComponent();
        }

        public EquipmentHandling(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            LoadStaffStatus();
            label5.Text = currentstaff.Username;
        }
       
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];

                // Example: Set radio buttons based on FootballStatus
                string football = row.Cells["FootballStatus"].Value.ToString();
                if (football == "Issued") radioButton2.Checked = true;
                else if (football == "Returned") radioButton4.Checked = true;
                else if (football == "Damaged") radioButton5.Checked = true;

                // Repeat for BibsStatus and ConesStatus similarly
            }
        }
        private void LoadStaffStatus()
        {
            try
            {
                string query = @"
            SELECT 
                TurfId,
                ScheduleDate,
                ScheduleTime,
                LightsStatus,
                FootballStatus,
                BibsStatus,
                ConesStatus
            FROM StaffManagement
            ORDER BY ScheduleDate, ScheduleTime";

                using (SqlConnection con = DatabaseHelper.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Visual enhancements
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.MultiSelect = false;

                    // Optional: color rows based on LightsStatus
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["LightsStatus"].Value != null)
                        {
                            string status = row.Cells["LightsStatus"].Value.ToString();
                            row.DefaultCellStyle.BackColor = status == "On" ? Color.LightGreen : Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading staff management: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var row = dataGridView1.SelectedRows[0];
            int turfId = Convert.ToInt32(row.Cells["TurfId"].Value);
            DateTime date = Convert.ToDateTime(row.Cells["ScheduleDate"].Value);
            TimeSpan time = (TimeSpan)row.Cells["ScheduleTime"].Value;

            string footballStatus = radioButton2.Checked ? "Issued" :
                                    radioButton4.Checked ? "Returned" : "Damaged";
            string bibsStatus = radioButton1.Checked ? "Issued" :
                                radioButton7.Checked ? "Returned" : "Damaged";
            string conesStatus = radioButton3.Checked ? "Issued" :
                                 radioButton9.Checked ? "Returned" : "Damaged";

            string updateQuery = @"
        UPDATE StaffManagement
        SET FootballStatus=@football, BibsStatus=@bibs, ConesStatus=@cones
        WHERE TurfId=@turf AND ScheduleDate=@date AND ScheduleTime=@time";

            using (SqlConnection con = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
            {
                cmd.Parameters.AddWithValue("@football", footballStatus);
                cmd.Parameters.AddWithValue("@bibs", bibsStatus);
                cmd.Parameters.AddWithValue("@cones", conesStatus);
                cmd.Parameters.AddWithValue("@turf", turfId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@time", time);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Equipment status updated successfully!");
                LoadStaffStatus(); // Refresh DataGridView
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
           
        
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];

                // Football Status
                string football = row.Cells["FootballStatus"].Value?.ToString() ?? "";
                radioButton2.Checked = football == "Issued";
                radioButton4.Checked = football == "Returned";
                radioButton5.Checked = football == "Damaged";

                // Bibs Status
                string bibs = row.Cells["BibsStatus"].Value?.ToString() ?? "";
                radioButton1.Checked = bibs == "Issued";
                radioButton7.Checked = bibs == "Returned";
                radioButton6.Checked = bibs == "Damaged";

                // Cones Status
                string cones = row.Cells["ConesStatus"].Value?.ToString() ?? "";
                radioButton3.Checked = cones == "Issued";
                radioButton9.Checked = cones == "Returned";
                radioButton8.Checked = cones == "Damaged";

                // Optional: LightsStatus color (if you want to show in UI)
                string lights = row.Cells["LightsStatus"].Value?.ToString() ?? "";
                if (lights == "On")
                    dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.LightGreen;
                else
                    dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.LightGray;
            }
        }
    }
    }

