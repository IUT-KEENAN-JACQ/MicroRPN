#ifndef IO_H
#define IO_H

//Affectation des pins des LEDS
#define JACK _RA1
#define LED_ORANGE _LATC10 
#define LED_BLEUE _LATG7
#define LED_BLANCHE _LATG6

// Prototypes fonctions
void InitIO();

#define FCY 40000000

#endif /* IO_H */