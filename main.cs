using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class main : Form
    {
        private Button activeButton;
        private MySqlConnection conn;

        private void ActivateButton(object senderButton)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.FromArgb(255, 255, 255); // Reset previous button color
            }

            activeButton = (Button)senderButton;
            activeButton.BackColor = Color.FromArgb(227, 238, 212); // Highlight the active button
        }

        public main()
        {
            InitializeComponent();
            loadform(new events());
            conn = new MySqlConnection("server=localhost;database=biometricapp;user=root;password=123456;");
        }

        public delegate void FormShownHandler();
        public event FormShownHandler OnFormShown;

        private void Form2_Load(object sender, EventArgs e)
        {
            OnFormShown?.Invoke(); // Notify subscribers
        }

        public void loadform (object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        public static class Session
        {
            public static int UserId { get; set; }
            public static string Username { get; set; }

            public static void ClearSession()
            {
                UserId = 0;
                Username = string.Empty;
            }
        }


        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res =  MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            } else
            {
                this.Show();
            }
        }

        private void dashboardbtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            loadform(new dashboard());
        }

        private void eventsbtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            loadform(new events());
        }

        private void studentsbtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            loadform(new EmployeeRegistrationForm());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //activeButton.BackColor = Color.FromArgb(174, 196, 176);
            loadform(new dashboard());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //activeButton.BackColor = Color.FromArgb(174, 196, 176);
            loadform(new dashboard());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.ClearSession(); // Clear session data

                this.Close(); // Close the admin form

                biometricapp login = new biometricapp();
                login.Show();
            }
        }
    }
}
