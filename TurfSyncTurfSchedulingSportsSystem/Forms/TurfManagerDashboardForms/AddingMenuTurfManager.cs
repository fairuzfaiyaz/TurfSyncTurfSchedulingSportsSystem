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

namespace TurfSyncTurfSchedulingSportsSystem.Forms
{
    public partial class AddingMenuTurfManager : Form
    {
        private readonly TurfScheduleService service = new TurfScheduleService();

        public AddingMenuTurfManager()
        {
            InitializeComponent();
        }

        private void AddingMenuTurfManager_Load(object sender, EventArgs e)
        {
            // Load Status dropdown
            status.Items.Clear();
            status.Items.Add("Available");
            status.Items.Add("Booked");
            status.Items.Add("Maintenance");
            status.SelectedIndex = 0;

            LoadGrid();
        }

        // Load DataGrid
        private void LoadGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.GetAllSchedules();
        }

        // SAVE
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                TurfSchedule slot = new TurfSchedule
                {
                    TurfId = int.Parse(turfidtxt.Text),
                    ScheduleDate = dateTimePickerslotdate.Value.Date,
                    ScheduleTime = dateTimePickerslottime.Value.TimeOfDay,
                    TurfLocation = locationtxt.Text,
                    Duration = int.Parse(durationtxt.Text),
                    Price = price.Value,
                    Status = status.Text
                };

                service.AddSchedule(slot);
                MessageBox.Show("Slot added successfully!");
                LoadGrid();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // UPDATE
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                TurfSchedule slot = new TurfSchedule
                {
                    TurfId = int.Parse(turfidtxt.Text),
                    ScheduleDate = dateTimePickerslotdate.Value.Date,
                    ScheduleTime = dateTimePickerslottime.Value.TimeOfDay,
                    TurfLocation = locationtxt.Text,
                    Duration = int.Parse(durationtxt.Text),
                    Price = price.Value,
                    Status = status.Text
                };

                service.UpdateSchedule(slot);
                MessageBox.Show("Slot updated successfully!");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // CLEAR
        private void button3_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            turfidtxt.Text = "";
            locationtxt.Text = "";
            durationtxt.Text = "";
            price.Value = 0;
            status.SelectedIndex = 0;
        }

        // GRID CLICK → LOAD DATA TO FIELDS
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            turfidtxt.Text = row.Cells["TurfId"].Value.ToString();
            locationtxt.Text = row.Cells["TurfLocation"].Value.ToString();
            durationtxt.Text = row.Cells["Duration"].Value.ToString();
            price.Value = Convert.ToDecimal(row.Cells["Price"].Value);
            status.Text = row.Cells["Status"].Value.ToString();
        }

        // NAVIGATION
        private void lbloverview_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TurfManagerDashboard().Show();
        }

        private void lblmaintain_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MaintainTurfManager().Show();
        }
    }
}
