namespace CsvWinFormsApp
{
    public partial class LoadingMessageForm : Form
    {
        public LoadingMessageForm(string message, string title)
        {
            InitializeComponent();
            SetupForm(message, title);
        }

        private void SetupForm(string message, string title)
        {
            // Configure the label
            labelMessage.Text = message;
            labelMessage.AutoSize = true;

            // Configure the progress bar
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            // Configure the form
            Text = title;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false; // Disable the close button
        }

        // Method to update the message label
        public void UpdateMessage(string newMessage)
        {
            labelMessage.Text = newMessage;
        }

    }
}
