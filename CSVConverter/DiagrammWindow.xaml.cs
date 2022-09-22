
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace CSVConverter
{
    /// <summary>
    /// Логика взаимодействия для DiagrammWindow.xaml
    /// </summary>
    public partial class DiagrammWindow : Window
    {
        private DataTable dataTable;
        public DiagrammWindow(DataTable data)
        {
            InitializeComponent();

            dataTable = data;
            chart.ChartAreas.Add(new ChartArea("defaultArea"));
            chart.Series.Add(new Series("Series"));
            
            chart.Series["Series"].ChartArea = "defaultArea";
            chart.Series["Series"].ChartType = SeriesChartType.Column;
            chart.Series["Series"].IsValueShownAsLabel = true;
            
            chart.ChartAreas["defaultArea"].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas["defaultArea"].AxisY.MajorGrid.Enabled = false;
            chart.ChartAreas["defaultArea"].AxisX.Interval = 1;
           
            string[] countries = data.AsEnumerable().Select(r => r[8].ToString()).Distinct().ToArray();
            
            int[] count = new int[countries.Length];
            for (int index=0; index < countries.Length; index++)
            {
                count[index] = data.AsEnumerable().Where(r => r[8].ToString()
                                                                  .Equals(countries[index])).Count();
            }
            
            chart.Series["Series"].Points.DataBindXY(countries, count);
            
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            Exporter exporter = new Exporter();
            exporter.ExportStrategy = new ExcelExporter();
            exporter.Export(dataTable);
        }
    }
}
