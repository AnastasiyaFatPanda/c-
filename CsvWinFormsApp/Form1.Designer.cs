namespace CsvWinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            buttonImport = new Button();
            buttonExport = new Button();
            radioButtonCsv = new RadioButton();
            radioButtonExcel = new RadioButton();
            textBoxName = new TextBox();
            textBoxSurname = new TextBox();
            textBoxDate = new TextBox();
            textBoxCity = new TextBox();
            textBoxCountry = new TextBox();
            labelDbInfo = new Label();
            groupBox = new GroupBox();
            buttonDeleteDbData = new Button();
            buttonClearFilters = new Button();
            progressBar1 = new ProgressBar();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 384);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // buttonImport
            // 
            buttonImport.BackColor = Color.Thistle;
            buttonImport.ForeColor = SystemColors.GrayText;
            buttonImport.Location = new Point(12, 12);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(457, 42);
            buttonImport.TabIndex = 1;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = false;
            buttonImport.Click += buttonImport_Click;
            // 
            // buttonExport
            // 
            buttonExport.BackColor = Color.Thistle;
            buttonExport.ForeColor = SystemColors.GrayText;
            buttonExport.Location = new Point(259, 172);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(175, 51);
            buttonExport.TabIndex = 2;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = false;
            buttonExport.Click += buttonExport_Click;
            // 
            // radioButtonCsv
            // 
            radioButtonCsv.AutoSize = true;
            radioButtonCsv.Checked = true;
            radioButtonCsv.ForeColor = SystemColors.ControlDarkDark;
            radioButtonCsv.Location = new Point(271, 26);
            radioButtonCsv.Name = "radioButtonCsv";
            radioButtonCsv.Size = new Size(56, 24);
            radioButtonCsv.TabIndex = 3;
            radioButtonCsv.TabStop = true;
            radioButtonCsv.Text = "CSV";
            radioButtonCsv.UseVisualStyleBackColor = true;
            // 
            // radioButtonExcel
            // 
            radioButtonExcel.AutoSize = true;
            radioButtonExcel.ForeColor = SystemColors.ControlDarkDark;
            radioButtonExcel.Location = new Point(271, 56);
            radioButtonExcel.Name = "radioButtonExcel";
            radioButtonExcel.Size = new Size(64, 24);
            radioButtonExcel.TabIndex = 4;
            radioButtonExcel.Text = "Excel";
            radioButtonExcel.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            textBoxName.BackColor = Color.OldLace;
            textBoxName.Location = new Point(20, 26);
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "Name";
            textBoxName.Size = new Size(221, 27);
            textBoxName.TabIndex = 5;
            // 
            // textBoxSurname
            // 
            textBoxSurname.BackColor = Color.OldLace;
            textBoxSurname.Location = new Point(21, 66);
            textBoxSurname.Name = "textBoxSurname";
            textBoxSurname.PlaceholderText = "Surname";
            textBoxSurname.Size = new Size(220, 27);
            textBoxSurname.TabIndex = 6;
            // 
            // textBoxDate
            // 
            textBoxDate.BackColor = Color.OldLace;
            textBoxDate.Location = new Point(21, 110);
            textBoxDate.Name = "textBoxDate";
            textBoxDate.PlaceholderText = "Date (dd-MM-yyyy format)";
            textBoxDate.Size = new Size(220, 27);
            textBoxDate.TabIndex = 7;
            textBoxDate.TextChanged += textBoxDate_TextChanged;
            textBoxDate.KeyPress += textBoxDate_KeyPress;
            // 
            // textBoxCity
            // 
            textBoxCity.BackColor = Color.OldLace;
            textBoxCity.Location = new Point(20, 153);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.PlaceholderText = "City";
            textBoxCity.Size = new Size(220, 27);
            textBoxCity.TabIndex = 8;
            // 
            // textBoxCountry
            // 
            textBoxCountry.BackColor = Color.OldLace;
            textBoxCountry.Location = new Point(20, 196);
            textBoxCountry.Name = "textBoxCountry";
            textBoxCountry.PlaceholderText = "Country";
            textBoxCountry.Size = new Size(220, 27);
            textBoxCountry.TabIndex = 9;
            // 
            // labelDbInfo
            // 
            labelDbInfo.AutoSize = true;
            labelDbInfo.BackColor = Color.OldLace;
            labelDbInfo.ForeColor = Color.Sienna;
            labelDbInfo.Location = new Point(13, 70);
            labelDbInfo.Name = "labelDbInfo";
            labelDbInfo.Size = new Size(50, 20);
            labelDbInfo.TabIndex = 10;
            labelDbInfo.Text = "label2";
            // 
            // groupBox
            // 
            groupBox.BackColor = Color.WhiteSmoke;
            groupBox.Controls.Add(buttonDeleteDbData);
            groupBox.Controls.Add(buttonClearFilters);
            groupBox.Controls.Add(textBoxName);
            groupBox.Controls.Add(buttonExport);
            groupBox.Controls.Add(textBoxCountry);
            groupBox.Controls.Add(radioButtonCsv);
            groupBox.Controls.Add(textBoxCity);
            groupBox.Controls.Add(radioButtonExcel);
            groupBox.Controls.Add(textBoxDate);
            groupBox.Controls.Add(textBoxSurname);
            groupBox.Location = new Point(13, 102);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(456, 279);
            groupBox.TabIndex = 11;
            groupBox.TabStop = false;
            // 
            // buttonDeleteDbData
            // 
            buttonDeleteDbData.Location = new Point(20, 244);
            buttonDeleteDbData.Name = "buttonDeleteDbData";
            buttonDeleteDbData.Size = new Size(414, 29);
            buttonDeleteDbData.TabIndex = 12;
            buttonDeleteDbData.Text = "Delete all DB data";
            buttonDeleteDbData.UseVisualStyleBackColor = true;
            buttonDeleteDbData.Click += buttonDeleteDbData_Click;
            // 
            // buttonClearFilters
            // 
            buttonClearFilters.BackColor = Color.Thistle;
            buttonClearFilters.ForeColor = SystemColors.GrayText;
            buttonClearFilters.Location = new Point(259, 98);
            buttonClearFilters.Name = "buttonClearFilters";
            buttonClearFilters.Size = new Size(175, 51);
            buttonClearFilters.TabIndex = 10;
            buttonClearFilters.Text = "Clear Filters";
            buttonClearFilters.UseVisualStyleBackColor = false;
            buttonClearFilters.Click += buttonClearFilters_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(272, 67);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(197, 29);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 12;
            progressBar1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(786, 484);
            Controls.Add(progressBar1);
            Controls.Add(groupBox);
            Controls.Add(labelDbInfo);
            Controls.Add(buttonImport);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QuickCSVExcel";
            Load += Form1_Load;
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonImport;
        private Button buttonExport;
        private RadioButton radioButtonCsv;
        private RadioButton radioButtonExcel;
        private TextBox textBoxName;
        private TextBox textBoxSurname;
        private TextBox textBoxDate;
        private TextBox textBoxCity;
        private TextBox textBoxCountry;
        private Label labelDbInfo;
        private GroupBox groupBox;
        private Button buttonClearFilters;
        private Button buttonDeleteDbData;
        private ProgressBar progressBar1;
    }
}
