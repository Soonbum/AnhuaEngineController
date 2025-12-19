using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnhuaEngineController;

public partial class AnhuaEngineController : Form
{
    private AnhuaEngine engine1 = new();
    private AnhuaEngine engine2 = new();
    private CancellationTokenSource _cts; // 작업 취소 토큰 소스
    private bool IsWorkingPeriodOperation = false;      // 현재 작동 중인지 확인하는 플래그

    private readonly string folderPath;
    private string filePath = "";
    private readonly long maxFileSizeInKiloBytes = 4096;

    public AnhuaEngineController()
    {
        InitializeComponent();

        RefreshPorts();
    }

    private void RefreshPorts()
    {
        ComboBoxPorts1.Items.Clear();
        ComboBoxPorts2.Items.Clear();

        string[] ports = SerialPort.GetPortNames();
        Array.Sort(ports);

        ComboBoxPorts1.Items.AddRange(ports);
        ComboBoxPorts2.Items.AddRange(ports);

        if (ports.Length > 0)
        {
            ComboBoxPorts1.SelectedIndex = 0;
            ComboBoxPorts2.SelectedIndex = 0;
        }
    }

    private void Log(string msg)
    {
        RichTextBoxLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
    }

    //private void ButtonAutoConnect_Click(object sender, EventArgs e)
    //{
    //    // 이미 연결된 포트가 있는 경우, 연결 해제
    //    if (engine.CurrentPort != "NoSerialPort")
    //    {
    //        engine.Disconnect();
    //        ButtonAutoConnect.Text = "Auto Connect";
    //        ButtonAutoConnect.BackColor = SystemColors.Control;
    //        Log("Disconnected");
    //        return;
    //    }

    //    ButtonAutoConnect.Enabled = false;
    //    ButtonAutoConnect.Text = "Scanning...";
    //    Cursor.Current = Cursors.WaitCursor; // 모래시계 커서

    //    // 비동기처럼 보이기 위해 UI 갱신을 잠시 허용 (간단한 방식)
    //    Application.DoEvents();

    //    // 자동 찾기 실행
    //    string foundPort = engine.FindPort();

    //    Cursor.Current = Cursors.Default;
    //    ButtonAutoConnect.Enabled = true;
    //    ButtonAutoConnect.Text = "Auto Connect";

    //    // 포트를 찾은 경우
    //    if (foundPort != null)
    //    {
    //        LabelPort.Text = foundPort;

    //        if (engine.Connect(foundPort))
    //        {
    //            MessageBox.Show($"Device found on {foundPort} and connected!", "Success");
    //            ButtonAutoConnect.Text = "Disconnect";
    //            ButtonAutoConnect.BackColor = Color.LightGreen;
    //        }
    //        else
    //        {
    //            MessageBox.Show($"Device found on {foundPort} but failed to connect.", "Error");
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show("Anhua UV Engine not found.\nPlease check USB connection and power.", "Not Found");
    //    }
    //}

    private void ButtonConnect1_Click(object sender, EventArgs e)
    {
        if (ComboBoxPorts1.SelectedItem == null) return;

        if (!engine1.IsConnected)
        {
            if (engine1.Connect(ComboBoxPorts1.SelectedItem.ToString()))
            {
                ButtonConnect1.Text = "Disconnect";
                ButtonConnect1.BackColor = Color.LightGreen;
                Log("Connected to " + ComboBoxPorts1.SelectedItem);
            }
        }
        else
        {
            engine1.Disconnect();
            ButtonConnect1.Text = "Connect";
            ButtonConnect1.BackColor = SystemColors.Control;
            Log("Disconnected");
        }
    }

    private void ButtonConnect2_Click(object sender, EventArgs e)
    {
        if (ComboBoxPorts2.SelectedItem == null) return;

        if (!engine2.IsConnected)
        {
            if (engine2.Connect(ComboBoxPorts2.SelectedItem.ToString()))
            {
                ButtonConnect2.Text = "Disconnect";
                ButtonConnect2.BackColor = Color.LightGreen;
                Log("Connected to " + ComboBoxPorts2.SelectedItem);
            }
        }
        else
        {
            engine2.Disconnect();
            ButtonConnect2.Text = "Connect";
            ButtonConnect2.BackColor = SystemColors.Control;
            Log("Disconnected");
        }
    }

    private void ButtonPowerOn_Click(object sender, EventArgs e)
    {
        string returnString1 = engine1.ProjectorOnOff(true);
        string returnString2 = engine2.ProjectorOnOff(true);

        Log($"Power ON Sent - (1) {returnString1} (2) {returnString2}");
    }

