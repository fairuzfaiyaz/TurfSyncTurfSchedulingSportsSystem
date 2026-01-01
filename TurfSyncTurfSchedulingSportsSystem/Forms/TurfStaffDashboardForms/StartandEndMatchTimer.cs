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
    public partial class StartandEndMatchTimer : Form
    {
        public StartandEndMatchTimer()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            TurfStaffDashboard turfStaffDashboard = new TurfStaffDashboard();
            turfStaffDashboard.Show();
        }
    }
}
