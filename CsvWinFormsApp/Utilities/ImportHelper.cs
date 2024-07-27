using CsvHelper;
using CsvWinFormsApp.Contexts;
using CsvWinFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvWinFormsApp.Utilities
{
    internal static class ImportHelper
    {
        public static async void ImportCsvData(Form form, MyContext _context, Action callback)
        {
            // Create and configure the OpenFileDialog
            OpenFileDialog openFileDialog = null;
            form.Invoke((MethodInvoker)(async () =>
                {  // Create and configure the OpenFileDialog
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
                        int BatchSize = 1000;
                        int addedRecords = 0;
                        var records = new List<Record>();
                        LoadingMessageForm loadingForm = new LoadingMessageForm("Trying to connect to specified Database...", "Import data to DB");
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
                                        await InsertRecordsAsync(records, _context);
                                        addedRecords += BatchSize;
                                        // Simulate some work
                                        await Task.Delay(1000); // Wait for 1 second before processing the next batch
                                        records.Clear();
                                    }
                                }

                                if (records.Count() > 0)
                                {
                                    addedRecords += records.Count;
                                    await InsertRecordsAsync(records, _context);
                                }

                                // Close the loading form
                                loadingForm.Invoke(new Action(() => loadingForm.Close()));
                                // Show success message
                                MessageBox.Show($"Inserted {addedRecords} records!");
                                callback();
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
                })
            );
        }

        private static async Task InsertRecordsAsync(List<Record> records, MyContext _context)
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
    }
}
