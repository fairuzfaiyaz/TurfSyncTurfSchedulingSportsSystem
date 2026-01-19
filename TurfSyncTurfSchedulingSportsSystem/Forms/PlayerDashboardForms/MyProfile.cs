using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class MyProfile : Form
    {
        Models.User currentUser;
        public MyProfile()
        {
            InitializeComponent();
        }
        public MyProfile(Models.User user)
        {
            InitializeComponent();
            currentUser = user;
            Email.Text = user.Email;
            password.Text = user.PhoneNumber; //number
            label1.Text = user.Gender;


            playerName.Text = user.FullName;
            playerUsername.Text = user.Username;
            //label8.Text = user.Email;
            accCreatDate.Text = user.CreatedAt.ToString("dd MMMM yyyy");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            ChangeProfile changeProfile = new ChangeProfile(currentUser);
            changeProfile.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
    }
}
