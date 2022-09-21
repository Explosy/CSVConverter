
using System;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace CSVConverter
{
    interface IExporter
    {
        void Export(DataTable dataTable);
    }

    class WordExporter : IExporter
    {
        public void Export(DataTable dataTable)
        {
            object objMissing = System.Reflection.Missing.Value;
            
            Word.Application application = new Word.Application();
            application.Visible = true;
            var document = application.Documents.Add();

            int tableRowCount = 10;
            int tableColumnCount = 10;
            
            object objEndOfDoc = "\\endofdoc";
            
            Word.Range WordRange = document.Bookmarks.get_Item(ref objEndOfDoc).Range;
            Word.Table wordTable = document.Tables.Add(WordRange, tableRowCount, 6, ref objMissing, ref objMissing);

            int iTableRow = 1;
            int iTableCol = 1;
            for (int i = 0; i < tableColumnCount; i++)
            {
                if (i > 0 && i < 5) continue;
                wordTable.Cell(iTableRow, iTableCol).Range.Text = dataTable.Columns[i].ColumnName;
                iTableCol++;
            }
            iTableRow++;
            for (int i = 0; i < tableRowCount; i++)
            {
                iTableCol = 1;
                for (int j = 0; j < tableColumnCount; j++)
                {
                    if (j > 0 && j < 5) continue;
                    wordTable.Cell(iTableRow, iTableCol).Range.Text = dataTable.Rows[i][j].ToString();
                    iTableCol++;
                }
                iTableRow++;
            }

            wordTable.Borders.Enable = 1;
        }
    }

    class ExcelExporter : IExporter
    {
        public void Export(DataTable dataTable)
        {
            Excel.Application application = new Excel.Application();
            application.Visible = true;
            var workbook = application.Workbooks.Add();
            Excel.Worksheet sheet = workbook.Worksheets[1];

            var allCountry = (from super in dataTable.AsEnumerable()
                              group super by super["Country"] into g
                              select new { Country = g.Key, Count = g.Count() }).ToList();
            
            string[] selectedCountries = new string[] {"Russia", "Japan", "China",
                                                     "United States", "Germany", "India"};

            var selected = allCountry.Where(c => selectedCountries.Contains(c.Country)).ToList();

            sheet.Name = "DataAndChart";
            sheet.Cells[1, 1].Value = "Страна";
            sheet.Cells[1, 2].Value = "Количество";

            for (int i = 0; i < selected.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = selected[i].Country;
                sheet.Cells[i + 2, 2].Value = selected[i].Count;
            }

            Excel.Chart chart = application.Charts.Add();
            chart.Location(Excel.XlChartLocation.xlLocationAsObject, "DataAndChart");
            Excel.ChartObject chartObj = sheet.ChartObjects(1);
            chartObj.Chart.ChartTitle.Text = "Распределение суперкомпьютеров по странам";

            chartObj.Chart.ChartType = Excel.XlChartType.xlPie;
        }
    }
}
