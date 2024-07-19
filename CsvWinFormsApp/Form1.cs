using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CsvWinFormsApp
{

    public partial class Form1 : Form
    {
        private readonly MyContext _context;

        public Form1()
        {
            InitializeComponent();
            // Get the connection string from appsettings.json
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

            // Initialize DbContext
            _context = new MyContext(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //List<MyEntity> entities = _context.MyEntities.Where(e => e.date > new DateTime(2000, 01, 01)).ToList();
            IQueryable<MyEntity> entities = from entity in _context.MyEntities
                                      where entity.date > new DateTime(2000, 01, 01) select entity;
            label1.Text = string.Join("\n", entities) + $"\n\n count: {entities.ToList().Count.ToString()}";
        }

        /* {
             string localhost = "okd4w1.okd.gomel.iba.by";
             string port = "31344";
             string Schema = "education";
             string InitialCatalog = "master";
             string Database = "dummy";
             string User = "sa";
             string Password = "MyStrongPassword1234";
             string Encrypt = "true";
             string TrustServerCertificate = "true";
             string connectionString = $"Server={localhost};Port={port};Database={InitialCatalog};User={User};Password={Password};SslMode=None;";
             connectionString = "Server=okd4w1.okd.gomel.iba.by;Port=31344;Initial Catalog=master;User Id=sa;Password=MyStrongPassword1234;SslMode=None;";
             label1.Text = connectionString;
             string table = $"education.dummy";
             string query = $"SELECT COUNT(*) FROM {table}"; // Replace 'mytable' with your table name

             using (MySqlConnection connection = new MySqlConnection(connectionString))
             {
                 try
                 {
                     connection.Open();
                     using (MySqlCommand command = new MySqlCommand(query, connection))
                     {
                         int recordCount = Convert.ToInt32(command.ExecuteScalar());
                         MessageBox.Show($"Number of records in the database: {recordCount}");
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show($"Error: {ex.Message}");
                 }
             }
         }*/
    }

}
