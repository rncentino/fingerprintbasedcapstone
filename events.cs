using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.Common;

namespace BiometricApp
{
    public partial class events : Form
    {
        private string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nLengthEllipse
            );

        public events()
        {
            InitializeComponent();
        }

        private void events_Load(object sender, EventArgs e)
        {
            LoadEvents();
            dataGridViewEvents.CellClick += dataGridViewEvents_CellClick;

            CreateEventBtn.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, CreateEventBtn.Width, CreateEventBtn.Height, 15, 15));

        }

        public void LoadEvents()
        {
            try
            {
                using (MySqlConnection MyConn = new MySqlConnection(MyConnection))
                {
                    MyConn.Open();
                    string Query = "SELECT event_id, event_name, location, date, is_public FROM events";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(Query, MyConn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewEvents.DataSource = dt;
                    dataGridViewEvents.Columns["event_id"].Visible = false;
                    dataGridViewEvents.Columns["is_public"].Visible = false;
                    dataGridViewEvents.Columns["location"].HeaderText = "VENUE";
                    dataGridViewEvents.Columns["date"].HeaderText = "DATE";

                    // Add Link Column for Event Name
                    if (dataGridViewEvents.Columns["ACTIVITY"] == null)
                    {
                        DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
                        linkColumn.DataPropertyName = "event_name";
                        linkColumn.Name = "ACTIVITY";
                        linkColumn.HeaderText = "ACTIVITY";
                        linkColumn.LinkColor = Color.Black;
                        linkColumn.TrackVisitedState = false;
                        dataGridViewEvents.Columns.Remove("event_name");
                        dataGridViewEvents.Columns.Insert(1, linkColumn);
                    }

                    AddActionButtons();

                    foreach (DataGridViewRow row in dataGridViewEvents.Rows)
                    {
                        if (row.Cells["is_public"].Value != DBNull.Value) // Prevent null errors
                        {
                            bool isPublic = Convert.ToBoolean(row.Cells["is_public"].Value);
                            row.Cells["TogglePublicButton"].Value = isPublic
                                ? Properties.Resources.show   // or Image.FromFile("path/to/eye_open.png")
                                : Properties.Resources.hide;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddActionButtons()
        {

            if (dataGridViewEvents.Columns["UpdateButton"] == null)
            {
                DataGridViewImageColumn updateButton = new DataGridViewImageColumn();
                updateButton.Name = "UpdateButton";
                updateButton.HeaderText = "EDIT";
                updateButton.Image = Properties.Resources.edit;
                updateButton.ImageLayout = DataGridViewImageCellLayout.Zoom;
                updateButton.Width = 50;
                dataGridViewEvents.Columns.Add(updateButton);
            }

            if (dataGridViewEvents.Columns["DeleteButton"] == null)
            {
                DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
                deleteButton.Name = "DeleteButton";
                deleteButton.HeaderText = "DELETE";
                deleteButton.Image = Properties.Resources.delete; 
                deleteButton.ImageLayout = DataGridViewImageCellLayout.Zoom;
                deleteButton.Width = 50;
                dataGridViewEvents.Columns.Add(deleteButton);
            }

            if (dataGridViewEvents.Columns["TogglePublicButton"] == null)
            {
                DataGridViewImageColumn publicButton = new DataGridViewImageColumn();
                publicButton.Name = "TogglePublicButton";
                publicButton.HeaderText = "VISIBILITY";
                publicButton.ImageLayout = DataGridViewImageCellLayout.Zoom;
                publicButton.Width = 50;
                dataGridViewEvents.Columns.Add(publicButton);
            }
        }

        private void dataGridViewEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                int eventId = Convert.ToInt32(dataGridViewEvents.Rows[e.RowIndex].Cells["event_id"].Value);

                if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "ACTIVITY")
                {
                    var eventDetailsForm = new eventDetails(eventId);
                    eventDetailsForm.ShowDialog();
                }
                else if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "UpdateButton")
                {
                    var updateForm = new updateActivity(eventId);
                    updateForm.FormClosed += (s, args) => LoadEvents();
                    updateForm.ShowDialog();
                }
                else if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "DeleteButton")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this event?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent(eventId);
                        LoadEvents();
                    }
                }
                else if (dataGridViewEvents.Columns[e.ColumnIndex].Name == "TogglePublicButton")
                {
                    ToggleEventVisibility(eventId);
                    LoadEvents();
                }
            }
        }

        private void ToggleEventVisibility(int eventId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();
                    string query = "UPDATE events SET is_public = NOT is_public WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteEvent(int eventId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();
                    string query = "DELETE FROM events WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Event deleted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CreateEventBtn_Click(object sender, EventArgs e)
        {
            var AddEventFrm = new addactivity();
            AddEventFrm.FormClosed += (s, args) => LoadEvents();
            AddEventFrm.ShowDialog();
        }
    }
}
