using SmartHome.utils;
using System;
using System.Collections.Generic;

using System.Windows.Media;

namespace SmartHome
{
    public class MainViewModel
    {
        public string NameApp { get; set; }
        public DateTime selectedDate { get; set; }
        public OxyPlotGraph oxyplotgraph { get; set; }
        public NetatmoData netatmoData { get; set; }
        public string timeLaspeDateNow { get; set; }
        public Color seuilColor { get; set; }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
            this.timeLaspeDateNow = "testdate";
            this.netatmoData = new NetatmoData();
            this.selectedDate = netatmoData.start;
            this.oxyplotgraph = new OxyPlotGraph();
        }

        public void playTimeLapse(DateTime snapDay, int interval, int delta)
        {
            var tld = this.netatmoData.getTimeLapsDico(snapDay, snapDay.Add(new TimeSpan(24,0,0)));
            var timelapseTest = new TimeLapse(tld);
            Dictionary<string, Mesure> snap = new Dictionary<string, Mesure>();
            while(true)
            {
                snap = timelapseTest.executeTimeLapse(tld, new TimeSpan(0, 0, interval, 0), new TimeSpan(0, 0, delta, 0), snapDay.Add(new TimeSpan(24, 0, 0)), true);
                if (snap == null)
                    return;
                foreach (var lieu in netatmoData.locationList)
                {
                    foreach(var capteur in lieu.capteurList)
                    {
                        if (snap.ContainsKey(capteur.id))
                        {
                            capteur.labelMesure = snap[capteur.id].value.ToString();
                            capteur.activeMesureColor = DataToColorConverter.fromDataToColor(snap[capteur.id].value, capteur.grandeurNom);
                        }
                    }
                }
            }
        }
    }    
}
