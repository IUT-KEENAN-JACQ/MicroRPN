#include <xc.h>
#include "IO.h"
#include "ADC.h"
#include "Timers.h"
#include "UART_protocole.h"
#include "Utilities.h"

#define DATA 0x0010
unsigned char message[1];

void InitTimer1()
{
    //T1CONbits.TON = 0;
    
    T1CON = 0x00;                                                               //Stops the Timer1 and reset control reg.
    TMR1 = 0x00;                                                                //Clear contents of the timer register
    //PR1 = 0xFFFF;                                                               //Load the Period register with the value 0xFFFF
    IPC0bits.T1IP = 0x01;                                                       //Setup Timer1 interrupt for desired priority level

    T1CONbits.TCS = 0;
    IFS0bits.T1IF = 0;                                                          //Clear the Timer1 interrupt status flag
    IEC0bits.T1IE = 1;                                                          //Enable Timer1 interrupts
    T1CONbits.TON = 1;
    
    SetFreqTimer1(100.0);                                                        //Start Timer1 with prescaler settings at 1:1
    
    getBytesFromInt32(message, 0, 1);
    getBytesFromInt32(message, 1, 2);
}

void SetFreqTimer1(float freq)
{
        T1CONbits.TCKPS = 0b00; //00 = 1:1 prescaler value
        if(FCY /freq > 65535)
    {
            T1CONbits.TCKPS = 0b01; //01 = 1:8 prescaler value
            if(FCY /freq / 8 > 65535)
        {
                T1CONbits.TCKPS = 0b10; //10 = 1:64 prescaler value
                if(FCY /freq / 64 > 65535)
            {
                T1CONbits.TCKPS = 0b11; //11 = 1:256 prescaler value
                PR1 = (int)(FCY / freq / 256);
            }
                else
                PR1 = (int)(FCY / freq / 64);
        }
            else
            PR1 = (int)(FCY / freq / 8);
    }
        else
        PR1 = (int)(FCY / freq);
}

void __attribute__((__interrupt__, __shadow__)) _T1Interrupt(void)
{
    LED_BLEUE = !LED_BLEUE;
    IFS0bits.T1IF = 0;                                                          //Flag down
    //StartADCSequence();
    UartEncodeAndSendMessage(DATA, 1, message);
}