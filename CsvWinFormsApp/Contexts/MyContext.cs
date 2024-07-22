using CsvWinFormsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CsvWinFormsApp.Contexts
{
    public class MyContext : DbContext
    {
        public DbSet<Record> MyEntities { get; set; }

        private readonly string _connectionString;

        public MyContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseSqlServer(_connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3, // The maximum number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(3), // The maximum delay between retries
                    errorNumbersToAdd: null // Additional error numbers to consider transient
                );
            });
        }

        public IQueryable<Record> GetFilteredEntities(FilterCriteria criteria)
        {
            var query = MyEntities.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(e => e.Name.Contains(criteria.Name));
            }

            if (!string.IsNullOrEmpty(criteria.Surname))
            {
                query = query.Where(e => e.Surname.Contains(criteria.Surname));
            }

            if (criteria.Date.HasValue)
            {
                query = query.Where(e => e.Date > criteria.Date.Value);
            }

            if (!string.IsNullOrEmpty(criteria.City))
            {
                query = query.Where(e => e.City == criteria.City);
            }

            if (!string.IsNullOrEmpty(criteria.Country))
            {
                query = query.Where(e => e.Country == criteria.Country);
            }

            return query;
        }
    }

}
