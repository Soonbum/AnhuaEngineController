using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnhuaEngineController;

public partial class AnhuaEngineController : Form
{
    private AnhuaEngine engine = new();

    public AnhuaEngineController()
    {
        InitializeComponent();
        RefreshPorts();
    }

    private void RefreshPorts()
    {
        ComboBoxPorts.Items.Clear();
        string[] ports = SerialPort.GetPortNames();
        Array.Sort(ports);
        ComboBoxPorts.Items.AddRange(ports);
        if (ports.Length > 0)
            ComboBoxPorts.SelectedIndex = 0;
    }

    private void Log(string msg)
    {
        RichTextBoxLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
    }

    private void ButtonAutoConnect_Click(object sender, EventArgs e)
    {
        // 이미 연결된 포트가 있는 경우, 연결 해제
        if (engine.CurrentPort != "NoSerialPort")
        {
            engine.Disconnect();
            ButtonAutoConnect.Text = "Auto Connect";
            ButtonAutoConnect.BackColor = SystemColors.Control;
            Log("Disconnected");
            return;
        }

        ButtonAutoConnect.Enabled = false;
        ButtonAutoConnect.Text = "Scanning...";
        Cursor.Current = Cursors.WaitCursor; // 모래시계 커서

        // 비동기처럼 보이기 위해 UI 갱신을 잠시 허용 (간단한 방식)
        Application.DoEvents();

        // 자동 찾기 실행
        string foundPort = engine.FindPort();

        Cursor.Current = Cursors.Default;
        ButtonAutoConnect.Enabled = true;
        ButtonAutoConnect.Text = "Auto Connect";

        // 포트를 찾은 경우
        if (foundPort != null)
        {
            LabelPort.Text = foundPort;

            if (engine.Connect(foundPort))
            {
                MessageBox.Show($"Device found on {foundPort} and connected!", "Success");
                ButtonAutoConnect.Text = "Disconnect";
                ButtonAutoConnect.BackColor = Color.LightGreen;
            }
            else
            {
                MessageBox.Show($"Device found on {foundPort} but failed to connect.", "Error");
            }
        }
        else
        {
            MessageBox.Show("Anhua UV Engine not found.\nPlease check USB connection and power.", "Not Found");
        }
    }

    private void ButtonConnect_Click(object sender, EventArgs e)
    {
        if (ComboBoxPorts.SelectedItem == null) return;

        if (!engine.IsConnected)
        {
            if (engine.Connect(ComboBoxPorts.SelectedItem.ToString()))
            {
                ButtonConnect.Text = "Disconnect";
                ButtonConnect.BackColor = Color.LightGreen;
                Log("Connected to " + ComboBoxPorts.SelectedItem);
            }
        }
        else
        {
            engine.Disconnect();
            ButtonConnect.Text = "Connect";
            ButtonConnect.BackColor = SystemColors.Control;
            Log("Disconnected");
        }
    }

    private void ButtonPowerOn_Click(object sender, EventArgs e)
    {
        string returnString = engine.ProjectorOnOff(true);
        Log($"Power ON Sent - {returnString}");
    }

    private void ButtonPowerOff_Click(object sender, EventArgs e)
    {
        string returnString = engine.ProjectorOnOff(false);
        Log($"Power OFF Sent - {returnString}");
    }

    private void ButtonLEDOn_Click(object sender, EventArgs e)
    {
        string returnString = engine.LEDOnOff(true);
        Log($"LED ON Sent - {returnString}");
    }

    private void ButtonLEDOff_Click(object sender, EventArgs e)
    {
        string returnString = engine.LEDOnOff(false);
        Log($"LED OFF Sent - {returnString}");
    }

    private void ButtonSetCurrent_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxCurrent.Text, out int current))
        {
            string returnString = engine.SetLEDCurrent(current);
            Log($"Set Current to {current} - {returnString}");
        }
        else
        {
            MessageBox.Show("숫자만 입력하세요 (0~1023)");
        }
    }

    private void ButtonSetFanSpeed1_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed1.Text, out int s))
        {
            string returnString = engine.SetFanSpeed(1, s); Log($"Set Fan1: {s}% - {returnString}");
        }
    }

    private void ButtonSetFanSpeed2_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed2.Text, out int s))
        {
            string returnString = engine.SetFanSpeed(2, s); Log($"Set Fan2: {s}% - {returnString}");
        }
    }

    private void ButtonSetFanSpeed3_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed3.Text, out int s))
        {
            string returnString = engine.SetFanSpeed(3, s); Log($"Set Fan3: {s}% - {returnString}");
        }
    }

    private void ButtonSetRotation_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxRotation.Text, out int mode))
        {
            if (mode >= 0 && mode <= 3)
            {
                string returnString = engine.SetFlipPicture(mode);
                Log($"Set Rotation Mode: {mode} - {returnString}");
            }
        }
    }

    private void ButtonLogClear_Click(object sender, EventArgs e)
    {
        RichTextBoxLog.Clear();
    }
}

public class AnhuaEngine
{
    private SerialPort serialPort;

    public AnhuaEngine()
    {
        serialPort = new SerialPort();
    }

