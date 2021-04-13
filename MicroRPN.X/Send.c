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

void SendData()
{
    volts = ADCGetResult() * 3.3 / 4096 * 3.2;
    
    unsigned char dataPayload[8];
    getBytesFromInt32(dataPayload, 0, timestamp);
    getBytesFromFloat(dataPayload, 4, volts);
    UartEncodeAndSendMessage(DATA, 8, dataPayload);
}
       
