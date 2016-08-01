using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class Lieu : IEquatable<Lieu>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int id { get; set; }
        public string name { get; set; }
        public ObservableCollection<Capteur> capteurList { get; set; }
        public string _concentrationC02 { get; set; }
        public string concentrationC02
        {
            get { return _concentrationC02; }
            set
            {
                _concentrationC02 = value; NotifyPropertyChanged();
            }
        }

        /*
        private string _activatedCapteur;
        public string activatedCapteur
        {
            get { return _activatedCapteur; }
            set
            {
                _activatedCapteur = value;
                NotifyPropertyChanged();
            }
        }*/

        public Lieu()
        {
            this.capteurList = new ObservableCollection<Capteur>();
            _concentrationC02 = "Présence : Capteur Non Existant ";
            //this.activatedCapteur = "";
        }

        public bool Equals(Lieu other)
        {
            if (other == null) return false;
            return (this.name.Equals(other.name));
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
