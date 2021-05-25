using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using ExtendedSerialPort;
using Utilities;
using ClassesNS;

namespace CommunicationNS
{
    public class Communication
    {
        Data data = new Data();
        ReliableSerialPort serialPort1;

        public Communication()
        {
            serialPort1 = new ReliableSerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
            serialPort1.DataReceived += SerialPort1_DataReceived;
            serialPort1.Open();
        }

        private void SerialPort1_DataReceived(object sender, DataReceivedArgs e)
        {
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
            int i = 0, j;
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
            if (function == 0x0016)
            {
                byte[] tab = payload.GetRange(4, 4);
                data.Time = BitConverter.ToUInt32(tab, 0);

                tab = payload.GetRange(8, 4);
                data.Value = tab.GetFloat();

                OnNewData(data.Time, data.Value);
            }
        }

        public event EventHandler<Data> OnNewDataEvent;
        public virtual void OnNewData(UInt32 time, float value)
        {
            var handler = OnNewDataEvent;
            if (handler != null)
            {
                handler(this, new Data { Time = time, Value = value });
            }
        }
    }
}