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
    public partial class GroundPrepare : Form
    {
        User currentstaff;
        public GroundPrepare()
        {
            InitializeComponent();
        }
        public GroundPrepare(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            LoadStaffManagementData();
            label3.Text = currentstaff.Username;
        }
        private void LoadStaffManagementData()
        {
           
            string query = @"SELECT StaffManagementId, TurfId, ScheduleDate, ScheduleTime,
                            TurfLocation, Duration, LightsStatus
                     FROM StaffManagement
                     ORDER BY ScheduleDate, ScheduleTime";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//back button
        {
            this.Close();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard(currentstaff);
            turfStaffDashboard.Show();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
           
            // 1. Check if a row is selected
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check if a radio button is selected
            string newStatus = "";
            if (radioButton2.Checked)
                newStatus = "On";
            else if (radioButton3.Checked)
                newStatus = "Off";
            else
            {
                MessageBox.Show("Please select Lights Status (On or Off).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Get selected row ID
            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StaffManagementId"].Value);

            // 4. Update the database
            
            string updateQuery = "UPDATE StaffManagement SET LightsStatus=@Status WHERE StaffManagementId=@Id";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@Id", selectedId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                    MessageBox.Show("Lights Status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 5. Reload data to reflect changes
            LoadStaffManagementData(); // Use the method we created earlier
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string currentStatus = dataGridView1.SelectedRows[0].Cells["LightsStatus"].Value.ToString();
                radioButton2.Checked = currentStatus == "On";
                radioButton3.Checked = currentStatus == "Off";
            }
        }
    }
    }

