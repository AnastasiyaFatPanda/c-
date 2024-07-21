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
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Drawing;
using Microsoft.Extensions.FileSystemGlobbing;
using CsvHelper;
using System.Globalization;
using MySqlX.XDevAPI.Common;

namespace CsvWinFormsApp
{

    public partial class Form1 : Form
    {
        private readonly MyContext _context;

        public Form1()
        {
            try
            {
                InitializeComponent();
                // Get the connection string from appsettings.json
                string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

                // Initialize DbContext
                _context = new MyContext(connectionString);
                CheckNumOfDBRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                  $"Cannot run the app. Please, check your VPN connection. \n\n Error: \n {ex.Message}",
                  "Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
                );

                // Exit the application with an error code
                // Environment.Exit(1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //List<MyEntity> entities = _context.MyEntities.Where(e => e.date > new DateTime(2000, 01, 01)).ToList();
            IQueryable<Record> entities = from entity in _context.MyEntities
                                          where entity.Date > new DateTime(2000, 01, 01)
                                          select entity;
            label1.Text = string.Join("\n", entities.First()) + $"\n\n count: {entities.ToList().Count.ToString()}";
        }


        public int CheckNumOfDBRecords()
        {
            List<Record> entities = _context.MyEntities.ToList();
            int numberOfRecords = entities.Count;

            labelDbInfo.Text = numberOfRecords == 1
                ? "Database contains one record"
                :
                $"Database contains {numberOfRecords} records";
            switch (numberOfRecords)
            {
                case 0:
                    labelDbInfo.Text = "Database has no data";
                    ChangeFieldEnalability(false);
                    break;
                case 1:
                    labelDbInfo.Text = "Database contains one record";
                    ChangeFieldEnalability(true);
                    break;
                default:
                    labelDbInfo.Text = $"Database contains {numberOfRecords} records";
                    ChangeFieldEnalability(true);
                    break;
            }

            return numberOfRecords;
        }

        private void ChangeFieldEnalability(bool enable)
        {
            groupBox.Enabled = enable;

            foreach (Control control in groupBox.Controls)
            {
                if (control.GetType() != typeof(RadioButton))
                    control.BackColor = enable ? Color.Wheat : Color.LightGray;
            }
        }

        private async void buttonImport_Click(object sender, EventArgs e)
        {
            // Create and configure the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            // Show the dialog and handle the file selection
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path
                string selectedFilePath = openFileDialog.FileName;
                int BatchSize = 3;
                int addedRecords = 0;
                var records = new List<Record>();

                using (var reader = new StreamReader(selectedFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Read the header before reading any fields
                    await csv.ReadAsync();
                    csv.ReadHeader();

                    while (await csv.ReadAsync())
                    {
                        var record = new Record
                        {
                            Id = Guid.NewGuid(),
                            Name = csv.GetField<string>("name"),
                            Surname = csv.GetField<string>("surname"),
                            Date = csv.GetField<DateTime>("date"),
                            City = csv.GetField<string>("city"),
                            Country = csv.GetField<string>("country")
                        };

                        records.Add(record);

                        if (records.Count == BatchSize)
                        {
                            await InsertRecordsAsync(records);
                            addedRecords += BatchSize;
                            labelDbInfo.Text = $"Inserted {addedRecords} records...";
                            await Task.Delay(2000); // Wait for 1 second before processing the next batch
                            records.Clear();
                        }
                    }

                    if (records.Any())
                    {
                        addedRecords += records.Count;
                        await InsertRecordsAsync(records);
                        MessageBox.Show($"Inserted {addedRecords} records!");
                        CheckNumOfDBRecords();
                    }
                }
            }
        }

        private async Task InsertRecordsAsync(List<Record> records)
        {

            await _context.MyEntities.AddRangeAsync(records);
            await _context.SaveChangesAsync();

        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            // TODO async
            var records = _context.MyEntities.ToList();

            var filePath = "records_export.csv";

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Write the records to the CSV file
                await csv.WriteRecordsAsync(records);
            }

            var result = MessageBox.Show($"Data exported successfully to {filePath}. DO you want to open created file?", "File was created", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            // Check user's response
            if (result == DialogResult.OK)
            {
                // Open the file in the default application
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }
    }

}
