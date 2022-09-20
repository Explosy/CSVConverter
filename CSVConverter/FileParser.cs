using GenericParsing;
using System;
using System.Data;

namespace CSVConverter
{
    internal class FileParser
    {
        public DataTable GetDataFromCSV(string filePath)
        {
            DataTable result = new DataTable();
            using (GenericParserAdapter parser = new GenericParserAdapter(filePath))
            {
                parser.ColumnDelimiter = ';';
                parser.FirstRowHasHeader = true;
                result = parser.GetDataTable();
            }
            return result;
        }

        public DataTable GetDataFromXML(string filePath)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataFromJSON(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}

