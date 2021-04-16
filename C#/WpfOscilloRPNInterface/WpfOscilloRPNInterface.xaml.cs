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
        StateData stateDataAffichage = new StateData();
        DispatcherTimer timerAffichage;
        int index = 0;
        public WpfOscilloRPN()
        {
            InitializeComponent();
            OscilloRPNMotor.AddOrUpdateLine(1, 100, "Ligne 1", true);

            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerAffichage.Tick += TimerAffichageTick;
            timerAffichage.Start();
        }

        private void TimerAffichageTick(object sender, EventArgs e)
        {
            //Point point = new Point(stateDataAffichage.timestamp, stateDataAffichage.unprocessedValue);
            //OscilloRPNMotor.AddPointToLine(1, point);

            //Decompresser les tableaux
            //Ajouter un nouveau point @1khz
        }

        public void DataUpdate(object sender, StateData stateDataTrans)
        {
            //stateDataAffichage.timestamp = stateDataTrans.timestamp;
            //stateDataAffichage.unprocessedValue = stateDataTrans.unprocessedValue;

            //Faire rentrer les tableaux
            stateDataAffichage.timestampArray = stateDataTrans.timestampArray;
            stateDataAffichage.unprocessedValueArray = stateDataTrans.unprocessedValueArray;

            for (index = 0; index <= 9; index++)
            {
                Point point = new Point(stateDataAffichage.timestampArray[index], stateDataAffichage.unprocessedValueArray[index]);
                OscilloRPNMotor.AddPointToLine(1, point); 
            }

        }
    }
}
