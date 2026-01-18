using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Forms.TurfManagerDashboardForms;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class TurfManagerDashboard : Form
    {
        public TurfManagerDashboard()
        {
            InitializeComponent();
        }
        public TurfManagerDashboard(User user)
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            //this.Dispose();
            //MaintainTurfManager maintainTurfManager = new MaintainTurfManager();
            //maintainTurfManager.ShowDialog();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.Close();
            AddingMenuTurfManager   adding = new AddingMenuTurfManager();
            adding.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();
        }

        private void lbladding_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddingMenuTurfManager().Show();
        }

        private void lblapprove_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Approve().Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TurfManagerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblpendingapproval_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblbooking_Click(object sender, EventArgs e)
        {

        }

        private void lblpendingapproval_Click_1(object sender, EventArgs e)
        {

        }

        private void lblcheckedin_Click(object sender, EventArgs e)
        {

        }

        private void lblrevenue_Click(object sender, EventArgs e)
        {

        }
    }
}
