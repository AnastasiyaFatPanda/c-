using System;
using System.Data;
using Microsoft.Data.SqlClient; // Ensure the correct namespace for SqlClient
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics;

public class SqlServerConnection
{
    private readonly string connectionString;
    public readonly string schema;
    private SqlConnection connection;

    // Constructor to initialize the connection string from configuration
    public SqlServerConnection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var databaseSettings = configuration.GetSection("DatabaseSettings");
        var host = databaseSettings["Host"];
        schema = databaseSettings["Schema"] ?? "education";
        var database = databaseSettings["Database"];
        var user = databaseSettings["User"];
        var password = databaseSettings["Password"];
        var encrypt = bool.Parse(databaseSettings["Encrypt"]);
        var trustServerCertificate = bool.Parse(databaseSettings["TrustServerCertificate"]);

        connectionString = $"Server={host};Initial Catalog={database};User Id={user};Password={password};Encrypt={encrypt};TrustServerCertificate={trustServerCertificate};";
        MessageBox.Show(connectionString);
    }

    // Method to open the connection
    public void OpenConnection()
    {
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            MessageBox.Show("Connection opened successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while opening the connection: {ex.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Method to close the connection
    public void CloseConnection()
    {
        try
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Connection closed successfully.");
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
        return dataTable;
    }

    // Method to execute a command (INSERT, UPDATE, DELETE)
    public int ExecuteCommand(string commandText)
    {
        int affectedRows = 0;
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
        return affectedRows;
    }
}
