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

            locationList = parseLocationsFromXml(pathToXmlFile);
            CapteurParseur(pathToXmlFile);
            MesureParseur(pathToDataFolder);
            capteursDico = getNoEmptyCapteur();

            foreach (KeyValuePair<string, Capteur> capteur in capteursDico)
            {
                foreach (Lieu lieu in locationList)
                {
                    if (lieu.name.Equals(capteur.Value.lieu))
                    {
                        lieu.capteurList.Add(capteur.Value);
                        break;
                    }
                }
            }
            capteursDico = null;
        }

        private List<Lieu> parseLocationsFromXml(string path)
        {
            List<Lieu> listLocations = new List<Lieu>();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList capteursListXML = doc.SelectSingleNode("capteurs").ChildNodes;

            for (int i = 0; i < capteursListXML.Count; i++)
            {
                XmlNode capteur = capteursListXML[i];
                if (!listLocations.Contains(new Lieu() { name = capteur.SelectSingleNode("lieu").InnerXml }))
                {
                    listLocations.Add(new Lieu() { name = capteur.SelectSingleNode("lieu").InnerXml });
                }
            }
            return (listLocations);
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

                    capteursDico.Add(id, new Capteur(id, description, place, unite));
                }
            }
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