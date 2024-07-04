using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CSVReaderFormsApp
{
    public partial class FormDataReader : Form
    {
        private SqlServerConnection sqlServerConnection;
        private BackgroundWorker bgWorker;
        public FormDataReader()
        {
            InitializeComponent();

            progressBar.Visible = true;
            labelNoDataInDb.Visible = false;
            // Initialize background worker
          //  bgWorker = new BackgroundWorker();
          //  bgWorker.WorkerReportsProgress = true;
         //   bgWorker.DoWork += BgWorker_DoWork;
         //   bgWorker.ProgressChanged += BgWorker_ProgressChanged;
         //   bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;

            // Initialize the SqlServerConnection
            sqlServerConnection = new SqlServerConnection();

            // Open the SQL Server connection
             sqlServerConnection.OpenConnection();

            // Add the FormClosed event handler
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);


            // Example of executing a query
            string sqlTable = $"{sqlServerConnection.schema}.dummy";
            string selectQuery = $"SELECT * FROM {sqlTable}";
            DataTable result = sqlServerConnection.ExecuteQuery(selectQuery);
            string dbData = "";
            foreach (DataRow row in result.Rows)
            {
                dbData += $"{row["name"]} {row["surname"]} \n";
            }
            MessageBox.Show(dbData);



            // Example of executing a command
            // string insertCommand = "INSERT INTO YourTable (ColumnName) VALUES ('Value')";
            // int rowsAffected = sqlServerConnection.ExecuteCommand(insertCommand);
            // Console.WriteLine($"Rows affected: {rowsAffected}");
        }

        // Event handler for FormClosed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Perform any cleanup or actions needed when the form is closed
            sqlServerConnection.CloseConnection();
            MessageBox.Show("Connection closed and form is closed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file reader
            openFileDialog.ShowDialog(this);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxFilter_Enter(object sender, EventArgs e)
        {

        }

        private void FormDataReader_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Check if background worker is already running
            if (!bgWorker.IsBusy)
            {
                // Start the background worker
                bgWorker.RunWorkerAsync();
            }
        }

      /**  private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Perform database connection here
            try
            {
                // Simulate connection progress
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(50); // Simulate work
                    bgWorker.ReportProgress(i); // Update progress
                }

                // Open the SqlConnection
                sqlServerConnection.OpenConnection();

                // Perform other database operations as needed
                // Example: Execute SQL commands, fetch data, etc.

                // Optional: You can return any result to the Completed event handler
                e.Result = "Connection successful";
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                e.Result = ex.Message;
            }
            finally
            {
                // Ensure the connection is closed when done
                sqlServerConnection.CloseConnection();
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            // Update the progress bar with the percentage value
            progressBar.Value = e.ProgressPercentage;
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Handle the completion of the background worker
            if (e.Error != null)
            {
                // Display error message
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Handle cancellation if needed
            }
            else
            {
                // Display success message or process any result
                MessageBox.Show(e.Result.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      **/
    }
}
