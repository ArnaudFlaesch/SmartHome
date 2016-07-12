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
            LineSeries serie = new LineSeries { Title = capteur.description, Tag = capteur.id };
            foreach (Mesure mesure in mesures)
            {
                serie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(mesure.date), mesure.value));
            }
            this.Series.Add(serie);
            serie.MouseDown += (s, e) =>
            {
                /*
                double coordX = ((LineSeries)s).InverseTransform(e.Position).X;
                Console.WriteLine(coordX);
                foreach (DataPoint point in serie.Points)
                {
                    if (point.X == coordX)
                    {
                        Console.WriteLine(point.Y);
                    }
                    
                }
                */
                foreach (LineSeries displayedSerie in serie.PlotModel.Series)
                {
                    // Avec Linq, chercher le DataPoint ayant le point X le plus proche (between X > 100 & X < 100 par exemple

                    //double value = displayedSerie.InverseTransform(e.Position).Y;
                    //MainViewModel.displayLocationColors(value, displayedSerie.Tag.ToString());
                }
                
            };
        }

        public void ajouteSeuil(string name, int value)
        {
            this.Annotations.Add(new RectangleAnnotation { MinimumY = value, Fill = OxyColors.Red, Text = name });
            this.InvalidatePlot(true);
        }
    }
}
