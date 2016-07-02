using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    //Classe portant les capteurs parsé dans CapteurParseur.
    class Capteur
    {
        public string id { get; set; }
        public string description { get; set; }
        public string lieu { get; set; }
        public string unite { get; set; }
        List<Mesure> mesures;
        public Capteur(string id, string description, string lieu, string unite)
        {
            this.id = id;
            this.description = description;
            this.lieu = lieu;
            this.unite = unite;
            this.mesures = new List<Mesure>();
        }

        public void addMesure(Mesure mesure)
        {
            this.mesures.Add(mesure);
        }
        public List<Mesure> getMesures()
        {
            return this.mesures;
        }

        public void setMesures(List<Mesure> list)
        {
            this.mesures = list;
        }

    }
}