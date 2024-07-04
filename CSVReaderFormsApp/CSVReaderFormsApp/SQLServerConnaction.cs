using System;
using System.Data;
using Microsoft.Data.SqlClient;

public class SqlServerConnection
{
    private readonly string connectionString;
    private SqlConnection connection;

    // Constructor to initialize the connection string
    public SqlServerConnection(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Method to open the connection
    public void OpenConnection()
    {
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connection opened successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while opening the connection: {ex.Message}");
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
                Console.WriteLine("Connection closed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while closing the connection: {ex.Message}");
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
            Console.WriteLine($"An error occurred while executing the query: {ex.Message}");
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
            Console.WriteLine($"An error occurred while executing the command: {ex.Message}");
        }
        return affectedRows;
    }
}
