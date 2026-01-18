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
    public partial class MaintainTurfManager : Form
    {
        public MaintainTurfManager()
        {
            InitializeComponent();
        }

        private void lbloverview_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TurfManagerDashboard().Show();

        }

        private void lbladding_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddingMenuTurfManager().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TurfManagerDashboard().Show();
        }
    }
}
