using DPFP;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    public partial class students : Form
    {
        private string MyConnection = "server=localhost;database=biometricapp;user=root;password=123456;";

        private DPFP.Template Template;


        public students()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.students_Load);
        }

        private void students_Load(object sender, EventArgs e)
        {
            LoadStudents();
            dataGridViewStudents.CellClick += dataGridViewStudents_CellClick;
        }

        public void LoadStudents()
        {
            try
            {
                using (MySqlConnection MyConn = new MySqlConnection(MyConnection))
                {
                    MyConn.Open();
                    string Query = "SELECT id, student_id, lastname, firstname, middlename, yearlevel, course FROM students";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(Query, MyConn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewStudents.DataSource = dt;
                    dataGridViewStudents.Columns["id"].Visible = false;
                    dataGridViewStudents.Columns["student_id"].HeaderText = "STUDENT ID";
                    dataGridViewStudents.Columns["lastname"].HeaderText = "LAST NAME";
                    dataGridViewStudents.Columns["firstname"].HeaderText = "FIRST NAME";
                    dataGridViewStudents.Columns["middlename"].HeaderText = "MIDDLE NAME";
                    dataGridViewStudents.Columns["yearlevel"].HeaderText = "YEAR LEVEL";
                    dataGridViewStudents.Columns["course"].HeaderText = "COURSE";

                    // Add Link Column for student id
                    if (dataGridViewStudents.Columns["ACTIVITY"] == null)
                    {
                        DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
                        linkColumn.DataPropertyName = "student_id";
                        linkColumn.Name = "student_id";
                        linkColumn.HeaderText = "STUDENT ID";
                        linkColumn.LinkColor = Color.Blue;
                        linkColumn.TrackVisitedState = false;
                        dataGridViewStudents.Columns.Remove("student_id");
                        dataGridViewStudents.Columns.Insert(1, linkColumn);
                    }

                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddActionButtons()
        {

            if (dataGridViewStudents.Columns["UpdateButton"] == null)
            {
                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                updateButton.Name = "UpdateButton";
                updateButton.HeaderText = "EDIT";
                updateButton.Text = "Update";
                updateButton.UseColumnTextForButtonValue = true;
                updateButton.DefaultCellStyle.BackColor = Color.LightGreen;
                dataGridViewStudents.Columns.Add(updateButton);
            }

            if (dataGridViewStudents.Columns["DeleteButton"] == null)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "DeleteButton";
                deleteButton.HeaderText = "DELETE";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                deleteButton.DefaultCellStyle.BackColor = Color.LightCoral;
                dataGridViewStudents.Columns.Add(deleteButton);
            }
        }

        private void dataGridViewStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dataGridViewStudents.Rows[e.RowIndex].Cells["id"].Value);

                if (dataGridViewStudents.Columns[e.ColumnIndex].Name == "ACTIVITY")
                {
                    //var eventDetailsForm = new eventDetails(studentId);
                    //eventDetailsForm.ShowDialog();
                }
                else if (dataGridViewStudents.Columns[e.ColumnIndex].Name == "UpdateButton")
                {
                    //var updateForm = new updateActivity(studentId);
                    //updateForm.FormClosed += (s, args) => LoadStudents();
                    //updateForm.ShowDialog();
                }
                else if (dataGridViewStudents.Columns[e.ColumnIndex].Name == "DeleteButton")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent(studentId);
                        LoadStudents();
                    }
                }
            }
        }

        private void DeleteEvent(int studentId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConnection))
                {
                    conn.Open();
                    string query = "DELETE FROM students WHERE id = @studentId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Event deleted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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

        private void addstudentsbtn_Click(object sender, EventArgs e)
        {
            enroll Enfrm = new enroll();
            Enfrm.OnTemplate += this.OnTemplate;
            Enfrm.ShowDialog();

        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            verify VeFrm = new verify();
            VeFrm.Verify(Template);
        }
    }
}
