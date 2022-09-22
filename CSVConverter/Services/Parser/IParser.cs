using GenericParsing;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CSVConverter
{
    /// <summary>
    /// Интерфейс парсинга файла в объект DataTable.
    /// </summary>
    interface IParser
    {
        DataTable GetData(string filePath);
        void SetupFileDialog(OpenFileDialog openFileDialog);
    }
    /// <summary>
    /// Реализация стратегии парсинга Csv файла в объекта DataTable.
    /// </summary>
    class CsvParser : IParser
    {
        /// <summary>
        /// Метод реализующий интерфейс IParser. Преобразует файл Csv в DataTable
        /// </summary>
        /// <param name="filePath">Путь к выбранному файлу</param>
        /// <returns>Возвращает объект DataTable</returns>
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
        /// <summary>
        /// Метод для настройки диалогового окна выбора файла
        /// </summary>
        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        }
    }
    /// <summary>
    /// Реализация стратегии парсинга Xml файла в объекта DataTable.
    /// </summary>
    class XmlParser : IParser
    {
        /// <summary>
        /// Метод реализующий интерфейс IParser. Преобразует файл Xml в DataTable
        /// </summary>
        /// <param name="filePath">Путь к выбранному файлу</param>
        /// <returns>Возвращает объект DataTable</returns>
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
        /// <summary>
        /// Метод для настройки диалогового окна выбора файла
        /// </summary>
        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        }
    }
    /// <summary>
    /// Реализация стратегии парсинга Json файла в объекта DataTable.
    /// </summary>
    class JsonParser : IParser
    {
        /// <summary>
        /// Метод реализующий интерфейс IParser. Преобразует файл Json в DataTable
        /// </summary>
        /// <param name="filePath">Путь к выбранному файлу</param>
        /// <returns>Возвращает объект DataTable</returns>
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
        /// <summary>
        /// Метод для настройки диалогового окна выбора файла
        /// </summary>
        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }
}
