using singup;
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

namespace TurfSyncTurfSchedulingSportsSystem
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SignupPage signupPage= new SignupPage();
            signupPage.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            //if (pictureBox7.Click)
            //{
            //    this.pictureBox7.Image = global::TurfSyncTurfSchedulingSportsSystem.Properties.Resources.hidden;
            //    textBox2.UseSystemPasswordChar = true;
            //}

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            PlayerDashboard playerDashboard = new PlayerDashboard();
            playerDashboard.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            SignupPage signupPage = new SignupPage();
            //Application.Run(signupPage);
            signupPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
