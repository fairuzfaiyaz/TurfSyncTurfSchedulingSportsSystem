using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class Post_Match_Clean_Up_Confirmation : Form
    {
        User currentstaff;
        public Post_Match_Clean_Up_Confirmation()
        {
            InitializeComponent();
        }

        public Post_Match_Clean_Up_Confirmation(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            label3.Text = currentstaff.Username;
            LoadStaffManagementGrid();
        }
        private void LoadStaffManagementGrid()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
SELECT 
    StaffManagementId,
    ScheduleDate,
    ScheduleTime,
    TurfLocation,
    TrashCleaned,
    TurfBrushed,
    WaterBottleRemoved
FROM StaffManagement
ORDER BY ScheduleDate, ScheduleTime";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                dataGridView1.DataSource = dt;

                // Hide ID column (but keep it for updates)
                dataGridView1.Columns["StaffManagementId"].Visible = false;

                StyleGridView();
            }
           }
        private void StyleGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.DefaultCellStyle.Font =
                new Font("Segoe UI", 9);
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard(currentstaff);
            turfStaffDashboard.Show();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var row = dataGridView1.SelectedRows[0];

            radioButton2.Checked = row.Cells["TrashCleaned"].Value?.ToString() == "Yes";
            radioButton3.Checked = row.Cells["TurfBrushed"].Value?.ToString() == "Yes";
            radioButton1.Checked = row.Cells["WaterBottleRemoved"].Value?.ToString() == "Yes";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            int staffManagementId =
                Convert.ToInt32(dataGridView1.SelectedRows[0]
                .Cells["StaffManagementId"].Value);

            string trash = radioButton2.Checked ? "Yes" : "No";
            string brushed = radioButton3.Checked ? "Yes" : "No";
            string bottles = radioButton1.Checked ? "Yes" : "No";

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        UPDATE StaffManagement
        SET 
            TrashCleaned = @Trash,
            TurfBrushed = @Brushed,
            WaterBottleRemoved = @Bottles
        WHERE StaffManagementId = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Trash", trash);
                cmd.Parameters.AddWithValue("@Brushed", brushed);
                cmd.Parameters.AddWithValue("@Bottles", bottles);
                cmd.Parameters.AddWithValue("@Id", staffManagementId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Clean-up status updated successfully.");

            LoadStaffManagementGrid();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
