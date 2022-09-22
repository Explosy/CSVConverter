using System.Data;



namespace CSVConverter
{
    internal class Exporter
    {
        public IExporter ExportStrategy { private get; set; }
        
        public void Export(DataTable dataTable)
        {
            TablePreparer tablePreparer = new TablePreparer();
            tablePreparer.DeleteColumns(dataTable, 7, new string[] { "Previous Rank", "First Appearance", "First Rank" });
            ExportStrategy.Export(dataTable);
        }
    }
}
