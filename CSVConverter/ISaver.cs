using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CSVConverter
{
    interface ISaver
    {
        void Save(DataTable data, string filePath);
        void SetupFileDialog(SaveFileDialog saveFileDialog);
    }

    class CsvSaver : ISaver
    {
        public void Save(DataTable data, string filePath)
        {
            throw new NotImplementedException();
        }

        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = "save.csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        }
    }

    class JsonSaver : ISaver
    {
        public void Save(DataTable data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(jsonString);
            }
        }

        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = "save.json";
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }

    class XmlSaver : ISaver
    {
        public void Save(DataTable data, string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                data.WriteXml(streamWriter);
            }
        }

        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = "save.xml";
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        }
    }
}
