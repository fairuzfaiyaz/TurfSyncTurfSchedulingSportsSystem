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
    public partial class Dashboard_Users : Form
    {
        public Dashboard_Users()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //AdminDashboard  adminDashboard = new AdminDashboard();
            //adminDashboard.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //TurfManagerDashboard managerDashboard = new TurfManagerDashboard();
            //managerDashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard();
            //turfStaffDashboard.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //PlayerDashboard playerDashboard = new PlayerDashboard();
            //playerDashboard.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage   loginPage = new LoginPage();
            loginPage.Show();
        }
    }
}
