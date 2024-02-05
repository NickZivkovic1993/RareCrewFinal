using Lib.Helpers;
using Lib.Models;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static WpfChartFinal.ChartHandler;

/// Izvinjavam se za kranje neuredan kod bio sam malo pd stresom , mozete pogledati moje druge projekte na githubu da vidite kako zapravo kucam
/// jos jednom izvinite pogledao sam opet , i ja se jedva snalazim


namespace WpfChartFinal
{
    public partial class MainWindow : Window
    {
        private LoaderClass loaderClass;

        private const string putanja = @"D:\ChartPng.png";

        public MainWindow()
        {
            InitializeComponent();

            loaderClass = new LoaderClass();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAndSavePieChart();
        }

        private async Task InitializeAndSavePieChart()
        {
            await InitializeAsync();
            
        }

        private async Task InitializeAsync()
        {
            List<RadnikModel> radnici = await loaderClass.Loader();

            SeriesCollection serija = new();

            foreach (var radnik in radnici)
            {
                serija.Add(new PieSeries
                {
                    Title = radnik.EmployeeName,
                    Values = new ChartValues<double> { radnik.TotalHoursWorked },
                });
            }

            await Dispatcher.Invoke(async () =>
            {
                pieChart.Series = serija;

                await Task.Delay(100); // Adjust the delay as needed
            });

            GenerateChartPng(
            radnici,
            putanja,
            new(1920, 1080));
        }

        

        
        }

    }




