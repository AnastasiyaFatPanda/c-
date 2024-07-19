using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using System.IO;


namespace CsvWinFormsApp
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static ConfigurationHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\jagfd\\source\\repos\\CsvWinFormsApp\\CsvWinFormsApp")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }




        public static string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }
    }

}
