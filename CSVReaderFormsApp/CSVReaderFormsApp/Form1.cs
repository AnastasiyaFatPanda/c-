namespace CSVReaderFormsApp
{
    public partial class FormDataReader : Form
    {
        public FormDataReader()
        {
            InitializeComponent();
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
    }
}
