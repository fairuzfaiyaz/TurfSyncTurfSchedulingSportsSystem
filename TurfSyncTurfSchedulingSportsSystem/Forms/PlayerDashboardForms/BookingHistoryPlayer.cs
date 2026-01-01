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
    public partial class BookingHistoryPlayer : Form
    {
        public BookingHistoryPlayer()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerDashboard playerDashboard = new PlayerDashboard();
            playerDashboard.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
