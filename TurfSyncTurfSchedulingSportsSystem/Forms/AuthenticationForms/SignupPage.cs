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
using TurfSyncTurfSchedulingSportsSystem.Interfaces;
using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class SignupPage : Form
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void userName_Click(object sender, EventArgs e)
        {

        }

        private void eMail_Click(object sender, EventArgs e)
        {

        }

        private void signUp_Click(object sender, EventArgs e)
        {

        }

        private void passWord_Click(object sender, EventArgs e)
        {

        }

        private void repeatPass_Click(object sender, EventArgs e)
        {

        }

        private void SignupPage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void signUpp_Click(object sender, EventArgs e)
        {
            //IAuthenticator auth = new Authenticator();
            //auth.Signup(txtUsername.Text, txtEmail.Text, txtPassword.Text);

            MessageBox.Show("Account created successfully");
            new LoginPage().Show();
            this.Hide();
        }
    }
}
