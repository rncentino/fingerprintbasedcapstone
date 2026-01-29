using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiometricApp
{

    internal class DatabaseInitializer
    {
        private const string DB_NAME = "TouchTrackApp";
        private const string SERVER =
            "Server=EYCHRON\\SQLEXPRESS;Integrated Security=true;";

        public static void Initialize()
        {
            CreateDatabaseIfNotExists();
            CreateTablesIfNotExists();
            SeedDefaultAdmin();
            SeedSchoolInfo();
        }

        // ---------------- DATABASE ----------------
        private static void CreateDatabaseIfNotExists()
        {
            using (SqlConnection con = new SqlConnection(
                SERVER + "Database=master;"))
            {
                con.Open();

                string sql = $@"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{DB_NAME}')
                CREATE DATABASE {DB_NAME}";

                new SqlCommand(sql, con).ExecuteNonQuery();
            }
        }

        // ---------------- TABLES ----------------
        private static void CreateTablesIfNotExists()
        {
            using (SqlConnection con = new SqlConnection(
                SERVER + $"Database={DB_NAME};"))
            {
                con.Open();

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='AdminUsers')
                CREATE TABLE AdminUsers (
                    AdminID INT IDENTITY PRIMARY KEY,
                    Username NVARCHAR(50) UNIQUE NOT NULL,
                    PasswordHash NVARCHAR(255) NOT NULL,
                    CreatedAt DATETIME DEFAULT GETDATE()
                )");

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Employees')
                CREATE TABLE Employees (
                    EmployeeID INT IDENTITY PRIMARY KEY,
                    EmployeeNumber NVARCHAR(50) UNIQUE NOT NULL,
                    LastName NVARCHAR(100),
                    FirstName NVARCHAR(100),
                    Role NVARCHAR(50),
                    CreatedAt DATETIME DEFAULT GETDATE()
                )");

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='SchoolInfo')
                CREATE TABLE SchoolInfo (
                    SchoolID INT IDENTITY PRIMARY KEY,
                    SchoolName NVARCHAR(200),
                    SchoolAddress NVARCHAR(300),
                    ContactNumber NVARCHAR(50),
                    Email NVARCHAR(100),
                    Logo VARBINARY(MAX)
                )");

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Schedules')
                CREATE TABLE Schedules (
                    ScheduleID INT IDENTITY PRIMARY KEY,
                    EmployeeID INT,
                    DayOfWeek NVARCHAR(20),
                    TimeIn TIME,
                    TimeOut TIME,
                    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
                )");

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Fingerprints')
                CREATE TABLE Fingerprints (
                    FingerprintID INT IDENTITY PRIMARY KEY,
                    EmployeeID INT,
                    FingerprintTemplate VARBINARY(MAX),
                    DateRegistered DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
                )");

                Execute(con, @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Attendance')
                CREATE TABLE Attendance (
                    AttendanceID INT IDENTITY PRIMARY KEY,
                    EmployeeID INT,
                    AttendanceDate DATE,
                    TimeIn TIME,
                    TimeOut TIME,
                    Status NVARCHAR(50),
                    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
                )");
            }
        }

        // ---------------- ADMIN SEED ----------------
        private static void SeedDefaultAdmin()
        {
            using (SqlConnection con = new SqlConnection(
                SERVER + $"Database={DB_NAME};"))
            {
                con.Open();

                int count = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM AdminUsers", con).ExecuteScalar();

                if (count == 0)
                {
                    string hash = HashPassword("Admin123");

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO AdminUsers (Username, PasswordHash) VALUES ('ADMIN', @PasswordHash)",
                        con);

                    cmd.Parameters.AddWithValue("@PasswordHash", hash);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void SeedSchoolInfo()
        {
            using (SqlConnection con = new SqlConnection(
                SERVER + $"Database={DB_NAME};"))
            {
                con.Open();

                // Check if SchoolInfo already exists
                int count = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM SchoolInfo", con).ExecuteScalar();

                if (count == 0)
                {
                    byte[] logoBytes = ImageToByteArray(
                        Properties.Resources.logo);

                    SqlCommand cmd = new SqlCommand(@"
                INSERT INTO SchoolInfo
                (SchoolName, SchoolAddress, ContactNumber, Email, Logo)
                VALUES
                (@Name, @Address, @Contact, @Email, @Logo)", con);

                    cmd.Parameters.AddWithValue("@Name", "NORTHERN SAMAR COLLEGES");
                    cmd.Parameters.AddWithValue("@Address", "Brgy. J.P. Rizal, Catarman, Northern Samar");
                    cmd.Parameters.AddWithValue("@Contact", "09123456789");
                    cmd.Parameters.AddWithValue("@Email", "nsceduc@gmail.com");
                    cmd.Parameters.AddWithValue("@Logo", logoBytes);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ---------------- HELPERS ----------------
        private static void Execute(SqlConnection con, string sql)
        {
            new SqlCommand(sql, con).ExecuteNonQuery();
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return Convert.ToBase64String(
                    sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
