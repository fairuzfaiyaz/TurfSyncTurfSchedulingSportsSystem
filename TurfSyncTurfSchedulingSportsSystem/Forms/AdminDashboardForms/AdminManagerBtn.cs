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
using TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms;
using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
   
    public partial class AdminManagerBtn : Form
    {
        List<adminStaff> ad = new List<adminStaff>();
        public AdminManagerBtn()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
         string.IsNullOrWhiteSpace(textBox2.Text) ||
         string.IsNullOrWhiteSpace(textBox3.Text) ||
         string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error");
                return;
            }

            string connectionString = @"Data Source=DESKTOP-JOLM288\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";

            string query = @"
        INSERT INTO Users 
        (FullName, Username, Email, PasswordHash, Role)
        VALUES 
        (@FullName, @Username, @Email, @PasswordHash, @Role)
    ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // IMPORTANT: adjust parameter names & order to match your textboxes
                    cmd.Parameters.AddWithValue("@FullName", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", textBox3.Text.Trim());

                    // Very important: NEVER store plain password!
                    // For real app → use proper hashing (BCrypt, Argon2, PBKDF2)
                    string plainPassword = textBox4.Text;
                    string hashedPassword = HashPassword(plainPassword);   // ← implement this!
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    // Decide role – example: hard-coded or from another control
                    cmd.Parameters.AddWithValue("@Role", "User");           // ← change as needed

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Staff member added successfully!", "Success");
                        ClearTextBoxes();
                        gridRefresh();     // reload DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No rows were inserted.", "Warning");
                    }
                }
            }
            catch (SqlException ex)
            {
                // Common errors: duplicate username, wrong connection, etc.
                MessageBox.Show($"Database error:\n{ex.Message}", "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error:\n{ex.Message}", "Error");
            }
        }

        // Quick helper
        private void ClearTextBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }


        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void gridRefresh()
        {
            string connectionString = @"Data Source=DESKTOP-JOLM288\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";

            string selectQuery = "SELECT UserId, FullName, Username, Email, Role, IsActive, CreatedAt FROM Users WHERE IsActive = 1";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    staffgridview.DataSource = null;    // clear old binding
                    staffgridview.DataSource = dt;      // bind new data
                                                        // Optional: staffgridview.Columns["UserId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing grid:\n" + ex.Message);
            }
        }
        public void managergridRefesh()
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (staffgridview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first!");
                return;
            }

            // Get UserId from the selected row
            int userId;
            try
            {
                userId = Convert.ToInt32(staffgridview.SelectedRows[0].Cells["UserId"].Value);
            }
            catch
            {
                MessageBox.Show("Cannot read User ID from selected row.");
                return;
            }

            // Confirm with user
            DialogResult result = MessageBox.Show("Really deactivate this user?",
                                                 "Confirm",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Your connection string
            string connectionString = @"Data Source=DESKTOP-JOLM288\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";

            string sql = "UPDATE Users SET IsActive = 0 WHERE UserId = @UserId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("User deactivated successfully.");
                        gridRefresh();  // reload the grid
                    }
                    else
                    {
                        MessageBox.Show("No record was updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void staffgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
