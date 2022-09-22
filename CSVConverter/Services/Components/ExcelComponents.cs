using System;
using System.Data;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace CSVConverter.Services.ExcelComponents
{
    internal class ExcelComponents
    {
        public Excel.Workbook CreateNewDoc()
        {
            Excel.Application application = new Excel.Application();
            application.Visible = true;
            var workbook = application.Workbooks.Add();
            Excel.Worksheet sheet = workbook.Worksheets[1];
            return workbook;
        }
        
        public void CreateDiagramm(DataTable dataTable, Excel.Workbook workbook)
        {
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

            Excel.Chart chart = workbook.Charts.Add();
            chart.Location(Excel.XlChartLocation.xlLocationAsObject, "DataAndChart");
            Excel.ChartObject chartObj = sheet.ChartObjects(1);
            chartObj.Chart.ChartTitle.Text = "Распределение суперкомпьютеров по странам";

            chartObj.Chart.ChartType = Excel.XlChartType.xlPie;
        }
    }
}
