using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartHome
{
    class NetatmoData
    {
        Dictionary<string, Capteur> capteursList { get; set; }
        Boolean debug;
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public NetatmoData(string pathCapteurs, string pathMesure, Boolean debug)
        {
            this.debug = debug;
            capteursList = new Dictionary<string, Capteur>();
            CapteurParseur(pathCapteurs);
            MesureParseur(pathMesure);
        }

        private void MesureParseur(string path)
        {
            if (debug)
            {
                Console.WriteLine("--------------- MESURES -------------------");
            }

            foreach (string file in Directory.GetFiles(@path))
            {
                if (debug)
                    Console.WriteLine("Fichier de mesure : " + file);
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
            if (debug)
                Console.WriteLine("---------------------------------------");
        }

        private void CapteurParseur(String path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            if (debug)
                Console.WriteLine("------------ CAPTEURS ------------------");
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

                    if (debug)
                        Console.WriteLine("Capteur : {0} || {1} || {2} || {3}", id, description, place, xn["grandeur"].Attributes["abreviation"].Value);
                }
            }
            if (debug)
                Console.WriteLine("---------------------------------------");
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

    }

}
