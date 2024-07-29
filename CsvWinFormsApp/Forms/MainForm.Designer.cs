namespace CsvWinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

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
            textBoxName = new TextBox();
            textBoxSurname = new TextBox();
            textBoxDate = new TextBox();
            textBoxCity = new TextBox();
            textBoxCountry = new TextBox();
            labelDbInfo = new Label();
            groupBox = new GroupBox();
            labelInfo3 = new Label();
            labelInfo2 = new Label();
            labelInfo1 = new Label();
            radioButtonXML = new RadioButton();
            buttonDeleteDbData = new Button();
            buttonClearFilters = new Button();
            panel1 = new Panel();
            groupBox.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonImport
            // 
            buttonImport.BackColor = Color.FromArgb(225, 215, 179);
            buttonImport.ForeColor = SystemColors.GrayText;
            buttonImport.Location = new Point(12, 12);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(559, 42);
            buttonImport.TabIndex = 1;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = false;
            buttonImport.Click += buttonImport_Click;
            // 
            // buttonExport
            // 
            buttonExport.BackColor = Color.FromArgb(225, 215, 179);
            buttonExport.ForeColor = SystemColors.GrayText;
            buttonExport.Location = new Point(361, 266);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(187, 78);
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
            radioButtonCsv.Location = new Point(361, 83);
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
            radioButtonExcel.Location = new Point(361, 113);
            radioButtonExcel.Name = "radioButtonExcel";
            radioButtonExcel.Size = new Size(64, 24);
            radioButtonExcel.TabIndex = 4;
            radioButtonExcel.Text = "Excel";
            radioButtonExcel.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            textBoxName.BackColor = Color.FloralWhite;
            textBoxName.ForeColor = SystemColors.ControlDarkDark;
            textBoxName.Location = new Point(20, 77);
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "Name";
            textBoxName.Size = new Size(317, 27);
            textBoxName.TabIndex = 5;
            textBoxName.KeyPress += textBoxName_KeyPress;
            // 
            // textBoxSurname
            // 
            textBoxSurname.BackColor = Color.FloralWhite;
            textBoxSurname.ForeColor = SystemColors.ControlDarkDark;
            textBoxSurname.Location = new Point(21, 117);
            textBoxSurname.Name = "textBoxSurname";
            textBoxSurname.PlaceholderText = "Surname";
            textBoxSurname.Size = new Size(316, 27);
            textBoxSurname.TabIndex = 6;
            textBoxSurname.KeyPress += textBoxSurname_KeyPress;
            // 
            // textBoxDate
            // 
            textBoxDate.BackColor = Color.FloralWhite;
            textBoxDate.ForeColor = SystemColors.ControlDarkDark;
            textBoxDate.Location = new Point(17, 312);
            textBoxDate.Name = "textBoxDate";
            textBoxDate.PlaceholderText = "Date (yyyy-MM-dd format)";
            textBoxDate.Size = new Size(316, 27);
            textBoxDate.TabIndex = 7;
            textBoxDate.TextChanged += textBoxDate_TextChanged;
            textBoxDate.KeyPress += textBoxDate_KeyPress;
            // 
            // textBoxCity
            // 
            textBoxCity.BackColor = Color.FloralWhite;
            textBoxCity.ForeColor = SystemColors.ControlDarkDark;
            textBoxCity.Location = new Point(19, 165);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.PlaceholderText = "City";
            textBoxCity.Size = new Size(316, 27);
            textBoxCity.TabIndex = 8;
            textBoxCity.KeyPress += textBoxCity_KeyPress;
            // 
            // textBoxCountry
            // 
            textBoxCountry.BackColor = Color.FloralWhite;
            textBoxCountry.ForeColor = SystemColors.ControlDarkDark;
            textBoxCountry.Location = new Point(19, 208);
            textBoxCountry.Name = "textBoxCountry";
            textBoxCountry.PlaceholderText = "Country";
            textBoxCountry.Size = new Size(316, 27);
            textBoxCountry.TabIndex = 9;
            textBoxCountry.KeyPress += textBoxCountry_KeyPress;
            // 
            // labelDbInfo
            // 
            labelDbInfo.AutoSize = true;
            labelDbInfo.BackColor = Color.OldLace;
            labelDbInfo.ForeColor = SystemColors.ControlDarkDark;
            labelDbInfo.Location = new Point(20, 10);
            labelDbInfo.Name = "labelDbInfo";
            labelDbInfo.Size = new Size(88, 20);
            labelDbInfo.TabIndex = 10;
            labelDbInfo.Text = "labelDbInfo";
            // 
            // groupBox
            // 
            groupBox.BackColor = Color.FromArgb(216, 228, 231);
            groupBox.Controls.Add(labelInfo3);
            groupBox.Controls.Add(labelInfo2);
            groupBox.Controls.Add(labelInfo1);
            groupBox.Controls.Add(radioButtonXML);
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
            groupBox.Size = new Size(558, 427);
            groupBox.TabIndex = 11;
            groupBox.TabStop = false;
            // 
            // labelInfo3
            // 
            labelInfo3.AutoSize = true;
            labelInfo3.ForeColor = SystemColors.WindowFrame;
            labelInfo3.Location = new Point(361, 54);
            labelInfo3.Name = "labelInfo3";
            labelInfo3.Size = new Size(170, 20);
            labelInfo3.TabIndex = 16;
            labelInfo3.Text = "Select Export file format";
            // 
            // labelInfo2
            // 
            labelInfo2.ForeColor = SystemColors.WindowFrame;
            labelInfo2.Location = new Point(21, 242);
            labelInfo2.Name = "labelInfo2";
            labelInfo2.Size = new Size(316, 67);
            labelInfo2.TabIndex = 15;
            labelInfo2.Text = "You can enter only digits. You will get all records after specified date. The filter must be in yyyy-MM-dd format";
            // 
            // labelInfo1
            // 
            labelInfo1.ForeColor = SystemColors.WindowFrame;
            labelInfo1.Location = new Point(19, 10);
            labelInfo1.Name = "labelInfo1";
            labelInfo1.Size = new Size(336, 64);
            labelInfo1.TabIndex = 14;
            labelInfo1.Text = "You can enter only letters. \r\nThe filter selects records containing, but not exactly matching, the entered values.";
            // 
            // radioButtonXML
            // 
            radioButtonXML.AutoSize = true;
            radioButtonXML.ForeColor = SystemColors.ControlDarkDark;
            radioButtonXML.Location = new Point(361, 143);
            radioButtonXML.Name = "radioButtonXML";
            radioButtonXML.Size = new Size(59, 24);
            radioButtonXML.TabIndex = 13;
            radioButtonXML.Text = "XML";
            radioButtonXML.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteDbData
            // 
            buttonDeleteDbData.BackColor = Color.FromArgb(225, 210, 220);
            buttonDeleteDbData.ForeColor = Color.FromArgb(154, 79, 79);
            buttonDeleteDbData.Location = new Point(17, 375);
            buttonDeleteDbData.Name = "buttonDeleteDbData";
            buttonDeleteDbData.Size = new Size(531, 29);
            buttonDeleteDbData.TabIndex = 12;
            buttonDeleteDbData.Text = "Delete all DB data";
            buttonDeleteDbData.UseVisualStyleBackColor = false;
            buttonDeleteDbData.Click += buttonDeleteDbData_Click;
            // 
            // buttonClearFilters
            // 
            buttonClearFilters.BackColor = Color.FromArgb(225, 215, 179);
            buttonClearFilters.ForeColor = SystemColors.GrayText;
            buttonClearFilters.Location = new Point(361, 182);
            buttonClearFilters.Name = "buttonClearFilters";
            buttonClearFilters.Size = new Size(187, 78);
            buttonClearFilters.TabIndex = 10;
            buttonClearFilters.Text = "Clear Filters";
            buttonClearFilters.UseVisualStyleBackColor = false;
            buttonClearFilters.Click += buttonClearFilters_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(216, 228, 231);
            panel1.Controls.Add(labelDbInfo);
            panel1.Location = new Point(13, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(558, 39);
            panel1.TabIndex = 12;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(209, 218, 220);
            ClientSize = new Size(583, 541);
            Controls.Add(panel1);
            Controls.Add(groupBox);
            Controls.Add(buttonImport);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QuickCSVExcelXML";
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
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
        private Panel panel1;
        private RadioButton radioButtonXML;
        private ToolTip toolTipName;
        private ToolTip toolTipDate;
        private Label labelInfo1;
        private Label labelInfo3;
        private Label labelInfo2;
    }
}
