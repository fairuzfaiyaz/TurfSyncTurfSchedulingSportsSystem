using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms;
using System.Data.SqlClient;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class AdminPricingBtn : Form
    {
        List<Adminpricing> ap = new List<Adminpricing>();
        public AdminPricingBtn()
        {
            InitializeComponent();
        }

        private void AdminPricingBtn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(textBox1.Text)|| (int)numericUpDown2.Value==0 || (int)numericUpDown1.Value == 0)
                return;

            Adminpricing apg = new Adminpricing(textBox1.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, checkBox1.Checked);
            ap.Add(apg);

            string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";

            //SqlConnection con = new SqlConnection(ConnectionString);
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();

            string t1= textBox1.Text;
            int n1 = (int)numericUpDown1.Value;
            int n2 = (int)numericUpDown2.Value;
            bool ck = checkBox1.Checked;
            string Query = "INSERT INTO Packages (PackageName, Price, Duration, IsAvailable)\r\nVALUES ('"+t1+"','"+ n1+"','"+n2 +"', '"+ ck+"')";
          
            SqlCommand cmd = new SqlCommand(Query,con);
           
            
            cmd.ExecuteNonQuery();
           

          

            con.Close();

          

            RefreshGrid();
            textBox1.Clear();
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            checkBox1.Checked = false;

        }
        public void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-JOLM288\\SQLEXPRESS;Initial Catalog=TurfSyncDB;Persist Security Info=True;User ID=sa;Password=123";
            //SqlConnection con = new SqlConnection(ConnectionString);
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string qry = "SELECT* FROM Packages";
            SqlCommand qgv = new SqlCommand(qry, con);
            var reader = qgv.ExecuteReader();
            DataTable table = new DataTable();  
            table.Load(reader);
            dataGridView1.DataSource = table;

        

            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selected = dataGridView1.SelectedRows[0].DataBoundItem as Adminpricing;

                if (selected != null)
                {
                    ap.Remove(selected);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ap;



                }
            }
        }
    }
}
