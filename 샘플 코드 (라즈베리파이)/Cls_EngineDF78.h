#ifndef CLS_ENGINEDF78_H
#define CLS_ENGINEDF78_H

#include <QObject>
#include <QSerialPort>
#include <QSerialPortInfo>

class MainWindow;

class Cls_EngineDF78 : public QObject
{
    Q_OBJECT

public:
    explicit Cls_EngineDF78(QObject *parent = 0);
    ~Cls_EngineDF78();

    bool projectorOnOff(bool projectorEnable);   // CM+PWRE=*\r\n    (*:0,1)
    bool LEDOnOff(bool LEDEnable);               // CM+LEDE=*\r\n    (*:0,1)
    bool setLEDCurrent(int iBrightness);         // CM+LEDS=****\r\n (****:0000~1023)
    bool setFanSpeed(int iFanNo, int iSpeed);    // CM+FAN@=***\r\n  (@:1~3, ***:000~100)
    bool setFlipPicture(int iFlipCommand);       // CM+SPJF=*\r\n

    void setPortName(const QString &portName);

private:
    QSerialPort serial;
    QString portName;

    MainWindow *pMainWindow;

    bool openSerial();
    bool reopenSerial();
    bool sendCommand(const QString &cmd, const bool &waitForReturn);
    int clamp(int value, int minVal, int maxVal);
};

#endif // CLS_ENGINEDF78_H
