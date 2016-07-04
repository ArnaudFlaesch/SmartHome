using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartHome.utils
{
    public class Parser
    {
        private static String pathToXmlFile = "../../capteurs.xtim";
        private static String pathToDataFolder = "../../netatmo";

        public static List<Lieu> parseLocationsFromXml()
        {
            List<Lieu> listLocations = new List<Lieu>();

            XmlDocument doc = new XmlDocument();
            doc.Load(pathToXmlFile);
            XmlNodeList capteursListXML = doc.SelectSingleNode("capteurs").ChildNodes;

            for (int i = 0; i < capteursListXML.Count; i++)
            {
                XmlNode capteur = capteursListXML[i];
                if (!listLocations.Contains(new Lieu() { name = capteur.SelectSingleNode("lieu").InnerXml } ))
                {
                    listLocations.Add(new Lieu() { name = capteur.SelectSingleNode("lieu").InnerXml } );
                }
            }
            return (listLocations);
        }
    }
}
