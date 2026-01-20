using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


using TurfSyncTurfSchedulingSportsSystem.DataDLL;


namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class AdminTurfBookingBtn : Form
    {
        List<AdminTurfBookings> atb = new List<AdminTurfBookings>();
        public AdminTurfBookingBtn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text))
                return;
            AdminTurfBookings atbs = new AdminTurfBookings(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            atb.Add(atbs);

            string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";
            
            //SqlConnection con = new SqlConnection(ConnectionString);
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();

            string t1 = textBox1.Text;
            string  n1 = textBox2.Text;
            string  n2 = textBox3.Text;
            string ck = textBox4.Text;
            string Query = "INSERT INTO turfBookings (Turf, CustomerName, BookingDate, BookingTime) \r\nVALUES ('" + t1 + "','" + n1 + "','" + n2 + "', '" + ck + "')";

            SqlCommand cmd = new SqlCommand(Query, con);


            cmd.ExecuteNonQuery();




            con.Close();


            RefreshGrid();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;

        }
       public void RefreshGrid()
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = atb;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Delete selected row(s)?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            
            var list = dataGridView3.DataSource as IList;  

            if (list == null)
            {
                MessageBox.Show("Cannot delete – unsupported data source.");
                return;
            }

           
            var itemsToRemove = new List<object>();

            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                if (row.IsNewRow) continue;
                var item = row.DataBoundItem;
                if (item != null) itemsToRemove.Add(item);
            }

            foreach (var item in itemsToRemove)
            {
                list.Remove(item);
            }

          
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = list;           




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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
