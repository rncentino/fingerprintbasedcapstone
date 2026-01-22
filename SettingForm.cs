using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            this.AcceptButton = btnChangePassword;
        }

        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private void ChangePassword()
        {
            if (!VerifyCurrentPassword(txtCurrentPassword.Text))
            {
                MessageBox.Show(
                    "Current password is incorrect.",
                    "Authentication Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            string conn = System.Configuration.ConfigurationManager
                            .ConnectionStrings["MyConn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = @"
            UPDATE AdminUsers
            SET PasswordHash = @newPasswordHash
            WHERE AdminID = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@newPasswordHash", SqlDbType.VarBinary, 32)
                       .Value = HashPassword(txtNewPassword.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Password changed successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ClearFields();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
       string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
       string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (txtNewPassword.Text.Length < 8)
            {
                MessageBox.Show(
                    "New password must be at least 8 characters long.",
                    "Weak Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show(
                    "New password and confirm password do not match.",
                    "Password Mismatch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (txtCurrentPassword.Text == txtNewPassword.Text)
            {
                MessageBox.Show(
                    "New password must be different from the current password.",
                    "Invalid Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ChangePassword();
        }

        private bool VerifyCurrentPassword(string currentPassword)
        {
            string conn = System.Configuration.ConfigurationManager
                            .ConnectionStrings["MyConn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = @"
            SELECT COUNT(*)
            FROM AdminUsers
            WHERE AdminID = 1
              AND PasswordHash = @passwordHash";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@passwordHash", SqlDbType.VarBinary, 32)
                       .Value = HashPassword(currentPassword);

                    con.Open();
                    return (int)cmd.ExecuteScalar() == 1;
                }
            }
        }

        private void ClearFields()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            txtCurrentPassword.Focus();
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

        bool isPasswordVisible = false;

        private void FocusTextBox1(object sender, EventArgs e)
        {
            txtCurrentPassword.Focus();
        }

        private void FocusTextBox2(object sender, EventArgs e)
        {
            txtNewPassword.Focus();
        }

        private void FocusTextBox3(object sender, EventArgs e)
        {
            txtConfirmPassword.Focus();
        }

        private void txtCurrentPassword_Enter(object sender, EventArgs e)
        {
            line1.Visible = true;
        }

        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
            line1.Visible = false;
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            line2.Visible = true;
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            line2.Visible = false;
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            line3.Visible = true;
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            line3.Visible = false;
        }

        private void showPassword(object sender, EventArgs e)
        {
            txtCurrentPassword.Focus();
            isPasswordVisible = !isPasswordVisible;
            txtCurrentPassword.UseSystemPasswordChar = !isPasswordVisible;
        }

        private void showPassword2(object sender, EventArgs e)
        {
            txtNewPassword.Focus();
            isPasswordVisible = !isPasswordVisible;
            txtNewPassword.UseSystemPasswordChar = !isPasswordVisible;
        }

        private void showPassword3(object sender, EventArgs e)
        {
            txtConfirmPassword.Focus();
            isPasswordVisible = !isPasswordVisible;
            txtConfirmPassword.UseSystemPasswordChar = !isPasswordVisible;
        }
    }
}
