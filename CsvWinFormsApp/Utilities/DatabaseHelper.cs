using CsvWinFormsApp.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CsvWinFormsApp.Utilities
{
    internal static class DatabaseHelper
    {
        public static async void DeleteAllDatabaseData(MyContext _context, Action callback)
        {
           string db = ConfigurationHelper.GetFullDatabaseName();
            // Show MessageBox to the user
            var result = MessageBox.Show(
                $"Are you sure you want to delete all records in DB?",
                "Delete all DB records",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            // Check user's response
            if (result == DialogResult.OK)
            {
                // Delete all data from MyEntities table
                var sql = $"DELETE FROM {db}";
                await _context.Database.ExecuteSqlRawAsync(sql);
                callback();
            }
        }
    }
}
