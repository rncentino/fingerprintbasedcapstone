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
    public partial class biometricapp : Form
    {
        public biometricapp()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadform(new mainhome());
        }

        public void loadform(object Form)
        {
            if (this.fmspanel.Controls.Count > 0)
                this.fmspanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.fmspanel.Controls.Add(f);
            this.fmspanel.Tag = f;
            f.Show();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void dashboardbtn_Click(object sender, EventArgs e)
        {
            loadform(new loginform(this));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new logattendance());
        }

        public void HideBiometricApp()
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new mainhome());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            loadform(new mainhome());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            loadform(new mainhome());
        }

        private void fmspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
