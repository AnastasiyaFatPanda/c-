using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvWinFormsApp.Models
{
    // if we know the name of table and schema:
    //   [Table("dummy", Schema = "education")]
    // otherwise, set it up in MyContext
    public class Record
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname} {Date} {City} {Country}";
        }
    }

}
