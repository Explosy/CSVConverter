using System;
using System.Data;
using System.Windows.Forms;

namespace CSVConverter
{
    /// <summary>
    /// Класс реализующий сохранение файла.
    /// </summary>
    internal class FileSaver
    {
        private SaveFileDialog saveFileDialog;
        /// <summary>
        /// Поле, содержащее текущую стратегию сохранения файла.
        /// </summary>
        public ISaver SaveStrategy { private get; set; }

        public FileSaver()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.AddExtension = true;
        }
        /// <summary>
        /// Метод, создающий диалоговое окно сохранения, а также запускающий реализацию текущей стратегии сохранения из SaveStrategy.
        /// </summary>
        /// <param name="data"></param>
        public void Save (DataTable data)
        {
            SaveStrategy.SetupFileDialog(saveFileDialog);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                SaveStrategy.Save(data, filePath);
            }
        }
    }

    
}
