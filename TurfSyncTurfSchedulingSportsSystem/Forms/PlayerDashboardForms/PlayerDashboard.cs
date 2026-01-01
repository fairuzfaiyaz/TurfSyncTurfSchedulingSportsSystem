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

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class PlayerDashboard : Form
    {
        public PlayerDashboard()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
            BookTurfSchedule bookTurfSchedule = new BookTurfSchedule();
            bookTurfSchedule.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            CancelBooking cancelBooking = new CancelBooking();
            cancelBooking.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Dispose();
            TrackStatus trackStatus = new TrackStatus();
            trackStatus.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            ChangeProfile changeProfile = new ChangeProfile();
            changeProfile.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MyProfile myProfile = new MyProfile();
            myProfile.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Dispose();
            RescheduleBooking rescheduleBooking = new RescheduleBooking();
            rescheduleBooking.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Dispose();
            BookingHistoryPlayer x = new BookingHistoryPlayer();
            x.Show();
        }
    }
}
