using DPFP;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    delegate void Function();

    public partial class EmployeeRegistrationForm : Form
    {
        private string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";

        private DPFP.Template Template;


        public EmployeeRegistrationForm()
        {
            InitializeComponent();
        }
                
        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                if(Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification", "Fingerprint Enrollment");
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid, repeat fingerprint scanning", "Fingerprint Enrollment");
                }
            }));
        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            verify VeFrm = new verify();
            VeFrm.Verify(Template);
        }

        private void registrationBtn_Click(object sender, EventArgs e)
        {
            enroll Enfrm = new enroll();
            Enfrm.OnTemplate += this.OnTemplate;
            Enfrm.ShowDialog();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
            "Are you sure you want to exit?",
            "Confirm Exit",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void registrationBtn_MouseEnter(object sender, EventArgs e)
        {
            registrationBtn.BackColor = Color.FromArgb(41, 128, 185);
        }

        private void registrationBtn_MouseLeave(object sender, EventArgs e)
        {
            registrationBtn.BackColor = Color.FromArgb(229, 229, 229);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SearchTxt.Focus();
        }
    }
}
