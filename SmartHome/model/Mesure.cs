using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class Mesure
    {
        public DateTime date
        {
            get; set;
        }

        public Double value
        {
            get; set;
        }

        public Mesure(DateTime date, Double value)
        {
            this.date = date;
            this.value = value;
        }
    }
}
