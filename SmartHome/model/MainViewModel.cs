using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class MainViewModel
    {
        public String NameApp
        {
            get; set;
        }

        public List<Lieu> listLocations
        {
            get; set;
        }

        public OxyPlotGraph oxyplotgraph
        {
            get; set;
        }

        public MainViewModel()
        {
            this.NameApp = "SmartHome";
        }
    }
}
