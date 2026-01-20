using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard(User user)
        {
            InitializeComponent();
        }
       
        AdminPricingBtn apb = new AdminPricingBtn();


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminManagerBtn amb = new AdminManagerBtn();
            amb.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdminTurfBookingBtn atbb = new AdminTurfBookingBtn();
            atbb.Show();
            this.Hide();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdminSystemReportBtn asrb = new AdminSystemReportBtn();
            asrb .Show(); 
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdminGlobalSettingsBtn agsb = new AdminGlobalSettingsBtn();
            agsb.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AdminPricingBtn apb = new AdminPricingBtn();
            apb.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            AdminTurfBookingBtn atbb = new AdminTurfBookingBtn();
            atbb.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            AdminTurfLocationBtn atlb = new AdminTurfLocationBtn();
            atlb .Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
    }
}
