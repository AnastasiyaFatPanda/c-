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

            // Initialize the SqlServerConnection
            sqlServerConnection = new SqlServerConnection();

            // Open the SQL Server connection
            sqlServerConnection.OpenConnection();

            int numOfRecordsDb = sqlServerConnection.CheckIfTableHasData();
            bool tableHasData = numOfRecordsDb > 0;
            if (tableHasData)
            {
                // progressBar.Visible = true;
                labelNoDataInDb.Visible = false;
                labelNumRecords.Visible = true;
                labelNumRecords.Text = numOfRecordsDb == 1 
                    ? "Database contains one record"
                    : $"Database contains {numOfRecordsDb} records";

                groupBoxFilter.Enabled = true;
                groupBoxRadioButtons.Enabled = true;
                buttonExport.Enabled = true;

            }
            // Add the FormClosed event handler
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        // Event handler for FormClosed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sqlServerConnection.CloseConnection("Connection closed and form is closed.");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            // open file reader
            // openFileDialog.ShowDialog(this);

            // Create and configure the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\", // Default directory
                Title = "Browse Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            // Show the dialog and handle the file selection
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path
                string selectedFilePath = openFileDialog.FileName;

                // Read the file or do something with the file path
                string fileContent = System.IO.File.ReadAllText(selectedFilePath);
                 MessageBox.Show(fileContent, "File Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
    }
}
