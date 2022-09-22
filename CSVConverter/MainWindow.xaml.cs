using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSVConverter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileParser fileParser;
        private FileSaver fileSaver;
        private Exporter exporter;
        private DataTable dataTable;
        //private 

        public MainWindow()
        {
            InitializeComponent();
            fileParser = new FileParser();
            fileSaver = new FileSaver();
            exporter = new Exporter();
        }

        private void OpenCSVFile(object sender, RoutedEventArgs e)
        {
            fileParser.ParseStrategy = new CsvParser();
            dataTable = fileParser.GetData();
            dataGrid.ItemsSource = dataTable.DefaultView;
            EnableButton();
        }

        private void OpenXMLFile(object sender, RoutedEventArgs e)
        {
            fileParser.ParseStrategy = new XmlParser();
            dataTable = fileParser.GetData();
            dataGrid.ItemsSource = dataTable.DefaultView;
            EnableButton();
        }

        private void OpenJSONFile(object sender, RoutedEventArgs e)
        {
            fileParser.ParseStrategy = new JsonParser();
            dataTable = fileParser.GetData();
            dataGrid.ItemsSource = dataTable.DefaultView;
            EnableButton();
        }
        
        private void SaveCSVFile(object sender, RoutedEventArgs e)
        {
            fileSaver.SaveStrategy = new CsvSaver();
            fileSaver.Save(dataTable);
        }

        private void SaveXMLFile(object sender, RoutedEventArgs e)
        {
            fileSaver.SaveStrategy = new XmlSaver();
            fileSaver.Save(dataTable);
        }

        private void SaveJSONFile(object sender, RoutedEventArgs e)
        {
            fileSaver.SaveStrategy = new JsonSaver();
            fileSaver.Save(dataTable);
        }
        
        private void OpenDiagramm(object sender, RoutedEventArgs e)
        {
            DiagrammWindow diagrammWindow = new DiagrammWindow(dataTable);
            diagrammWindow.Title = "Diagramm";
            diagrammWindow.Show();
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            exporter.ExportStrategy = new ExcelExporter();
            exporter.Export(dataTable);
        }

        private void ExportToWord(object sender, RoutedEventArgs e)
        {
            exporter.ExportStrategy = new WordExporter();
            exporter.Export(dataTable);
        }

        private void EnableButton()
        {
            IEnumerable<Button> buttons = btnStack.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
        }
    }
}
