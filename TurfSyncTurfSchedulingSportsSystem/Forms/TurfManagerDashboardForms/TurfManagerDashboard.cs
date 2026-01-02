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
    public partial class TurfManagerDashboard : Form
    {
        public TurfManagerDashboard()
        {
            InitializeComponent();
        }
        public TurfManagerDashboard(User user)
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            this.Dispose();
            MaintainTurfManager maintainTurfManager = new MaintainTurfManager();
            maintainTurfManager.ShowDialog();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.Close();
            AddingMenuTurfManager   adding = new AddingMenuTurfManager();
            adding.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginPage loginPage = new LoginPage();
            loginPage.ShowDialog();
        }
    }
}
