using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using SmartHome.view;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Net.Mail;
using System.Text;
using System.Threading;
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

namespace SmartHome
{

    public partial class MainWindow : Window
    {
        private double defaultSliderValue = 10;
        public MainViewModel model { get; set; }
        public ImageBrush myImage { get; set; }
        //public Image myImage2 { get; set; }

        public MainWindow()
        {
            this.model = new MainViewModel();
            this.DataContext = model;
            InitializeComponent();
            this.AmplitudeSlider.Value = this.defaultSliderValue;

            try
            {
                myImage = new ImageBrush(new BitmapImage(new Uri(@"../../images/people_icon.png", UriKind.Relative)));
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            
        }

        /* EVENTS */
        
        private void dateHasChanged(object sender, SelectionChangedEventArgs e)
        {
            this.model.selectedDate = (DateTime)((Calendar)sender).SelectedDate;
            this.refreshGraph();
        }
        
        private void checkCaptor(object sender, RoutedEventArgs e)
        {
            this.refreshGraph();
            this.AmplitudeSlider.Value = this.defaultSliderValue;
        }

        private void unCheckCaptor(object sender, RoutedEventArgs e)
        {
            this.refreshGraph();
            this.AmplitudeSlider.Value = this.defaultSliderValue;
        }

        private void refreshGraph()
        {
            this.model.oxyplotgraph.Series.Clear();
            List<Capteur> capteurList = new List<Capteur>();
            foreach (Lieu lieu in this.model.netatmoData.locationList)
            {
                foreach (Capteur capteur in lieu.capteurList)
                {
                    if (capteur.isActivated)
                    {
                        this.model.oxyplotgraph.addMesuresFromCapteur(capteur, this.model.selectedDate, (int)this.AmplitudeSlider.Value);
                    }
                }
            }
            this.model.oxyplotgraph.InvalidatePlot(true);
        }

        private void validateSeuil(object sender, RoutedEventArgs e)
        {
            this.model.oxyplotgraph.ajouteSeuil(SeuilTextBox.Text, Int32.Parse(SeuilValue.Text));
        }

        private void OnAmplitudeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.refreshGraph();
        }

        private void exportPNG(object sender, RoutedEventArgs e)
        {
            var pngExporter = new PngExporter { Height = 600, Width = 900, Background = OxyColors.White };
            SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "PNG Files (*.png)|*.png";
            Nullable<bool> result = saveDialog.ShowDialog();
            if (result == true)
            {
                string filename = saveDialog.FileName;
                pngExporter.ExportToFile(this.model.oxyplotgraph, filename);
            }
        }

        private void exportPDF(object sender, RoutedEventArgs e)
        {
            var pdfExporter = new PdfExporter { Width = 600, Height = 900 };
            SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            Nullable<bool> result = saveDialog.ShowDialog();
            if (result == true)
            {
                string filename = saveDialog.FileName;
                var stream = File.Create(filename);
                pdfExporter.Export(this.model.oxyplotgraph, stream);
            }
        }

        private void exportMail(object sender, RoutedEventArgs e)
        {
             MailPopUP inputDialog = new MailPopUP();
             var pngExporter = new PngExporter { Height = 600, Width = 900, Background = OxyColors.White };
             string pathToFile = "../../Data_Oxyplot.png";
             pngExporter.ExportToFile(this.model.oxyplotgraph, pathToFile);
            
             inputDialog.ShowDialog();
            
        }

        private void timeLapse(object sender, RoutedEventArgs e)
        {
            TimelapseModal modal = new TimelapseModal(this);
            modal.Show(); 
        }

        internal void DoPlayTimelapse(int interval, int delta)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                this.model.playTimeLapse(this.model.selectedDate, interval, delta);

            }).Start();
        }
    }
}