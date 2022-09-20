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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                dataTable = fileParser.GetDataFromCSV(openFileDialog.FileName);
            }
            dataTable.TableName = openFileDialog.SafeFileName;
            dataGrid.ItemsSource = dataTable.DefaultView;
            IEnumerable<Button> buttons = btnStack.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
        }

        private void OpenXMLFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                dataTable = fileParser.GetDataFromXML(openFileDialog.FileName);
            }
            dataTable.TableName = openFileDialog.SafeFileName;
            dataGrid.ItemsSource = dataTable.DefaultView;
            IEnumerable<Button> buttons = btnStack.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
        }

        private void OpenJSONFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                dataTable = fileParser.GetDataFromJSON(openFileDialog.FileName);
            }
            dataTable.TableName = openFileDialog.SafeFileName;
            dataGrid.ItemsSource = dataTable.DefaultView;
            IEnumerable<Button> buttons = btnStack.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
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
    }
}
