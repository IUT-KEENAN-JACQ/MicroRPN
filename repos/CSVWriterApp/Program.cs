using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;
using CsvHelper;
using Interface;
using ClassesNS;
using CommunicationNS;

namespace CSVWriterApp
{
    class Program
    {
        static Thread t1;
        static Communication communication = new Communication();
        static List<Data> valuelist = new List<Data>();
        static MainWindow window;

        static void Main(string[] args)
        {
            Data data = new Data();

            communication.OnNewDataEvent += DataUpdate;

            StartInterface();

            while (true) ;
        }

        static void StartInterface()
        {
            t1 = new Thread(() =>
            {
                window = new MainWindow();
                window.ShowDialog();

                window.OnExportPressedEvent += Export;
            });
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();

            Console.WriteLine("SETUP UI");
        }

        static void DataUpdate(object sender, Data data)
        {
            valuelist.Add(new Data() { Value = data.Value, Time = data.Time });
        }

        static void Export(object sender)
        {
            var writer = new StreamWriter("C:\\Documents personels\\file.csv");
            var csvwriter = new CsvWriter(writer, CultureInfo.InvariantCulture); //?

            csvwriter.WriteRecords(valuelist);

            csvwriter.Dispose();
            writer.Dispose();
        }
    }
}
