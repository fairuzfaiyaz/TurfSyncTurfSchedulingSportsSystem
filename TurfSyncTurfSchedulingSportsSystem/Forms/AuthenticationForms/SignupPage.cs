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
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;



namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class SignupPage : Form
    {
        private readonly Authenticator authenticator;

        public SignupPage()
        {
            InitializeComponent();
            authenticator = new Authenticator(new UserRepository());
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

            string fullName = textBox5.Text.Trim();
            string username = textBox3.Text.Trim();
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;
            string repeatPassword = textBox4.Text;

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkBox1.Checked)
            {
                MessageBox.Show("You must agree to the terms and conditions to sign up.", "Agreement Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool created = authenticator.Signup(fullName, username, email, password, repeatPassword);
                if (created)
                {
                    MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close signup form
                    new LoginPage().Show();
                }
                else
                {
                    MessageBox.Show("Username already exists. Choose another one.", "Signup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Signup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
