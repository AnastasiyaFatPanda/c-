using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReaderFormsApp.Models
{
    internal class DatabaseData
    {
        public readonly Random id = new Random();
        public string name;
        public string surname;
        public string date;
        public string country;
        public string city;
    }
}
