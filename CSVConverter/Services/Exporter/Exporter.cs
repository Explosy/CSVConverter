using System.Data;



namespace CSVConverter
{
    internal class Exporter
    {
        public IExporter ExportStrategy { private get; set; }
        
        public void Export(DataTable dataTable)
        {
            DataTable newDataTable = dataTable.Copy();
            TablePreparer tablePreparer = new TablePreparer();
            tablePreparer.DeleteColumns(newDataTable, 7, new string[] { "Previous Rank", "First Appearance", "First Rank" });
            ExportStrategy.Export(newDataTable);
        }
    }
}
