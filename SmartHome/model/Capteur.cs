using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class Capteur
    {
        public String id
        {
            get; set;
        }

        public String description
        {
            get; set;
        }

        public List<Mesure> listMesures
        {
            get; set;
        }

        public Capteur(String id, String description)
        {
            this.id = id;
            this.description = description;
        }
    }
}