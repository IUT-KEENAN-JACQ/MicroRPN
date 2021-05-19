#include <xc.h>
#include "IO.h"

int count = 0;
int result = 0;
float resultFL = 0.0;

void InitSDADC()
{
    //ADC and SD config.
    
    SD1CON1bits.SDON = 0;
    //SD1CON1bits.SDRST = 1;
    SD1CON1bits.DITHER = 0b10;                                                  //medium dither
    
    //SD1CON1bits.SDREFN = 0;                                                   //0v
    //SD1CON1bits.SDREFP = 0;                                                   //3.33v

    SD1CON2bits.SDWM = 0b01;                                                    //update on every interrupt regardless of SDRDY
    SD1CON2bits.SDINT = 0b11;                                                   //useful..??
    SD1CON2bits.RNDRES = 0b00;                                                  // Round result to 16-bit
    
    SD1CON3bits.SDDIV = 0b010;                                                  //OSCSR / 8, 32/8 = 4mHz, max adc freq
    SD1CON3bits.SDOSR = 0b011;                                                  //1:128 oversampling ratio
    //SD1CON3bits.SDCH = 0b000;                                                 //CH0+/-
    
    //Init  
    IFS6bits.SDA1IF = 0;                                                        // Clear interrupt flag
    IEC6bits.SDA1IE = 1;                                                        // Interrupt is enabled
    SD1CON1bits.SDON = 1;                                                       //s/d enabled!!
    
    //flush the 8 first samples
    for(count=0; count<8; count++)
    {
        IFS6bits.SDA1IF = 0;                                                    //Clear interrupt flag.
        while(IFS6bits.SDA1IF == 0);                                            //Wait for the result ready.
    }
}

void __attribute__((interrupt, no_auto_psv)) _SDA1Interrupt(void)
{
    IFS6bits.SDA1IF = 0;                                                        //clear flag
    result = SD1RESH;                                                           //get result int
    resultFL = (result * 3.3 / 65536)+1.65;                                     //conversion des 16 bits en float, valeur en volts - offset
    result = resultFL / 3.3 * 1024;                                             //convertion int 10bits
    DAC1DAT = result;                                                           //remplissage
    //DAC1DAT = 512;
    LED = !LED;
}

