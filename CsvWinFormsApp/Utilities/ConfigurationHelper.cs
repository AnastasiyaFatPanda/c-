using Microsoft.Extensions.Configuration;

namespace CsvWinFormsApp.Utilities
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }
        private static IConfigurationSection _configurationDatabaseInfo { get; set; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            _configurationDatabaseInfo = Configuration.GetSection("DatabaseInfo");
        }

        public static string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public static string GetTableName()
        {
            return _configurationDatabaseInfo["DB"];
        }

        public static string GetSchemaName()
        {

            return _configurationDatabaseInfo["Schema"];
        }

        public static string GetFullDatabaseName()
        {
            string catalogName = _configurationDatabaseInfo["Catalog"];
            string schemaName = _configurationDatabaseInfo["Schema"];
            string databaseName = _configurationDatabaseInfo["DB"];

            if (catalogName == null || schemaName == null || databaseName == null)
            {
                HandleErrorsAndExit();
            }

            return $"{catalogName}.{schemaName}.{databaseName}";
        }

        public static void HandleErrorsAndExit(Exception ex = null)
        {
            var result = MessagesHelper.ShowErrorMessage($"Cannot run the app. Please, check your appsettings.json file.\n {ex?.Message}");

            // Close the app after the error message
            if (result != DialogResult.None)
            {
                // Exit the application with an error code
                Environment.Exit(1);
            }
        }
    }

}
