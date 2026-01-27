using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class HomeForm : Form
    {
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

        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel1.Width, panel1.Height, 15, 15));

            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel2.Width, panel2.Height, 15, 15));

            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel3.Width, panel3.Height, 15, 15));

            panel4.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel4.Width, panel4.Height, 15, 15));

            dateTimeToday.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy • hh:mm tt");
        }
    }
}
