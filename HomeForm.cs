using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BiometricApp
{
    public partial class HomeForm : Form
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

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

            LoadDashboardStats();
            LoadAttendanceLogs();
        }

        private void LoadDashboardStats()
        {
            LoadTotalEmployees();
            LoadPresentToday();
            LoadLateArrivals();
            LoadAbsentToday();
        }

        private void LoadTotalEmployees()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "SELECT COUNT(*) FROM Employees";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int totalEmployees = Convert.ToInt32(cmd.ExecuteScalar());
                    totalEmployeesLbl.Text = totalEmployees.ToString();
                }
            }
        }

        private void LoadPresentToday()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT COUNT(DISTINCT EmployeeID)
            FROM Attendance
            WHERE CAST(TimeIn AS DATE) = CAST(GETDATE() AS DATE)", con);

                con.Open();
                presentTodayLbl.Text =
                    Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }

        private void LoadLateArrivals()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT COUNT(DISTINCT A.EmployeeID)
            FROM Attendance A
            JOIN Schedules S ON A.EmployeeID = S.EmployeeID
            WHERE CAST(A.TimeIn AS DATE) = CAST(GETDATE() AS DATE)
            AND CAST(A.TimeIn AS TIME) > S.TimeIn", con);

                con.Open();
                lateArrivalsLbl.Text =
                    Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }

        private void LoadAbsentToday()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT COUNT(*)
            FROM Schedules S
            WHERE S.DayOfWeek = DATENAME(WEEKDAY, GETDATE())
            AND NOT EXISTS (
                SELECT 1
                FROM Attendance A
                WHERE A.EmployeeID = S.EmployeeID
                AND CAST(A.TimeIn AS DATE) = CAST(GETDATE() AS DATE)
            )", con);

                con.Open();
                absentTodayLbl.Text =
                    Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }




        private void LoadAttendanceLogs()
        {
            try
            {
                string query = @"
                SELECT
                    a.AttendanceID,
                    e.EmployeeNumber,
                    CONCAT(e.LastName, ', ', e.FirstName) AS FullName,
                    e.Role,
                    a.AttendanceDate,
                    a.TimeIn,
                    a.TimeOut,
                    a.Status
                FROM Attendance a
                LEFT JOIN Employees e
                    ON a.EmployeeID = e.EmployeeID
                WHERE a.AttendanceDate >= CAST(GETDATE() AS DATE)
                    AND a.AttendanceDate <  DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                ORDER BY a.TimeIn DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAttendanceLog.DataSource = dt;

                dgvAttendanceLog.Columns["AttendanceID"].Visible = false;
                dgvAttendanceLog.Columns["EmployeeNumber"].HeaderText = "ID";
                dgvAttendanceLog.Columns["FullName"].HeaderText = "NAME";
                dgvAttendanceLog.Columns["Role"].Visible = false;
                dgvAttendanceLog.Columns["AttendanceDate"].Visible = false;
                dgvAttendanceLog.Columns["TimeIn"].HeaderText = "TIME IN";
                dgvAttendanceLog.Columns["TimeOut"].HeaderText = "TIME OUT";
                dgvAttendanceLog.Columns["Status"].HeaderText = "STATUS";

                dgvAttendanceLog.Columns["EmployeeNumber"].Width = 60;
                dgvAttendanceLog.Columns["FullName"].Width = 130;
                dgvAttendanceLog.Columns["TimeIn"].Width = 80;
                dgvAttendanceLog.Columns["TimeOut"].Width = 80;
                dgvAttendanceLog.Columns["Status"].Width = 70;

                dgvAttendanceLog.Columns["TimeIn"].DefaultCellStyle.Format = "hh:mm tt";
                dgvAttendanceLog.Columns["TimeOut"].DefaultCellStyle.Format = "hh:mm tt";

                // header style
                dgvAttendanceLog.EnableHeadersVisualStyles = false;
                dgvAttendanceLog.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvAttendanceLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                dgvAttendanceLog.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 229, 229);

                dgvAttendanceLog.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvAttendanceLog.GridColor = Color.LightGray;
                dgvAttendanceLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
