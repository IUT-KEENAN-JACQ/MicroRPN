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

namespace Interface
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("SETUP UX");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //enregistrer


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //mettre en liste

        }

        public event EventHandler OnExportPressedEvent;
        public virtual void OnExportPressed()
        {
            var handler = OnExportPressedEvent;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}
