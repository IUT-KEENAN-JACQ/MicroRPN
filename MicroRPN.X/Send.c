#include <stdio.h>
#include <stdlib.h>
#include <xc.h>
#include <libpic30.h>
#include "ChipConfig.h"
#include "IO.h"
#include "timer.h"
#include "ADC.h"
#include "main.h"
#include "UART.h"
#include "CB_TX1.h"
#include "CB_RX1.h"
#include "UART_protocole.h"
#include "Utilities.h"

#define DATA 0x0010
float volts;
float voltsArray[10];
long timestampArray[10];
int i = 0, y = 0;

void GetData()
{           
    voltsArray[i] = ADCGetResult() * 3.3 / 4096 * 3.2;
    timestampArray[i] = timestamp;
    i++;
    
    if(i > 9)
    {
        SendData();
        i = 0;
    }
}

void SendData()
{   
    int x, y = 0;
    unsigned char dataPayload[80];
    //LED_ORANGE = !LED_ORANGE;
    
    for(x = 0; x < 10; x++)
    {
        getBytesFromInt32(dataPayload, y, timestampArray[x]);
        y = y+4;
        getBytesFromFloat(dataPayload, y, voltsArray[x]);
        y = y+4;
        
        //dataPayload[y] = dataPayload[y]; 
    }
    
    UartEncodeAndSendMessage(DATA, 80, dataPayload);
    
//    volts = ADCGetResult() * 3.3 / 4096 * 3.2;
//    unsigned char dataPayload[8];
//    getBytesFromInt32(dataPayload, 0, timestamp);
//    getBytesFromFloat(dataPayload, 4, volts);
//    UartEncodeAndSendMessage(DATA, 8, dataPayload);
}
       
