#include <xc.h>
#include "IO.h"

void InitADC()
{
    IEC0bits.AD1IE = 1;                                                         //Enable Interrupt(!!!!)
    IFS0bits.AD1IF = 0;                                                         //clear flag
    
    
    ADCON1bits.PWRLVL = 1;                                                      //up to 10Mhz
    ADCON3bits.ADRC = 1;                                                        //FRCPLL @4MHz (?) (src)
    //ADCON3bits.ADCS = 0b00000000;                                             //src (8Mhz or 4Mhz?)
    ADL0CONHbits.SLINT = 0b01;                                                  //interrupt on every sample 
    ADTBL0bits.ADCH = 0b0001101;                                                //AN13 selected on 0
    ADL0CONHbits.SAMC = 0b00001;                                                //1 Tad
    
    ADCON1bits.ADON = 1;                                                        //Enabled!
    while(ADSTATHbits.ADREADY != 1);                                            //wait until its ready
    ADCON1bits.ADCAL = 1;                                                       //Calibration
    while(ADSTATHbits.ADREADY != 1);                                            //wait until its ready
    ADL0CONLbits.SAMP = 1;
    
    //LED = 1;
}

void __attribute__((interrupt, no_auto_psv)) _ADC1Interrupt(void)
{    
    IFS0bits.AD1IF = 0;                                                         //clear flag
    //int result = ADRES0;                                                        //get result int
    //float resultFL = result * 3.3 / 4096;                                       //conversion des 12 bits en float, valeur en volts
    //result = resultFL / 3.3 * 1024;                                             //convertion int 10bits
    //DAC1DAT = result;                                                           //into DAC buffer
    LED = !LED;
}

void StartADCSequence(void)
{
    //ADL0CONLbits.SAMP = 1;                                                      //SAMP works??
}