
using SmartHome.utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media;

namespace SmartHome
{
    public class MainViewModel
    {
        public string NameApp { get; set; }
        public DateTime selectedDate { get; set; }
        public DateTime selectedDateEnd { get; set; }
        public string selectedTypeCaptor { get; set; }
        public OxyPlotGraph oxyplotgraph { get; set; }
        public NetatmoData netatmoData { get; set; }
        public DateTimeLapse timeLaspeDateNow { get; set; }
        public Color seuilColor { get; set; }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
            this.timeLaspeDateNow = new DateTimeLapse();
            this.netatmoData = new NetatmoData();
            this.selectedDate = netatmoData.start;
            this.selectedDateEnd = netatmoData.start;
            this.oxyplotgraph = new OxyPlotGraph();
        }

        public void playTimeLapse(DateTime snapDay, DateTime snapEnd, int interval, int delta, int waitTime)
        {
            snapEnd = snapEnd.AddDays(1);
            snapEnd = new DateTime(snapEnd.Year, snapEnd.Month, snapEnd.Day, 0, 0, 0);
            var tld = this.netatmoData.getTimeLapsDico(snapDay, snapEnd);
            var timelapseTest = new TimeLapse(tld);
            Dictionary<string, Mesure> snap = new Dictionary<string, Mesure>();
            while(true)
            {
                snap = timelapseTest.executeTimeLapse(tld, new TimeSpan(0, 0, interval, 0), new TimeSpan(0, 0, delta, 0), snapEnd, true);
                if (snap == null)
                    break;
                foreach (var lieu in netatmoData.locationList)
                {
                    foreach(var capteur in lieu.capteurList)
                    {

                        if (snap.ContainsKey(capteur.id))
                        {
                            if (capteur.grandeurNom == "Co2")
                                lieu.concentrationC02 = "Présence : "+((int)snap[capteur.id].value/450).ToString();
                            capteur.labelMesure = snap[capteur.id].value.ToString();
                            timeLaspeDateNow.dateTimeLapse = snap[capteur.id].date.ToString();
                            capteur.activeMesureColor = DataToColorConverter.fromDataToColor(snap[capteur.id].value, capteur.grandeurNom);
                        }
                    }
                }
                
                Thread.Sleep(waitTime);
            }

            timeLaspeDateNow.dateTimeLapse = "Lancer le TimeLapse";
        }
    }    
}
