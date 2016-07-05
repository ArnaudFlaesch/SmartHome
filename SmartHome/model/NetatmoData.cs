using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartHome
{
    public class NetatmoData
    {
        private string pathToXmlFile = "../../capteurs.xtim";
        private string pathToDataFolder = "../../netatmo";

        public List<Lieu> locationList { get; set; }
        public Dictionary<string, Capteur> capteursDico { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public NetatmoData()
        {
            capteursDico = new Dictionary<string, Capteur>();
            locationList = CapteurParseur(pathToXmlFile);
            MesureParseur(pathToDataFolder);
            capteursDico = getNoEmptyCapteur();
        }


  /*      public Dictionary<string, Mesure> timeLaps(Dictionary<string, Capteur> dico, TimeSpan interval, TimeSpan deltaTime, int numIteration)
        {
            DateTime snapDate = DateTime.Now;

            foreach (KeyValuePair<string, Capteur> capteur in dico)
            {
                if (snapDate.CompareTo(capteur.Value.getMesures()[0].date) < 0)
                    snapDate = capteur.Value.getMesures()[0].date;
            }

            var snapShotDico = new Dictionary<string, Mesure>();
            foreach (KeyValuePair<string, Capteur> capteur in dico)
            {
                if (dateTmp.CompareTo(begin) > 0 && dateTmp.CompareTo(end) < 0)
                {

                }
            }

            return snapShotDico;
        }*/


        public Dictionary<string, Capteur> getTimeLapsDico(DateTime begin, DateTime end)
        {
            Dictionary<string, Capteur> timeLaps = new Dictionary<string, Capteur>();
            foreach (KeyValuePair<string, Capteur> capteur in capteursDico)
            {
                var listMesure = capteur.Value.getMesures();
                var newCapteur = new Capteur(capteur.Value);
                timeLaps.Add(capteur.Value.id, newCapteur);
                List<Mesure> tmpList = new List<Mesure>(); ;
                DateTime dateTmp;

                foreach (var mesure in listMesure)
                {
                    dateTmp = mesure.date;
                    if (dateTmp.CompareTo(begin) > 0 && dateTmp.CompareTo(end) < 0)
                    {
                        //Console.WriteLine("Date Entre deux " + dateTmp.CompareTo(begin));
                        tmpList.Add(mesure);
                    }
                }

                newCapteur.setMesures(tmpList);
            }
            return timeLaps;
        }

        private List<Lieu> CapteurParseur(String path)
        {
            List<Lieu> listLocations = new List<Lieu>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList xnList = xmlDoc.SelectNodes("/capteurs/capteur");
            foreach (XmlNode xn in xnList)
            {
                if (xn.Attributes["type"].Value == "mesure")
                {
                    string id = xn["id"].InnerText;
                    string description = xn["description"].InnerText;
                    string place = xn["lieu"].InnerText;
                    string unite = xn["grandeur"].Attributes["abreviation"].Value;

                    if (!listLocations.Contains(new Lieu() { name = place }))
                    {
                        listLocations.Add(new Lieu() { name = place });
                    }

                    capteursDico.Add(id, new Capteur(id, description, place, unite));
                }
            }
            return (listLocations);
        }

        private void MesureParseur(string path)
        {
            foreach (string file in Directory.GetFiles(@path))
            {
                foreach (string line in File.ReadLines(file))
                {
                    string[] splitLine = line.Split(' ');
                    if (capteursDico.ContainsKey(splitLine[2]))
                    {
                        capteursDico[splitLine[2]].addMesure(new Mesure(splitLine[3], splitLine[0] + " " + splitLine[1]));
                        DateTime tmpDT = capteursDico[splitLine[2]].getMesures()[capteursDico[splitLine[2]].getMesures().Count - 1].date;
                        if (DateTime.Compare(start, tmpDT) > 0 || start == default(DateTime))
                        {
                            start = tmpDT;
                        }
                        else if (DateTime.Compare(end, tmpDT) < 0)
                        {
                            end = tmpDT;
                        }
                    }
                }
            }
        }

        private Dictionary<string, Capteur> getNoEmptyCapteur()
        {
            Dictionary<string, Capteur> noEmptyCapteur = new Dictionary<string, Capteur>();
            foreach (KeyValuePair<string, Capteur> capteur in capteursDico)
            {
                if (capteur.Value.getMesures().Count != 0)
                {
                    noEmptyCapteur.Add(capteur.Value.id, capteur.Value);
                }
            }
            return noEmptyCapteur;
        }
    }
}