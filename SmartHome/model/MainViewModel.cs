using SmartHome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartHome
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public void playTimeLapse(DateTime snapDay)
        {
            var tld = this.netatmoData.getTimeLapsDico(snapDay, snapDay.Add(new TimeSpan(24,0,0)));
            var timelapseTest = new TimeLapse(tld);
            Dictionary<string, Mesure> snap = new Dictionary<string, Mesure>();
            while(true)
            {

               
                snap = timelapseTest.executeTimeLapse(tld, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 1, 0), snapDay.Add(new TimeSpan(24, 0, 0)), true);

                if (snap == null)
                    return;
                foreach (var lieu in netatmoData.locationList)
                {
                    foreach(var capteur in lieu.capteurList)
                    {
                        if (snap.ContainsKey(capteur.id))
                        {
                            /*Random r = new Random();
                            int red = r.Next(0, byte.MaxValue + 1);
                            int green = r.Next(0, byte.MaxValue + 1);
                            int blue = r.Next(0, byte.MaxValue + 1);
                            var brush = new SolidColorBrush(Color.FromArgb(100,(byte)red, (byte)green, (byte)blue));

                            capteur.activeMesure = brush;*/
                            capteur.labelMesure = snap[capteur.id].value.ToString();
                            timeLapseNow = snap[capteur.id].date.ToString();
                        }
                            
                    }
                }
                //snap = timelapseTest.executeTimeLapse(tld, new TimeSpan(0, 0, 30, 0), new TimeSpan(0, 1, 0), new DateTime(2014, 02, 5), true);
             

            }


        }

    }
}
