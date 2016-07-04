using SmartHome.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class MainViewModel
    {
        public String NameApp
        {
            get; set;
        }

        public List<Lieu> listLocations { get; set; }

        public DateTime selectedDate { get; set; }

        public OxyPlotGraph oxyplotgraph { get; set; }

        public NetatmoData dataCapteurs { get; set; }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
            this.dataCapteurs = new NetatmoData(); // parse tous les fichiers dispo, false/true = pour les logs
            this.dataCapteurs.getNoEmptyCapteur(true); // Récupère un dictionnaire avec seulement les capteurs contenant des mesures
            this.listLocations = Parser.parseLocationsFromXml();
            this.selectedDate = dataCapteurs.start;
            this.oxyplotgraph = new OxyPlotGraph();
            this.oxyplotgraph.createDateGraph();
        }
    }
}
