using System;
using System.Collections.Generic;
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

namespace SmartHome
{

    public partial class MainWindow : Window
    {
        public MainViewModel model { get; set; }

        public MainWindow()
        {
            this.model = new MainViewModel();
            this.DataContext = model;
            InitializeComponent();
            this.initLocationButtons();
        }

        public void initLocationButtons()
        {
            int row = 0, column = 0;
            int maxRows = model.netatmoData.locationList.Count / 3;
            int remainingButtons = model.netatmoData.locationList.Count % 3;
            if (remainingButtons != 0)
            {
                maxRows++;
            }
            
            for (int i = 0; i<maxRows; i++)
            {
                BottomGrid.RowDefinitions.Add(new RowDefinition());
            }

            foreach (Lieu lieu in model.netatmoData.locationList)
            {
                Button locationButton = new Button();
                locationButton.Content = lieu.name;
                Grid.SetColumn(locationButton, column);
                Grid.SetRow(locationButton, row);
                //locationButton.Click += clickOnLocationButton;
                
                if (row == maxRows - 1 && remainingButtons == 1)
                {
                    Grid.SetColumnSpan(locationButton, 3);
                }
                BottomGrid.Children.Add(locationButton);
                row = (column == 2) ? row+=1 : row;
                column = (column == 2) ? column = 0 : column+=1;
            }
        }


        /* EVENTS */

        private void dateHasChanged(object sender, SelectionChangedEventArgs e)
        {
            this.model.selectedDate = (DateTime)((Calendar)sender).SelectedDate;
            this.refreshGraph();
        }

        /*
        private void clickOnLocationButton(object sender, RoutedEventArgs e)
        {
            String locationName = ((Button)sender).Content.ToString();
            //this.model.netatmoData.getCapteursIdFromLocationName(locationName);
        }
        */

        
        private void checkCaptor(object sender, RoutedEventArgs e)
        {
            this.refreshGraph();
        }

        private void unCheckCaptor(object sender, RoutedEventArgs e)
        {
            this.refreshGraph();
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
                        this.model.oxyplotgraph.addMesuresFromCapteur(capteur, this.model.selectedDate);
                    }
                }
            }
            this.model.oxyplotgraph.InvalidatePlot(true);
        }

        private void validateSeuil(object sender, RoutedEventArgs e)
        {
            this.model.oxyplotgraph.ajouteSeuil(SeuilTextBox.Text, Int32.Parse(SeuilValue.Text));
        }
    }
}