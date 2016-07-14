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
using System.Windows.Media;

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

        public void addMesuresFromCapteur(Capteur capteur, DateTime selectedDate, int amplitude)
        {
            DateTime end = selectedDate.AddDays(1);
            end = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);
            List<Mesure> mesures = capteur.mesureList.FindAll(mesure => mesure.date >= selectedDate && mesure.date < end);
            mesures.ForEach(x => x.value = x.initial_value);

            //---------- Fonction de Moyenne locales : Deux arguement -> La liste de mesure / le nombre de points a prendre (a gauche et a droite du point courant) pour calculer la moyenne
            transfoMoyennelocal(mesures, amplitude);

            LineSeries serie = new LineSeries { Title = capteur.description, Tag = capteur.id };
            foreach (Mesure mesure in mesures)
            {
                serie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(mesure.date), mesure.value));
            }
            this.Series.Add(serie);
        }

        public void ajouteSeuil(string name, int value, Color seuilColor)
        {
            this.Annotations.Add(new RectangleAnnotation { MinimumY = value, MaximumY = value + 100, Fill = OxyColor.FromAColor(50, OxyColor.FromRgb(seuilColor.R, seuilColor.G, seuilColor.B)), Text = name });
            this.InvalidatePlot(true);
        }

        private void transfoMoyennelocal(List<Mesure> mesures, int amplitude)
        {

            int tmpAmplitude;
            for (int i = 0; i < mesures.Count; i++)
            {
                tmpAmplitude = amplitude;
                while (amplitudeOutOfBounds(i, tmpAmplitude, mesures.Count()))
                {
                    tmpAmplitude--;
                }

                double somme = 0;
                for(int j = i - tmpAmplitude; j <= i + tmpAmplitude;j++)
                {
                    somme += mesures[j].value;
                }
                mesures[i].value = somme / (tmpAmplitude * 2 + 1);
            }
            
        }   

        private bool amplitudeOutOfBounds(int currentIndex, int amplitude, int sizeList)
        {
            return (currentIndex - amplitude < 0 || currentIndex + amplitude >= sizeList);
        }
    }
}
