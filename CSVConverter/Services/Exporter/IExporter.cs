using System;
using System.Data;
using System.Linq;
using CSVConverter.Services.WordComponents;
using Excel = Microsoft.Office.Interop.Excel;

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
            WordComponents wordComponents = new WordComponents();
            
            var document = wordComponents.CreateNewDoc();
            wordComponents.AddTitle("Сводный отчет", document);
            wordComponents.AddText("Таблица. Информация о топ 10 суперкомпьютеров в мире", document);
            wordComponents.AddTable(dataTable, 10, document);
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
