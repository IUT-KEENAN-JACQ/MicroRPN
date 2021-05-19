/* 
 * File:   UART_protocole.h
 * Author: TP_EO_6
 *
 * Created on 14 octobre 2020, 12:54
 */

#ifndef UART_PROTOCOLE_H
#define	UART_PROTOCOLE_H

void UartDecodeMessage (unsigned char c );
unsigned char UartCalculateChecksum(int msgFunction, int msgPayloadLength, unsigned char * msgPayload);
void UartEncodeAndSendMessage( int msgFunction, int msgPayloadLength, unsigned char msgPayload[]);
void UartMessageProcessor (int msgFunction, unsigned char msgPayload[]);

#endif	/* UART_PROTOCOLE_H */

