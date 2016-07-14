using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class Mesure
    {
        public double value { get; set; }
        public DateTime date { get; set; }
        public double initial_value { get; set; }

        public Mesure(string value, string date)
        {
            this.value = Convert.ToDouble(value);
            this.initial_value = this.value;
            date = date.Trim('"');
            this.date = DateTime.Parse(date);
        }

        public Mesure(Mesure mesure)
        {
            this.value = mesure.value;
            this.date = mesure.date;
            this.initial_value = this.value;
        }

    }
}
