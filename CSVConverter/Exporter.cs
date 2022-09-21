using System;
using System.Data;



namespace CSVConverter
{
    internal class Exporter
    {
        public IExporter ExportStrategy { private get; set; }
        
        public void Export(DataTable dataTable)
        {
            ExportStrategy.Export(dataTable);
        }
    }
}
