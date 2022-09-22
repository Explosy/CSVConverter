using System.Data;


namespace CSVConverter
{
    internal class TablePreparer
    {
        internal void DeleteColumns(DataTable dataTable,int desiredSize, params string[] columnsNameToDelete)
        {
            foreach (var name in columnsNameToDelete)
            {
                dataTable.Columns.Remove(name);
            }
            while (dataTable.Columns.Count > desiredSize)
            {
                dataTable.Columns.RemoveAt(desiredSize);
            }
        }
    }
}
