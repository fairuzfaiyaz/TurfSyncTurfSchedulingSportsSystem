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
    public partial class Post_Match_Clean_Up_Confirmation : Form
    {
        User currentstaff;
        public Post_Match_Clean_Up_Confirmation()
        {
            InitializeComponent();
        }
        public Post_Match_Clean_Up_Confirmation(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard();
            turfStaffDashboard.Show();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
