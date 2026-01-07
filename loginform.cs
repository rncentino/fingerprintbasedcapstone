using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using BiometricApp;

namespace BiometricApp
{
    public partial class loginform : Form
    {
        private biometricapp _parentForm;

        public loginform(biometricapp parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            usernametxt.TabIndex = 0;
            passwordtxt.TabIndex = 1;
            loginbtn.TabIndex = 2;
        }

        MySqlConnection conn = new MySqlConnection("server=localhost;database=biometricapp;user=root;password=123456;");

        private void loginbtn_Click(object sender, EventArgs e)
        {
            String username, password;
            username = usernametxt.Text.Trim();
            password = passwordtxt.Text.Trim();

            try
            {
                conn.Open();
                string query = "SELECT user_id, username FROM users WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Store session data
                    BiometricApp.Session.UserId = reader.GetInt32("user_id");
                    BiometricApp.Session.Username = reader.GetString("username");

                    _parentForm.HideBiometricApp(); // Hide main app

                    // Proceed to admin dashboard
                    main adminform = new main();
                    adminform.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernametxt.Clear();
                    passwordtxt.Clear();
                    usernametxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /*
            private void loginbtn_Click(object sender, EventArgs e)
            {
                String username, password;

                username = usernametxt.Text.Trim();
                password = passwordtxt.Text.Trim();

                try
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    DataTable dtable = new DataTable();
                    sda.Fill(dtable);

                    if(dtable.Rows.Count > 0)
                    {
                        _parentForm.HideBiometricApp(); // Hide main app

                        // Proceed to admin dashboard
                        main adminform = new main();
                        adminform.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login credentials!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usernametxt.Clear();
                        passwordtxt.Clear();
                        usernametxt.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        */

        private void FirstPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usernametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            usernametxt.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            passwordtxt.Focus();
        }
    }
}
