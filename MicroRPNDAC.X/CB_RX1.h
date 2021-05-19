/* 
 * File:   CB_RX1.h
 * Author: TP_EO_6
 *
 * Created on 6 octobre 2020, 08:29
 */

#ifndef CB_RX1_H
#define	CB_RX1_H

#ifdef	__cplusplus
extern "C" {
#endif

void CB_RX1_Add(unsigned char value );
unsigned char CB_RX1_Get( void);
unsigned char CB_RX1_IsDataAvailable ( void);
void __attribute__ ( ( interrupt , no_auto_psv ) ) _U1RXInterrupt ( void);
int CB_RX1_GetRemainingSize ( void);
int CB_RX1_GetDataSize ( void);


#ifdef	__cplusplus
}
#endif

#endif	/* CB_RX1_H */

