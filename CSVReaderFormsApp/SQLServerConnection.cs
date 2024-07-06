using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;

public class SqlServerConnection
{
    private string connectionString;
    private string schema;
    public string database;
    public string sqlTableName;
    private SqlConnection connection;
    private bool isConnectionOpened = false;

    public SqlServerConnection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var databaseSettings = configuration.GetSection("DatabaseSettings");
        var host = databaseSettings["Host"];
        schema = databaseSettings["Schema"];
        database = databaseSettings["Database"];
        sqlTableName = $"{schema}.{database}";
        var initialCatalog = databaseSettings["InitialCatalog"];
        var user = databaseSettings["User"];
        var password = databaseSettings["Password"];
        var encrypt = bool.Parse(databaseSettings["Encrypt"]);
        var trustServerCertificate = bool.Parse(databaseSettings["TrustServerCertificate"]);

        connectionString = $"Server={host};Initial Catalog={initialCatalog};User Id={user};Password={password};Encrypt={encrypt};TrustServerCertificate={trustServerCertificate};";
        // MessageBox.Show(connectionString);
    }

    // Method to open the connection
    public void OpenConnection()
    {
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            isConnectionOpened = true;
            MessageBox.Show("Connection opened successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while opening the connection: {ex.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Method to close the connection
    public void CloseConnection(string message = "Connection closed successfully.")
    {
        try
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                isConnectionOpened = false;
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while closing the connection: {ex.Message}");
        }
    }

    // Method to execute a query (SELECT statement)
    public DataTable ExecuteQuery(string query)
    {
        DataTable dataTable = new DataTable();
        if (isConnectionOpened)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while executing the query: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Open DB connection first!");
        }
        return dataTable;
    }

    // Example of executing a command
    // string insertCommand = "INSERT INTO YourTable (ColumnName) VALUES ('Value')";
    // int rowsAffected = sqlServerConnection.ExecuteCommand(insertCommand);
    // Console.WriteLine($"Rows affected: {rowsAffected}");

    // Method to execute a command (INSERT, UPDATE, DELETE)
    public int ExecuteCommand(string commandText)
    {
        int affectedRows = 0;
        if (isConnectionOpened)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while executing the command: {ex.Message}");
            }

            MessageBox.Show($"Rows affected: {affectedRows}");
        }
        else
        {
            MessageBox.Show("Open DB connection first!");
        }

        return affectedRows;
    }

    public int CheckIfTableHasData()
    {
        // Use a try-catch block to handle potential exceptions
        try
        {
            string resColumnName = "countedRecords";
            string query = $"SELECT COUNT(*) as {resColumnName} FROM {sqlTableName}";

            SqlCommand command = new SqlCommand(query, connection);
            // Execute the query and get the result
            int rowCount = (int)command.ExecuteScalar();

            // MessageBox.Show(rowCount.ToString());
            // Return the row count
            return rowCount;
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log it)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return 0;
        }
    }


}
