#include <xc.h>
#include "IO.h"
#include "Timers.h"
#include "ChipConfig.h"
#include "SDADC.h"
#include "DAC.h"
#include "ADC.h"
#include "UART.h"

int main(void) 
{
    InitIO();
    InitTimer1();
    InitOscillator();
    InitSDADC();
    InitDAC();
    InitADC();
    InitUART();
    
    while(1)
    {
        //bnothin
    }
}