    private void ButtonPowerOff_Click(object sender, EventArgs e)
    {
        string returnString1 = engine1.ProjectorOnOff(false);
        string returnString2 = engine2.ProjectorOnOff(false);

        Log($"Power OFF Sent - (1) {returnString1} (2) {returnString2}");
    }

    private void ButtonLEDOn_Click(object sender, EventArgs e)
    {
        string returnString1 = engine1.LEDOnOff(true);
        string returnString2 = engine2.LEDOnOff(true);

        Log($"LED ON Sent - (1) {returnString1} (2) {returnString2}");
    }

    private void ButtonLEDOff_Click(object sender, EventArgs e)
    {
        string returnString1 = engine1.LEDOnOff(false);
        string returnString2 = engine2.LEDOnOff(false);

        Log($"LED OFF Sent - (1) {returnString1} (2) {returnString2}");
    }

    private void ButtonSetCurrent_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxCurrent.Text, out int current))
        {
            string returnString1 = engine1.SetLEDCurrent(current);
            string returnString2 = engine2.SetLEDCurrent(current);

            Log($"Set Current to {current} - (1) {returnString1} (2) {returnString2}");
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
            string returnString1 = engine1.SetFanSpeed(1, s);
            string returnString2 = engine2.SetFanSpeed(1, s);

            Log($"Set Fan1: {s}% - (1) {returnString1} (2) {returnString2}");
        }
    }

    private void ButtonSetFanSpeed2_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed2.Text, out int s))
        {
            string returnString1 = engine1.SetFanSpeed(2, s);
            string returnString2 = engine2.SetFanSpeed(2, s);

            Log($"Set Fan2: {s}% - (1) {returnString1} (2) {returnString2}");
        }
    }

    private void ButtonSetFanSpeed3_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxFanSpeed3.Text, out int s))
        {
            string returnString1 = engine1.SetFanSpeed(3, s);
            string returnString2 = engine2.SetFanSpeed(3, s);

            Log($"Set Fan3: {s}% - (1) {returnString1} (2) {returnString2}");
        }
    }

    private void ButtonSetRotation_Click(object sender, EventArgs e)
    {
        if (int.TryParse(TextBoxRotation.Text, out int mode))
        {
            if (mode >= 0 && mode <= 3)
            {
                string returnString1 = engine1.SetFlipPicture(mode);
                string returnString2 = engine2.SetFlipPicture(mode);

                Log($"Set Rotation Mode: {mode} - (1) {returnString1} (2) {returnString2}");
            }
        }
    }

    private void ButtonLogClear_Click(object sender, EventArgs e)
    {
        RichTextBoxLog.Clear();
    }

    private async void ButtonStart_Click(object sender, EventArgs e)
    {
        // 이미 작동 중인 경우 중지(Stop)
        if (IsWorkingPeriodOperation)
        {
            _cts?.Cancel(); // 비동기 작업 취소 요청
            IsWorkingPeriodOperation = false;
            ButtonStart.Text = "Start"; // 버튼 텍스트 원상복구

            // 안전을 위해 중지 시 LED를 확실히 끔
            engine1.LEDOnOff(false);
            engine2.LEDOnOff(false);
            return;
        }

        bool IsValidPeriod = int.TryParse(TextBoxPeriod.Text, out int period);
        bool IsValidOnTime = int.TryParse(TextBoxOnTime.Text, out int onTime);

        if (!IsValidPeriod)
        {
            MessageBox.Show("Period 값이 올바르지 않습니다. 초 단위 정수를 입력하세요.");
            return;
        }
        if (!IsValidOnTime)
        {
            MessageBox.Show("On Time 값이 올바르지 않습니다. 초 단위 정수를 입력하세요.");
            return;
        }
        if (!(onTime < period))
        {
            MessageBox.Show("On Time 값은 Period 값보다 작아야 합니다.");
            return;
        }

        // 시작(Start)
        IsWorkingPeriodOperation = true;
        ButtonStart.Text = "Stop"; // 버튼 텍스트를 중지로 변경

        // UI 입력값은 '초' 단위라고 가정하므로 밀리초(ms)로 변환 (필요 시 수정)
        int periodMs = period * 1000;
        int onTimeMs = onTime * 1000;

        CreateLogFile(); // 로그 파일 생성

        _cts = new CancellationTokenSource(); // 새로운 취소 토큰 생성

        try
        {
            // 별도의 비동기 메서드로 LED 제어 루프 시작
            await RunLedCycleAsync(periodMs, onTimeMs, _cts.Token);
        }
        catch (OperationCanceledException)
        {
            // 취소(Stop)되었을 때 발생하는 예외 무시 (정상 흐름)
        }
        catch (Exception ex)
        {
            MessageBox.Show($"오류 발생: {ex.Message}");
        }
        finally
        {
            // 작업이 끝나거나 취소되면 상태 초기화
            IsWorkingPeriodOperation = false;
            ButtonStart.Text = "Start";
            _cts?.Dispose();
            _cts = null;

            // 루프 종료 후 안전하게 LED 끄기
            engine1.LEDOnOff(false);
            engine2.LEDOnOff(false);
        }
    }

    // 실제 주기적으로 LED를 켜고 끄는 비동기 메서드
    private async Task RunLedCycleAsync(int periodMs, int onTimeMs, CancellationToken token)
    {
        int offTimeMs = periodMs - onTimeMs;

        UInt128 loopCounter = 0;
        string currentTime;

        string returnStr1, returnStr2;

        // 취소 요청이 들어오기 전까지 무한 반복
        while (!token.IsCancellationRequested)
        {
            currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 1. LED 켜기
            returnStr1 = engine1.LEDOnOff(true);
            returnStr2 = engine2.LEDOnOff(true);
            WriteLogFile($"{currentTime} - ({loopCounter}) LED ON - {returnStr1}, {returnStr2}");

            // On 시간만큼 대기 (취소 토큰 감지 포함)
            await Task.Delay(onTimeMs, token);

            // 2. LED 끄기
            engine1.LEDOnOff(false);
            engine2.LEDOnOff(false);
            WriteLogFile($"{currentTime} - ({loopCounter}) LED OFF - {returnStr1}, {returnStr2}");

            // Off 시간만큼 대기 (취소 토큰 감지 포함)
            await Task.Delay(offTimeMs, token);

            loopCounter++;
        }
    }

    public void CreateLogFile()
    {
        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        filePath = Path.Combine(appPath, "EventLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")) + ".txt";

        try
        {
            using FileStream fs = new(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Failed to create event log file: {ex.Message}");
        }
    }

    public void WriteLogFile(string textLine)
    {
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            CreateLogFile();

        try
        {
            // --- 파일 크기 검사 로직 (시작) ---
            bool createNewFile = false;

            if (string.IsNullOrEmpty(filePath))
            {
                createNewFile = true; // 파일 경로가 아예 없음 (초기 실행)
            }
            else
            {
                FileInfo fileInfo = new(filePath);
                if (!fileInfo.Exists)
                {
                    createNewFile = true; // 파일이 (삭제되는 등) 존재하지 않음
                }
                // 파일 크기가 최대 크기 초과
                else if (fileInfo.Length > maxFileSizeInKiloBytes)
                {
                    createNewFile = true;
                }
            }

            // 새 파일이 필요하면 생성
            if (createNewFile)
            {
                CreateLogFile(); // 이 메서드가 'filePath'를 새 경로로 업데이트합니다.
            }
            // --- 파일 크기 검사 로직 (끝) ---


            // 'filePath'는 이제 유효하거나 새로 생성된 경로임
            using FileStream fileStream = new(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true))
            {
                AutoFlush = true
            };

            // LogMessage에서 TrimEnd()로 NewLine을 제거했으므로 WriteLine 사용
            streamWriter.WriteLine(textLine);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Failed to write event log: {ex.Message}");
        }
        catch (Exception ex) // FileInfo 등 다른 예외 처리
        {
            Console.WriteLine($"Unexpected error in WriteEventLogFile: {ex.Message}");
        }
    }
}

