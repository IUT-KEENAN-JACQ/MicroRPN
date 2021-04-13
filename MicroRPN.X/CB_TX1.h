#ifndef CB_TX1_H
#define	CB_TX1_H
#ifdef	__cplusplus
#endif
    
void CB_TX1_Add(unsigned char value);
unsigned char CB_TX1_Get(void);
void __attribute__ ( ( interrupt , no_auto_psv ) ) _U1TXInterrupt (void);
void SendOne();
unsigned char CB_TX1_IsTranmitting(void);
int CB_TX1_RemainingSize(void);
void SendMessage (unsigned char * message ,int length);

#ifdef	__cplusplus
#endif
#endif	/* CB_TX1_H */

