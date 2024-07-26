using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Configuration;


namespace CsvWinFormsApp.Utilities
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }
        private static IConfigurationSection _configurationDatabaseInfo { get; set; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\jagfd\\Documents\\GitHub\\CsvWinFormsApp\\CsvWinFormsApp")
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
                var result = MessageBox.Show(
                   "Cannot run the app. Please, check your appsettings.json file",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
                 );

                // Close the app after the error message
                if (result != DialogResult.None)
                {
                    // Exit the application with an error code
                    Environment.Exit(1);
                }
            }

            return $"{catalogName}.{schemaName}.{databaseName}";
        }
    }

}
