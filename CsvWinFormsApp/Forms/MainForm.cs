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
using CsvWinFormsApp.Models;
using CsvWinFormsApp.Contexts;
using CsvWinFormsApp.Utilities;
using System.Xml.Linq;
using CsvWinFormsApp.Enums;

namespace CsvWinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly MyContext _context;
        private char _lastKeyPressed;

        public MainForm()
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

                // Initialize DbContext
                _context = new MyContext(connectionString);
                UpdateFormComponents();

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


        public int UpdateFormComponents()
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
                if (control.GetType() == typeof(TextBox))
                    control.BackColor = enable ? Color.FloralWhite : Color.LightGray;
                else if (control.GetType() == typeof(Button))
                    control.BackColor = enable ? ColorTranslator.FromHtml("#e1d7b3") : Color.LightGray;
            }

            if (enable)
            {
                buttonDeleteDbData.BackColor = ColorTranslator.FromHtml("#e1d2dc");
                buttonDeleteDbData.ForeColor = ColorTranslator.FromHtml("#9a4f4f");
            }
        }

        private async void buttonImport_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                ImportHelper.ImportCsvData(this, _context, () => UpdateFormComponents());
            });

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            List<Record> records = GetFilteredRecords();

            ExportEnum exportFormat = radioButtonCsv.Checked
                ? ExportEnum.CSV
                : radioButtonExcel.Checked
                    ? ExportEnum.EXCEL
                    : ExportEnum.XML;
            // Show SaveFileDialog to let the user choose the file path and name
            SaveFileDialog saveFileDialog = ExportHelper.CreateSaveFileDialog(exportFormat);

            DialogResult showsaveDialog = saveFileDialog.ShowDialog();
            string filePath = saveFileDialog.FileName;

            // Show the dialog and handle the file selection
            // CSV format
            if (showsaveDialog == DialogResult.OK)
            {
                ExportHelper.GenerateFile(exportFormat, filePath, records);
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

            // Allow only digits, backspace characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the key press
            }
        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {
            string datePattern = @"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-(19|20)\d\d$";
            string input = textBoxDate.Text;

            if (input.Length == 0 || _lastKeyPressed == (char)Keys.Back) return;

            // Format the text to dd-MM-yyyy
            if (input.Length == 2)
            {
                input = input.Insert(2, "/");
            }
            if (input.Length == 5)
            {
                input = input.Insert(5, "/");
            }

            // Ensure the text doesn't exceed the desired length
            if (input.Length > 10)
            {
                input = input.Substring(0, 10);
            }

            if (input.Length == 10 && !Regex.IsMatch(input, datePattern))
            {
                MessageBox.Show("Invalid format. Please use dd/MM/yyyy format.");
            }

            textBoxDate.Text = input;
            textBoxDate.SelectionStart = textBoxDate.Text.Length; // Move cursor to the end
        }

        private async void buttonDeleteDbData_Click(object sender, EventArgs e)
        {
            DatabaseHelper.DeleteAllDatabaseData(_context);
            UpdateFormComponents();
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, backspace characters
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true; // Ignore the key press
            }
        }
    }
}
