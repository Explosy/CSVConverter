using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

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
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in data.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(";", fields));
            }

            File.WriteAllText(filePath, sb.ToString());
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
