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

                // (선택) 연결 되자마자 상태 한번 읽어오기
                // Log("Status: " + _engine.GetLedState());
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
        engine.PowerOn();
        Log("Power ON Sent");
    }

    private void ButtonPowerOff_Click(object sender, EventArgs e)
    {
        engine.PowerOff();
        Log("Power OFF Sent");
    }

    private void ButtonLEDOn_Click(object sender, EventArgs e)
    {
        engine.LedOn();
        Log("LED ON Sent");
    }

    private void ButtonLEDOff_Click(object sender, EventArgs e)
    {
        engine.LedOff();
        Log("LED OFF Sent");
    }

    private void ButtonSetCurrent_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxR.Text, out int r) && int.TryParse(TextBoxG.Text, out int g) && int.TryParse(TextBoxB.Text, out int b))
        {
            engine.SetCurrent(r, g, b);
            Log($"Set Current: R{r} G{g} B{b}");
        }
        else
        {
            MessageBox.Show("숫자만 입력하세요 (0~874)");
        }
    }

    private void ButtonSetFanSpeed1_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed1.Text, out int s))
        {
            engine.SetFan1(s); Log($"Set Fan1: {s}%");
        }
    }

    private void ButtonSetFanSpeed2_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed2.Text, out int s))
        {
            engine.SetFan2(s); Log($"Set Fan2: {s}%");
        }
    }

    private void ButtonSetRotation_Click(object sender, EventArgs e)
    {
        int.TryParse(TextBoxRotation.Text, out int mode);
        if (mode >= 0 && mode <= 3)
        {
            engine.SetRotation(mode);
            Log($"Set Rotation Mode: {mode}");
        }
    }

    private void ButtonGetTemperature_Click(object sender, EventArgs e)
    {
        string val = engine.GetTemperature();
        LabelTemperature.Text = val;
        Log($"Temp: {val}");
    }

    private void ButtonGetVersion_Click(object sender, EventArgs e)
    {
        string val = engine.GetVersion();
        LabelVersion.Text = val;
        Log($"Ver: {val}");
    }

    private void ButtonGetTime_Click(object sender, EventArgs e)
    {
        string val = engine.GetWorkingTime();
        LabelTime.Text = val;
        Log($"Time: {val}");
    }

    private void ButtonGetPWM_Click(object sender, EventArgs e)
    {
        string val = engine.GetPWM();
        LabelPWM.Text = val;
        Log($"PWM: {val}");
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
    public string CurrentPort => serialPort.PortName;

    public string FindPort()
    {
        string[] ports = SerialPort.GetPortNames();

        // 포트가 없으면 즉시 종료
        if (ports.Length == 0) return null;

        foreach (string port in ports)
        {
            // 이미 메인 포트가 열려있고 그게 이 포트라면 건너뜀 (선택 사항)
            if (serialPort.IsOpen && serialPort.PortName == port) continue;

            SerialPort testPort = new(port, 9600, Parity.None, 8, StopBits.One)
            {
                ReadTimeout = 200, // 스캔 속도를 위해 타임아웃을 짧게 설정 (0.2초)
                WriteTimeout = 200
            };

            try
            {
                testPort.Open();

                // 상태 확인 명령어 전송 (Ping)
                // Command query: 0x2A 0x53 0x0D
                byte[] pingCmd = [0x2A, 0x53, 0x0D];
                testPort.DiscardInBuffer();
                testPort.Write(pingCmd, 0, pingCmd.Length);

                // 응답 대기
                Thread.Sleep(100); // 데이터 수신 대기

                if (testPort.BytesToRead >= 3) // 최소 3바이트 이상 응답이 와야 함
                {
                    byte[] buffer = new byte[testPort.BytesToRead];
                    testPort.Read(buffer, 0, buffer.Length);

                    // 유효성 검사
                    // 올바른 응답은 0x2A로 시작해야 함 
                    // 응답 예시: 0x2A 0x4B...(ON) or 0x2A 0x47...(OFF)
                    if (buffer[0] == 0x2A && (buffer[1] == 0x4B || buffer[1] == 0x47))
                    {
                        testPort.Close();
                        return port; // 포트 발견
                    }
                }
            }
            catch
            {
                // 열기 실패하거나 타임아웃 등 오류 발생 시 무시하고 다음 포트로
            }
            finally
            {
                if (testPort.IsOpen) testPort.Close();
                testPort.Dispose(); // 리소스 해제
            }
        }

        return null; // 모든 포트를 뒤져도 못 찾음
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
        if (serialPort.IsOpen) serialPort.Close();
    }

    // 공통 송수신 함수
    private byte[] SendAndReceive(byte[] command, int expectedResponseLength)
    {
        if (!serialPort.IsOpen) return null;

        // 버퍼 비우기
        serialPort.DiscardInBuffer();
        serialPort.DiscardOutBuffer();
        
        // 전송
        serialPort.Write(command, 0, command.Length);

        // 응답 대기 (간단한 동기 방식)
        try
        {
            // 데이터가 들어올 때까지 잠시 대기
            int timeout = 0;
            while (serialPort.BytesToRead < expectedResponseLength && timeout < 10)
            {
                Thread.Sleep(50);
                timeout++;
            }

            if (serialPort.BytesToRead > 0)
            {
                int bytesToRead = serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort.Read(buffer, 0, bytesToRead);
                return buffer;
            }
        }
        catch { }
        return null;
    }

    public void PowerOn() => SendAndReceive([0x2A, 0xFA, 0x0D], 0);

    public void PowerOff() => SendAndReceive([0x2A, 0xFB, 0x0D], 0);

    public void LedOn() => SendAndReceive([0x2A, 0x4B, 0x0D], 0);

    public void LedOff() => SendAndReceive([0x2A, 0x47, 0x0D], 0);

    public void ResetWorkingTime() => SendAndReceive([0x2A, 0xFE, 0x0D], 0);

    // Save current & rotation state
    public void SaveConfiguration() => SendAndReceive([0x2A, 0xFC, 0x0D], 0);

    // Command query (Check LED State)
    public string GetLedState()
    {
        byte[] response = SendAndReceive([0x2A, 0x53, 0x0D], 4);
        if (response != null && response.Length >= 2)
        {
            if (response[1] == 0x4B) return "LED ON";
            if (response[1] == 0x47) return "LED OFF";
        }
        return "Unknown";
    }

    // Engine Current Change (DLPC6540 Protocol) - Range 0 ~ 874
    public void SetCurrent(int r, int g, int b)
    {
        r = Math.Max(0, Math.Min(874, r));
        g = Math.Max(0, Math.Min(874, g));
        b = Math.Max(0, Math.Min(874, b));

        byte r_l = (byte)(r & 0xFF); byte r_m = (byte)((r >> 8) & 0xFF);
        byte g_l = (byte)(g & 0xFF); byte g_m = (byte)((g >> 8) & 0xFF);
        byte b_l = (byte)(b & 0xFF); byte b_m = (byte)((b >> 8) & 0xFF);

        // Header(0x55) + Len(0x07) + Cmd(0x84) + Data(6 bytes) + Checksum
        List<byte> packet = [0x55, 0x07, 0x84, r_l, r_m, g_l, g_m, b_l, b_m];

        // Checksum Formula[cite: 228]: ~(Sum & 0xFF)
        int sum = 0;
        foreach (byte val in packet) sum += val;
        // *주의: Header(0x55)는 체크섬 계산에 포함되는지 사양서 4.1 공식 확인 필요.
        // 사양서 224줄 예시 코드를 보면 USART_RX_BUF 전체를 더합니다.
        // 즉, 패킷의 모든 바이트 합계의 반전입니다.

        byte checksum = (byte)(~(sum & 0xFF));
        packet.Add(checksum);

        SendAndReceive([.. packet], 0);
    }

    // LED Temperature
    public string GetTemperature()
    {
        byte[] res = SendAndReceive([0x2A, 0x4E, 0x0D], 4);
        // Feedback: 0x2A 0x4E 0xXX 0x0D
        if (res != null && res.Length >= 3 && res[1] == 0x4E)
        {
            return $"{res[2]} °C";
        }
        return "Error";
    }

    // Get LED working time
    public string GetWorkingTime()
    {
        byte[] res = SendAndReceive([0x2A, 0x4F, 0x0D], 5);
        // Feedback: 0x2A 0x4F 0xAA 0xXX 0x0D (Low, High)
        if (res != null && res.Length >= 4 && res[1] == 0x4F)
        {
            int time = res[2] + (res[3] << 8); // Lower + Upper
            return $"{time} Hours";
        }
        return "Error";
    }

    // Get Software Version
    public string GetVersion()
    {
        byte[] res = SendAndReceive([0x2A, 0xF5, 0x0D], 10);
        if (res != null && res.Length > 2)
        {
            // ASCII String 변환
            return Encoding.ASCII.GetString(res).Trim();
        }
        return "Error";
    }

    // Set FAN 1 Speed
    public void SetFan1(int speed)
    {
        byte val = (byte)Math.Max(0, Math.Min(100, speed)); // 0-100%
        SendAndReceive([0x2A, 0xEE, val], 0);
    }

    // Set FAN 2 Speed
    public void SetFan2(int speed)
    {
        byte val = (byte)Math.Max(0, Math.Min(100, speed)); // 0-100%
        SendAndReceive([0x2A, 0xEF, val], 0);
    }

    // Set Screen Rotation
    public void SetRotation(int mode)
    {
        // Mode: "00: Normal", "01: 90 Deg", "02: 180 Deg", "03: 270 Deg"
        byte val = (byte)Math.Max(0, Math.Min(3, mode));
        SendAndReceive([0x2A, 0xF6, val], 0);
    }

    // Read PWM Value
    public string GetPWM()
    {
        byte[] res = SendAndReceive([0x2A, 0x54, 0x0D], 5);
        // Feedback: 0x2A 0x54 High Low 0x0D
        if (res != null && res.Length >= 4 && res[1] == 0x54)
        {
            int pwm = res[3] + (res[2] << 8); // 주의: 상위 바이트가 먼저 옴(PWM_H, PWM_L)
            return pwm.ToString();
        }
        return "Error";
    }
}