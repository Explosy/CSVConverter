using System;
using System.Data;
using System.Windows.Forms;

namespace CSVConverter
{
    internal class FileParser
    {
        private OpenFileDialog openFileDialog;
        public IParser ParseStrategy { private get; set; }

        public FileParser()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
        }

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

