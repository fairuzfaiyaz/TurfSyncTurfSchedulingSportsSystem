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
    public partial class CancelBooking : Form
    {
        public CancelBooking()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            PlayerDashboard playerDashboard = new PlayerDashboard();
            playerDashboard.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
