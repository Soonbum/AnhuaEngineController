#include "Cls_EngineDF78.h"
#include <QThread>

#include "mainwindow.h"

Cls_EngineDF78::Cls_EngineDF78(QObject *parent) : QObject(parent)
{
    portName = "/dev/ttyUSB_DF78";
    pMainWindow = static_cast<MainWindow *>(this->parent());
    openSerial();
}


Cls_EngineDF78::~Cls_EngineDF78()
{
    if (serial.isOpen())
    {
        serial.close();
    }
}

void Cls_EngineDF78::setPortName(const QString &port)
{
    portName = port;
}

int Cls_EngineDF78::clamp(int value, int minVal, int maxVal)
{
    if (value < minVal)
    {
        return minVal;
    }

    if (value > maxVal)
    {
        return maxVal;
    }

    return value;
}

bool Cls_EngineDF78::openSerial()
{
    if (serial.isOpen())
    {
        return true;
    }

    emit pMainWindow->writeSystemLog("Command", "[DF78] Opening serial with port : " + portName, false);

    serial.setPortName(portName);
    serial.setBaudRate(QSerialPort::Baud9600);
    serial.setDataBits(QSerialPort::Data8);
    serial.setStopBits(QSerialPort::OneStop);
    serial.setParity(QSerialPort::NoParity);
    serial.setFlowControl(QSerialPort::NoFlowControl);

    if (!serial.open(QIODevice::ReadWrite))
    {
        return false;
        emit pMainWindow->writeSystemLog("Fatal", "[DF78] Failed to open serial port", false);
    }

        emit pMainWindow->writeSystemLog("System", "[DF78] Serial port opened", false);

    return true;
}

bool Cls_EngineDF78::reopenSerial()
{
    emit pMainWindow->writeSystemLog("System", "[DF78] Reopening serial port", false);

    if (serial.isOpen())
    {
        serial.close();
        emit pMainWindow->writeSystemLog("Status", "[DF78] Serial closed", false);
    }

    return openSerial();
}

bool Cls_EngineDF78::sendCommand(const QString &cmd, const bool &waitForReturn)
{
    if (!serial.isOpen())
    {
        emit pMainWindow->writeSystemLog("Fatal", "[DF78] Serial port was now opended before " + cmd, false);
        reopenSerial();
        emit pMainWindow->writeSystemLog("Fatal", "[DF78] Failed to open serial port", false);
        return false;
    }

    if (!QFile::exists("/dev/i2c-1"))
    {
        emit pMainWindow->writeSystemLog("Fatal", "[DF78] I2C device not available (/dev/i2c-1)", false);
        return false;
    }

    QString fullCmd = cmd + "\r\n";
    QByteArray cmdBytes = fullCmd.toUtf8();

    emit pMainWindow->writeSystemLog("Command", "[DF78] SEND → " + fullCmd.trimmed(), true);

    qint64 bytesWritten = serial.write(cmdBytes);
    if (bytesWritten != cmdBytes.size())
    {
        emit pMainWindow->writeSystemLog("Fatal", QString("[DF78] Partial write (%1 / %2 bytes)")
                                         .arg(bytesWritten).arg(cmdBytes.size()), false);
        return false;
    }

    if (!serial.waitForBytesWritten(100))
    {
        emit pMainWindow->writeSystemLog("Fatal", "[DF78] Timeout while writing command", false);
        reopenSerial();
        return false;
    }

    if (!waitForReturn) { return true; }

    QByteArray response;
    int totalWaitMs = 0;
    const int maxWaitMs = 18000;
    const int stepWaitMs = 200;

    while (totalWaitMs < maxWaitMs)
    {
        if (serial.waitForReadyRead(stepWaitMs))
        {
            response += serial.readAll();
            if (response.contains("OK") || response.contains("ERROR"))
            {
                break;
            }
        }
        else
        {
            totalWaitMs += stepWaitMs;
        }
    }

    QString strResponse = QString::fromUtf8(response).trimmed();

    emit pMainWindow->writeSystemLog("Command", "[DF78] RECV ← " + strResponse, true);

    if (strResponse.contains("OK"))
    {
        return true;
    }

    emit pMainWindow->writeSystemLog("Fatal", "[DF78] Invalid or negative response from projector", false);
    return false;
}

bool Cls_EngineDF78::projectorOnOff(bool projectorEnable)
{
    QString cmd = QString("CM+PWRE=%1").arg(projectorEnable ? 1 : 0);
    return sendCommand(cmd, true);
}

bool Cls_EngineDF78::LEDOnOff(bool LEDEnable)
{
    QString cmd = QString("CM+LEDE=%1").arg(LEDEnable ? 1 : 0);
    return sendCommand(cmd, false);
}

bool Cls_EngineDF78::setLEDCurrent(int iBrightness)
{
    iBrightness = clamp(iBrightness, 0, 1023);
    QString cmd = QString("CM+LEDS=%1").arg(iBrightness, 4, 10, QChar('0'));
    return sendCommand(cmd, false);
}

bool Cls_EngineDF78::setFanSpeed(int iFanNo, int iSpeed)
{
    if (iFanNo < 1 || iFanNo > 3)
    {
        return false;
    }

    iSpeed = clamp(iSpeed, 0, 100);
    QString cmd = QString("CM+FAN%1=%2").arg(iFanNo).arg(iSpeed, 3, 10, QChar('0'));
    return sendCommand(cmd, false);
}

bool Cls_EngineDF78::setFlipPicture(int iFlipCommand)
{
    QString cmd = QString("CM+SPJF=%1").arg(iFlipCommand);
    return sendCommand(cmd, false);
}
