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
    public partial class BookTurfSchedule : Form
    {
        public BookTurfSchedule()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
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
