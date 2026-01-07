using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class eventDetails: Form
    {
        private string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";
        private int EventID;

        public eventDetails(int eventId)
        {
            InitializeComponent();
            this.EventID = eventId;
            LoadEventDetails();
        }

        private void eventDetails_Load(object sender, EventArgs e)
        {
            LoadAttendanceLogs();
            LoadYearLevels();
            LoadCourses();
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbYearLevel.SelectedIndexChanged += FilterChanged;
            cmbCourse.SelectedIndexChanged += FilterChanged;


        }

        private void LoadEventDetails()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();
                    string query = "SELECT event_name, location, date FROM events WHERE event_id = @eventId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", EventID);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            lblEventName.Text = reader["event_name"].ToString();
                            lblLocation.Text = reader["location"].ToString();
                            lblDate.Text = Convert.ToDateTime(reader["date"]).ToString("MMMM dd, yyyy");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadAttendanceLogs(string searchTerm = "", string yearLevel = "All", string course = "All")
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();

                    string query = @"
                    SELECT 
                        s.student_id AS 'STUDENT ID',
                        CONCAT(s.lastname, ', ', s.firstname, ' ', LEFT(s.middlename, 1), '.') AS 'NAME',
                        s.yearlevel AS 'YEAR LEVEL',
                        s.course AS 'COURSE',
                        DATE_FORMAT(l.date, '%M %e, %Y') AS 'DATE',
                        TIME_FORMAT(l.time_in, '%h:%i %p') AS 'TIME IN',
                        TIME_FORMAT(l.time_out, '%h:%i %p') AS 'TIME OUT'
                    FROM 
                        logattendance l
                    INNER JOIN 
                        students s ON l.id = s.id
                    WHERE 
                        l.event_id = @eventId AND (
                        s.student_id LIKE @search OR 
                        s.firstname LIKE @search OR 
                        s.lastname LIKE @search OR 
                        s.course LIKE @search OR
                        s.yearlevel LIKE @search
                        )";

                    if (yearLevel != "All")
                        query += " AND s.yearlevel = @yearLevel";

                    if (course != "All")
                        query += " AND s.course = @course";

                    query += " ORDER BY l.time_in DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", EventID);
                        cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                        if (yearLevel != "All")
                            cmd.Parameters.AddWithValue("@yearLevel", yearLevel);

                        if (course != "All")
                            cmd.Parameters.AddWithValue("@course", course);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            AttendanceLogView.DataSource = dt;

                            AttendanceLogView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8, FontStyle.Bold);
                            AttendanceLogView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            foreach (DataGridViewColumn column in AttendanceLogView.Columns)
                            {
                                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            }

                            AttendanceLogView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadYearLevels()
        {
            cmbYearLevel.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(MyConnection))
            {
                conn.Open();
                string query = "SELECT DISTINCT yearlevel FROM students ORDER BY yearlevel ASC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbYearLevel.Items.Add(reader["yearlevel"].ToString());
                    }
                }
            }

            cmbYearLevel.Items.Insert(0, "All");
            cmbYearLevel.SelectedIndex = 0;
        }

        private void LoadCourses()
        {
            cmbCourse.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(MyConnection))
            {
                conn.Open();
                string query = "SELECT DISTINCT course FROM students ORDER BY course ASC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCourse.Items.Add(reader["course"].ToString());
                    }
                }
            }

            cmbCourse.Items.Insert(0, "All");
            cmbCourse.SelectedIndex = 0;
        }

        private void ExportAttendanceToPDF(DataGridView dgv, string filename)
        {
            try
            {
                PdfPTable pdfTable = new PdfPTable(dgv.ColumnCount);
                pdfTable.DefaultCell.Padding = 4;
                pdfTable.WidthPercentage = 100;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText))
                    {
                        BackgroundColor = new BaseColor(230, 230, 230),
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    pdfTable.AddCell(cell);
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdfTable.AddCell(cell.Value?.ToString() ?? string.Empty);
                    }
                }

                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Paragraph nsc = new Paragraph("NORTHERN SAMAR COLLEGES", FontFactory.GetFont("Arial", 16));
                    nsc.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(nsc);
                    Paragraph title = new Paragraph(lblEventName.Text + " - Attendance Logs", FontFactory.GetFont("Arial", 16));
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    pdfDoc.Add(new Paragraph("Location: " + lblLocation.Text));
                    pdfDoc.Add(new Paragraph("Date: " + lblDate.Text));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(pdfTable);

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("PDF Exported Successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF Export Error: " + ex.Message);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = lblEventName.Text.Replace(" ", "_") + "_AttendanceLogs.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportAttendanceToPDF(AttendanceLogView, sfd.FileName);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadAttendanceLogs(searchTerm);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadAttendanceLogs(searchTerm);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string year = cmbYearLevel.SelectedItem?.ToString() ?? "All";
            string course = cmbCourse.SelectedItem?.ToString() ?? "All";

            LoadAttendanceLogs(search, year, course);
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res;

            res = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }
    }
}
