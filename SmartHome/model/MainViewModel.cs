using SmartHome;
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
        public string uriSendMessage { get; set; }
        public DateTime selectedDate { get; set; }
        public OxyPlotGraph oxyplotgraph { get; set; }
        public NetatmoData netatmoData { get; set; }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
            this.netatmoData = new NetatmoData(); // parse tous les fichiers dispo, false/true = pour les logs
            try
            {
                this.uriSendMessage = "mailto:?subject=Smarthome";
            }
            catch (UriFormatException error) { Console.WriteLine(error); }
            
            //----------- Partie TimeLapse
            /*
            var tld = this.netatmoData.getTimeLapsDico(new DateTime(2014, 02, 01), new DateTime(2014, 02, 14));
            var timelapseTest = new TimeLapse(tld);
            for(int i = 0; i < 10; i++)
            {
                timelapseTest.executeTimeLapse(tld, new TimeSpan(0, 0, 5, 0), new TimeSpan(0, 1, 0),true);
                Console.WriteLine("--------------------------------------------");
            }
            */
            //-----------------------------------

            this.selectedDate = netatmoData.start;
            this.oxyplotgraph = new OxyPlotGraph();
        }

        public static void displayLocationColors(double value, string capteurId)
        {
            string[] tmp = value.ToString().Split(',');
            double mesure = Convert.ToDouble(tmp[0]);
            Console.WriteLine(mesure);
            Console.WriteLine(capteurId);
        }
    }
}
