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
    public partial class MaintainTurfManager : Form
    {
        private readonly TurfScheduleService service = new TurfScheduleService();

        public MaintainTurfManager()
        {
            InitializeComponent();
        }

        private void MaintainTurfManager_Load(object sender, EventArgs e)
        {
            // Fill status dropdown
            Status.Items.Clear();
            Status.Items.Add("Available");
            Status.Items.Add("Maintenance");
            Status.Items.Add("Booked");
            Status.SelectedIndex = 0;

            LoadGrid();
        }

        private void LoadGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.GetAllSchedules();
        }

        private void button2_Click(object sender, EventArgs e) // Save status
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a schedule from the table.");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            DateTime date = (DateTime)row.Cells["ScheduleDate"].Value;
            TimeSpan time = (TimeSpan)row.Cells["ScheduleTime"].Value;
            string location = row.Cells["TurfLocation"].Value.ToString();
            string newStatus = Status.SelectedItem.ToString();

            try
            {
                service.UpdateStatusByRow(date, time, location, newStatus);
                MessageBox.Show("Schedule status updated!");
                LoadGrid(); // refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        //price control



        //save btn for price control
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get all schedules
                List<TurfSchedule> schedules = service.GetAllSchedules();

                foreach (var schedule in schedules)
                {
                    decimal oldPrice = schedule.Price;
                    decimal newPrice = oldPrice;
                    bool priceChanged = false;

                    // Check night pricing (after 6 PM)
                    if (chkNight.Checked && schedule.ScheduleTime.Hours >= 18)
                    {
                        newPrice += 500;
                        priceChanged = true;
                    }

                    // Check weekend pricing (Friday/Saturday)
                    if (chkWeekend.Checked &&
                        (schedule.ScheduleDate.DayOfWeek == DayOfWeek.Friday ||
                         schedule.ScheduleDate.DayOfWeek == DayOfWeek.Saturday))
                    {
                        newPrice += 500;
                        priceChanged = true;
                    }

                    // Update price if it changed
                    if (priceChanged)
                    {
                        service.UpdatePriceByRow(schedule.ScheduleDate, schedule.ScheduleTime, schedule.TurfLocation, newPrice);

                        // Optional: show a message for each row updated
                        Console.WriteLine($"Updated {schedule.TurfLocation} on {schedule.ScheduleDate:d} from {oldPrice:C} to {newPrice:C}");
                    }
                }

                MessageBox.Show("Pricing updated automatically based on night/weekend settings!");
                LoadGrid(); // refresh the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating prices: " + ex.Message);
            }
        }

       
    }

}


