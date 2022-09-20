using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace CSVConverter
{
    internal class FileSaver
    {
        public void SaveDataToJSON(DataTable data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(jsonString);
            }
        }

        public void SaveDataToXML(DataTable data, string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                data.WriteXml(streamWriter);
            }
        }

    }
}
