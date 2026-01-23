using DPFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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
        private DPFP.Template Template;

        public EmployeeRegistrationForm()
        {
            InitializeComponent();

        }

        private void LoadEmployees()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = @"SELECT 
                                EmployeeID,
                                EmployeeNumber,
                                LastName,
                                FirstName,
                                Role
                             FROM Employees";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEmployees.DataSource = dt;

                    dgvEmployees.Columns["EmployeeID"].Visible = false; // 👈 hidden but usable
                    dgvEmployees.Columns["EmployeeNumber"].HeaderText = "Employee No";
                    dgvEmployees.Columns["LastName"].HeaderText = "Last Name";
                    dgvEmployees.Columns["FirstName"].HeaderText = "First Name";

                    AddDeleteButton();

                }
                catch (Exception ex)
                {
                MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void AddDeleteButton()
        {
            if (dgvEmployees.Columns["Delete"] != null) return;

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.Name = "Delete";
            deleteBtn.HeaderText = "Delete";
            deleteBtn.Text = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;

            dgvEmployees.Columns.Add(deleteBtn);
        }


        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvEmployees.Columns[e.ColumnIndex].Name == "Delete")
            {
                int employeeId = Convert.ToInt32(
                    dgvEmployees.Rows[e.RowIndex].Cells["EmployeeID"].Value);

                DeleteEmployee(employeeId);
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            DialogResult result = MessageBox.Show(
                "Deleting this employee will also remove the fingerprint.\n\nContinue?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            string connStr = ConfigurationManager
                .ConnectionStrings["MyConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            DELETE FROM Fingerprints WHERE EmployeeID = @id;
            DELETE FROM Employees WHERE EmployeeID = @id;
        ", conn);

                cmd.Parameters.AddWithValue("@id", employeeId);
                cmd.ExecuteNonQuery();
            }

            LoadEmployees(); // 🔄 refresh
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

            LoadEmployees();
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

        private void EmployeeRegistrationForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

    }
}
