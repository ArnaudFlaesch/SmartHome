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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*public MainViewModel model
        {
            get; set;
        }*/

        public MainWindow()
        {

            NetatmoData nd = new NetatmoData("../../capteurs.xtim", "../../netatmo", false); // parse tous les fichiers dispo, false/true = pour les logs
            Dictionary<string, Capteur> qzd = nd.getNoEmptyCapteur(false); // Récupère un dictionnaire avec seulement les capteurs contenant des mesures
            nd.getNoEmptyCapteur(true); // renplace le dico interne a nd avec un dico sans capteurs vides
            Console.WriteLine("Première mesure : " + nd.start + "  -  Dernière mesure : " + nd.end);

            nd.getTimeLapsDico(, DateTime.Now);
            InitializeComponent();

            this.LeftGrid.Children.Add(createCalendar(new DateTime(2015, 1, 18), new DateTime(2015, 1, 25)));
        }


        private Calendar createCalendar(DateTime start, DateTime end)
        {
            /*
            calendar.SetBinding(Calendar.DisplayDateProperty, new Binding()
                {
                    Path = new PropertyPath("selectedDate"),
                    Source = calendar.SelectedDate
                });
            */

            Calendar calendar = new Calendar();
            calendar.DisplayDate = start;
            calendar.DisplayDateStart = start;
            calendar.DisplayDateEnd = end;
            calendar.SelectedDatesChanged += dateHasChanged;
            return (calendar);
        }

        private void dateHasChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (DateTime)((Calendar)sender).SelectedDate;
        }

        private void clickOnLocationButton(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Bouton de lieu cliqué");
        }

        private void checkCaptor(object sender, RoutedEventArgs e)
        {

        }

        private void unCheckCaptor(object sender, RoutedEventArgs e)
        {

        }
    }
}