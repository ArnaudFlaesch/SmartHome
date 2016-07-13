using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartHome
{
    //Classe portant les capteurs parsé dans CapteurParseur.
    public class Capteur : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string id { get; set; }
        public string description { get; set; }
        public string lieu { get; set; }
        public string grandeurNom { get; set; }
        public List<Mesure> mesureList;
        public bool isActivated { get; set; }

        private SolidColorBrush _activeMesure;
        private string _labelMesure;

        public string labelMesure
        {
            get { return _labelMesure; }
            set
            {
                _labelMesure = description+" : "+ value;
                NotifyPropertyChanged();
            }
        }
        public SolidColorBrush activeMesure
        {
            get { return _activeMesure; }
            set
            {
                _activeMesure = value;
                NotifyPropertyChanged();
            }
        }

        public Capteur(string id, string description, string lieu, string grandeurNom)
        {
            this.id = id;
            this.description = description;
            this.lieu = lieu;
            this.grandeurNom = grandeurNom;
            this.mesureList = new List<Mesure>();
            this.labelMesure ="";
            
        }

        public void addMesure(Mesure mesure)
        {
            this.mesureList.Add(mesure);
        }

        public List<Mesure> getMesures()
        {
            return this.mesureList;
        }

        public void setMesures(List<Mesure> list)
        {
            this.mesureList = list;
        }

        public Capteur(Capteur capteur)
        {
            this.id = capteur.id;
            this.description = capteur.description;
            this.lieu = capteur.lieu;
            this.grandeurNom = capteur.grandeurNom;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}