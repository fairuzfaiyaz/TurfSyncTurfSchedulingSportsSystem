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
using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;



namespace TurfSyncTurfSchedulingSportsSystem.Forms.TurfManagerDashboardForms
{
    public partial class Approve : Form
    {
        private readonly PendingRequestService service;

        public Approve()
        {
            InitializeComponent();
            service = new PendingRequestService(); // init service
        }

        // 🔹 Load data when form opens
        private void Approve_Load(object sender, EventArgs e)
        {
            LoadRequests();
        }

        // 🔹 Load ALL requests (Pending + Approved)
        private void LoadRequests()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.GetAllPending();
        }

        // ✅ APPROVE
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request first.");
                return;
            }

            string status =
                dataGridView1.SelectedRows[0].Cells["RequestStatus"].Value.ToString();

            if (status == "Approved")
            {
                MessageBox.Show("This request is already approved.");
                return;
            }

            int requestId = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["RequestId"].Value);

            service.Approve(requestId);
            LoadRequests(); // auto refresh
        }

        // ❌ CANCEL
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request first.");
                return;
            }

            int requestId = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["RequestId"].Value);

            service.Cancel(requestId);
            LoadRequests(); // auto refresh
        }

        // 🔙 BACK TO DASHBOARD
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

        private void lblmaintain_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new LoginPage().ShowDialog();
        }
    }
}
