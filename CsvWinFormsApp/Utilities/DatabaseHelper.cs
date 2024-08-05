using CsvWinFormsApp.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CsvWinFormsApp.Utilities
{
    internal static class DatabaseHelper
    {
        public static async void DeleteAllDatabaseData(MyContext _context, Action callback)
        {
            string db = ConfigurationHelper.GetFullDatabaseName();
            
            var result = MessagesHelper.ShowWarningMessage("Are you sure you want to delete all records in DB?", MessageBoxButtons.OKCancel);

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
