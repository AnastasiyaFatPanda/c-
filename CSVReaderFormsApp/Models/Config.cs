using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CSVReaderFormsApp.Models
{
    public class Config
    {
        public string host;
        public string schema;
        public string database;
        public string table;
        public string initialCatalog;
        public string user;
        public string password;
        public bool encrypt = true;
        public bool trustServerCertificate = true;

        public Config(IConfigurationRoot configurationRoot)
        {
            IConfigurationSection configuration = configurationRoot.GetSection("DatabaseSettings");
            host = configuration["Host"];
            schema = configuration["Schema"];
            database = configuration["Database"];
            table = $"{schema}.{database}";
            initialCatalog = configuration["InitialCatalog"];
            user = configuration["User"];
            password = configuration["Password"];
            encrypt = bool.Parse(configuration["Encrypt"]);
            trustServerCertificate = bool.Parse(configuration["TrustServerCertificate"]);
        }

        public string GetConnectionString()
        {
            string connectionString = $"Server={host};Initial Catalog={initialCatalog};User Id={user};Password={password};Encrypt={encrypt};TrustServerCertificate={trustServerCertificate};";
            return connectionString;
        }
    }

}
