using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CsvWinFormsApp
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
                    maxRetryCount: 5, // The maximum number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(10), // The maximum delay between retries
                    errorNumbersToAdd: null // Additional error numbers to consider transient
                );
            });
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.HasDefaultSchema("education");
        //}
    }

}
