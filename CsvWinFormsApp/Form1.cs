using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using ClosedXML.Excel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Drawing;
using Microsoft.Extensions.FileSystemGlobbing;
using CsvHelper;
using System.Globalization;
using MySqlX.XDevAPI.Common;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace CsvWinFormsApp
{

    public partial class Form1 : Form
    {
        private readonly MyContext _context;
        private char _lastKeyPressed;
        private string _db;

        public Form1()
        {
            InitializeComponent();

            // Display the loading form
            LoadingMessageForm loadingForm = new LoadingMessageForm("Trying to connect ot specified Database...", "Connecting to DB");
            Task.Run(() =>
            {
                Application.Run(loadingForm);
            });

            // Simulate some work
            Task.Delay(1000).Wait(); // Simulate 1 second delay for loading

            try
            {
                // Get the connection string from appsettings.json
                string connectionString = ConfigurationHelper.GetConnectionString();
                _db = ConfigurationHelper.GetDatabaseName();

                // Initialize DbContext
                _context = new MyContext(connectionString);
                CheckNumOfDBRecords();

                // Close the loading form
                loadingForm.Invoke(new Action(() => loadingForm.Close()));
            }
            catch (Exception ex)
            {
                // Close the loading form
                loadingForm.Invoke(new Action(() => loadingForm.Close()));

                var result = MessageBox.Show(
                  $"Cannot run the app. Please, check your VPN connection. \n\n Error: \n {ex.Message}",
                  "Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
                );

                if (result != DialogResult.None)
                {
                    // Exit the application with an error code
                    Environment.Exit(1);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //List<MyEntity> entities = _context.MyEntities.Where(e => e.date > new DateTime(2000, 01, 01)).ToList();
            IQueryable<Record> entities = from entity in _context.MyEntities
                                          where entity.Date > new DateTime(2000, 01, 01)
                                          select entity;
            if (entities.ToList().Count() != 0)
                label1.Text = string.Join("\n", entities.First()) + $"\n\n count: {entities.ToList().Count.ToString()} {_db}";
            else
                label1.Text = "No data";
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

            if (enable)
            {
                buttonDeleteDbData.BackColor = Color.LightPink;
                buttonDeleteDbData.ForeColor = Color.Red;
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
                int BatchSize = 1;
                int addedRecords = 0;
                var records = new List<Record>();
                LoadingMessageForm loadingForm = new LoadingMessageForm("Trying to connect ot specified Database...", "Import data to DB");
                loadingForm.Show();

                try
                {
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
                                loadingForm.Invoke(new Action(() => loadingForm.UpdateMessage($"Inserted {addedRecords} records...")));
                                await InsertRecordsAsync(records);
                                addedRecords += BatchSize;
                                labelDbInfo.Text = $"Inserted {addedRecords} records...";
                                await Task.Delay(1000); // Wait for 1 second before processing the next batch
                                records.Clear();
                            }
                        }

                        if (records.Count() > 0)
                        {
                            addedRecords += records.Count;
                            await InsertRecordsAsync(records);
                            // Close the loading form
                        }

                        loadingForm.Invoke(new Action(() => loadingForm.Close()));
                        // Show success message
                        MessageBox.Show($"Inserted {addedRecords} records!");
                        CheckNumOfDBRecords();
                    }
                }
                catch (Exception ex)
                {
                    loadingForm.Invoke(new Action(() => loadingForm.Close()));
                    MessageBox.Show(
                      $"Cannot import file data from the file to database. \n\n Error: \n {ex.Message}",
                      "Error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error
                    );
                }
            }
        }

        private async Task InsertRecordsAsync(List<Record> records)
        {
            try
            {
                await _context.MyEntities.AddRangeAsync(records);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                  $"Cannot save data. \n\n Error: \n {ex.Message}",
                  "Error",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
                );

            }
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            // TODO async
            List<Record> records = GetFilteredRecords();

            //TODO filter records

            // Show SaveFileDialog to let the user choose the file path and name
            SaveFileDialog saveFileDialog = radioButtonCsv.Checked
                ? new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    Title = "Save an Export CSV File",
                    DefaultExt = "csv",
                    FileName = "records_export.csv"
                }
                : new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Save an Export File",
                    DefaultExt = "xlsx",
                    FileName = "records_export.xlsx"
                };

            var showsaveDialog = saveFileDialog.ShowDialog();

            // Show the dialog and handle the file selection
            // CSV format
            if (showsaveDialog == DialogResult.OK && radioButtonCsv.Checked)
            {
                var filePath = saveFileDialog.FileName;

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    await csv.WriteRecordsAsync(records);
                }

                // Show MessageBox to the user
                var result = MessageBox.Show(
                    $"Data exported successfully to {filePath}. Do you want to open the created file?",
                    "File Created",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                );

                // Check user's response
                if (result == DialogResult.OK)
                {
                    // Open the file in the default application
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            // Excel format
            else if (showsaveDialog == DialogResult.OK)
            {
                {
                    var filePath = saveFileDialog.FileName;

                    // Create a new workbook and worksheet
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Records");

                        // Add header row
                        worksheet.Cell(1, 1).Value = "Id";
                        worksheet.Cell(1, 2).Value = "Name";
                        worksheet.Cell(1, 3).Value = "Surname";
                        worksheet.Cell(1, 4).Value = "Date";
                        worksheet.Cell(1, 5).Value = "City";
                        worksheet.Cell(1, 6).Value = "Country";

                        // Add data rows
                        for (int i = 0; i < records.Count; i++)
                        {
                            var record = records[i];
                            worksheet.Cell(i + 2, 1).Value = record.Id.ToString();
                            worksheet.Cell(i + 2, 2).Value = record.Name;
                            worksheet.Cell(i + 2, 3).Value = record.Surname;
                            worksheet.Cell(i + 2, 4).Value = record.Date;
                            worksheet.Cell(i + 2, 5).Value = record.City;
                            worksheet.Cell(i + 2, 6).Value = record.Country;
                        }

                        // Save the workbook
                        workbook.SaveAs(filePath);
                    }

                    // Show MessageBox to the user
                    var result = MessageBox.Show(
                        $"Data exported successfully to {filePath}. Do you want to open the created file?",
                        "File Created",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information
                    );

                    // Check user's response
                    if (result == DialogResult.OK)
                    {
                        // Open the file in the default application
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                }
            }

        }


        private List<Record> GetFilteredRecords()
        {

            string dateString = textBoxDate.Text;
            DateTime filterDate;
            try
            {
                DateTime.TryParse(dateString, out filterDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect Date value");
                throw ex;
            }

            var criteria = new FilterCriteria
            {
                Name = textBoxName.Text,
                Surname = textBoxSurname.Text,
                Date = filterDate,
                City = textBoxCity.Text,
                Country = textBoxCountry.Text,

            };

            List<Record> records = _context.GetFilteredEntities(criteria).ToList();
            return records;
        }
        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            radioButtonCsv.Checked = true;

            foreach (Control control in groupBox.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = null;
            }
        }

        private void textBoxDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Store the last key pressed
            _lastKeyPressed = e.KeyChar;

            // Allow only digits and '-' characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true; // Ignore the key press
            }
        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {
            string datePattern = @"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-(19|20)\d\d$";
            string input = textBoxDate.Text;

            if (input.Length == 0 || _lastKeyPressed == (char)Keys.Back) return;

            // Remove non-digit and non-hyphen characters

            // Format the text to dd-MM-yyyy
            if (input.Length == 2)
            {
                input = input.Insert(2, "-");
            }
            if (input.Length == 5)
            {
                input = input.Insert(5, "-");
            }

            // Ensure the text doesn't exceed the desired length
            if (input.Length > 10)
            {
                input = input.Substring(0, 10);
            }

            if (input.Length == 10)
            {
                if (Regex.IsMatch(input, datePattern))
                {
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                    {
                        MessageBox.Show("Valid date: " + parsedDate.ToString("dd-MM-yyyy"));
                    }
                    else
                    {
                        MessageBox.Show("Invalid date. Please enter a valid date in dd-MM-yyyy format.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid format. Please use dd-MM-yyyy format.");
                    // input = textBoxDate.Text.Substring(0, input.Length - 1);
                }
            }

            textBoxDate.Text = input;
            textBoxDate.SelectionStart = textBoxDate.Text.Length; // Move cursor to the end
        }

        private async void buttonDeleteDbData_Click(object sender, EventArgs e)
        {
            // Show MessageBox to the user
            var result = MessageBox.Show(
                $"Are you sure you want to delete all records in DB?",
                "Delete all DB records",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            // Check user's response
            if (result == DialogResult.OK)
            {
                // Delete all data from MyEntities table
                var sql = $"DELETE FROM {_db}";
                await _context.Database.ExecuteSqlRawAsync(sql);

                CheckNumOfDBRecords();

            }
        }
    }

}
