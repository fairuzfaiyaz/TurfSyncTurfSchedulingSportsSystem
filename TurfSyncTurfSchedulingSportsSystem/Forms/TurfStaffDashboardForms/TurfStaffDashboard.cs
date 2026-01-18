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
    public partial class TurfStaffDashboard : Form
    {
        User currentstaff;
        public TurfStaffDashboard()
        {
            InitializeComponent();
        }
        public TurfStaffDashboard(User user)
        {
            InitializeComponent();
            this.currentstaff = user;
            label1.Text = currentstaff.Username;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerArrivalCheck check = new CustomerArrivalCheck(currentstaff);
            check.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            StartandEndMatchTimer timer = new StartandEndMatchTimer(currentstaff);
            timer.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            GroundPrepare groundPrepare
                = new GroundPrepare(currentstaff);
            groundPrepare.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            EquipmentHandling equipmentHandling = new EquipmentHandling(currentstaff);
            equipmentHandling.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Post_Match_Clean_Up_Confirmation post_Match_Clean_Up_Confirmation = new Post_Match_Clean_Up_Confirmation(currentstaff);
            post_Match_Clean_Up_Confirmation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile profile = new MyProfile(currentstaff);
            profile.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
