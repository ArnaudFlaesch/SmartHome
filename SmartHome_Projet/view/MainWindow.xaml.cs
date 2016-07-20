using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Wpf;
using SmartHome.view;
using System;
using System.Collections.Generic;
using System.IO;

using System.Threading;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;


namespace SmartHome
{

    public partial class MainWindow : Window
    {
        private double defaultSliderValue = 10;
        public MainViewModel model { get; set; }

        public MainWindow()
        {
            this.model = new MainViewModel();
            this.DataContext = model;
            InitializeComponent();
            this.AmplitudeSlider.Value = this.defaultSliderValue;           
        }

        /* EVENTS */
        
        private void dateHasChanged(object sender, SelectionChangedEventArgs e)
        {
            this.model.selectedDate = (DateTime)((Calendar)sender).SelectedDate;
            EndCalendar.DisplayDateStart = this.model.selectedDate;
            if (this.model.selectedDate > this.model.selectedDateEnd)
            {
                EndCalendar.DisplayDate = this.model.selectedDate;
                this.model.selectedDateEnd = this.model.selectedDate;
            }
            this.refresh();
        }

        private void dateEndHasChanged(object sender, SelectionChangedEventArgs e)
        {
            this.model.selectedDateEnd = (DateTime)((Calendar)sender).SelectedDate;
            this.refresh();
        }

        private void refresh()
        {
            if (this.model.oxyplotgraph.graphMode == 1)
            {
                this.refreshGraph();
            }
            else if (this.model.oxyplotgraph.graphMode == 2)
            {
                this.refreshBarGraph();
            }
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
            this.model.oxyplotgraph.createDateGraph();
            List<Capteur> capteurList = new List<Capteur>();
            foreach (Lieu lieu in this.model.netatmoData.locationList)
            {
                foreach (Capteur capteur in lieu.capteurList)
                {
                    if (capteur.isActivated)
                    {
                        this.model.oxyplotgraph.addMesuresFromCapteur(capteur, this.model.selectedDate, this.model.selectedDateEnd, (int)this.AmplitudeSlider.Value);
                    }
                }
            }
            this.model.oxyplotgraph.InvalidatePlot(true);
        }

        private void validateSeuil(object sender, RoutedEventArgs e)
        {
            if (SeuilTextBox.Text.Length != 0 && SeuilValue.Text.Length != 0 && this.model.seuilColor != null)
            {
                this.model.oxyplotgraph.ajouteSeuil(SeuilTextBox.Text, Int32.Parse(SeuilValue.Text), this.model.seuilColor);
            }
        }

        private void OnAmplitudeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.refreshGraph();
        }

        private void onSelectedCaptorTypeChanged(object sender, RoutedEventArgs e)
        {
            this.model.selectedTypeCaptor = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString();
            this.refreshBarGraph();
        }

        private void refreshBarGraph()
        {
            this.model.oxyplotgraph.Series.Clear();
            this.model.oxyplotgraph.createBarGraph();
            List<Capteur> capteurList = new List<Capteur>();
            foreach (Lieu lieu in this.model.netatmoData.locationList)
            {
                foreach (Capteur capteur in lieu.capteurList)
                {
                    if (capteur.grandeurNom.Equals(this.model.selectedTypeCaptor))
                    {
                        this.model.oxyplotgraph.addBarDataFromCapteur(capteur, this.model.selectedDate, this.model.selectedDateEnd);
                    }
                }
            }
            this.model.oxyplotgraph.InvalidatePlot(true);
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

        internal void DoPlayTimelapse(int interval, int delta, int waitTime)
        {
            
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

            this.model.playTimeLapse(this.model.selectedDate, this.model.selectedDateEnd, interval, delta, waitTime);

            }).Start();
        }

        private void clearSeuils(object sender, RoutedEventArgs e)
        {
            this.model.oxyplotgraph.Annotations.Clear();
            this.model.oxyplotgraph.InvalidatePlot(true);
        }
    }
}