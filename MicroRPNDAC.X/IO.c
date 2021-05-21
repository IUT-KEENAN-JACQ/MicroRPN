#include <xc.h>
#include "IO.h"



void InitIO()
    {
    asm volatile ("MOV #OSCCON, w1 \n"
                "MOV #0x46, w2 \n"
                "MOV #0x57, w3 \n"
                "MOV.b w2, [w1] \n"
                "MOV.b w3, [w1] \n"
                "BCLR OSCCON, #6") ;
    //Unlock
    
        // LED
        _TRISE5 = 0; //LED Bleue
        _TRISE6 = 0; //LED 2
        
        //DAC
        _TRISG9 = 1; //analog?
        _ANSG9 = 1;
        
        //ADC
        _TRISB13 = 1;
        _ANSB13 = 1;
        
        //uart
        _U1RXR = 2;
        _RP12R = 0b00011;
        
        
    asm volatile ("MOV #OSCCON, w1 \n"
                "MOV #0x46, w2 \n"
                "MOV #0x57, w3 \n"
                "MOV.b w2, [w1] \n"
                "MOV.b w3, [w1] \n"
                "BSET OSCCON, #6") ;
    //lock again
        
    }

void InitOscillator()
{
    CLKDIVbits.RCDIV = 0b001;                                                   //post FRC div = 2
    CLKDIVbits.CPDIV = 0b00;                                                    //post pll div = 3*1
    CLKDIVbits.PLLEN = 1;                                                       //pll enabled
    
    __builtin_write_OSCCONH(0x01);                                              //next osc is FRCPLL (NOSC = 0b001)
    __builtin_write_OSCCONL(OSCCON | 0x01);                                     //change osc for the (n)ew osc (OSWEN = 1)
    
    while(OSCCONbits.LOCK != 1);                                                //wait until pll is locked
    
    //LED = 1;                                                                  //done!!
}
