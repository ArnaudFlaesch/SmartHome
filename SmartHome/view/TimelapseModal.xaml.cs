using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour TimelapseModal.xaml
    /// </summary>
    public partial class TimelapseModal : Window
    {
        MainWindow _mainView;
        public TimelapseModal(MainWindow m)
        {
            this._mainView = m;
            InitializeComponent();
        }

        private void DoPlayTimelapse(object sender, RoutedEventArgs e)
        {
            this._mainView.DoPlayTimelapse((int)this.IntervalSlider.Value, (int)this.DeltaSlider.Value, (int)this.WaitTime.Value);
            this.Close();
        }
    }
}
