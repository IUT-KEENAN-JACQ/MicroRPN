#include <xc.h>
#include "IO.h"

void InitIO()
    {
        //Configuration des sorties : _TRISxx = 0
    
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
        _U1RXR = 23;
        _RP24R = 0b00001 ;
        
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
