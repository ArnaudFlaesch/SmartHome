using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    class Mesure
    {
        public double value { get; set; }
        public DateTime date { get; set; }

        public Mesure(string value, string date)
        {
            this.value = Convert.ToDouble(value);
            date = date.Trim('"');
            this.date = DateTime.Parse(date);

            //Console.WriteLine("Date Parsing : " + date + " --> " + this.date);
        }

    }
}
