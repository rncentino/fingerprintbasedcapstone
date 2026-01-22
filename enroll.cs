using BiometricApp;
using DPFP;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricApp
{
    public partial class enroll : capture
    {

        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate;

        private DPFP.Processing.Enrollment Enroller;

        string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Enrollment";
            Enroller = new DPFP.Processing.Enrollment();
            UpdateStatus();
        }

        protected override void Process(Sample Sample)
        {
            base.Process(Sample);

            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if(features != null)
                try
                {
                    MakeReport("The fingerprint feature set was created");
                    Enroller.AddFeatures(features);
                }
                finally
                {
                    UpdateStatus();

                    switch(Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:
                            {
                                int count = 0;
                                OnTemplate?.Invoke(Enroller.Template);
                                byte[] bytes;
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    Enroller.Template.Serialize(ms);
                                    bytes = ms.ToArray();
                                }

                                try
                                {
                                    //string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
                                    string Query = "SELECT COUNT(*) FROM Employees WHERE EmployeeNumber = @EmployeeNumber";

                                    using (SqlConnection con = new SqlConnection(conn))
                                    using (SqlCommand cmd = new SqlCommand(Query, con))
                                    {
                                        con.Open();
                                        cmd.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);
                                        count = (int)cmd.ExecuteScalar();
                                    }

                                    MakeReport("Total employees found: " + count);

                                    if (count > 0)
                                    {
                                        MessageBox.Show("Employee Number already exists.");
                                        Enroller.Clear();
                                        UpdateStatus();
                                        Stop();   // ADD THIS
                                        Start();  // optional restart
                                        break;
                                    }

                                    else
                                    {
                                        try
                                        {
                                            //string conn1 = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
                                            List<ScheduleItem> schedules = GetSchedulesForDatabase();

                                            using (SqlConnection con1 = new SqlConnection(conn))
                                            {
                                                con1.Open();
                                                SqlTransaction tran = con1.BeginTransaction();
                                                try
                                                {
                                                    // 1️⃣ INSERT EMPLOYEE
                                                    SqlCommand empCmd = new SqlCommand(
                                                        @"INSERT INTO Employees (EmployeeNumber, LastName, FirstName, Role, CreatedAt)
                                                           OUTPUT INSERTED.EmployeeID VALUES (@EmployeeNumber, @LastName, @FirstName, @Role, GETDATE())", con1, tran);

                                                    empCmd.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);
                                                    empCmd.Parameters.AddWithValue("@LastName", LastName);
                                                    empCmd.Parameters.AddWithValue("@FirstName", FirstName);
                                                    empCmd.Parameters.AddWithValue("@Role", Role);
                                                    int employeeId = (int)empCmd.ExecuteScalar();

                                                    // 2️⃣ INSERT FINGERPRINT
                                                    SqlCommand fpCmd = new SqlCommand(
                                                        @"INSERT INTO Fingerprints (EmployeeID, FingerprintTemplate, DateRegistered)
                                                           VALUES (@EmployeeID, @FingerprintTemplate, GETDATE())", con1, tran);

                                                    fpCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                                                    fpCmd.Parameters.AddWithValue("@FingerprintTemplate", SqlDbType.VarBinary).Value = bytes;
                                                    fpCmd.ExecuteNonQuery();

                                                    // 3️⃣ INSERT SCHEDULE
                                                    foreach (var sched in schedules)
                                                    {
                                                        SqlCommand schCmd = new SqlCommand(@"
                                                            INSERT INTO Schedules
                                                            (EmployeeID, [DayOfWeek], TimeIn, TimeOut)
                                                            VALUES (@EmployeeID, @DayOfWeek, @TimeIn, @TimeOut)",
                                                            con1, tran);

                                                        schCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                                                        schCmd.Parameters.AddWithValue("@DayOfWeek", sched.DayOfWeek);
                                                        schCmd.Parameters.AddWithValue("@TimeIn", sched.TimeIn);
                                                        schCmd.Parameters.AddWithValue("@TimeOut", sched.TimeOut);

                                                        schCmd.ExecuteNonQuery();
                                                    }


                                                    // ✅ ALL OK
                                                    tran.Commit();
                                                    MessageBox.Show("Employee enrolled successfully!");

                                                    Enroller.Clear();
                                                    UpdateStatus();
                                                    ClearFields();   // from base form
                                                    Start();
                                                }
                                                catch (Exception ex)
                                                {
                                                    // ❌ ANY FAILURE = ROLLBACK
                                                    tran.Rollback();
                                                    MessageBox.Show("Enrollment failed: " + ex.Message);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error during registration: " + ex.Message);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error: " + ex.Message);
                                }

                                break;
                            }

                        /*
                        {
                            int count = 0;

                            OnTemplate(Enroller.Template);

                            MemoryStream fingerprintData = new MemoryStream();
                            Enroller.Template.Serialize(fingerprintData);
                            fingerprintData.Position = 0;
                            BinaryReader br = new BinaryReader(fingerprintData);

                            byte[] bytes = br.ReadBytes((Int32)fingerprintData.Length);

                            try
                            {
                                string MyConnection = "datasource=localhost;username=root;password=123456";
                                string Query = "SELECT * FROM biometricapp.students WHERE UPPER(firstname) = '"+FirstName.ToUpper()+" '";
                                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                                MyConn.Open();

                                MySqlDataReader myReader = MyCommand.ExecuteReader();

                                while (myReader.Read())
                                {
                                    count = count + 1;
                                }

                                MakeReport("Total student found : " + count);

                                if(count > 0)
                                {
                                    MessageBox.Show("The student you want to register is already exists");
                                    Stop();
                                    Start();
                                } else
                                {
                                    try
                                    {
                                        string MyConnection1 = "datasource=localhost;username=root;password=123456";
                                        string Query1 = "INSERT INTO biometricapp.students (user_id, student_id, lastname, firstname, middlename, yearlevel, course, fingerprint) VALUES (@userId,', @finger)";
                                        MySqlConnection MyConn1 = new MySqlConnection(MyConnection1);
                                        MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn1);

                                        MyCommand1.Parameters.AddWithValue("@userId", BiometricApp.Session.UserId);
                                        MyCommand1.Parameters.AddWithValue("@finger", bytes).DbType = DbType.Binary;
                                        MySqlDataReader MyReader1;
                                        MyConn1.Open();
                                        MyReader1 = MyCommand1.ExecuteReader();

                                        //MyConn1.Close();
                                        //Stop();

                                        MyConn1.Close();
                                        MessageBox.Show("Student registered successfully!");
                                        ClearForm(); // Method to reset form fields
                                        Stop();
                                        Start(); // Restart scanning for the next input


                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                }
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" +ex.Message);
                            }

                            break;
                        }

                        */

                        case DPFP.Processing.Enrollment.Status.Failed:
                            {
                                Enroller.Clear();
                                Stop();
                                UpdateStatus();
                                OnTemplate(null);
                                Start();
                                break;
                            }
                    }
                }

        }

        private void UpdateStatus()
        {
            SetStatus(String.Format("Fingerprint sample needed: {0}", Enroller.FeaturesNeeded));
        }

        private void ClearForm()
        {
            // Reset fields in memory
            //StudentID = "";
            //FirstName = "";
            //Lastname = "";
            //Middlename = "";
            //YearLevel = "";
            //Course = "";

            // Clear UI fields in the capture form
            if (this is capture)
            {
                ((capture)this).ClearFields(); // Call the method in capture form
            }

            // Reinitialize Enroller to accept a new fingerprint
            Enroller.Clear();
            UpdateStatus();
        }


    }
}
