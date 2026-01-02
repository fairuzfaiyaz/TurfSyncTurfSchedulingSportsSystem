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
    public partial class ChangeProfile : Form
    {
        User currentUser;
        public ChangeProfile(User user)
        {
            InitializeComponent();
            currentUser = user;
            emailTextBox.Text = user.Email;
            nameTextBox.Text = user.FullName;
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerDashboard playerDashboard = new PlayerDashboard();
            playerDashboard.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Password Updated Successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Profile Updated Successfully!");

        }
    }
}
