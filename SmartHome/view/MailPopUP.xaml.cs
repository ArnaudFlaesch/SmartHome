using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartHome.view
{
    /// <summary>
    /// Logique d'interaction pour MailPopUP.xaml
    /// </summary>
    public partial class MailPopUP : Window, INotifyPropertyChanged
    {

        public MailPopUP()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Object { get; set; }
        public int Age { get; set; }
        public string Mails { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            if (!Validation.GetHasError(textBox1) && !Validation.GetHasError(textBox2))
            {
                
                model.MAPI mapi = new model.MAPI();
                mapi.AddAttachment(System.IO.Path.GetFullPath("../../Data_Oxyplot.png"));
                mapi.AddRecipientTo(textBox2.Text);
                mapi.SendMailPopup(textBox1.Text, "Hey look at my awsome Netatmo data !");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    
    }
}
