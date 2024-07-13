using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVReaderFormsApp.Models;
using Microsoft.Extensions.Configuration;

namespace CSVReaderFormsApp.Services
{
    internal class ConfigService
    {
        private string jsonFilePath = "appsettings.json";
        private Config config;

        public ConfigService()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile(jsonFilePath);
            var configuration = builder.Build();

            config = new Config(configuration);

        }
        public string GetConnectionString()
        {
            return config.GetConnectionString();
        }

        public string GetSchema()
        {
            return config.schema;
        }
        public string GetDB()
        {
            return config.database;
        }
        public string GetTable()
        {
            return config.table;
        }

    }
}
