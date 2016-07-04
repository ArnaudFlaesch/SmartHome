using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class OxyPlotGraph : PlotModel
    {

        public OxyPlotGraph()
        {
            this.Title = "SmartHome";
            this.createDateGraph();
        }

        public void createDateGraph()
        {
            this.InvalidatePlot(true);
            this.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm", IntervalType = DateTimeIntervalType.Hours });
            this.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
        }

        public void createBarGraph()
        {
            this.InvalidatePlot(true);
            //this.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm", IntervalType = DateTimeIntervalType.Hours });
            this.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
        }

        public void ajouteSeuil()
        {
            this.Annotations.Add(new LineAnnotation { Type = LineAnnotationType.Vertical, X = 100, /*MaximumY = 4,*/ Color = OxyColors.Gold, Text = "Seuil" });
        }
    }
}
