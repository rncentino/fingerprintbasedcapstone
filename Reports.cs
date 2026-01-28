using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BiometricApp
{
    public partial class Reports : Form
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

        private void Reports_Load(object sender, EventArgs e)
        {
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel1.Width, panel1.Height, 15, 15));

            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel2.Width, panel2.Height, 15, 15));

            panel4.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel4.Width, panel4.Height, 15, 15));
            
            dateTimePicker1.ValueChanged += DateTimePicker_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker_ValueChanged;

            UpdateTotalDays(); // initial calculation
            LoadAttendanceLogs();
        }

        private void UpdateTotalDays()
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (endDate < startDate)
            {
                totalDaysLbl.Text = "0 days";
                return;
            }

            int totalDays = (endDate - startDate).Days + 1; // inclusive
            totalDaysLbl.Text = totalDays == 1 ? "1 day" : $"{totalDays} days";
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalDays();
            LoadAttendanceLogs();
        }

        public Reports()
        {
            InitializeComponent();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM dd, yyyy";
            dateTimePicker1.Value = new DateTime(2026, 1, 1);

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MMMM dd, yyyy";
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
            CASE
    WHEN a.TimeIn IS NOT NULL AND a.TimeOut IS NOT NULL THEN
        CAST(DATEDIFF(MINUTE, a.TimeIn, a.TimeOut) / 60.0 AS DECIMAL(10,1))
    ELSE
        NULL
    END AS HOURS
        FROM Attendance a
        LEFT JOIN Employees e
            ON a.EmployeeID = e.EmployeeID
        WHERE a.AttendanceDate BETWEEN @StartDate AND @EndDate
        ORDER BY a.AttendanceDate DESC, a.TimeIn DESC";

                using (SqlConnection sqlCon = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.Add("@StartDate", SqlDbType.Date)
                        .Value = dateTimePicker1.Value.Date;

                    cmd.Parameters.Add("@EndDate", SqlDbType.Date)
                        .Value = dateTimePicker2.Value.Date;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvAttendanceLog.DataSource = dt;

                    dgvAttendanceLog.Columns["AttendanceID"].Visible = false;
                    dgvAttendanceLog.Columns["EmployeeNumber"].HeaderText = "ID";
                    dgvAttendanceLog.Columns["FullName"].HeaderText = "NAME";
                    dgvAttendanceLog.Columns["Role"].Visible = false;
                    dgvAttendanceLog.Columns["AttendanceDate"].HeaderText = "DATE";
                    dgvAttendanceLog.Columns["AttendanceDate"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    dgvAttendanceLog.Columns["TimeIn"].HeaderText = "TIME IN";
                    dgvAttendanceLog.Columns["TimeOut"].HeaderText = "TIME OUT";

                    dgvAttendanceLog.Columns["TimeIn"].DefaultCellStyle.Format = "hh:mm tt";
                    dgvAttendanceLog.Columns["TimeOut"].DefaultCellStyle.Format = "hh:mm tt";

                    dgvAttendanceLog.EnableHeadersVisualStyles = false;
                    dgvAttendanceLog.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    dgvAttendanceLog.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                    dgvAttendanceLog.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 229, 229);

                    dgvAttendanceLog.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgvAttendanceLog.GridColor = Color.LightGray;
                    dgvAttendanceLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);


                    int totalRecords = dgvAttendanceLog.AllowUserToAddRows
                        ? dgvAttendanceLog.Rows.Count - 1
                        : dgvAttendanceLog.Rows.Count;

                    labelTotalRecords.Text = totalRecords.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading attendance logs: " + ex.Message,
                    "Error",
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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {

        }
    }
}
