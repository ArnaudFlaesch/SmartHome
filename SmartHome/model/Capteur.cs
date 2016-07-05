using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    //Classe portant les capteurs parsé dans CapteurParseur.
    public class Capteur
    {
        public string id { get; set; }
        public string description { get; set; }
        public string lieu { get; set; }
        public string unite { get; set; }
        public List<Mesure> mesureList;

        public Capteur(string id, string description, string lieu, string unite)
        {
            this.id = id;
            this.description = description;
            this.lieu = lieu;
            this.unite = unite;
            this.mesureList = new List<Mesure>();
        }

        public void addMesure(Mesure mesure)
        {
            this.mesureList.Add(mesure);
        }
        public List<Mesure> getMesures()
        {
            return this.mesureList;
        }

    }
}