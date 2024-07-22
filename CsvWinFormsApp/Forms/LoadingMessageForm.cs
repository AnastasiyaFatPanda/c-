using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false; // Disable the close button
        }

        // Method to update the message label
        public void UpdateMessage(string newMessage)
        {
            labelMessage.Text = newMessage;
        }

    }
}
