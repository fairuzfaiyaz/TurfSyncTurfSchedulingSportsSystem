using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.Forms.PlayerDashboardForms
{
    public partial class RateTurfService : Form
    {
        User currentUser;
        
        public RateTurfService()
        {
            InitializeComponent();
        }
        public RateTurfService(User user)
        {
            InitializeComponent();
            currentUser = user;
            label1.Text = user.FullName;
            label2.Text = user.Username;
            DisableRatingControls();
            LoadArrivedRequests();   // dataGridView2
            LoadRatingOptions();
            LoadRatedServices();     // dataGridView1 ✅
            //dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;

        }
        private void DisableRatingControls()
        {
            cmbOverallRating.Enabled = false;
            cmbServiceQuality.Enabled = false;
            cmbTurfCondition.Enabled = false;
            cmbLighting.Enabled = false;
            cmbEquipment.Enabled = false;
            cmbCleanliness.Enabled = false;

            CommentTextBox.Enabled = false;
            button2.Enabled = false; // Rate selected match
        }

        private void LoadArrivedRequests()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        SELECT 
            RequestId,
            TurfId,
            ScheduleDate,
            ScheduleTime,
            Duration,
            Price,
            RequestedBy,
            RequestedAt
        FROM PendingRequests
        WHERE RequestStatus = 'Arrived'
        AND RequestedBy = @user
        ORDER BY ScheduleDate DESC, ScheduleTime DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", currentUser.Username);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;
            }

            StylePlayedMatchesGrid();

            // 🔴 IMPORTANT FIX
            dataGridView2.ClearSelection();
            DisableRatingControls();
        }

        private void LoadRatedServices()
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        SELECT 
            sr.RatingId,
            sr.RequestId,
            sr.TurfId,
            sr.OverallRating,
            sr.ServiceQualityRating,
            sr.TurfConditionRating,
            sr.LightingRating,
            sr.EquipmentRating,
            sr.CleanlinessRating,
            sr.Comment,
            sr.RatedAt
        FROM ServiceRatings sr
        WHERE sr.RatedBy = @user
        ORDER BY sr.RatedAt DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", currentUser.Username);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }

            StyleRatedServicesGrid();
        }
        private void StyleRatedServicesGrid()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Century Gothic", 11, FontStyle.Bold);

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void StylePlayedMatchesGrid()
        {
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;

            dataGridView2.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font =
                new Font("Century Gothic", 11, FontStyle.Bold);

            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
        }


        private void LoadRatingOptions()
        {
            string[] ratings = { "Excellent", "Good", "Average", "Bad", "Poor" };

            cmbOverallRating.Items.Clear();
            cmbServiceQuality.Items.Clear();
            cmbTurfCondition.Items.Clear();
            cmbLighting.Items.Clear();
            cmbEquipment.Items.Clear();
            cmbCleanliness.Items.Clear();


            cmbOverallRating.Items.AddRange(ratings);
            cmbServiceQuality.Items.AddRange(ratings);
            cmbTurfCondition.Items.AddRange(ratings);
            cmbLighting.Items.AddRange(ratings);
            cmbEquipment.Items.AddRange(ratings);
            cmbCleanliness.Items.AddRange(ratings);

            cmbOverallRating.SelectedIndex = 0;
            cmbServiceQuality.SelectedIndex = 0;
            cmbTurfCondition.SelectedIndex = 0;
            cmbLighting.SelectedIndex = 0;
            cmbEquipment.SelectedIndex = 0;
            cmbCleanliness.SelectedIndex = 0;
        }


        private void RateTurfService_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                DisableRatingControls();
                return;
            }

            var row = dataGridView2.SelectedRows[0];

            turfidlabel.Text = row.Cells["TurfId"].Value.ToString();
            dateTB.Text = Convert.ToDateTime(row.Cells["ScheduleDate"].Value).ToString("yyyy-MM-dd");
            timeslotTB.Text = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString()).ToString(@"hh\:mm");
            durationlabel.Text = row.Cells["Duration"].Value + " mins";
            amountPaidLabel.Text = row.Cells["Price"].Value + " BDT";
            RequestedbyLabel.Text = row.Cells["RequestedBy"].Value.ToString();
            CreatedAtLabel.Text = Convert.ToDateTime(row.Cells["RequestedAt"].Value).ToString("yyyy-MM-dd HH:mm");

            CalculateRemainingTime(row);
            ClearRatingInputs();

            EnableRatingControls();
        }
        private bool AlreadyRated(int requestId)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT COUNT(*) FROM ServiceRatings 
                         WHERE RequestId = @rid AND RatedBy = @user";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rid", requestId);
                cmd.Parameters.AddWithValue("@user", currentUser.Username);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void EnableRatingControls()
        {
            cmbOverallRating.Enabled = true;
            cmbServiceQuality.Enabled = true;
            cmbTurfCondition.Enabled = true;
            cmbLighting.Enabled = true;
            cmbEquipment.Enabled = true;
            cmbCleanliness.Enabled = true;

            CommentTextBox.Enabled = true;
            button2.Enabled = true;
        }

        private void CalculateRemainingTime(DataGridViewRow row)
        {
            DateTime startDate = Convert.ToDateTime(row.Cells["ScheduleDate"].Value);
            TimeSpan startTime = TimeSpan.Parse(row.Cells["ScheduleTime"].Value.ToString());
            int durationMinutes = Convert.ToInt32(row.Cells["Duration"].Value);

            DateTime startDateTime = startDate.Date + startTime;
            DateTime endTime = startDateTime.AddMinutes(durationMinutes);

            TimeSpan remaining = endTime - DateTime.Now;

            if (remaining.TotalMinutes <= 0)
                remaingtimeLabel.Text = "Completed";
            else
                remaingtimeLabel.Text = $"{remaining.Hours}h {remaining.Minutes}m";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlayerDashboard playerDashboard = new PlayerDashboard(currentUser);
            playerDashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a match first.");
                return;
            }

            int requestId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["RequestId"].Value);
            int turfId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["TurfId"].Value);

            if (AlreadyRated(requestId))
            {
                MessageBox.Show("You have already rated this match.");
                return;
            }

            if (string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                MessageBox.Show("Please write a comment.");
                return;
            }

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        INSERT INTO ServiceRatings
        (RequestId, TurfId, RatedBy, OverallRating, ServiceQualityRating,
         TurfConditionRating, LightingRating, EquipmentRating, CleanlinessRating, Comment)
        VALUES
        (@RequestId, @TurfId, @RatedBy, @Overall, @ServiceQuality,
         @TurfCondition, @Lighting, @Equipment, @Cleanliness, @Comment)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@RequestId", requestId);
                cmd.Parameters.AddWithValue("@TurfId", turfId);
                cmd.Parameters.AddWithValue("@RatedBy", currentUser.Username);
                cmd.Parameters.AddWithValue("@Overall", cmbOverallRating.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ServiceQuality", cmbServiceQuality.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@TurfCondition", cmbTurfCondition.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Lighting", cmbLighting.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Equipment", cmbEquipment.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Cleanliness", cmbCleanliness.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Comment", CommentTextBox.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thank you for rating our service!");
            LoadRatedServices();   // 🔥 THIS is what you wanted
            ClearRatingInputs();
        }
        private void ClearRatingInputs()
        {
            cmbOverallRating.SelectedIndex = 0;
            cmbServiceQuality.SelectedIndex = 0;
            cmbTurfCondition.SelectedIndex = 0;
            cmbLighting.SelectedIndex = 0;
            cmbEquipment.SelectedIndex = 0;
            cmbCleanliness.SelectedIndex = 0;

            CommentTextBox.Clear();
        }

        private void updateExistingRating_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rating to update.");
                return;
            }

            int ratingId = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["RatingId"].Value
            );

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        UPDATE ServiceRatings SET
            OverallRating = @Overall,
            ServiceQualityRating = @ServiceQuality,
            TurfConditionRating = @TurfCondition,
            LightingRating = @Lighting,
            EquipmentRating = @Equipment,
            CleanlinessRating = @Cleanliness,
            Comment = @Comment
        WHERE RatingId = @RatingId
        AND RatedBy = @user";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Overall", cmbOverallRating.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ServiceQuality", cmbServiceQuality.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@TurfCondition", cmbTurfCondition.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Lighting", cmbLighting.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Equipment", cmbEquipment.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Cleanliness", cmbCleanliness.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Comment", CommentTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@RatingId", ratingId);
                cmd.Parameters.AddWithValue("@user", currentUser.Username);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Rating updated successfully.");

            LoadRatedServices();
            ClearRatingInputs();
        }


        private void deleteExistingRating_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rating to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this rating?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            int ratingId = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["RatingId"].Value
            );

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                string query = @"
        DELETE FROM ServiceRatings
        WHERE RatingId = @RatingId
        AND RatedBy = @user";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@RatingId", ratingId);
                cmd.Parameters.AddWithValue("@user", currentUser.Username);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Rating deleted successfully.");

            LoadRatedServices();
            ClearRatingInputs();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var row = dataGridView1.SelectedRows[0];

            cmbOverallRating.SelectedItem = row.Cells["OverallRating"].Value.ToString();
            cmbServiceQuality.SelectedItem = row.Cells["ServiceQualityRating"].Value.ToString();
            cmbTurfCondition.SelectedItem = row.Cells["TurfConditionRating"].Value.ToString();
            cmbLighting.SelectedItem = row.Cells["LightingRating"].Value.ToString();
            cmbEquipment.SelectedItem = row.Cells["EquipmentRating"].Value.ToString();
            cmbCleanliness.SelectedItem = row.Cells["CleanlinessRating"].Value.ToString();
            CommentTextBox.Text = row.Cells["Comment"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyProfile myProfile = new MyProfile(currentUser);
            myProfile.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeProfile changeProfile = new ChangeProfile(currentUser);
            changeProfile.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
