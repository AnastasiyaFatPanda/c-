using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvWinFormsApp
{
    public class FilterCriteria
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? Date { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
