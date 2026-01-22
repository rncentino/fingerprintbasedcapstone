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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            LoadForm(new HomeForm());

            SetupNavButton(btnHome);
            SetupNavButton(btnAttendance);
            SetupNavButton(btnEmployee);
            SetupNavButton(btnSetting);

            SetActiveButton(btnHome); // default
        }

        Color dangerHover = Color.FromArgb(192, 57, 43);
        Color dangerClick = Color.FromArgb(231, 76, 60);
        Color navNormal = Color.Transparent;
        Color navHover = Color.FromArgb(63, 154, 174);
        Color navActive = Color.FromArgb(229, 229, 229);
        Color NavNormalText = Color.FromArgb(229, 229, 229);
        Color NavActiveText = Color.FromArgb(41, 128, 185);

        private void SetupNavButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.UseVisualStyleBackColor = false;
            btn.BackColor = navNormal;
            btn.ForeColor = NavNormalText;

            btn.MouseEnter += NavButton_MouseEnter;
            btn.MouseLeave += NavButton_MouseLeave;
        }

        private void NavButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor != navActive)
                btn.BackColor = navHover;
        }

        private void NavButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor != navActive)
                btn.BackColor = navNormal;
        }

        private Button currentActiveButton = null;

        private void SetActiveButton(Button activeBtn)
        {
            if (currentActiveButton != null)
            {
                currentActiveButton.BackColor = navNormal;
                currentActiveButton.ForeColor = NavNormalText;
            }

            currentActiveButton = activeBtn;
            currentActiveButton.BackColor = navActive;
            currentActiveButton.ForeColor = NavActiveText;
        }

        private void LoadForm(Form form)
        {
            panelMain.Controls.Clear();

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;

            panelMain.Controls.Add(form);
            form.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHome);
            LoadForm(new HomeForm());
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAttendance);
            LoadForm(new AttendanceForm());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnEmployee);
            LoadForm(new students());
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSetting);
            LoadForm(new SettingForm());
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to log out?",
            "Confirm Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // End session and return to login
                login loginForm = new login();
                loginForm.Show();

                this.Close(); // Close dashboard
            }
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = dangerHover;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.Transparent;
        }

        private void logoutbtn_MouseDown(object sender, MouseEventArgs e)
        {
            logoutbtn.BackColor = dangerClick;
        }

        private void logoutbtn_MouseUp(object sender, MouseEventArgs e)
        {
            logoutbtn.BackColor = dangerHover;
        }
    }
}
