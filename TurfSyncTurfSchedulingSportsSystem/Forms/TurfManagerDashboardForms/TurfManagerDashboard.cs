using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.Forms.TurfManagerDashboardForms;
using TurfSyncTurfSchedulingSportsSystem.Models;


using TurfSyncTurfSchedulingSportsSystem.ServicesBLL;

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class TurfManagerDashboard : Form
    {
        private readonly PendingRequestService service;

        public TurfManagerDashboard()
        {
            InitializeComponent();
            service = new PendingRequestService(); // init service
        }

        public TurfManagerDashboard(User user)
        {
            InitializeComponent();
            service = new PendingRequestService(); // init service
        }

        private void TurfManagerDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            // 1️⃣ Get all requests
            List<PendingRequest> allRequests = service.GetAllPending();

            // 2️⃣ Bind to DataGridView
            dataGridView1.DataSource = allRequests;

            // 3️⃣ Update counts
            lblbooking.Text = allRequests.Count.ToString();
            lblpendingapproval.Text = allRequests.Count(r => r.RequestStatus == "Pending").ToString();
            lblcheckedin.Text = allRequests.Count(r => r.RequestStatus == "Approved").ToString();

            // 4️⃣ Revenue = sum of Price for Approved requests
            decimal revenue = allRequests
                .Where(r => r.RequestStatus == "Approved")
                .Sum(r => r.Price);

            lblrevenue.Text = revenue.ToString("C"); // formatted as currency
        }

        // Navigate to Approve page
        private void lblapprove_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Approve().Show();
        }

        // Navigate to Adding page
        private void lbladding_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddingMenuTurfManager().Show();
        }

        // Navigate to Maintain page
        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();
        }

        // Logout
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new LoginPage().ShowDialog();
        }

        private void lblcheckedin_Click(object sender, EventArgs e)
        {

        }

        private void lblmaintain_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();
        }
    }
}
