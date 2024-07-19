using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvWinFormsApp
{
    [Table("dummy", Schema = "education")]
    public class MyEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime date { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public override string ToString()
        {
            return $"{name} {surname} {date} {city} {country}";
        }
    }

}
