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
        public string StudentID = "";
        public string Lastname = "";
        public string FirstName = "";
        public string Middlename = "";
        public string YearLevel = "";
        public string Course = "";

        public capture()
        {
            InitializeComponent();

            student_id.KeyPress += student_id_KeyPress;


            // Populate the ComboBox with items
            yearLevel.Items.Add("Select your year level...");
            yearLevel.SelectedIndex = 0;
            yearLevel.SelectedIndexChanged += yearLevel_SelectedIndexChanged;
            yearLevel.Items.Add("First Year");
            yearLevel.Items.Add("Second Year");
            yearLevel.Items.Add("Third Year");
            yearLevel.Items.Add("Fourth Year");

            course.Items.Add("Select your course/program...");
            course.SelectedIndex = 0;
            course.SelectedIndexChanged += course_SelectedIndexChanged;
            course.Items.Add("BSIT");
            course.Items.Add("BSOA");
            course.Items.Add("BSHM");
            course.Items.Add("BSeD");
            course.Items.Add("PTCP");
        }

        private void student_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numeric digits, backspace, and decimal point
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                // Suppress the key press if it's not a digit, backspace, or decimal point
                e.Handled = true;
            }
        }

        private void yearLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yearLevel.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a valid option.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                yearLevel.SelectedIndex = -1; // Clear selection
            }
        }

        private void course_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (course.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a valid option.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                course.SelectedIndex = -1; // Clear selection
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            student_id.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            lname.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            fname.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            mname.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            yearLevel.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            course.Focus();
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

        protected void Setstudent_id(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                student_id.Text = value;
            }));
        }

        protected void Setlname(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                lname.Text = value;
            }));
        }

        protected void Setfname(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                fname.Text = value;
            }));
        }

        protected void Setmname(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                mname.Text = value;
            }));
        }

        protected void SetyearLevel(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                yearLevel.Text = value;
            }));
        }

        protected void Setcourse(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                course.Text = value;
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

        private void student_id_TextChanged(object sender, EventArgs e)
        {
            StudentID = student_id.Text;
        }

        private void lname_TextChanged(object sender, EventArgs e)
        {
            Lastname = lname.Text;
        }

        private void fname_TextChanged(object sender, EventArgs e)
        {
            FirstName = fname.Text;
        }

        private void mname_TextChanged(object sender, EventArgs e)
        {
            Middlename = mname.Text;
        }

        private void yearLevel_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            YearLevel = yearLevel.Text;
        }

        private void course_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Course = course.Text;
        }

        public void ClearFields()
        {
            this.Invoke(new Action(() =>
            {
                student_id.Text = "";
                lname.Text = "";
                fname.Text = "";
                mname.Text = "";
                yearLevel.SelectedIndex = -1;
                course.SelectedIndex = -1;

                StudentID = "";
                Lastname = "";
                FirstName = "";
                Middlename = "";
                YearLevel = "";
                Course = "";
            }));
        }

    }
}