    public bool IsConnected => serialPort.IsOpen;
    public string CurrentPort => (serialPort != null && serialPort.IsOpen) ? serialPort.PortName : "NoSerialPort";

    public string FindPort()
    {
        string[] ports = SerialPort.GetPortNames();

        // 포트가 하나도 없으면 null 반환
        if (ports.Length == 0) return null;

        foreach (var port in ports)
        {
            // 이미 연결된 포트는 건너뜀
            if (serialPort.IsOpen && serialPort.PortName == port) continue;

            SerialPort testPort = null;
            try
            {
                testPort = new(port, 9600)
                {
                    ReadTimeout = 5000,
                    WriteTimeout = 500
                };
                testPort.Open();
                testPort.DiscardInBuffer(); // 기존 잡동사니 데이터 비우기

                // 연결하자마자 Power On 명령을 내림 (정상이면 OK 문자열 올 것)
                testPort.Write("CM+PWRE=1\r\n");

                // 데이터 수신 대기 (약간의 딜레이 필요)
                System.Threading.Thread.Sleep(1000);

                string response = testPort.ReadExisting();

                // 디버깅용: 실제 어떤 응답이 오는지 콘솔에 출력해 보세요.
                Console.WriteLine($"Port {port} Response: {response}");

                if (response.Contains("OK"))
                {
                    // 어떤 응답이라도 온다는 것은 9600bps로 통신하는 장비라는 뜻입니다.
                    // 더 정확히 하려면 if (response.Contains("ERROR")) 로 변경하세요.
                    testPort.Close();
                    return port;
                }
            }
            catch
            {
                // 포트 열기 실패 또는 타임아웃 시 무시
            }
            finally
            {
                if (testPort != null && testPort.IsOpen)
                    testPort.Close();
            }
        }
        return null;
    }

    public bool Connect(string portName)
    {
        try
        {
            if (serialPort.IsOpen) serialPort.Close();
            serialPort.PortName = portName;
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.ReadTimeout = 500;
            serialPort.Open();
            return true;
        }
        catch { return false; }
    }

    public void Disconnect()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
            serialPort.PortName = "NoSerialPort";
        }
    }

    private string SendCommand(string cmd)
    {
        if (!serialPort.IsOpen) return "Error: Port Closed";

        try
        {
            // C++ 코드: fullCmd = cmd + "\r\n";
            string fullCmd = cmd + "\r\n";

            // 전송
            serialPort.Write(fullCmd);
            Console.WriteLine($"[TX] {fullCmd.Trim()}"); // 디버깅용 로그

            // C++은 OK 또는 ERROR를 기다립니다.
            // 여기서는 간단히 0.5초 대기 후 버퍼를 읽습니다.
            Thread.Sleep(500);

            string response = serialPort.ReadExisting();
            Console.WriteLine($"[RX] {response.Trim()}");

            return response.Trim();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // 1. Projector On/Off
    // C++: QString("CM+PWRE=%1").arg(projectorEnable ? 1 : 0)
    public string ProjectorOnOff(bool enable)
    {
        int val = enable ? 1 : 0;
        string cmd = $"CM+PWRE={val}";
        return SendCommand(cmd);
    }

    // 2. LED On/Off
    // C++: QString("CM+LEDE=%1").arg(LEDEnable ? 1 : 0)
    public string LEDOnOff(bool enable)
    {
        int val = enable ? 1 : 0;
        string cmd = $"CM+LEDE={val}";
        return SendCommand(cmd);
    }

    // 3. Set LED Current (밝기)
    // C++: QString("CM+LEDS=%1").arg(iBrightness, 4, 10, QChar('0'))
    // 4자리 숫자로 맞추고 빈 곳은 0으로 채움 (예: 50 -> 0050)
    public string SetLEDCurrent(int brightness)
    {
        // Clamp (0 ~ 1023)
        int val = Math.Max(0, Math.Min(1023, brightness));

        // C# 포맷팅: "D4"는 10진수 4자리를 의미 (0050)
        string cmd = $"CM+LEDS={val:D4}";
        return SendCommand(cmd);
    }

    // 4. Set Fan Speed
    // C++: QString("CM+FAN%1=%2").arg(iFanNo).arg(iSpeed, 3, 10, QChar('0'))
    // 팬 번호는 그대로, 속도는 3자리 0 채움 (예: 100 -> 100, 50 -> 050)
    public string SetFanSpeed(int fanNo, int speed)
    {
        if (fanNo < 1 || fanNo > 3) return "InvalidFanNo"; // 팬 번호 유효성 검사

        // Clamp (0 ~ 100)
        int val = Math.Max(0, Math.Min(100, speed));

        // "D3"는 10진수 3자리 (050)
        string cmd = $"CM+FAN{fanNo}={val:D3}";
        return SendCommand(cmd);
    }

    // 5. Flip Picture (화면 뒤집기)
    // C++: QString("CM+SPJF=%1").arg(iFlipCommand)
    public string SetFlipPicture(int flipCommand)
    {
        string cmd = $"CM+SPJF={flipCommand}";
        return SendCommand(cmd);
    }
}