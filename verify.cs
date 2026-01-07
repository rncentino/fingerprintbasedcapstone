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
using DPFP;
using MySql.Data.MySqlClient;

namespace BiometricApp
{
    public partial class verify: capture
    {

        DPFP.Template Template;
        DPFP.Verification.Verification Verificator;

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            try
            {
                string MyConnection = "datasource=localhost;username=root;password=123456;database=biometricapp";
                string Query = "SELECT student_id, firstname, lastname, middlename, yearlevel, course, fingerprint FROM students";

                using (MySqlConnection MyConn = new MySqlConnection(MyConnection))
                {
                    MyConn.Open();
                    using (MySqlCommand MyCommand = new MySqlCommand(Query, MyConn))
                    using (MySqlDataReader myReader = MyCommand.ExecuteReader())
                    {
                        while (myReader.Read()) // Loop through each enrolled fingerprint
                        {
                            byte[] storedTemplateBytes = (byte[])myReader["fingerprint"];
                            MemoryStream ms = new MemoryStream(storedTemplateBytes);
                            Template = new DPFP.Template(ms);  // Load stored fingerprint template

                            base.Process(Sample);

                            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
                            if (features == null)
                            {
                                MakeReport("Failed to extract fingerprint features. Try again.");
                                return;
                            }

                            DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                            Verificator.Verify(features, Template, ref result);

                            UpdateStatus(result.FARAchieved);

                            if (result.Verified)
                            {
                                // If fingerprint matches, display student information
                                MakeReport($"Fingerprint VERIFIED: {myReader["firstname"]} {myReader["lastname"]}");

                                Setstudent_id(myReader["student_id"].ToString());
                                Setlname(myReader["lastname"].ToString());
                                Setfname(myReader["firstname"].ToString());
                                Setmname(myReader["middlename"].ToString());
                                SetyearLevel(myReader["yearlevel"].ToString());
                                Setcourse(myReader["course"].ToString());

                                return; // Exit after the first match
                            }
                        }

                        // If no match was found
                        MakeReport("Fingerprint NOT VERIFIED! No matching record found.");
                        Setstudent_id("NO DATA FOUND!");
                        Setlname("NO DATA FOUND!");
                        Setfname("NO DATA FOUND!");
                        Setmname("NO DATA FOUND!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during verification: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Verification";
            Verificator = new DPFP.Verification.Verification();
            UpdateStatus(0);
        }

        private void UpdateStatus(int FAR)
        {
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }
    }
}
