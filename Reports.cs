using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void ExportAttendanceToPDF(DataGridView dgv, string filename)
        {
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    PdfPTable header = new PdfPTable(2);
                    header.WidthPercentage = 60;
                    header.SetWidths(new float[] { 1, 4 });

                    // 🔹 Logo cell
                    PdfPCell logoCell = new PdfPCell();
                    logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    System.Drawing.Image logoImage = Properties.Resources.logo;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        logoImage.Save(ms, ImageFormat.Png);
                        iTextSharp.text.Image pdfLogo = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        pdfLogo.ScaleToFit(60f, 60f);
                        logoCell.AddElement(pdfLogo);
                    }

                    header.AddCell(logoCell);

                    // 🔹 Text cell
                    PdfPCell textCell = new PdfPCell();
                    textCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    textCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    textCell.AddElement(new Paragraph(
                        "NORTHERN SAMAR COLLEGES",
                        FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD))
                    {
                        Alignment = Element.ALIGN_CENTER
                    });

                    textCell.AddElement(new Paragraph(
                        "Brgy. J.P. Rizal, Catarman, Northern Samar",
                        FontFactory.GetFont("Arial", 11))
                    {
                        Alignment = Element.ALIGN_CENTER
                    });

                    textCell.AddElement(new Paragraph(
                        "Email: nsceduc@email.com",
                        FontFactory.GetFont("Arial", 10))
                    {
                        Alignment = Element.ALIGN_CENTER
                    });

                    header.AddCell(textCell);

                    // Add header to PDF
                    pdfDoc.Add(header);

                    // Small spacing before table
                    pdfDoc.Add(new Paragraph(" "));

                    // Columns you WANT in the PDF (by Name)
                    string[] visibleColumns =
                    {
                        "EmployeeNumber",
                        "FullName",
                        "AttendanceDate",
                        "TimeIn",
                        "TimeOut",
                        "HOURS"
                    };

                    PdfPTable pdfTable1 = new PdfPTable(visibleColumns.Length);
                    pdfTable1.WidthPercentage = 100;
                    pdfTable1.DefaultCell.Padding = 4;

                    foreach (string colName in visibleColumns)
                    {
                        var col = dgv.Columns[colName];
                        PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText))
                        {
                            BackgroundColor = new BaseColor(230, 230, 230),
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };

                        pdfTable1.AddCell(cell);
                    }

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (string colName in visibleColumns)
                        {
                            string value = row.Cells[colName].Value?.ToString() ?? "";

                            // ✅ FORMAT ONLY THE DATE COLUMN
                            if (colName == "AttendanceDate")
                            {
                                DateTime dt;
                                if (DateTime.TryParse(value, out dt))
                                {
                                    value = dt.ToString("MMM dd, yyyy");
                                }
                            }

                            // 🔹 TIME-IN / TIME-OUT COLUMNS
                            else if (colName == "TimeIn" || colName == "TimeOut")
                            {
                                DateTime time;
                                if (DateTime.TryParse(value, out time))
                                    value = time.ToString("hh:mm tt").ToUpper();
                            }

                            PdfPCell dataCell = new PdfPCell(new Phrase(value));
                            pdfTable1.AddCell(dataCell);
                        }
                    }


                    float[] columnWidths =
                    {
                        1.5f, // EmployeeNumber
                        3f,   // FullName
                        2f,   // AttendanceDate
                        1.5f, // TimeIn
                        1.5f, // TimeOut
                        1f    // HOURS
                    };

                    pdfTable1.SetWidths(columnWidths);

                    Dictionary<string, string> headerMap = new Dictionary<string, string>()
                    {
                        {"EmployeeNumber", "ID" },
                        { "FullName", "NAME" },
                        { "AttendanceDate", "DATE" },
                        { "TimeIn", "TIME IN" },
                        { "TimeOut", "TIME OUT" },
                        { "HOURS", "HOURS" }
                    };





                    pdfDoc.Add(pdfTable1);

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
                sfd.FileName = totalDaysLbl.Text.Replace(" ", "_") + "_AttendanceLogs.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportAttendanceToPDF(dgvAttendanceLog, sfd.FileName);
                }
            }
        }
    }
}
