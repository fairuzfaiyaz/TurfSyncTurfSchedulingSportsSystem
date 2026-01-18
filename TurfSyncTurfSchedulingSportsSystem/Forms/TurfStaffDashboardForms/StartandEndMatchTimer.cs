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
    public partial class StartandEndMatchTimer : Form
    {
        User currentstaff;
        public StartandEndMatchTimer()
        {
            InitializeComponent();
        }
        public StartandEndMatchTimer(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            label4.Text = currentstaff.Username;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard(currentstaff);
            turfStaffDashboard.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
