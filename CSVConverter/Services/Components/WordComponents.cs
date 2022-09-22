using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace CSVConverter.Services.WordComponents
{
    class WordComponents
    {
        public Word.Document CreateNewDoc()
        {
            Word.Application application = new Word.Application();
            application.Visible = true;
            var document = application.Documents.Add();
            return document;
        }

        public void AddTitle(string title, Word.Document document)
        {
            //Создание параграфа в конце документа
            object oMissing = System.Reflection.Missing.Value;
            document.Paragraphs.Add(ref oMissing);
            //Выбор последнего параграфа
            Word.Paragraph paragraph = document.Paragraphs[document.Paragraphs.Count];
            //Word.Paragraph paragraph = document.Paragraphs[1];
            //Настройка свойств параграфа
            object styleHeading = "Заголовок 1";
            paragraph.Range.set_Style(styleHeading);
            paragraph.Range.Font.Size = 20;
            paragraph.Range.Font.Name = "Times New Roman";
            paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //Вставка текста в параграф
            paragraph.Range.Text = title;
        }

        public void AddText(string text, Word.Document document)
        {
            //Создание параграфа в конце документа
            object oMissing = System.Reflection.Missing.Value;
            document.Paragraphs.Add(ref oMissing);
            //Выбор последнего параграфа
            Word.Paragraph paragraph = document.Paragraphs[document.Paragraphs.Count];
            //Настройка свойств параграфа
            paragraph.Range.Font.Size = 14;
            paragraph.Range.Font.Name = "Times New Roman";
            //Вставка текста в параграф
            paragraph.Range.Text = text;
        }

        public void AddTable(DataTable dataTable, int tableRowCount, Word.Document document)
        {
            //Создание параграфа в документе и его настройка
            object oMissing = System.Reflection.Missing.Value;
            document.Paragraphs.Add(ref oMissing);
            Word.Paragraph paragraph = document.Paragraphs[document.Paragraphs.Count];
            paragraph.Range.Font.Size = 12;

            int tableColumnCount = dataTable.Columns.Count;
            //Создание таблицы заданного размера
            Word.Table wordTable = document.Tables.Add(paragraph.Range, tableRowCount, tableColumnCount,
                                                        Word.WdDefaultTableBehavior.wdWord8TableBehavior,
                                                        Word.WdAutoFitBehavior.wdAutoFitWindow);

            int iTableRow = 1;
            int iTableCol = 1;
            
            //Заполнение шапки таблицы
            for (int columnIndex = 0; columnIndex < tableColumnCount; columnIndex++)
            {
                wordTable.Cell(iTableRow, iTableCol).Range.Text = dataTable.Columns[columnIndex].ColumnName;
                iTableCol++;
            }
            iTableRow++;
            //Заполнение таблицы данными
            for (int rowIndex = 0; rowIndex < tableRowCount; rowIndex++)
            {
                iTableCol = 1;
                for (int columnIndex = 0; columnIndex < tableColumnCount; columnIndex++)
                {
                    wordTable.Cell(iTableRow, iTableCol).Range.Text = dataTable.Rows[rowIndex][columnIndex].ToString();
                    iTableCol++;
                }
                iTableRow++;
            }
            
            wordTable.Borders.Enable = 1;
        }
    }
}
