using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class MainViewModel
    {
        public string NameApp { get; set; }
        public DateTime selectedDate { get; set; }
        public OxyPlotGraph oxyplotgraph { get; set; }
        public NetatmoData netatmoData { get; set; }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
            this.netatmoData = new NetatmoData(); // parse tous les fichiers dispo, false/true = pour les logs
            this.selectedDate = netatmoData.start;
            this.oxyplotgraph = new OxyPlotGraph();
            this.oxyplotgraph.createDateGraph();
        }
    }
}
