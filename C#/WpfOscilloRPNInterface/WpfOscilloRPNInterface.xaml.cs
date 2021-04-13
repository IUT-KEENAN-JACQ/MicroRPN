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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Utilities;

namespace WpfOscilloRPNInterface
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class WpfOscilloRPN : Window
    {
        UInt32 Timestamp;
        float UnprocessedValue;
        DispatcherTimer timerAffichage;
        //int i;

        public WpfOscilloRPN()
        {
            InitializeComponent();
            OscilloRPNMotor.AddOrUpdateLine(1, 100, "Ligne 1", true);

            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timerAffichage.Tick += TimerAffichageTick;
            timerAffichage.Start();
        }

        private void TimerAffichageTick(object sender, EventArgs e)
        {
            Point point = new Point(UnprocessedValue, Timestamp);
            //i++;
            //Point point = new Point(i, i);
            OscilloRPNMotor.AddPointToLine(1, point);
        }

        public void DataUpdate(object sender, StateData stateData)
        {
            Timestamp = stateData.timestamp;
            UnprocessedValue = stateData.unprocessedValue;
        }
    }
}
