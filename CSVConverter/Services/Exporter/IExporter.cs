using System;
using System.Data;
using System.Linq;
using CSVConverter.Services.ExcelComponents;
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
            ExcelComponents excelComponents = new ExcelComponents();
            
            var workbook = excelComponents.CreateNewDoc();
            excelComponents.CreateDiagramm(dataTable, workbook);
        }
    }
}
