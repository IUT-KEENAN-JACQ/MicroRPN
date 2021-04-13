#ifndef UART_PROTOCOLE_H
#define	UART_PROTOCOLE_H

void UartDecodeMessage (unsigned char c );
unsigned char UartCalculateChecksum(int msgFunction, int msgPayloadLength, unsigned char * msgPayload);
void UartEncodeAndSendMessage( int msgFunction, int msgPayloadLength, unsigned char msgPayload[]);
void UartMessageProcessor (int msgFunction, unsigned char msgPayload[]);

#endif	/* UART_PROTOCOLE_H */

