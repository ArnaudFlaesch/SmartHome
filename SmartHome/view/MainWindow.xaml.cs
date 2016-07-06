using System;
using System.Collections.Generic;
using System.IO;
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
        public ImageBrush myImage { get; set; }
        //public Image myImage2 { get; set; }

        public MainWindow()
        {
            this.model = new MainViewModel();
            this.DataContext = model;
            InitializeComponent();

            try
            {
                myImage = new ImageBrush(new BitmapImage(new Uri(@"../../images/people_icon.png", UriKind.Relative)));
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            
            /*
            myImage = new Image();
            BitmapImage myImageSource = new BitmapImage();
            myImageSource.BeginInit();
            myImageSource.UriSource = new Uri("images/people_icon.png");
            myImageSource.EndInit();
            myImage.Source = myImageSource;*/


            this.initLocationButtons();
        }

        public void initLocationButtons()
        {
            /*
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
                Grid locationButton = createLocationGrid(lieu);
                Grid.SetColumn(locationButton, column);
                Grid.SetRow(locationButton, row);

                if (row == maxRows - 1 && remainingButtons == 1)
                {
                    Grid.SetColumnSpan(locationButton, 3);
                }
                BottomGrid.Children.Add(locationButton);
                row = (column == 2) ? row += 1 : row;
                column = (column == 2) ? column = 0 : column += 1;
            }
        }

        private Grid createLocationGrid(Lieu lieu)
        {
            Grid locationButton = new Grid();
            RowDefinition header = new RowDefinition();
            header.Height = new GridLength(40, GridUnitType.Star);
            RowDefinition body = new RowDefinition();
            body.Height = new GridLength(20, GridUnitType.Star);
            RowDefinition footer = new RowDefinition();
            footer.Height = new GridLength(40, GridUnitType.Star);

            locationButton.RowDefinitions.Add(header);
            locationButton.RowDefinitions.Add(body);
            locationButton.RowDefinitions.Add(footer);

            Grid bodyGrid = new Grid();
            for (int i = 0; i < lieu.capteurList.Count; i++)
            {
                bodyGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Label labelLieu = new Label { Content = lieu.name };
            Grid.SetRow(labelLieu, 0);
            locationButton.Children.Add(labelLieu);
            Grid.SetRow(bodyGrid, 1);
            locationButton.Children.Add(bodyGrid);

            Grid numberOfPeopleGrid = new Grid();
            for (int j = 0; j<3; j++)
            {
                numberOfPeopleGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Label peopleNumber = new Label { Background = myImage };
                peopleNumber.Height = locationButton.Height;
                peopleNumber.Width = myImage.ImageSource.Width;
                Grid.SetColumn(peopleNumber, j);                
            }
            Grid.SetRow(numberOfPeopleGrid, 2);
            locationButton.Children.Add(numberOfPeopleGrid);
            return (locationButton);*/
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