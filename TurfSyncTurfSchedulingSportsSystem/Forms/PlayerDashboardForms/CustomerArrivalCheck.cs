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
    public partial class CustomerArrivalCheck : Form
    {
        User currentstaff;
        public CustomerArrivalCheck()
        {
            InitializeComponent();
        }
        public CustomerArrivalCheck(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            LoadBookedRequests();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int requestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RequestId"].Value);

                string query = "UPDATE dbo.PendingRequests SET RequestStatus = 'Arrived' WHERE RequestId = @RequestId";

                try
                {
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@RequestId", requestId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Customer arrival confirmed!");
                                LoadBookedRequests(); 
                            }
                            else
                            {
                                MessageBox.Show("Failed to update status.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a request first.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
        WHERE RequestStatus IN ('Booked', 'Arrived')";

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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard();
            turfStaffDashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
