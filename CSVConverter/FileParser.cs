using GenericParsing;
using Newtonsoft.Json;
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
            DataSet dataSet = new DataSet();
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                dataSet.ReadXml(streamReader);
            }
            result = dataSet.Tables[0];
            return result;
        }

        public DataTable GetDataFromJSON(string filePath)
        {
            DataTable result = new DataTable();
            string jsonstring;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                jsonstring = streamReader.ReadToEnd();
            }
            result = JsonConvert.DeserializeObject<DataTable>(jsonstring);
            return result;
        }
    }
}

