#ifndef ADC_H
#define	ADC_H

void ADC1StartConversionSequence(void);
void ADCClearConversionFinishedFlag(void);
void InitADC1(void);
float ADCGetResult(void);
unsigned char ADCIsConversionFinished(void);

#endif	/* ADC_H */
