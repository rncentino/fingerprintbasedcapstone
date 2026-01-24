using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using DPFP.Verification;
using System.IO;
using System.Xml;


namespace BiometricApp
{
    public partial class fingerpintLogin: Form, DPFP.Capture.EventHandler
    {

        private DPFP.Capture.Capture Capturer;
        private DPFP.Verification.Verification Verificator;
        private DPFP.Template Template;

        private string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";
        private int eventId;

        private void fingerpintLogin_Load(object sender, EventArgs e)
        {
            LoadEventDetails();
            LoadAttendanceLogs();
            StartCapture();
        }

        public fingerpintLogin(int eventId)
        {
            InitializeComponent();
            Init();
            this.eventId = eventId;
            this.Load += fingerpintLogin_Load;
            this.FormClosed += fingerpintLogin_FormClosed;
            StartCapture();
        }

        private void fingerpintLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopCapture();
        }

        protected void MakeReport(string message)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                    {
                    StatusText.AppendText(message + "\r\n");
                }));
            }
            else
            {
                Console.WriteLine("Form handle not created yet. Skipping message: " + message);
            }
        }

        private void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                Verificator = new DPFP.Verification.Verification();

                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                }
                else
                {
                    MessageBox.Show("Failed to initialize fingerprint scanner.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                UpdateStatus(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing: " + ex.Message);
            }
        }

        private void StartCapture()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StartCapture();
                    Prompt.Text = "Place your finger on the scanner.";
                }
                catch
                {
                    Prompt.Text = "Fingerprint scanner not detected!";
                }
            }
        }

        private void StopCapture()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    MakeReport("Failed to stop capture.");
                }
            }
        }

        private DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();

            Extractor.CreateFeatureSet(Sample, DPFP.Processing.DataPurpose.Verification, ref feedback, ref features);
            return (feedback == DPFP.Capture.CaptureFeedback.Good) ? features : null;
        }

        private Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        private void ProcessSample(DPFP.Sample Sample)
        {
            fImage.Image = ConvertSampleToBitmap(Sample);

            DPFP.FeatureSet features = ExtractFeatures(Sample);
            if (features == null)
            {
                MakeReport("Failed to extract fingerprint features. Try again.");
                return;
            }

            VerifyFingerprint(features);
        }

        private void VerifyFingerprint(DPFP.FeatureSet features)
        {
            try
            {
                string connectionString = "datasource=localhost;username=root;password=123456;database=biometricapp";
                string query = "SELECT id, firstname, lastname, middlename, yearlevel, course, fingerprint FROM students";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] storedFingerprint = (byte[])reader["fingerprint"];
                            using (MemoryStream ms = new MemoryStream(storedFingerprint))
                            {
                                Template = new DPFP.Template(ms);
                            }

                            DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                            Verificator.Verify(features, Template, ref result);
                            UpdateStatus(result.FARAchieved);

                            if (result.Verified)
                            {
                                int studentId = Convert.ToInt32(reader["id"]);
                                MakeReport($"Fingerprint VERIFIED: {reader["firstname"]} {reader["lastname"]}");

                                // Insert or update log entry
                                InsertOrUpdateLog(studentId, eventId);

                                return;
                            }
                        }
                        MakeReport("Fingerprint NOT VERIFIED!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during verification: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertOrUpdateLog(int studentId, int eventId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();

                    // Check if the student already logged in today
                    string checkQuery = @"SELECT log_id, time_out FROM logattendance  
                                  WHERE id = @studentId AND event_id = @eventId 
                                  AND date = @date LIMIT 1";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@studentId", studentId);
                        checkCmd.Parameters.AddWithValue("@eventId", eventId);
                        checkCmd.Parameters.AddWithValue("@date", DateTime.Now.Date);

                        using (MySqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read()) // Student already logged in
                            {
                                int logId = Convert.ToInt32(reader["log_id"]);
                                object timeOut = reader["time_out"];

                                reader.Close(); // Close reader before executing another command

                                if (timeOut == DBNull.Value) // If time_out is NULL, update it
                                {
                                    string updateQuery = @"UPDATE logattendance 
                                                   SET time_out = @timeOut 
                                                   WHERE log_id = @logId";

                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@timeOut", DateTime.Now.TimeOfDay);
                                        updateCmd.Parameters.AddWithValue("@logId", logId);

                                        updateCmd.ExecuteNonQuery();
                                        MakeReport("Time-out recorded successfully.");
                                        LoadAttendanceLogs(); // Refresh table
                                    }
                                }
                                else
                                {
                                    MakeReport("You have already logged out for this event.");
                                    LoadAttendanceLogs(); // Refresh table
                                }
                            }
                            else // First time logging in today, insert new record
                            {
                                reader.Close(); // Close reader before inserting

                                string insertQuery = @"INSERT INTO logattendance (user_id, id, event_id, date, time_in) 
                                               VALUES (@userId, @studentId, @eventId, @date, @timeIn)";

                                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@userId", 1); // Replace with actual user ID if available
                                    insertCmd.Parameters.AddWithValue("@studentId", studentId);
                                    insertCmd.Parameters.AddWithValue("@eventId", eventId);
                                    insertCmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                                    insertCmd.Parameters.AddWithValue("@timeIn", DateTime.Now.TimeOfDay);

                                    insertCmd.ExecuteNonQuery();
                                    MakeReport("Attendance successfully LOGGED.");
                                    LoadAttendanceLogs(); // Refresh table
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting/updating log: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    MakeReport("Fingerprint sample captured.");
                    ProcessSample(Sample);
                }));
            }
            else
            {
                MakeReport("Fingerprint sample captured.");
                ProcessSample(Sample);
            }
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was touched");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            MakeReport((CaptureFeedback == DPFP.Capture.CaptureFeedback.Good) ? "Good fingerprint sample." : "Poor fingerprint sample.");
        }

        private void LoadEventDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();
                    string query = "SELECT event_name, location, date, is_public FROM events WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                lblEventName.Text = dt.Rows[0]["event_name"].ToString();
                                lblLocation.Text = dt.Rows[0]["location"].ToString();
                                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("MMMM dd, yyyy");
                            }
                            else
                            {
                                MessageBox.Show("No event found for this ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading event details: " + ex.Message);
            }
        }

        private void LoadAttendanceLogs()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    s.student_id AS 'STUDENT ID',
                    CONCAT(s.lastname, ', ', s.firstname, ' ', LEFT(s.middlename, 1), '.') AS 'NAME',
                    s.yearlevel AS 'YEAR LEVEL',
                    s.course AS 'COURSE',
                    DATE_FORMAT(l.date, '%M %e, %Y') AS 'DATE',
                    TIME_FORMAT(l.time_in, '%h:%i %p') AS 'IN',
                    TIME_FORMAT(l.time_out, '%h:%i %p') AS 'OUT'
                FROM 
                    logattendance l
                INNER JOIN 
                    students s ON l.id = s.id
                WHERE 
                    l.event_id = @eventId
                ORDER BY 
                    l.time_in DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewLogs.DataSource = dt;

                            //header style
                            dataGridViewLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                            dataGridViewLogs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            //default column width
                            foreach (DataGridViewColumn column in dataGridViewLogs.Columns)
                            {
                                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            }

                            dataGridViewLogs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                StopCapture();
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void UpdateStatus(int FAR)
        {
            Statuslabel.Text = String.Format("False Accept Rate (FAR) = {0}", FAR);
        }
    }
}