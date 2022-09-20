using GenericParsing;
using System;
using System.Data;
using System.IO;

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
            DataTable result = new DataTable();
            DataSet ds = new DataSet();
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                ds.ReadXml(streamReader);
            }
            result = ds.Tables[0];
            return result;
        }

        public DataTable GetDataFromJSON(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}

