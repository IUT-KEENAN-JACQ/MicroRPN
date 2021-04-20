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
        StateData stateDataAffichageBuf = new StateData();
        Queue<Point> incomingDataQueue = new Queue<Point>();
        RollingList<Point> pointList = new RollingList<Point>(1000);
        DispatcherTimer timerAffichage;

        public WpfOscilloRPN()
        {
            InitializeComponent();
            OscilloRPNMotor.AddOrUpdateLine(1, 100, "Ligne 1", true);

            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerAffichage.Tick += TimerAffichageTick;
            timerAffichage.Start();
        }

        private void TimerAffichageTick(object sender, EventArgs e)
        {
            //Point point = new Point(stateDataAffichage.timestamp, stateDataAffichage.unprocessedValue);
            //OscilloRPNMotor.AddPointToLine(1, point);

            //Decompresser les tableaux
            //Ajouter un nouveau point @1khz

            while (incomingDataQueue.Count() > 0)
                pointList.Add(incomingDataQueue.Dequeue());

            OscilloRPNMotor.UpdatePointListOfLine(1, pointList._list.ToList<Point>());

            incomingDataQueue.Clear();
            pointList.Clear();
        }

        public void DataUpdate(object sender, StateData stateDataTrans)
        {
            //stateDataAffichage.timestamp = stateDataTrans.timestamp;
            //stateDataAffichage.unprocessedValue = stateDataTrans.unprocessedValue;

            //Faire rentrer les tableaux
            //stateDataAffichage.timestampArray = stateDataTrans.timestampArray;
            //stateDataAffichage.unprocessedValueArray = stateDataTrans.unprocessedValueArray;
                        
            for (int i = 0; i < stateDataTrans.timestampArray.Length; i++)
            {
                incomingDataQueue.Enqueue(new Point(stateDataTrans.timestampArray[i], stateDataTrans.unprocessedValueArray[i]));
                //Console.WriteLine("Timestamp : " + stateDataAffichage.timestampArray[indexB] + " Value : " + stateDataAffichage.unprocessedValueArray[indexB]);
            }

            //for (index = 0; index <= 9; index++)
            //{
            //    Point point = new Point(stateDataAffichage.timestampArray[index], stateDataAffichage.unprocessedValueArray[index]);
            //    OscilloRPNMotor.AddPointToLine(1, point);
            //}

        }
    }
}
