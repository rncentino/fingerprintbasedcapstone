using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class logattendance : Form
    {
        private readonly string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";

        public logattendance()
        {
            InitializeComponent();
        }

        private void logattendance_Load(object sender, EventArgs e)
        {
            LoadEventButtons();
            this.Resize += (s, ev) => LoadEventButtons(); // Recalculate spacing on resize
        }

        private void LoadEventButtons()
        {
            try
            {
                using (MySqlConnection MyConn = new MySqlConnection(MyConnection))
                {
                    MyConn.Open();
                    string Query = "SELECT event_id, event_name FROM events WHERE is_public = 1";
                    using (MySqlCommand cmd = new MySqlCommand(Query, MyConn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanelEvents.Controls.Clear();

                        // Configuration
                        int buttonWidth = 200;
                        int buttonHeight = 50;
                        int panelWidth = flowLayoutPanelEvents.ClientSize.Width;

                        int buttonsPerRow = Math.Max(1, panelWidth / (buttonWidth + 20));
                        int totalSpacing = panelWidth - (buttonsPerRow * buttonWidth);
                        int spacing = totalSpacing / (buttonsPerRow * 2); // space-around

                        while (reader.Read())
                        {
                            int eventId = Convert.ToInt32(reader["event_id"]);
                            string eventName = reader["event_name"].ToString();

                            Button eventButton = new Button
                            {
                                Text = eventName,
                                Tag = eventId,
                                Width = buttonWidth,
                                Height = buttonHeight,
                                BackColor = Color.LightBlue,
                                ForeColor = Color.White,
                                Font = new Font("Bookman Old Style", 12, FontStyle.Bold),
                                Margin = new Padding(spacing, 10, spacing, 10),
                                TextAlign = ContentAlignment.MiddleCenter
                            };

                            eventButton.Click += EventButton_Click;
                            flowLayoutPanelEvents.Controls.Add(eventButton);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EventButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is object obj && int.TryParse(obj.ToString(), out int eventId))
            {
                var detailsForm = new fingerpintLogin(eventId);
                detailsForm.ShowDialog();
            }
        }
    }
}
