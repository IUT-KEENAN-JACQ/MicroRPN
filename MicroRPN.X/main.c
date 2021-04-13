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

int main(void) 
    {
        InitOscillator();
        InitIO();
        InitTimer23();
        InitTimer1();
        InitTimer4();
        InitADC1();
        InitUART();
        
        while (1) 
            {
            }
         
    }