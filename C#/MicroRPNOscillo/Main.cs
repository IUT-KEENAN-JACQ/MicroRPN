using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ExtendedSerialPort;
using System.Threading;
using Utilities;
using OtherClassesNS;
using WpfOscilloscopeControl;
using SciChart.Charting.Visuals;
using WpfOscilloRPNInterface;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MicroRPNOscilloNS
{
    public class Moteur
    {
        static WpfOscilloscope wpfOscilloscope;
        static WpfOscilloRPN wpfOscilloRPNInterface;
        static Thread t1;
        static Communication communication = new Communication();

        public static void Main()
        {
            SciChartSurface.SetRuntimeLicenseKey("EKagHX/XzTICAKa+9LX8w3580rRyA+0s/+YRALNcEMiJA7N1AetbJrXyzQMHfWPCfTlRHgP4XLorSnP+QNKo/sgJy4qNeBqsftcvUiu1bNkxVncfVJglCswYBJE1/Sw1i+6vAhCi78bQ9wA21I1V5+oV78FFdMBi+gP4Us/z5NQXmxpy4lAodG1vDnhpQFc9bwfNeVoxFB2wAHPDQxIsuUsdzsC34p4fSs71WpzqxM/ZOG55DJpYK+sb/zH7nnlM7EHURoiZYJXgBjkE6IoqqNAkRBa22eHx+dtCkgZifZqm7dC+iWgxp7fefkDv1OfdWjB3MPVyfdg+AQfDwIzAdMwySjeYmpJ4vHxBPKZhXNdjgS7PQHmk4R8i412hh+xI8UL8vyBCnZ4ihgIR9E6gWWsXjOC736jockUcNiu2rhzx39jxgDjUC8N+h0ptGyCbEv0Q+0a0kB/ZD6ggoWc3YGHBUSih0ODWuhZDE7k35MkPgj+QuVCm77w8K7oYWBzM9A==");

            StartRobotInterface();
            while (true) ;
        }

        static void StartRobotInterface()
        {
            t1 = new Thread(() =>
            {
                //Attention, il est nécessaire d'ajouter PresentationFramework, PresentationCore, WindowBase and your wpf window application aux ressources.
                wpfOscilloscope = new WpfOscilloscope();
                wpfOscilloRPNInterface = new WpfOscilloRPN();
                wpfOscilloRPNInterface.Loaded += RegisterRobotInterfaceEvents;
                communication.OnNewDataEvent += wpfOscilloRPNInterface.DataUpdate;
                wpfOscilloRPNInterface.ShowDialog();
            });
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        static void RegisterRobotInterfaceEvents(object sender, EventArgs e)
        {
            //oui :)
        }

    }   

    public class Communication
    {
        StateData stateData = new StateData();
        //ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();
        ReliableSerialPort serialPort1;
        Stopwatch watch = new Stopwatch();

        public Communication()
        {
            serialPort1 = new ReliableSerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
            serialPort1.DataReceived += SerialPort1_DataReceived;
            serialPort1.Open();
        }

        private void SerialPort1_DataReceived(object sender, DataReceivedArgs e)
        {
            watch.Restart();
            for (int i = 0; i < e.Data.Length; i++)
            {
                DecodeMessage(e.Data[i]);
            }
        }

        byte CalculateChecksum(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            byte cheksum = 0xFE;

            cheksum = (byte)(cheksum ^ msgFunction);
            cheksum = (byte)(cheksum ^ msgPayloadLength);

            for (int i = 0; i < msgPayloadLength; i++)
                cheksum ^= msgPayload[i];

            return cheksum;
        }

        void UartEncodeAndSendMessage(ushort msgFunction, ushort msgPayloadLength, byte[] msgPayload)
        {
            int i = 0, j = 0;
            byte[] msg = new byte[6 + msgPayloadLength];

            msg[i++] = 0xFE;

            msg[i++] = (byte)(msgFunction >> 8);
            msg[i++] = (byte)msgFunction;

            msg[i++] = (byte)(msgPayloadLength >> 8);
            msg[i++] = (byte)msgPayloadLength;

            for (j = 0; j < msgPayloadLength; j++)
                msg[i++] = msgPayload[j];

            msg[i++] = CalculateChecksum(msgFunction, msgPayloadLength, msgPayload);

            serialPort1.Write(msg, 0, msg.Length);
        }

        public enum StateReception
        {
            Waiting,
            FunctionMSB,
            FunctionLSB,
            PayloadLengthMSB,
            PayloadLengthLSB,
            Payload,
            CheckSum
        }

        //definitions
        StateReception rcvState = StateReception.Waiting;

        byte[] msgDecodedPayload;
        byte msgDecodedChecksum;
        byte msgCalculatedChecksum;

        UInt16 msgDecodedFunction = 0;
        UInt16 msgDecodedPayloadLength = 0;
        int msgDecodedPayloadIndex = 0;

        private void DecodeMessage(byte c)
        {
            //Console.Write("0x" + c.ToString("X2") + " ");
            switch (rcvState)
            {
                case StateReception.Waiting:
                    if (c == 0xFE)
                    {
                        //Console.Write("0x" + c.ToString("X2") + " ");
                        rcvState = StateReception.FunctionMSB;
                    }
                    break;

                case StateReception.FunctionMSB:
                    msgDecodedFunction = (UInt16)(c << 8);
                    rcvState = StateReception.FunctionLSB;
                    break;

                case StateReception.FunctionLSB:
                    msgDecodedFunction += c;
                    rcvState = StateReception.PayloadLengthMSB;
                    break;

                case StateReception.PayloadLengthMSB:
                    msgDecodedPayloadLength = (UInt16)(c << 8);
                    rcvState = StateReception.PayloadLengthLSB;
                    break;

                case StateReception.PayloadLengthLSB:
                    msgDecodedPayloadLength += c;
                    if (msgDecodedPayloadLength == 0)
                    {
                        rcvState = StateReception.CheckSum;
                    }
                    else if (msgDecodedPayloadLength < 1024)
                    {
                        msgDecodedPayload = new byte[msgDecodedPayloadLength];
                        msgDecodedPayloadIndex = 0;
                        rcvState = StateReception.Payload;
                    }
                    else
                    {
                        rcvState = StateReception.Waiting;
                    }
                    break;

                case StateReception.Payload:
                    msgDecodedPayload[msgDecodedPayloadIndex++] = c;
                    if (msgDecodedPayloadIndex >= msgDecodedPayloadLength)
                        rcvState = StateReception.CheckSum;
                    break;

                case StateReception.CheckSum:
                    msgDecodedChecksum = c;
                    msgCalculatedChecksum = CalculateChecksum(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload);
                    //Console.WriteLine("CHECKSUM : " + msgCalculatedChecksum.ToString("X2"));
                    if (msgDecodedChecksum == msgCalculatedChecksum)
                    {
                        //messageQueue.Enqueue(new Message(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload));
                        MessageProcessor(msgDecodedFunction, msgDecodedPayload, msgDecodedPayloadLength); //msgDecodedPayloadLength
                    }
                    else
                    {
                        Console.WriteLine("Wrong Message Checksum");
                    }
                    rcvState = StateReception.Waiting;
                    break;

                default:
                    rcvState = StateReception.Waiting;
                    break;
            }
        }

        private void MessageProcessor(UInt16 function, byte[] payload, ushort length)
        {
            //A faire
            if (function == 16)
            {
                //watch.Start();
                int y = 0, index, indexMax = length/8;
                stateData.timestampArray = new UInt32[indexMax];
                stateData.unprocessedValueArray = new float[indexMax];

                for(index = 0; index < indexMax; index++)
                {
                    byte[] tab = payload.GetRange(y, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(tab);
                    stateData.timestampArray[index] = BitConverter.ToUInt32(tab, 0);
                    y = y + 4;

                    tab = payload.GetRange(y, 4);
                    stateData.unprocessedValueArray[index] = tab.GetFloat();
                    y = y + 4;
                }

                OnNewData(stateData.timestampArray, stateData.unprocessedValueArray);

                //byte[] tab = payload.GetRange(0, 4);
                //if (BitConverter.IsLittleEndian)
                //    Array.Reverse(tab);
                //stateData.timestamp = BitConverter.ToUInt32(tab, 0);

                //tab = payload.GetRange(4, 4);
                //stateData.unprocessedValue = tab.GetFloat();

                //Console.WriteLine("Timestamp : " + stateData.timestamp + " Value : " + stateData.unprocessedValue);
                //OnNewData(stateData.timestamp, stateData.unprocessedValue);
            }
        }

        public event EventHandler<StateData> OnNewDataEvent;
        public virtual void OnNewData(UInt32[] timestampArray, float[] unprocessedValueArray)
        {
            var handler = OnNewDataEvent;
            if (handler != null)
            {
                handler(this, new StateData { timestampArray = timestampArray, unprocessedValueArray = unprocessedValueArray });
            }
        }
        
    }
}