using System;
using System.Data;
using System.Windows.Forms;

namespace CSVConverter
{
    internal class FileSaver
    {
        private SaveFileDialog saveFileDialog;
        public ISaver SaveStrategy { private get; set; }

        public FileSaver()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.AddExtension = true;
        }

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
