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
        private static double SEUIL_MAX_NOISE = 35;
        private static double SEUIL_MAX_RAIN = 2;
        private static double SEUIL_MAX_PRESSURE = 200;
        private static double SEUIL_MIN_PRESSURE = 900;
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
                    if (pourcentage == 0)
                    {
                        pourcentage = 1;
                    }
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Co2":
                {
                    pourcentage = value * 100 / SEUIL_MAX_CO2;
                    if (pourcentage == 0)
                    {
                        pourcentage = 1;
                    }
                    red = (byte)(255 - 2.5 * pourcentage);
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Humidité":
                {
                    pourcentage = value;
                    if (pourcentage == 0)
                    {
                        pourcentage = 1;
                    }
                    red = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - ((2.5 * pourcentage) / 2));
                    break;
                }

                case "Bruit":
                {
                    pourcentage = (value - SEUIL_MAX_NOISE) * 100 / SEUIL_MAX_NOISE;
                    if (pourcentage == 0)
                    {
                        pourcentage = 1;
                    }
                    blue = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }

                case "Pluie":
                {
                    pourcentage = value * 100 / SEUIL_MAX_RAIN;
                    if (pourcentage == 0.00)
                    {
                        pourcentage = 1;
                    }
                    red = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }
                case "Pression":
                {
                    pourcentage = (value - SEUIL_MIN_PRESSURE) * 100 / SEUIL_MAX_PRESSURE;
                    if (pourcentage == 0.00)
                    {
                        pourcentage = 1;
                    }
                    red = (byte)(255 - 2.5 * pourcentage);
                    green = (byte)(255 - 2.5 * pourcentage);
                    break;
                }
            }

            color = new SolidColorBrush(Color.FromRgb(red, green, blue));
            color.Freeze();
            return (color);
        }
    }
}
