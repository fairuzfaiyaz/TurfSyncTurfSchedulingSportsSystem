using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Forms;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class PlayerDashboard : Form
    {
        User currentUser;
        public PlayerDashboard()
        {
            InitializeComponent();
        }
        public PlayerDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
            playerName.Text = user.FullName;
            playerUsername.Text= user.Username;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookTurfSchedule bookTurfSchedule = new BookTurfSchedule(currentUser);
            bookTurfSchedule.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            CancelBooking cancelBooking = new CancelBooking(currentUser);
            cancelBooking.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrackStatus trackStatus = new TrackStatus();
            trackStatus.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        //change profile button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeProfile changeProfile = new ChangeProfile(currentUser);
            changeProfile.Show();
        }


        //my profilebutton
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile myProfile = new MyProfile(currentUser);
            myProfile.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            RescheduleBooking rescheduleBooking = new RescheduleBooking();
            rescheduleBooking.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingHistoryPlayer x = new BookingHistoryPlayer();
            x.Show();
        }
    }
}
