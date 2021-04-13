#include <xc.h>
#include "UART.h"
#include "ChipConfig.h"

#define BAUDRATE 115200
#define BRGVAL ((FCY/BAUDRATE)/4)-1

void InitUART(void)
{
    U1MODEbits.STSEL = 0; // 1-stop bit
    U1MODEbits.PDSEL = 0; // No Parity, 8-data bits
    U1MODEbits.ABAUD = 0; // Auto-Baud Disabled
    U1MODEbits.BRGH = 1; // Low Speed mode
    U1BRG = BRGVAL;// BAUD Rate Setting
    
    U1STAbits.UTXISEL0 = 0;
    U1STAbits.UTXISEL1 = 0;
    IFS0bits.U1TXIF = 0;
    IEC0bits.U1TXIE = 1;
    
    U1STAbits.URXISEL = 0;
    IFS0bits.U1RXIF = 0;
    IEC0bits.U1RXIE = 1;
    
    U1MODEbits.UARTEN = 1;
    U1STAbits.UTXEN = 1;
}

void SendMessageDirect  (unsigned char *message, int length)
{
    unsigned char i =0;
    for(i=0; i<length ; i++)
    {
        while(U1STAbits.UTXBF);
        U1TXREG = *(message)++;
    }
}