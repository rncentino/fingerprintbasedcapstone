using BiometricApp;
using DPFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

                                SyncEmployeeData();

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
                                                    fpCmd.Parameters.Add("@FingerprintTemplate", SqlDbType.VarBinary).Value = bytes;
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

                                                    this.Invoke((Action)(() =>
                                                    {
                                                        // UI updates
                                                        MessageBox.Show("Employee enrolled successfully!");
                                                    }));

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

                        case DPFP.Processing.Enrollment.Status.Failed:
                            {
                                Enroller.Clear();
                                Stop();
                                UpdateStatus();
                                OnTemplate?.Invoke(null);
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
    }
}
