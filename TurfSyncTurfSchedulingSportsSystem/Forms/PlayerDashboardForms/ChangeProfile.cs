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
using TurfSyncTurfSchedulingSportsSystem.Models;
using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{

    public partial class ChangeProfile : Form
    {
        private readonly Authenticator authenticator;
        private User currentUser;
        private UserRepository userRepository; 
        public ChangeProfile(User user)
        {
            InitializeComponent();
            currentUser = user;
            userRepository = new UserRepository();
            authenticator = new Authenticator(userRepository);


            email.Text = user.Email;
            fullName.Text = user.FullName;
            username.Text = user.Username;
            role.Text = user.Role;
            phonenumber.Text = user.PhoneNumber;
            Gender.Text = user.Gender;
            DateOfBirth.Text = user.DateOfBirth?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");

            fullNameTextBox.Text = user.FullName;
            usernameTextBox.Text = user.Username;
            EmailTextBox.Text = user.Email;
            PhoneNumberTextBox.Text = user.PhoneNumber;
            dateTimePicker.Value = user.DateOfBirth ?? DateTime.Now;


            if (user.Gender == "Male")
                MaleRadioBtn.Checked = true;
            else if (user.Gender == "Female")
                femaleRadioBtn.Checked = true;
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
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string current_Password = currentPassword.Text.Trim();
            string new_Password = newPassword.Text.Trim();
            string repeat_Password = RptNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(current_Password) ||
                string.IsNullOrEmpty(new_Password) ||
                string.IsNullOrEmpty(repeat_Password))
            {
                MessageBox.Show("Please fill all password fields.");
                return;
            }

            if (new_Password != repeat_Password)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            bool success = authenticator.ChangePassword(
                currentUser,
                current_Password,
                new_Password
            );

            if (success)
            {
                MessageBox.Show("Password updated successfully!");

                currentPassword.Clear();
                newPassword.Clear();
                RptNewPassword.Clear();
                this.Dispose();
                new LoginPage().Show();
            }
            else
            {
                MessageBox.Show("Current password is incorrect.");
            }
        }


        //profile update button
        private void button2_Click(object sender, EventArgs e)
        {
            // Update the currentUser object from form fields
            currentUser.FullName = fullNameTextBox.Text.Trim();
            currentUser.Username = usernameTextBox.Text.Trim();
            currentUser.Email = EmailTextBox.Text.Trim();
            currentUser.PhoneNumber = PhoneNumberTextBox.Text.Trim();
            //currentUser.Gender = GenderTextBox.Text.Trim();
            currentUser.DateOfBirth = dateTimePicker.Value;
            if (MaleRadioBtn.Checked)
                currentUser.Gender = "Male";
            else if (femaleRadioBtn.Checked)
                currentUser.Gender = "Female";
            else
                currentUser.Gender = null; // Optional: if none selected
            // Call repository to save changes
            bool success = userRepository.UpdateUserProfile(currentUser);

            if (success)
            {
                // Update labels immediately
                fullName.Text = currentUser.FullName;
                username.Text = currentUser.Username;
                email.Text = currentUser.Email;
                phonenumber.Text = currentUser.PhoneNumber;
                Gender.Text = currentUser.Gender;
                DateOfBirth.Text = currentUser.DateOfBirth?.ToString("yyyy-MM-dd") ?? "";

                MessageBox.Show("Profile Updated Successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.");
            }
        }
    }
}
