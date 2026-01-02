//using singup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Forms;
using TurfSyncTurfSchedulingSportsSystem.Interfaces;
using TurfSyncTurfSchedulingSportsSystem.Models;
using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;


namespace TurfSyncTurfSchedulingSportsSystem
{
    public partial class LoginPage : Form
    {
        private readonly Authenticator authenticator;

        public LoginPage()
        {
            InitializeComponent();
            authenticator = new Authenticator(new UserRepository());
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
            //Dashboard_Users playerDashboard = new Dashboard_Users();
            //playerDashboard.Show();
            //this.Hide();

            string usernameInput = username.Text.Trim();
            string passwordInput = textBox1.Text.Trim();


            if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (authenticator.Login(usernameInput, passwordInput, out var user))
            {
                MessageBox.Show($"Welcome, {user.FullName}! Role: {user.Role}", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Form dashboard = null;

                // Open dashboard based on user role
                switch (user.Role.ToLower())
                {
                    case "player":
                        dashboard = new PlayerDashboard(user);
                        break;
                    case "admin":
                        dashboard = new AdminDashboard(user);
                        break;
                    case "manager":
                        dashboard = new TurfManagerDashboard(user);
                        break;
                    case "staff":
                        dashboard = new TurfStaffDashboard(user);
                        break;
                    default:
                        MessageBox.Show("Unknown role. Cannot open dashboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                dashboard.Show();
                this.Hide(); // Hide the login form

                // Open main dashboard or next form
                // Example:
                // var dashboard = new DashboardForm(user);
                // dashboard.Show();
                // this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            // Optional: clear password textbox
                //textBox1.Clear();
                //textBox1.Focus();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignupPage signupPage = new SignupPage();
            //Application.Run(signupPage);
            signupPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            //if (username.Click() )
            //{   
            //    username.Clear();
        }
    }
}