public class AnhuaEngine
{
    // 모듈명: DF78 기준

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

    // 6. 상태 확인 (Status Check)
    public string GetStatus(int component)
    {
        // component별 의미와 기대할 수 있는 리턴값
        // (0) Power On(1)/Off(0)
        // (1) LED On(1)/Off(0)
        // (2) Rotation: (0) Front project (1) Lifting PLifting project (2) Lifting Back project (3) Back project
        // (3) LED Current: 0~1023
        // (4) Fan speed (범위: 0~100) - LED Fan
        // (5) Fan speed (범위: 0~100) - PCB 1 Fan
        // (6) Fan speed (범위: 0~100) - PCB 2 Fan
        // (무응답) 통신 오류

        string cmd = $"CM+STAT={component}";
        return SendCommand(cmd);
    }

    public double GetTemperatureLED()
    {
        string cmd = "CM+GTMP";
        string response = SendCommand(cmd);
        if (double.TryParse(response, out double value))
            return value;
        else
            return double.NaN; // 오류 시 NaN 반환
    }

    public double GetTemperaturePCB()
    {
        string cmd = "CM+GTMB";
        string response = SendCommand(cmd);
        if (double.TryParse(response, out double value))
            return value;
        else
            return double.NaN; // 오류 시 NaN 반환
    }
}