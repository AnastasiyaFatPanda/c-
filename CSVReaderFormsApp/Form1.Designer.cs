namespace CSVReaderFormsApp
{
    partial class FormDataReader
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
            buttonImport = new Button();
            buttonExport = new Button();
            radioButtonCsv = new RadioButton();
            radioButtonExcel = new RadioButton();
            groupBoxRadioButtons = new GroupBox();
            groupBoxFilter = new GroupBox();
            labelCity = new Label();
            labelCountry = new Label();
            labelSurname = new Label();
            labelName = new Label();
            labelDate = new Label();
            textBoxCity = new TextBox();
            textBoxCountry = new TextBox();
            textBoxSurname = new TextBox();
            textBoxName = new TextBox();
            dateTimePicker = new DateTimePicker();
            progressBar = new ProgressBar();
            openFileDialog = new OpenFileDialog();
            labelNoDataInDb = new Label();
            groupBoxRadioButtons.SuspendLayout();
            groupBoxFilter.SuspendLayout();
            SuspendLayout();
            // 
            // buttonImport
            // 
            buttonImport.Location = new Point(20, 21);
            buttonImport.Margin = new Padding(1, 1, 1, 1);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(398, 43);
            buttonImport.TabIndex = 0;
            buttonImport.Text = "Import File";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += button1_Click;
            // 
            // buttonExport
            // 
            buttonExport.Enabled = false;
            buttonExport.Location = new Point(290, 208);
            buttonExport.Margin = new Padding(1, 1, 1, 1);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(128, 84);
            buttonExport.TabIndex = 1;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            // 
            // radioButtonCsv
            // 
            radioButtonCsv.AutoSize = true;
            radioButtonCsv.Location = new Point(12, 30);
            radioButtonCsv.Margin = new Padding(1, 1, 1, 1);
            radioButtonCsv.Name = "radioButtonCsv";
            radioButtonCsv.Size = new Size(56, 24);
            radioButtonCsv.TabIndex = 2;
            radioButtonCsv.TabStop = true;
            radioButtonCsv.Text = "CSV";
            radioButtonCsv.UseVisualStyleBackColor = true;
            // 
            // radioButtonExcel
            // 
            radioButtonExcel.AutoSize = true;
            radioButtonExcel.Location = new Point(12, 54);
            radioButtonExcel.Margin = new Padding(1, 1, 1, 1);
            radioButtonExcel.Name = "radioButtonExcel";
            radioButtonExcel.Size = new Size(64, 24);
            radioButtonExcel.TabIndex = 3;
            radioButtonExcel.TabStop = true;
            radioButtonExcel.Text = "Excel";
            radioButtonExcel.UseVisualStyleBackColor = true;
            // 
            // groupBoxRadioButtons
            // 
            groupBoxRadioButtons.BackColor = Color.FromArgb(224, 224, 224);
            groupBoxRadioButtons.Controls.Add(radioButtonExcel);
            groupBoxRadioButtons.Controls.Add(radioButtonCsv);
            groupBoxRadioButtons.Enabled = false;
            groupBoxRadioButtons.Location = new Point(290, 104);
            groupBoxRadioButtons.Margin = new Padding(1, 1, 1, 1);
            groupBoxRadioButtons.Name = "groupBoxRadioButtons";
            groupBoxRadioButtons.Padding = new Padding(1, 1, 1, 1);
            groupBoxRadioButtons.Size = new Size(128, 90);
            groupBoxRadioButtons.TabIndex = 4;
            groupBoxRadioButtons.TabStop = false;
            groupBoxRadioButtons.Text = "Export Format";
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.BackColor = Color.FromArgb(224, 224, 224);
            groupBoxFilter.Controls.Add(labelCity);
            groupBoxFilter.Controls.Add(labelCountry);
            groupBoxFilter.Controls.Add(labelSurname);
            groupBoxFilter.Controls.Add(labelName);
            groupBoxFilter.Controls.Add(labelDate);
            groupBoxFilter.Controls.Add(textBoxCity);
            groupBoxFilter.Controls.Add(textBoxCountry);
            groupBoxFilter.Controls.Add(textBoxSurname);
            groupBoxFilter.Controls.Add(textBoxName);
            groupBoxFilter.Controls.Add(dateTimePicker);
            groupBoxFilter.Enabled = false;
            groupBoxFilter.Location = new Point(20, 104);
            groupBoxFilter.Margin = new Padding(1, 1, 1, 1);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Padding = new Padding(1, 1, 1, 1);
            groupBoxFilter.Size = new Size(250, 188);
            groupBoxFilter.TabIndex = 5;
            groupBoxFilter.TabStop = false;
            groupBoxFilter.Text = "Filters";
            groupBoxFilter.Enter += groupBoxFilter_Enter;
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Location = new Point(14, 145);
            labelCity.Margin = new Padding(1, 0, 1, 0);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(34, 20);
            labelCity.TabIndex = 9;
            labelCity.Text = "City";
            // 
            // labelCountry
            // 
            labelCountry.AutoSize = true;
            labelCountry.Location = new Point(14, 116);
            labelCountry.Margin = new Padding(1, 0, 1, 0);
            labelCountry.Name = "labelCountry";
            labelCountry.Size = new Size(60, 20);
            labelCountry.TabIndex = 8;
            labelCountry.Text = "Country";
            // 
            // labelSurname
            // 
            labelSurname.AutoSize = true;
            labelSurname.Location = new Point(14, 85);
            labelSurname.Margin = new Padding(1, 0, 1, 0);
            labelSurname.Name = "labelSurname";
            labelSurname.Size = new Size(67, 20);
            labelSurname.TabIndex = 7;
            labelSurname.Text = "Surname";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(14, 55);
            labelName.Margin = new Padding(1, 0, 1, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(49, 20);
            labelName.TabIndex = 6;
            labelName.Text = "Name";
            labelName.Click += label2_Click;
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.Location = new Point(14, 25);
            labelDate.Margin = new Padding(1, 0, 1, 0);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(41, 20);
            labelDate.TabIndex = 5;
            labelDate.Text = "Date";
            // 
            // textBoxCity
            // 
            textBoxCity.Location = new Point(87, 148);
            textBoxCity.Margin = new Padding(1, 1, 1, 1);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(138, 27);
            textBoxCity.TabIndex = 4;
            // 
            // textBoxCountry
            // 
            textBoxCountry.Location = new Point(87, 119);
            textBoxCountry.Margin = new Padding(1, 1, 1, 1);
            textBoxCountry.Name = "textBoxCountry";
            textBoxCountry.Size = new Size(138, 27);
            textBoxCountry.TabIndex = 3;
            // 
            // textBoxSurname
            // 
            textBoxSurname.Location = new Point(87, 88);
            textBoxSurname.Margin = new Padding(1, 1, 1, 1);
            textBoxSurname.Name = "textBoxSurname";
            textBoxSurname.Size = new Size(138, 27);
            textBoxSurname.TabIndex = 2;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(87, 58);
            textBoxName.Margin = new Padding(1, 1, 1, 1);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(138, 27);
            textBoxName.TabIndex = 1;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(87, 25);
            dateTimePicker.Margin = new Padding(1, 1, 1, 1);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(138, 27);
            dateTimePicker.TabIndex = 0;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 76);
            progressBar.Margin = new Padding(1, 1, 1, 1);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(398, 12);
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // labelNoDataInDb
            // 
            labelNoDataInDb.AutoSize = true;
            labelNoDataInDb.BackColor = Color.MistyRose;
            labelNoDataInDb.ForeColor = SystemColors.ControlDarkDark;
            labelNoDataInDb.Location = new Point(74, 68);
            labelNoDataInDb.Margin = new Padding(1, 0, 1, 0);
            labelNoDataInDb.Name = "labelNoDataInDb";
            labelNoDataInDb.RightToLeft = RightToLeft.Yes;
            labelNoDataInDb.Size = new Size(314, 20);
            labelNoDataInDb.TabIndex = 7;
            labelNoDataInDb.Text = "There is no data in DB. Please, import CSV file.";
            // 
            // FormDataReader
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(438, 311);
            Controls.Add(labelNoDataInDb);
            Controls.Add(progressBar);
            Controls.Add(groupBoxFilter);
            Controls.Add(groupBoxRadioButtons);
            Controls.Add(buttonExport);
            Controls.Add(buttonImport);
            Margin = new Padding(1, 1, 1, 1);
            Name = "FormDataReader";
            Text = "DataReader";
            Load += FormDataReader_Load;
            groupBoxRadioButtons.ResumeLayout(false);
            groupBoxRadioButtons.PerformLayout();
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonImport;
        private Button buttonExport;
        private RadioButton radioButtonCsv;
        private RadioButton radioButtonExcel;
        private GroupBox groupBoxRadioButtons;
        private GroupBox groupBoxFilter;
        private Label labelCity;
        private Label labelCountry;
        private Label labelSurname;
        private Label labelName;
        private Label labelDate;
        private TextBox textBoxCity;
        private TextBox textBoxCountry;
        private TextBox textBoxSurname;
        private TextBox textBoxName;
        private DateTimePicker dateTimePicker;
        private ProgressBar progressBar;
        private OpenFileDialog openFileDialog;
        private Label labelNoDataInDb;
    }
}
