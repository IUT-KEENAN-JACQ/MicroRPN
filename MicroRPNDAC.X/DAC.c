#include <xc.h>
#include "IO.h"

void InitDAC(void)
{
    DAC1CONbits.DACREF = 0b10;                                                  //Vref = 3.33v, same as ADC, AVDD
    
    //DAC1CONbits.DACTRIG = 1;
    //DAC1CONbits.DACTSEL = 0b101;                                                //ouais
    
    DAC1CONbits.DACEN = 1;                                                      //DAC ENABLED
}
