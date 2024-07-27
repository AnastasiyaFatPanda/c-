using CsvWinFormsApp.Contexts;
using CsvWinFormsApp.Utilities;

namespace CsvWinFormsApp
{
    internal static class Program
    {
        private static MyContext _context;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            LoadingForm loadingForm = new LoadingForm("Trying to connect ot specified Database...", "Connecting to DB");

            // Run the main form initialization asynchronously
            Task.Run(() =>
            {
                CreateDbContext();

                // Simulate delay for LoadingForm demonstration
                Task.Delay(1000).Wait();
                // Once the main form is initialized, close the loading form
                loadingForm.Invoke(new Action(() => loadingForm.Close()));
            });

            // Show the loading form modally (this blocks the main thread until the form is closed)
            loadingForm.ShowDialog();

            // Initialize and run the main form after the loading form is closed
            MainForm mainForm = new MainForm(_context);
            Application.Run(mainForm);
        }

        private static void CreateDbContext()
        {
            string connectionString = ConfigurationHelper.GetConnectionString();
            _context = new MyContext(connectionString);
        }
    }
}