namespace CsvWinFormsApp
{
    partial class LoadingMessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelMessage = new Label();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Location = new Point(28, 27);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(50, 20);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "label1";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(28, 66);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(529, 29);
            progressBar.TabIndex = 1;
            // 
            // LoadingMessageForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 136);
            Controls.Add(progressBar);
            Controls.Add(labelMessage);
            Name = "LoadingMessageForm";
            Text = "Loading...";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMessage;
        private ProgressBar progressBar;
    }
}