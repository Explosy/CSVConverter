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
    /// <summary>
    /// Интерфейс сохранения объекта DataTable в файл.
    /// </summary>
    interface ISaver
    {
        /// <summary>
        /// Метод для сохранения объекта DataTable в файл.
        /// </summary>
        /// <param name="data">Объект DataTable для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
        void Save(DataTable data, string filePath);
        /// <summary>
        /// Метод настройки диалогового окна настройки
        /// </summary>
        /// <param name="saveFileDialog"></param>
        void SetupFileDialog(SaveFileDialog saveFileDialog);
    }
    /// <summary>
    /// Реализация стратегии сохранения объекта DataTable в Csv файл
    /// </summary>
    class CsvSaver : ISaver
    {
        /// <summary>
        /// Метод реализующий интерфейс ISaver. Сохраняет таблицу в CSV файл
        /// </summary>
        /// <param name="data">Объект DataTable для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
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
        /// <summary>
        /// Метод для настройки диалогового окна сохранения
        /// </summary>
        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = "save.csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        }
    }
    /// <summary>
    /// Реализация стратегии сохранения объекта DataTable в Json файл
    /// </summary>
    class JsonSaver : ISaver
    {
        /// <summary>
        /// Метод реализующий интерфейс ISaver. Сохраняет таблицу в Json файл
        /// </summary>
        /// <param name="data">Объект DataTable для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
        public void Save(DataTable data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(jsonString);
            }
        }
        /// <summary>
        /// Метод для настройки диалогового окна сохранения
        /// </summary>
        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = "save.json";
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }
    /// <summary>
    /// Реализация стратегии сохранения объекта DataTable в Xml файл
    /// </summary>
    class XmlSaver : ISaver
    {
        /// <summary>
        /// Метод реализующий интерфейс ISaver. Сохраняет таблицу в XML файл
        /// </summary>
        /// <param name="data">Объект DataTable для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
        public void Save(DataTable data, string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                data.WriteXml(streamWriter);
            }
        }
        /// <summary>
        /// Метод для настройки диалогового окна сохранения
        /// </summary>
        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = "save.xml";
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        }
    }
}
