using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    public class DateTimeLapse : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _dateTimeLapse { get; set; }
        public string dateTimeLapse
        {
            get { return _dateTimeLapse; }
            set
            {
                _dateTimeLapse = value; NotifyPropertyChanged();
            }
        }

        public DateTimeLapse()
        {
            this._dateTimeLapse = "Lancer le TimeLapse";
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
