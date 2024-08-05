using CsvHelper;
using CsvHelper.Configuration;
using CsvWinFormsApp.Contexts;
using CsvWinFormsApp.Models;
using System.Globalization;

namespace CsvWinFormsApp.Utilities
{
    internal static class ImportHelper
    {
        public static async void ImportCsvData(Form form, MyContext _context, Action callback)
        {
            form.Invoke((MethodInvoker)(async () =>
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
                        int BatchSize = 2500;
                        int addedRecords = 0;
                        var records = new List<Record>();
                        LoadingMessageForm loadingForm = new LoadingMessageForm("Trying to connect to specified Database...", "Import data to DB");
                        loadingForm.Show();

                        // Create a configuration with a semicolon separator
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ";", // Specify the separator here
                            HasHeaderRecord = true, // Set to false if your CSV does not have a header
                        };

                        try
                        {
                            using (var reader = new StreamReader(selectedFilePath))
                            using (var csv = new CsvReader(reader, config))
                            {
                                // Starting to read the first row
                                csv.Read();
                                csv.ReadHeader();

                                // Continur file reading
                                while (csv.Read())
                                {

                                    var record = new Record
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = csv.GetField<string>("FirstName"),
                                        SecondName = csv.GetField<string>("SecondName"),
                                        Surname = csv.GetField<string>("SurName"),
                                        Date = csv.GetField<DateTime>("Date"),
                                        City = csv.GetField<string>("City"),
                                        Country = csv.GetField<string>("Country")
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
                                MessagesHelper.ShowInfoMessage($"Inserted {addedRecords} records!");
                                callback();
                            }
                        }
                        catch (Exception ex)
                        {
                            loadingForm.Invoke(new Action(() => loadingForm.Close()));
                            MessagesHelper.ShowErrorMessage($"Cannot import file data from the file to database. \n\n Error: \n {ex.Message}");
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
                MessagesHelper.ShowErrorMessage($"Cannot save data. \n\n Error: \n {ex.Message}");
            }
        }
    }
}
