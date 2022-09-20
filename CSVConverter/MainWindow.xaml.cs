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
        private DataTable dataTable;

        public MainWindow()
        {
            InitializeComponent();
            fileParser = new FileParser();
            fileSaver = new FileSaver();
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
