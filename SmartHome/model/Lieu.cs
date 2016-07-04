using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class Lieu : IEquatable<Lieu>
    {
        public Lieu()
        {
            this.listIdCapteur = new List<String>();
        }

        public int id
        {
            get; set;
        }

        public String name
        {
            get; set;
        }

        public List<String> listIdCapteur
        {
            get; set;
        }

        public bool Equals(Lieu other)
        {
            if (other == null) return false;
            return (this.name.Equals(other.name));
        }
    }
}
