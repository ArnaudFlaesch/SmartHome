using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.Axes.Clear();
            this.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm", IntervalType = DateTimeIntervalType.Hours });
            this.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            this.InvalidatePlot(true);
        }

        public void createBarGraph()
        {
            this.Axes.Clear();
            //this.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm", IntervalType = DateTimeIntervalType.Hours });
            this.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            this.InvalidatePlot(true);
        }

        public void addMesuresFromCapteur(Capteur capteur, DateTime selectedDate)
        {
            DateTime end = selectedDate.AddDays(1);
            end = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);
            List<Mesure> mesures = capteur.mesureList.FindAll(mesure => mesure.date >= selectedDate && mesure.date < end);
            LineSeries serie = new LineSeries { Title = capteur.description };
            foreach (Mesure mesure in mesures)
            {
                serie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(mesure.date), mesure.value));
            }
            this.Series.Add(serie);
        }


        public void ajouteSeuil(string name, int value)
        {
            this.Annotations.Add(new RectangleAnnotation { MinimumY = value, MaximumY = value+100, Fill = OxyColors.Red, Text = name });
            this.InvalidatePlot(true);
        }
    }
}
