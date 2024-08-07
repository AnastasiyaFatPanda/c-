using System.Text.RegularExpressions;
using CsvWinFormsApp.Models;
using CsvWinFormsApp.Contexts;
using CsvWinFormsApp.Utilities;
using CsvWinFormsApp.Enums;

namespace CsvWinFormsApp
{
    public partial class MainForm : Form
    {
        private MyContext _context;
        private char _lastDateKeyPressed;
        private string _datePattern = @"^(19|20)[0-9][0-9]-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";

        public MainForm(MyContext context)
        {
            InitializeComponent();
            _context = context;
            UpdateFormComponents();
        }

        // Override the Dispose method
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            _context?.Dispose();

            base.Dispose(disposing);
        }

        public int UpdateFormComponents()
        {
            List<Record> entities = _context.MyEntities.ToList();
            int numberOfRecords = entities.Count;

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
            ClearFilters();
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
            try
            {
                List<Record> records = GetFilteredRecords();

                if (records.Count == 0)
                {
                    MessagesHelper.ShowWarningMessage("There is no data to export.");
                    return;
                }

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
                if (showsaveDialog == DialogResult.OK)
                {
                    ExportHelper.GenerateFile(exportFormat, filePath, records);
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowErrorMessage($"Incorrect filter request. \n\n Error: {ex.Message}");
            }
        }

        private List<Record> GetFilteredRecords()
        {
            string dateString = textBoxDate.Text;
            DateTime filterDate;

            try
            {
                if (!Regex.IsMatch(dateString, _datePattern) && !string.IsNullOrEmpty(dateString))
                {
                    throw new Exception("Incorrect Date value format");
                }
                DateTime.TryParse(dateString, out filterDate);
            }
            catch (Exception ex)
            {
                textBoxDate.BackColor = ColorTranslator.FromHtml("#ffbc98");
                throw ex;
            }

            textBoxDate.BackColor = Color.FloralWhite;
            var criteria = new FilterCriteria
            {
                Name = textBoxName.Text,
                SecondName = textBoxSecondName.Text,
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
            ClearFilters();
        }

        private void ClearFilters()
        {
            radioButtonCsv.Checked = true;

            foreach (Control control in groupBox.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = null;
            }
        }

        private void buttonDeleteDbData_Click(object sender, EventArgs e)
        {
            DatabaseHelper.DeleteAllDatabaseData(_context, () => UpdateFormComponents());
        }

        private void textBoxDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Store the last key pressed
            _lastDateKeyPressed = e.KeyChar;

            // Allow only digits, backspace characters
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignore the key press
            }
        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {
            string input = textBoxDate.Text;

            if (input.Length == 0 || _lastDateKeyPressed == (char)Keys.Back) return;

            // Format the text to yyyy-MM-dd
            if (input.Length == 4)
            {
                input = input.Insert(4, "-");
            }
            if (input.Length == 7)
            {
                input = input.Insert(7, "-");
            }

            // Ensure the text doesn't exceed the desired length
            if (input.Length > 10)
            {
                input = input.Substring(0, 10);
            }

            if (input.Length == 10 && !Regex.IsMatch(input, _datePattern))
            {
                MessagesHelper.ShowErrorMessage("Invalid format. Please use yyyy-MM-dd format.");
            }

            textBoxDate.Text = input;
            // Move cursor to the end
            textBoxDate.SelectionStart = textBoxDate.Text.Length;
        }

        private bool TextBoxAllowedKeyPress(KeyPressEventArgs e)
        {
            return !char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space;
        }

        private void TextBoxCheckMaxLength(TextBox textBox)
        {
            const int maxLength = 100;

            if (textBox.Text.Length > maxLength)
            {
                textBox.Text = textBox.Text.Substring(0, maxLength);
                textBox.SelectionStart = maxLength;
                MessagesHelper.ShowErrorMessage("You cannot enter a string longer than 100 characters!");
            }
        }

        private void CheckTextBoxValue(KeyPressEventArgs e, TextBox textBox)
        {
            // Allow only letters, space and backspace
            if (TextBoxAllowedKeyPress(e))
            {
                e.Handled = true;
            }

            TextBoxCheckMaxLength(textBox);
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckTextBoxValue(e, textBoxName);
        }

        private void textBoxSecondName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckTextBoxValue(e, textBoxSecondName);
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckTextBoxValue(e, textBoxSurname);
        }

        private void textBoxCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckTextBoxValue(e, textBoxCity);
        }

        private void textBoxCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckTextBoxValue(e, textBoxCountry);
        }
    }
}
