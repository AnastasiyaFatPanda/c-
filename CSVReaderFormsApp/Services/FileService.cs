using System.Text;

namespace CSVReaderFormsApp.Services
{
    public class FileService
    {
        public void ReadFile(string selectedFile)
        {
            using (var reader = new StreamReader(selectedFile))
            // using (var reader = new StreamReader(@"C:\test.csv"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }
        }

        public string InsertCsvDataIntoDatabase(string csvFilePath, string tableName/*, string connectionString*/)
        {
            // Read the CSV file
            var lines = File.ReadAllLines(csvFilePath);

            if (lines.Length < 2)
            {
                Console.WriteLine("CSV file is empty or does not have enough data.");
                return null;
            }

            // Get the column names from the first line
            var columns = lines[0].Split(',');

            // Initialize a StringBuilder to hold the SQL INSERT statements
            var sb = new StringBuilder();

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                // Build the INSERT statement
                var sql = BuildInsertStatement(tableName, columns, values);
                sb.AppendLine(sql);
            }

            MessageBox.Show(sb.ToString());

            return sb.ToString();
            // Execute the SQL INSERT statements
            //ExecuteInsertStatements(sb.ToString(), connectionString);
        }

        private static string BuildInsertStatement(string tableName, string[] columns, string[] values)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO {0} (", tableName);

            for (int i = 0; i < columns.Length; i++)
            {
                sb.Append(columns[i]);
                if (i < columns.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(") VALUES (");

            for (int i = 0; i < values.Length; i++)
            {
                sb.AppendFormat("'{0}'", values[i].Replace("'", "''")); // Handle single quotes in values
                if (i < values.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(");");

            return sb.ToString();
        }
    }
}
