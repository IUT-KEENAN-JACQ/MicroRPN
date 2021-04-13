#include <xc.h>
#include <stdio.h>
#include <stdlib.h>
#include "CB_RX1.h"
#include "CB_TX1.h"
#include "UART.h"
#include "UART_protocole.h"
#include "IO.h"
#include "Utilities.h"

int msgDecodedFunction =0;
int msgDecodedPayloadLength = 0;
unsigned char msgDecodedPayload [128];
int msgDecodedPayloadIndex = 0;
int decodedFlag =0;
unsigned char msgDecodedChecksum;
unsigned char msgCalculatedChecksum;
int  isCkecksumOk=-1;
int flag = 0;    

enum StateReception
{
    Waiting,
    FunctionMSB,
    FunctionLSB,
    PayloadLengthMSB,
    PayloadLengthLSB,
    Payload,
    CheckSum
};

enum StateReception rcvState = Waiting ;

unsigned char UartCalculateChecksum(int msgFunction, int msgPayloadLength, unsigned char * msgPayload)
{
    unsigned char  checksum = 0xFE;

    checksum = checksum ^ msgFunction;
    checksum = checksum ^ msgPayloadLength;

            int i ;
            for (i= 0; i < msgPayloadLength; i++)
                checksum ^= msgPayload[i];

            return checksum;
}
 
    void UartEncodeAndSendMessage( int msgFunction, int msgPayloadLength, unsigned char msgPayload[])
        {
            int i=0, j =0;
            unsigned char msg[6 + msgPayloadLength];
       
            msg[i++] = 0xFE;
            msg[i++] = msgFunction >> 8;
            msg[i++] = msgFunction;

            msg[i++] = msgPayloadLength >> 8;
            msg[i++] = msgPayloadLength;

            for (j = 0; j < msgPayloadLength; j++)
                msg[i++] = msgPayload[j];

            msg[i++] = UartCalculateChecksum(msgFunction, msgPayloadLength, msgPayload);
            
            SendMessage(msg,i);

 }
     void UartDecodeMessage (unsigned char c ){
          switch (rcvState)
            {
                case Waiting:
                    if (c == 0xFE)
                    {
                        rcvState = FunctionMSB;
                        decodedFlag = 0;
                    }
                    break;

                case FunctionMSB:
                    msgDecodedFunction = c;
                    msgDecodedFunction <<= 8;
                    rcvState = FunctionLSB;
                    break;

                case FunctionLSB:
                    msgDecodedFunction += c;
                    rcvState = PayloadLengthMSB;
                    break;

                case PayloadLengthMSB:
                    msgDecodedPayloadLength = c;
                    msgDecodedPayloadLength <<= 8;
                    rcvState = PayloadLengthLSB;
                    break;

                case PayloadLengthLSB:
                    msgDecodedPayloadLength += c;          
                    if (msgDecodedPayloadLength == 0)
                        rcvState = CheckSum;
                    else
                    {
                        rcvState = Payload;
                        msgDecodedPayloadIndex = 0;
                    }
                    break;

                case Payload:
                    msgDecodedPayload[msgDecodedPayloadIndex++] = c;
                    if (msgDecodedPayloadIndex == msgDecodedPayloadLength)
                        rcvState = CheckSum;
                    break;

                case CheckSum:
                    msgDecodedChecksum = c;
                    msgCalculatedChecksum = UartCalculateChecksum(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload);
                    decodedFlag = 1;
                    if (msgDecodedChecksum == msgCalculatedChecksum)
                    {
                        isCkecksumOk = 1;
                        UartMessageProcessor(msgDecodedFunction, msgDecodedPayload);
                    }
                    else
                        isCkecksumOk = 0;
                    
                    rcvState = Waiting;
                    break;

                default:
                    rcvState = Waiting;
                    break;
          }
    }
     
     void UartMessageProcessor (int msgFunction, unsigned char msgPayload[])
     {
         //inutile
     }
     
   