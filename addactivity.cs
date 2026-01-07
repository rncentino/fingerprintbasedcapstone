using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BiometricApp
{
    public partial class addactivity: Form
    {

        private string connectionString = "server=localhost;database=biometricapp;user=root;password=123456;";
        protected int? eventId = null;

        public addactivity()
        {
            InitializeComponent();
        }

        protected addactivity(int eventId) // Protected constructor for updateActivity
        {
            InitializeComponent();
            this.eventId = eventId;
            LoadEventData(eventId);
            CreateEventBtn.Text = "Update Event"; // Change button text for updating
        }

        protected void LoadEventData(int eventId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT event_name, location, date FROM events WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                eventName.Text = reader["event_name"].ToString();
                                location.Text = reader["location"].ToString();
                                date.Value = Convert.ToDateTime(reader["date"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        protected void SaveEvent()
        {
            if (string.IsNullOrWhiteSpace(eventName.Text) || string.IsNullOrWhiteSpace(location.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (eventId.HasValue)
            {
                UpdateEvent(); // Call update method
            }
            else
            {
                CreateNewEvent(); // Call insert method
            }
        }

        protected void CreateNewEvent()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO events (user_id, event_name, location, date) VALUES (@userId, @eventName, @location, @date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", BiometricApp.Session.UserId);
                        cmd.Parameters.AddWithValue("@eventName", eventName.Text);
                        cmd.Parameters.AddWithValue("@location", location.Text);
                        cmd.Parameters.AddWithValue("@date", date.Value);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Event created successfully!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        protected void UpdateEvent()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE events SET event_name = @eventName, location = @location, date = @date WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.Parameters.AddWithValue("@eventName", eventName.Text);
                        cmd.Parameters.AddWithValue("@location", location.Text);
                        cmd.Parameters.AddWithValue("@date", date.Value);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Event updated successfully!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void addactivity_Load(object sender, EventArgs e)
        {

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void CreateEventBtn_Click(object sender, EventArgs e)
        {
            SaveEvent(); // Call the appropriate save method
        }
    }
}
