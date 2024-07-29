using ClosedXML.Excel;
using CsvHelper;
using CsvWinFormsApp.Enums;
using CsvWinFormsApp.Models;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace CsvWinFormsApp.Utilities
{
    internal static class ExportHelper
    {
        public static SaveFileDialog CreateSaveFileDialog(ExportEnum exportFormat)
        {
            switch (exportFormat)
            {
                case ExportEnum.CSV:
                    return new SaveFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                        Title = "Save an Export CSV File",
                        DefaultExt = "csv",
                        FileName = "records_export.csv"
                    };
                case ExportEnum.EXCEL:
                    return new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        Title = "Save an Export Excel File",
                        DefaultExt = "xlsx",
                        FileName = "records_export.xlsx"
                    };
                case ExportEnum.XML:
                    return new SaveFileDialog
                    {
                        Title = "Save an Export XML File",
                        DefaultExt = "xml",
                        FileName = "records_export.xml"
                    };
                default: return new SaveFileDialog { Title = "Save an Export File" };
            }
        }
        public static void GenerateFile(ExportEnum exportFormat, string filePath, List<Record> records)
        {
            try
            {
                switch (exportFormat)
                {
                    case ExportEnum.CSV:
                        {
                            GenerateCSV(records, filePath);
                            break;
                        }
                    case ExportEnum.EXCEL:
                        {
                            GenerateExcel(records, filePath);
                            break;
                        }
                    case ExportEnum.XML:
                        {
                            GenerateXML(records, filePath);
                            break;
                        }
                    default: break;
                }
            }
            catch
            {
                MessagesHelper.ShowErrorMessage("Unexpected error occured during file saving!");
            }
            finally
            {
                var result = MessagesHelper.ShowInfoMessage($"Data exported successfully to {filePath}. Do you want to open the created file?");

                // Check user's response
                if (result == DialogResult.OK)
                {
                    // Open the file in the default application
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        }

        private static void GenerateExcel(List<Record> records, string filePath)
        {
            // Create a new workbook and worksheet
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Records");

                // Add header row
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "SecondName";
                worksheet.Cell(1, 4).Value = "Surname";
                worksheet.Cell(1, 5).Value = "Date";
                worksheet.Cell(1, 6).Value = "City";
                worksheet.Cell(1, 7).Value = "Country";

                // Add data rows
                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    worksheet.Cell(i + 2, 1).Value = record.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = record.Name;
                    worksheet.Cell(i + 2, 3).Value = record.Surname;
                    worksheet.Cell(i + 2, 4).Value = record.Date;
                    worksheet.Cell(i + 2, 5).Value = record.City;
                    worksheet.Cell(i + 2, 6).Value = record.Country;
                }

                // Save the workbook
                workbook.SaveAs(filePath);
            }

        }

        private static void GenerateXML(List<Record> records, string filePath)
        {
            XElement root = new XElement("TestProgram",
                    records.Select(d =>
                         new XElement("Record",
                            new XElement("Date", d.Date),
                            new XElement("Name", d.Name),
                            new XElement("SecondName", d.SecondName),
                            new XElement("Surname", d.Surname),
                            new XElement("City", d.City),
                            new XElement("Country", d.Country)
                        )
                    )
                );

            root.Save(filePath);
        }

        private static void GenerateCSV(List<Record> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }

    }
}
