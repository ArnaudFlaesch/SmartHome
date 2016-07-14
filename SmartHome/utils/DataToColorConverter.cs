using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartHome.utils
{
    public class DataToColorConverter
    {
        private static double SEUIL_MAX_TEMP = 30;
        private static double SEUIL_MAX_CO2 = 3450;
        // Les capteurs humidity renvoie déjà un pourcentage
        private static double SEUIL_MAX_NOISE = 35;
        private static double SEUIL_MAX_RAIN = 0.4;
        //Manque pression atmosphérique et pluviométre

        public static SolidColorBrush fromDataToColor(double value, string typeCapteur)
        {
            SolidColorBrush color;
            byte red = 255, green = 255, blue = 255;
            double pourcentage;

            switch (typeCapteur)
            {
                case "Temperature":
                {
                    pourcentage = value * 100 / SEUIL_MAX_TEMP;
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Co2":
                {
                    pourcentage = value * 100 / SEUIL_MAX_CO2;
                    red = (byte)(255 - 2.5 * pourcentage);
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Humidité":
                {
                    pourcentage = value;
                    red = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - ((2.5 * pourcentage) / 2));
                    break;
                }

                case "Bruit":
                {
                    pourcentage = value * 100 / SEUIL_MAX_NOISE;
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Pluie":
                {
                    pourcentage = value * 100 / SEUIL_MAX_RAIN;
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

            }
            

            color = new SolidColorBrush(Color.FromRgb(red, green, blue));
            color.Freeze();
            //Console.WriteLine(value);
            //Console.WriteLine(typeCapteur);
            return (color);
        }
    }
}
