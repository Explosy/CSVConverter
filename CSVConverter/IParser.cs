using GenericParsing;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CSVConverter
{
    interface IParser
    {
        DataTable GetData(string filePath);
        void SetupFileDialog(OpenFileDialog openFileDialog);
    }

    class CsvParser : IParser
    {
        public DataTable GetData(string filePath)
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

        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        }
    }

    class XmlParser : IParser
    {
        public DataTable GetData(string filePath)
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

        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        }
    }

    class JsonParser : IParser
    {
        public DataTable GetData(string filePath)
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

        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }
}
