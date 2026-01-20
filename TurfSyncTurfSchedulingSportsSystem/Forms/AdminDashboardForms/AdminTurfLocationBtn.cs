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

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class AdminTurfLocationBtn : Form
    {
        List<turflocation> tfn = new List<turflocation>();
        public AdminTurfLocationBtn()
        {
            InitializeComponent();
        }

        private void d(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AdminTurfLocationBtn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
            
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                { return; }
            turflocation tf = new turflocation (textBox1.Text , textBox2.Text);
            tfn.Add (tf);

            string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";
            //SqlConnection con = new SqlConnection(ConnectionString);

            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string qry = "SELECT* FROM turfLocation";
            SqlCommand qgv = new SqlCommand(qry, con);
            var reader = qgv.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView3.DataSource = table;



            con.Close();

            dataGridView3.DataSource = null;
            dataGridView3.DataSource = tfn;
            textBox1.Text = string .Empty;
            textBox2 .Text = string .Empty;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first!");
                return;
            }

            // Get selected item
            turflocation selected = dataGridView3.SelectedRows[0].DataBoundItem as turflocation;

            if (selected == null) return;

            // Ask for confirmation
            if (MessageBox.Show("Delete this turf?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";

                //using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();

                    string sql = "DELETE FROM turflocation WHERE Id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", selected.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Remove from list and refresh grid
                tfn.Remove(selected);
                RefeshGrid();

                MessageBox.Show("Deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void RefeshGrid()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = tfn;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";
            //SqlConnection con = new SqlConnection(ConnectionString);
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string qry = "SELECT* FROM turfBookings";
            SqlCommand qgv = new SqlCommand(qry, con);
            var reader = qgv.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView3.DataSource = table;



            con.Close();
        }
    }
}
