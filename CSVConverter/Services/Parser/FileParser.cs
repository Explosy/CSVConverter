using System;
using System.Data;
using System.Windows.Forms;

namespace CSVConverter
{
    /// <summary>
    /// Класс реализующий парсинг файла в объект DataTable.
    /// </summary>
    internal class FileParser
    {
        private OpenFileDialog openFileDialog;
        /// <summary>
        /// Поле, содержащее текущую стратегию парсинга файла.
        /// </summary>
        public IParser ParseStrategy { private get; set; }

        public FileParser()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
        }
        /// <summary>
        /// Метод, создающий диалоговое окно выбора файла, а также запускающий реализацию текущей стратегии парсинга из ParseStrategy.
        /// </summary>
        /// <returns>Возвращает объект DataTable</returns>
        public DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            ParseStrategy.SetupFileDialog(openFileDialog);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                dataTable = ParseStrategy.GetData(filePath);
            }
            dataTable.TableName = openFileDialog.SafeFileName;
            return dataTable;
        }
    }
}

