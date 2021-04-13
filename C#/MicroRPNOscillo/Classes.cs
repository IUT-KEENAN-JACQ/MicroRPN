using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace OtherClassesNS
{
    class Message
    {

        public UInt16 Function;
        public UInt16 PayloadLength;
        public byte[] Payload;

        public Message(UInt16 function, UInt16 payloadLength, byte[] payload)
        {
            Function = function;
            PayloadLength = payloadLength;
            Payload = payload;
        }
    }
}
