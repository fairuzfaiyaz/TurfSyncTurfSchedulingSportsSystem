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
    public partial class TurfStaffDashboard : Form
    {
        public TurfStaffDashboard()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Dashboard_Users dashboard_Users = new Dashboard_Users();
            dashboard_Users.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerArrivalCheck check = new CustomerArrivalCheck();
            check.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            StartandEndMatchTimer timer = new StartandEndMatchTimer();
            timer.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            GroundPrepare groundPrepare
                = new GroundPrepare();
            groundPrepare.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            EquipmentHandling equipmentHandling = new EquipmentHandling();
            equipmentHandling.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Post_Match_Clean_Up_Confirmation post_Match_Clean_Up_Confirmation = new Post_Match_Clean_Up_Confirmation();
            post_Match_Clean_Up_Confirmation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }
    }
}
