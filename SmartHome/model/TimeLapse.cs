using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    class TimeLapse
    {
        public DateTime firstDate { get; set; }
        private int numIteration = 1;

        public TimeLapse(Dictionary<string, Capteur> dico)
        {
            firstDate = DateTime.Now;

            foreach (KeyValuePair<string, Capteur> capteur in dico)
            {
                if ((capteur.Value.getMesures().Count() == 0))
                    continue;
                if (firstDate.CompareTo(capteur.Value.getMesures()[0].date) > 0)
                    firstDate = capteur.Value.getMesures()[0].date;
            }

        }


        public Dictionary<string, Mesure> executeTimeLapse(Dictionary<string, Capteur> dico, TimeSpan interval, TimeSpan deltaTime, bool debug=false)
        {
            var snapShotDico = new Dictionary<string, Mesure>();
            var curseurDate = firstDate.Add(TimeSpan.FromTicks(interval.Ticks * numIteration));

            if (debug)
                Console.WriteLine("********* Curseur: " + curseurDate + " / Interval: "+ interval+" / DeltaTime: "+deltaTime+" **********");

            foreach (KeyValuePair<string, Capteur> capteur in dico)
            {
                foreach(Mesure mDate in capteur.Value.getMesures())
                {
                    
                    if (curseurDate.CompareTo(mDate.date.Add(-deltaTime)) > 0 && curseurDate.CompareTo(mDate.date.Add(deltaTime)) < 0)
                    {
                        if(debug)
                            Console.WriteLine("{0} --- {1} --- {2}",mDate.date, capteur.Key, mDate.value);
                        snapShotDico.Add(capteur.Key, mDate);
                        break;
                    }
                }

            }
            numIteration++;
            return snapShotDico;
        }

    }
}
