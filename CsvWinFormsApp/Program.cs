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

            try
            {
                LoadingMessageForm loadingForm = new LoadingMessageForm("Trying to connect ot specified Database...", "Connecting to DB");
                string connectionString = ConfigurationHelper.GetConnectionString();

                Task.Run(() =>
                {
                    _context = new MyContext(connectionString);
                    bool isAvalaible = _context.Database.CanConnect();

                    if (isAvalaible)
                    {
                        loadingForm.Invoke(new Action(() => loadingForm.Close()));
                    }
                    else
                    {
                        _context?.Dispose();
                        ConfigurationHelper.HandleErrorsAndExit();
                    }
                });

                // Show the loading form modally (this blocks the main thread until the form is closed)
                loadingForm.ShowDialog();

                MainForm mainForm = new MainForm(_context);
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                ConfigurationHelper.HandleErrorsAndExit();
            }
        }
    }
}