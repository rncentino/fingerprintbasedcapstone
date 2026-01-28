using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using DPFP.Verification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class TouchTrackApp : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        private DPFP.Verification.Verification Verificator;

        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

        public TouchTrackApp()
        {
            InitializeComponent();
            Init();
            StartCapture();
        }

        

        private void TouchTrackApp_Load(object sender, EventArgs e)
        {
            timerDateTime.Start();
            LoadAttendanceLogs();
            StartCapture();
        }

        private void TouchTrackApp_FormClosed(object sender, FormClosedEventArgs e)
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
                string Query = "SELECT FingerprintID, EmployeeID, FingerprintTemplate FROM Fingerprints";

                using (SqlConnection sqlCon = new SqlConnection(conn))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] templateBytes = (byte[])reader["FingerprintTemplate"];
                                DPFP.Template template = new DPFP.Template();
                                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(templateBytes))
                                {
                                    template.DeSerialize(ms);
                                }
                                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                                Verificator.Verify(features, template, ref result);
                                
                                bool isMatched = false;

                                if (result.Verified)
                                {
                                    isMatched = true;
                                    string employeeID = reader["EmployeeID"].ToString();
                                    MakeReport("Fingerprint matched for Employee ID: " + employeeID);
                                    // Log attendance here
                                    InsertOrUpdateLog(employeeID);

                                    break;
                                }

                                if (!isMatched)
                                {
                                    MakeReport("Fingerprint NOT VERIFIED!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during verification: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertOrUpdateLog(string employeeId)
        {
            try
            {
                using (SqlConnection conn1 = new SqlConnection(conn))
                {
                    conn1.Open();

                    string checkQuery = @"
                SELECT TOP 1 AttendanceID, TimeIn, TimeOut
                FROM Attendance
                WHERE EmployeeID = @EmployeeID
                  AND AttendanceDate = CAST(GETDATE() AS DATE)";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn1))
                    {
                        checkCmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read()) // Attendance exists today
                            {
                                int attendanceId = Convert.ToInt32(reader["AttendanceID"]);
                                DateTime timeIn = Convert.ToDateTime(reader["TimeIn"]);
                                object timeOutObj = reader["TimeOut"];

                                reader.Close();

                                // Already timed out
                                if (timeOutObj != DBNull.Value)
                                {
                                    MessageBox.Show("Attendance already completed for today.");
                                    return;
                                }

                                // Check if 1 hour has passed
                                if ((DateTime.Now - timeIn).TotalMinutes < 60)
                                {
                                    MessageBox.Show("You just timed in. Please try again after 1 hour.");
                                    return;
                                }

                                // TIME OUT
                                string updateQuery = @"
                            UPDATE Attendance
                            SET TimeOut = GETDATE(), Status = 'OUT'
                            WHERE AttendanceID = @AttendanceID";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn1))
                                {
                                    updateCmd.Parameters.AddWithValue("@AttendanceID", attendanceId);
                                    updateCmd.ExecuteNonQuery();
                                }

                                MessageBox.Show("Time-out recorded successfully.");
                            }
                            else // No attendance yet today → TIME IN
                            {
                                reader.Close();

                                string insertQuery = @"
                            INSERT INTO Attendance
                                (EmployeeID, AttendanceDate, TimeIn, Status)
                            VALUES
                                (@EmployeeID, CAST(GETDATE() AS DATE), GETDATE(), 'IN')";

                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn1))
                                {
                                    insertCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                                    insertCmd.ExecuteNonQuery();
                                }

                                MessageBox.Show("Time-in recorded successfully.");
                            }
                        }
                    }
                }



                LoadAttendanceLogs(); // refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Attendance error: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void LoadAttendanceLogs()
        {
            try
            {
                string query = @"
                SELECT
    a.AttendanceID,
    e.EmployeeNumber,
    CONCAT(e.LastName, ', ', e.FirstName) AS FullName,
    e.Role,
    a.AttendanceDate,
    a.TimeIn,
    a.TimeOut,
    a.Status
FROM Attendance a
LEFT JOIN Employees e
    ON a.EmployeeID = e.EmployeeID
WHERE a.AttendanceDate >= CAST(GETDATE() AS DATE)
  AND a.AttendanceDate <  DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
ORDER BY a.TimeIn DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAttendanceLog.DataSource = dt;

                dgvAttendanceLog.Columns["AttendanceID"].Visible = false;
                dgvAttendanceLog.Columns["EmployeeNumber"].HeaderText = "ID";
                dgvAttendanceLog.Columns["FullName"].HeaderText = "NAME";
                dgvAttendanceLog.Columns["Role"].Visible = false;
                dgvAttendanceLog.Columns["AttendanceDate"].Visible = false;
                dgvAttendanceLog.Columns["TimeIn"].HeaderText = "TIME IN";
                dgvAttendanceLog.Columns["TimeOut"].HeaderText = "TIME OUT";
                dgvAttendanceLog.Columns["Status"].HeaderText = "STATUS";

                dgvAttendanceLog.Columns["EmployeeNumber"].Width = 60;
                dgvAttendanceLog.Columns["FullName"].Width = 130;
                dgvAttendanceLog.Columns["TimeIn"].Width = 80;
                dgvAttendanceLog.Columns["TimeOut"].Width = 80;
                dgvAttendanceLog.Columns["Status"].Width = 70;

                dgvAttendanceLog.Columns["TimeIn"].DefaultCellStyle.Format = "hh:mm tt";
                dgvAttendanceLog.Columns["TimeOut"].DefaultCellStyle.Format = "hh:mm tt";

                // header style
                dgvAttendanceLog.EnableHeadersVisualStyles = false;
                dgvAttendanceLog.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvAttendanceLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                dgvAttendanceLog.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvAttendanceLog.GridColor = Color.LightGray;
                dgvAttendanceLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to exit?",
            "Confirm Exit",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void UpdateStatus(int FAR)
        {
            Statuslabel.Text = String.Format("False Accept Rate (FAR) = {0}", FAR);
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            dateTimeToday.Text =
        DateTime.Now.ToString("dddd, MMMM dd, yyyy • hh:mm:ss tt");
        }
    }
}
