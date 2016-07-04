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
        private static String pathToXmlFile = "../../capteurs.xtim";
        private static String pathToDataFolder = "../../netatmo";

        Dictionary<string, Capteur> capteursList { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public NetatmoData()
        {
            capteursList = new Dictionary<string, Capteur>();
            CapteurParseur(pathToXmlFile);
            MesureParseur(pathToDataFolder);
        }

        private void MesureParseur(string path)
        {
            foreach (string file in Directory.GetFiles(@path))
            {
                foreach (string line in File.ReadLines(file))
                {
                    string[] splitLine = line.Split(' ');
                    if (capteursList.ContainsKey(splitLine[2]))
                    {
                        capteursList[splitLine[2]].
                           addMesure(new Mesure(splitLine[3], splitLine[0] + " " + splitLine[1]));
                        DateTime tmpDT = capteursList[splitLine[2]].getMesures()[capteursList[splitLine[2]].getMesures().Count - 1].date;
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

        private void CapteurParseur(String path)
        {
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

                    capteursList.Add(id, new Capteur(id, description, place, unite));
                }
            }
        }

        public Dictionary<string, Capteur> getNoEmptyCapteur(bool replace)
        {
            Dictionary<string, Capteur> noEmptyCapteur = new Dictionary<string, Capteur>();
            foreach (KeyValuePair<string, Capteur> capteur in capteursList)
            {
                if (capteur.Value.getMesures().Count != 0)
                {
                    noEmptyCapteur.Add(capteur.Value.id, capteur.Value);
                }
            }
            if (replace)
            {
                capteursList = noEmptyCapteur;
                return null;
            }
            else
            {
                return noEmptyCapteur;
            }
        }

        public void displayIdCapteurs(string lieu)
        {
            foreach(KeyValuePair<string, Capteur> capteur in capteursList)
            {
                if (capteur.Value.lieu.Equals(lieu))
                {
                    Console.WriteLine(capteur.Value.description);
                }
            }
        }

    }

}
