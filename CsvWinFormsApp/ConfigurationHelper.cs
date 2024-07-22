using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Configuration;


namespace CsvWinFormsApp
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\jagfd\\Documents\\GitHub\\CsvWinFormsApp\\CsvWinFormsApp")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public static string GetDatabaseName()
        {
            IConfigurationSection configurationDatabaseInfo = Configuration.GetSection("DatabaseInfo");
            string catalogName = configurationDatabaseInfo["Catalog"];
            string schemaName = configurationDatabaseInfo["Schema"];
            string databaseName = configurationDatabaseInfo["DB"];

            if (catalogName == null || schemaName == null || databaseName == null)
            {
                var result = MessageBox.Show(
                 "Cannot run the app. Please, check your appsettings.json file",
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

            string db = $"{catalogName}.{schemaName}.{databaseName}";
            return $"{catalogName}.{schemaName}.{databaseName}";
        }
    }

}
