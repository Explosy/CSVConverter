using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace CSVConverter
{
    internal class FileSaver
    {
        public void SaveDataToJSON(DataTable data, string path)
        {
            string jsonString = JsonConvert.SerializeObject(data.AsEnumerable()
                                                                .Take(10), Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.Write(jsonString);
            }
        }

        public void SaveDataToXML(DataTable data, string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                data.WriteXml(streamWriter);
            }
        }

    }
}
