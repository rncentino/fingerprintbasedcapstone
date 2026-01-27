using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            this.AcceptButton = loginbtn;
        }

        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(229, 229, 229);
            panel4.BackColor = Color.FromArgb(41, 128, 185);
            panel3.BackColor = Color.FromArgb(229, 229, 229);
            textBox2.BackColor = Color.FromArgb(41, 128, 185);
            textBox1.ForeColor = Color.FromArgb(41, 128, 185);
            textBox2.ForeColor = Color.FromArgb(229, 229, 229);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(41, 128, 185);
            textBox2.BackColor = Color.FromArgb(41, 128, 185);
            panel3.BackColor = Color.FromArgb(41, 128, 185);
            panel4.BackColor = Color.FromArgb(41, 128, 185);
            textBox1.ForeColor = Color.FromArgb(229, 229, 229);
            textBox2.ForeColor = Color.FromArgb(229, 229, 229);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.FromArgb(229, 229, 229);
            textBox1.BackColor = Color.FromArgb(41, 128, 185);
            panel3.BackColor = Color.FromArgb(41, 128, 185);
            panel4.BackColor = Color.FromArgb(229, 229, 229);
            textBox1.ForeColor = Color.FromArgb(229, 229, 229);
            textBox2.ForeColor = Color.FromArgb(41, 128, 185);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(41, 128, 185);
            textBox2.BackColor = Color.FromArgb(41, 128, 185);
            panel3.BackColor = Color.FromArgb(41, 128, 185);
            panel4.BackColor = Color.FromArgb(41, 128, 185);
            textBox1.ForeColor = Color.FromArgb(229, 229, 229);
            textBox2.ForeColor = Color.FromArgb(229, 229, 229);
        }

        private void FocusTextBox1(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        bool isPasswordVisible = false;

        private void FocusTextBox2(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void showPassword(object sender, EventArgs e)
        {
            textBox2.Focus();
            isPasswordVisible = !isPasswordVisible;
            textBox2.UseSystemPasswordChar = !isPasswordVisible;
        }

        private void TouchTrackAppBtn_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (TouchTrackApp touchTrackApp = new TouchTrackApp())
            {
                touchTrackApp.ShowDialog();
            }

            this.Close();
        }

        public void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Please enter username and password.",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"
                        SELECT COUNT(*) 
                        FROM AdminUsers
                        WHERE Username = @username
                        AND PasswordHash = @passwordHash";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;

                        byte[] hash = HashPassword(password);
                        cmd.Parameters.Add("@passwordHash", SqlDbType.VarBinary, hash.Length).Value = hash;

                        con.Open();

                        int result = (int)cmd.ExecuteScalar();

                        if (result == 1)
                        {
                            // ✅ LOGIN SUCCESS
                            dashboard dash = new dashboard();
                            dash.FormClosed += (s, args) =>
                            {
                                this.Show();
                                ClearFields();
                            };
                            dash.Show();
                            this.Hide();

                        }
                        else
                        {
                            // ❌ INVALID CREDENTIALS
                            MessageBox.Show(
                                "Invalid username or password.",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while logging in.\n\n" + ex.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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
    }
}
