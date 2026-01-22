using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using DPFP.Verification;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BiometricApp
{
    public partial class capture : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;

        public string EmployeeNumber { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;

        public class ScheduleItem
        {
            public string DayOfWeek { get; set; }
            public TimeSpan TimeIn { get; set; }
            public TimeSpan TimeOut { get; set; }
        }

        protected List<ScheduleItem> GetSchedulesForDatabase()
        {
            List<ScheduleItem> schedules = new List<ScheduleItem>();

            foreach (DataGridViewRow row in schedPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                schedules.Add(new ScheduleItem
                {
                    DayOfWeek = Convert.ToString(row.Cells["DayOfWeek"].Value),
                    TimeIn = row.Cells["TimeIn"].Tag is TimeSpan ti ? ti : TimeSpan.Zero,
                    TimeOut = row.Cells["TimeOut"].Tag is TimeSpan to ? to : TimeSpan.Zero
                });
            }

            return schedules;
        }



        private void SetupScheduleGrid()
        {
            schedPreview.Columns.Clear();

            schedPreview.Columns.Add("DayOfWeek", "Day Of Week");
            schedPreview.Columns.Add("TimeIn", "Time In");
            schedPreview.Columns.Add("TimeOut", "Time Out");

            DataGridViewButtonColumn btnRemove = new DataGridViewButtonColumn();
            btnRemove.Name = "Action";
            btnRemove.HeaderText = "Action";
            btnRemove.Text = "Remove";
            btnRemove.UseColumnTextForButtonValue = true;

            schedPreview.Columns.Add(btnRemove);
        }

        private void addSchedBtn_Click(object sender, EventArgs e)
        {
            // Validate day selection
            if (cmbDayOfWeek.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a day.");
                return;
            }

            // Validate time range
            if (timeOut.Value <= timeIn.Value)
            {
                MessageBox.Show("Time Out must be later than Time In.");
                return;
            }

            // Check for overlapping schedules (same day)
            foreach (DataGridViewRow row in schedPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string existingDay = Convert.ToString(row.Cells["DayOfWeek"].Value);
                if (existingDay != cmbDayOfWeek.Text)
                    continue;

                TimeSpan existingIn = (TimeSpan)row.Cells["TimeIn"].Tag;
                TimeSpan existingOut = (TimeSpan)row.Cells["TimeOut"].Tag;

                TimeSpan newIn = timeIn.Value.TimeOfDay;
                TimeSpan newOut = timeOut.Value.TimeOfDay;

                if (newIn < existingOut && newOut > existingIn)
                {
                    MessageBox.Show(
                        "Schedule time overlaps with an existing schedule for this day.",
                        "Schedule Conflict",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
            }

            // ADD ROW TO DATAGRIDVIEW (TEMP STORAGE)
            schedPreview.Rows.Add(
                cmbDayOfWeek.Text,
                timeIn.Value.ToShortTimeString(),
                timeOut.Value.ToShortTimeString()
            );

            // STORE REAL TIME VALUES IN TAG (IMPORTANT)
            int lastRow = schedPreview.Rows.Count - 1;
            schedPreview.Rows[lastRow].Cells["TimeIn"].Tag = timeIn.Value.TimeOfDay;
            schedPreview.Rows[lastRow].Cells["TimeOut"].Tag = timeOut.Value.TimeOfDay;

            // OPTIONAL UX CLEANUP
            cmbDayOfWeek.SelectedIndex = -1;
            timeIn.Value = DateTime.Today;
            timeOut.Value = DateTime.Today;
        }

        private void schedPreview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (schedPreview.Columns[e.ColumnIndex].Name == "Action")
            {
                DialogResult result = MessageBox.Show(
                    "Remove this schedule?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    schedPreview.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void validateScan()
        {
            bool isReady =
            !string.IsNullOrWhiteSpace(txtEmployeeNumber.Text) &&
            !string.IsNullOrWhiteSpace(txtFirstName.Text) &&
            !string.IsNullOrWhiteSpace(txtLastName.Text) &&
            cmbRole.SelectedIndex != -1 &&
            schedPreview.Rows.Count > 0;

            start_scan.Visible = isReady;
        }


        private void closebtn_Click(object sender, EventArgs e)
        {
            Stop();
            Close();
        }

        private void testbtn_Click(object sender, EventArgs e)
        {
            List<ScheduleItem> schedules = GetSchedulesForDatabase();

            MessageBox.Show(
                $"Schedules collected: {schedules.Count}"
            );
        }

        public capture()
        {
            InitializeComponent();
            SetupScheduleGrid();

            timeIn.Format = DateTimePickerFormat.Custom;
            timeIn.CustomFormat = "hh:mm tt";
            timeIn.ShowUpDown = true;

            timeOut.Format = DateTimePickerFormat.Custom;
            timeOut.CustomFormat = "hh:mm tt";
            timeOut.ShowUpDown = true;

            start_scan.Visible = false;
        }

        private void focusEmployeeID(object sender, EventArgs e)
        {
            txtEmployeeNumber.Focus();
        }

        private void focusLastName(object sender, EventArgs e)
        {
            txtLastName.Focus();
        }

        private void focusFirstName(object sender, EventArgs e)
        {
            txtFirstName.Focus();
        }

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            validateScan();
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            validateScan();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            validateScan();
        }

        private void cmbRole_TextChanged(object sender, EventArgs e)
        {
            validateScan();
        }

        private void schedPreview_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            validateScan();
        }

        private void schedPreview_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            validateScan();
        }

        private void txtEmployeeID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // block input
            }
        }

        protected void SetEmployeeNumber(string employeeNumber)
        {
            this.Invoke(new Action(() =>
            {
                txtEmployeeNumber.Text = employeeNumber;
            }));
        }

        protected void SetLastName(string LastName)
        {
            this.Invoke(new Action(() =>
            {
                txtLastName.Text = LastName;
            }));
        }

       protected void SetFirstName(string FirstName)
        {
            this.Invoke(new Action(() =>
            {
                txtFirstName.Text = FirstName;
            }));
        }

        protected void SetRole(string role)
        {
            this.Invoke(new Action(() =>
            {
                cmbRole.Text = role;
            }));
        }

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate
            {
                Prompt.Text = prompt;
            }));
        }

        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate
            {
                Statuslabel.Text = status;
            }));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate ()
            {
                fImage.Image = new Bitmap(bitmap, fImage.Size);
            }));
        }


        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (null != Capturer)
                    Capturer.EventHandler = this;
                else
                    SetPrompt("Can't initiate capture operation!");
            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation!");
            }
        }

        //Process
        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }


        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        protected void Start()
        {
            if(null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint");
                }
                catch
                {
                    SetPrompt("Can't initiate capture");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                    timer1.Dispose();
                }
                catch
                {
                    SetPrompt("Can't terminate capture");
                }
            }
        }

        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusText.AppendText(message + "\r\n");
            }));
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction(); 
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured");
            SetPrompt("Scan the same fingerprint again");
            Process(Sample);
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
            if (CaptureFeedback == CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good");
            else
                MakeReport("The quality of the fingerprint sample is poor");
        }

        private void start_scan_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Start();
        }

        private void capture_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected void SyncEmployeeData()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SyncEmployeeData));
                return;
            }

            EmployeeNumber = txtEmployeeNumber.Text.Trim();
            FirstName = txtFirstName.Text.Trim();
            LastName = txtLastName.Text.Trim();
            Role = cmbRole.Text.Trim();
        }

        public void ClearFields()
        {
            this.Invoke(new Action(() =>
            {
                txtEmployeeNumber.Text = "";
                txtLastName.Text = "";
                txtFirstName.Text = "";
                cmbRole.SelectedIndex = -1;
                schedPreview.Rows.Clear();

                EmployeeNumber = "";
                LastName = "";
                FirstName = "";
                Role = "";
                start_scan.Visible = false;
            }));
        }
    }
}
