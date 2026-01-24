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
    public partial class TouchTrackApp : Form
    {
        public TouchTrackApp()
        {
            InitializeComponent();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (login loginForm = new login())
            {
                loginForm.ShowDialog();
            }

            this.Close();
        }
    }
}
