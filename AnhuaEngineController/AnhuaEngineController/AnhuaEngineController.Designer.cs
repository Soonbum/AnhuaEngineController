namespace AnhuaEngineController
{
    partial class AnhuaEngineController
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonAutoConnect = new Button();
            ButtonPowerOn = new Button();
            ButtonPowerOff = new Button();
            ButtonLEDOff = new Button();
            ButtonLEDOn = new Button();
            ButtonSetCurrent = new Button();
            TextBoxCurrent = new TextBox();
            ButtonSetFanSpeed1 = new Button();
            ButtonSetFanSpeed2 = new Button();
            TextBoxFanSpeed1 = new TextBox();
            TextBoxFanSpeed2 = new TextBox();
            TextBoxRotation = new TextBox();
            ButtonSetRotation = new Button();
            RichTextBoxLog = new RichTextBox();
            LabelPort = new Label();
            ButtonConnect = new Button();
            ComboBoxPorts = new ComboBox();
            TextBoxFanSpeed3 = new TextBox();
            ButtonSetFanSpeed3 = new Button();
            ButtonLogClear = new Button();
            TextBoxPeriod = new TextBox();
            TextBoxOnTime = new TextBox();
            LabelPeriod = new Label();
            LabelOnTime = new Label();
            ButtonStart = new Button();
            TestPanel = new Panel();
            TestPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonAutoConnect
            // 
            ButtonAutoConnect.Location = new Point(30, 14);
            ButtonAutoConnect.Name = "ButtonAutoConnect";
            ButtonAutoConnect.Size = new Size(104, 34);
            ButtonAutoConnect.TabIndex = 6;
            ButtonAutoConnect.Text = "Auto Connect";
            ButtonAutoConnect.UseVisualStyleBackColor = true;
            ButtonAutoConnect.Click += ButtonAutoConnect_Click;
            // 
            // ButtonPowerOn
            // 
            ButtonPowerOn.Location = new Point(277, 14);
            ButtonPowerOn.Name = "ButtonPowerOn";
            ButtonPowerOn.Size = new Size(101, 34);
            ButtonPowerOn.TabIndex = 7;
            ButtonPowerOn.Text = "Power On";
            ButtonPowerOn.UseVisualStyleBackColor = true;
            ButtonPowerOn.Click += ButtonPowerOn_Click;
            // 
            // ButtonPowerOff
            // 
            ButtonPowerOff.Location = new Point(277, 54);
            ButtonPowerOff.Name = "ButtonPowerOff";
            ButtonPowerOff.Size = new Size(101, 34);
            ButtonPowerOff.TabIndex = 8;
            ButtonPowerOff.Text = "Power Off";
            ButtonPowerOff.UseVisualStyleBackColor = true;
            ButtonPowerOff.Click += ButtonPowerOff_Click;
            // 
            // ButtonLEDOff
            // 
            ButtonLEDOff.Location = new Point(392, 54);
            ButtonLEDOff.Name = "ButtonLEDOff";
            ButtonLEDOff.Size = new Size(101, 34);
            ButtonLEDOff.TabIndex = 10;
            ButtonLEDOff.Text = "LED Off";
            ButtonLEDOff.UseVisualStyleBackColor = true;
            ButtonLEDOff.Click += ButtonLEDOff_Click;
            // 
            // ButtonLEDOn
            // 
            ButtonLEDOn.Location = new Point(392, 14);
            ButtonLEDOn.Name = "ButtonLEDOn";
            ButtonLEDOn.Size = new Size(101, 34);
            ButtonLEDOn.TabIndex = 9;
            ButtonLEDOn.Text = "LED On";
            ButtonLEDOn.UseVisualStyleBackColor = true;
            ButtonLEDOn.Click += ButtonLEDOn_Click;
            // 
            // ButtonSetCurrent
            // 
            ButtonSetCurrent.Location = new Point(30, 113);
            ButtonSetCurrent.Name = "ButtonSetCurrent";
            ButtonSetCurrent.Size = new Size(104, 46);
            ButtonSetCurrent.TabIndex = 11;
            ButtonSetCurrent.Text = "Set Current\r\n0~1023";
            ButtonSetCurrent.UseVisualStyleBackColor = true;
            ButtonSetCurrent.Click += ButtonSetCurrent_Click;
            // 
            // TextBoxCurrent
            // 
            TextBoxCurrent.Location = new Point(30, 171);
            TextBoxCurrent.Name = "TextBoxCurrent";
            TextBoxCurrent.Size = new Size(101, 23);
            TextBoxCurrent.TabIndex = 12;
            TextBoxCurrent.Text = "400";
            // 
            // ButtonSetFanSpeed1
            // 
            ButtonSetFanSpeed1.Location = new Point(139, 113);
            ButtonSetFanSpeed1.Name = "ButtonSetFanSpeed1";
            ButtonSetFanSpeed1.Size = new Size(114, 46);
            ButtonSetFanSpeed1.TabIndex = 15;
            ButtonSetFanSpeed1.Text = "Set Fan Speed 1\r\n0~100";
            ButtonSetFanSpeed1.UseVisualStyleBackColor = true;
            ButtonSetFanSpeed1.Click += ButtonSetFanSpeed1_Click;
            // 
            // ButtonSetFanSpeed2
            // 
            ButtonSetFanSpeed2.Location = new Point(259, 113);
            ButtonSetFanSpeed2.Name = "ButtonSetFanSpeed2";
            ButtonSetFanSpeed2.Size = new Size(114, 46);
            ButtonSetFanSpeed2.TabIndex = 16;
            ButtonSetFanSpeed2.Text = "Set Fan Speed 2\r\n0~100";
            ButtonSetFanSpeed2.UseVisualStyleBackColor = true;
            ButtonSetFanSpeed2.Click += ButtonSetFanSpeed2_Click;
            // 
            // TextBoxFanSpeed1
            // 
            TextBoxFanSpeed1.Location = new Point(164, 171);
            TextBoxFanSpeed1.Name = "TextBoxFanSpeed1";
            TextBoxFanSpeed1.Size = new Size(65, 23);
            TextBoxFanSpeed1.TabIndex = 17;
            TextBoxFanSpeed1.Text = "50";
            // 
            // TextBoxFanSpeed2
            // 
            TextBoxFanSpeed2.Location = new Point(284, 171);
            TextBoxFanSpeed2.Name = "TextBoxFanSpeed2";
            TextBoxFanSpeed2.Size = new Size(65, 23);
            TextBoxFanSpeed2.TabIndex = 18;
            TextBoxFanSpeed2.Text = "50";
            // 
            // TextBoxRotation
            // 
            TextBoxRotation.Location = new Point(428, 254);
            TextBoxRotation.Name = "TextBoxRotation";
            TextBoxRotation.Size = new Size(65, 23);
            TextBoxRotation.TabIndex = 20;
            TextBoxRotation.Text = "0";
            // 
            // ButtonSetRotation
            // 
            ButtonSetRotation.Location = new Point(30, 241);
            ButtonSetRotation.Name = "ButtonSetRotation";
            ButtonSetRotation.Size = new Size(381, 46);
            ButtonSetRotation.TabIndex = 19;
            ButtonSetRotation.Text = "Set Rotation\r\n0 (0), 1 (90), 2 (180), 3 (270)";
            ButtonSetRotation.UseVisualStyleBackColor = true;
            ButtonSetRotation.Click += ButtonSetRotation_Click;
            // 
            // RichTextBoxLog
            // 
            RichTextBoxLog.Location = new Point(34, 307);
            RichTextBoxLog.Name = "RichTextBoxLog";
            RichTextBoxLog.Size = new Size(459, 187);
            RichTextBoxLog.TabIndex = 31;
            RichTextBoxLog.Text = "";
            // 
            // LabelPort
            // 
            LabelPort.AutoSize = true;
            LabelPort.Location = new Point(150, 24);
            LabelPort.Name = "LabelPort";
            LabelPort.Size = new Size(29, 15);
            LabelPort.TabIndex = 32;
            LabelPort.Text = "Port";
            // 
            // ButtonConnect
            // 
            ButtonConnect.Location = new Point(30, 54);
            ButtonConnect.Name = "ButtonConnect";
            ButtonConnect.Size = new Size(104, 34);
            ButtonConnect.TabIndex = 33;
            ButtonConnect.Text = "Connect";
            ButtonConnect.UseVisualStyleBackColor = true;
            ButtonConnect.Click += ButtonConnect_Click;
            // 
            // ComboBoxPorts
            // 
            ComboBoxPorts.FormattingEnabled = true;
            ComboBoxPorts.Location = new Point(150, 61);
            ComboBoxPorts.Name = "ComboBoxPorts";
            ComboBoxPorts.Size = new Size(101, 23);
            ComboBoxPorts.TabIndex = 34;
            // 
            // TextBoxFanSpeed3
            // 
            TextBoxFanSpeed3.Location = new Point(404, 171);
            TextBoxFanSpeed3.Name = "TextBoxFanSpeed3";
            TextBoxFanSpeed3.Size = new Size(65, 23);
            TextBoxFanSpeed3.TabIndex = 36;
            TextBoxFanSpeed3.Text = "50";
            // 
            // ButtonSetFanSpeed3
            // 
            ButtonSetFanSpeed3.Location = new Point(379, 113);
            ButtonSetFanSpeed3.Name = "ButtonSetFanSpeed3";
            ButtonSetFanSpeed3.Size = new Size(114, 46);
            ButtonSetFanSpeed3.TabIndex = 35;
            ButtonSetFanSpeed3.Text = "Set Fan Speed 3\r\n0~100";
            ButtonSetFanSpeed3.UseVisualStyleBackColor = true;
            ButtonSetFanSpeed3.Click += ButtonSetFanSpeed3_Click;
            // 
            // ButtonLogClear
            // 
            ButtonLogClear.Location = new Point(34, 500);
            ButtonLogClear.Name = "ButtonLogClear";
            ButtonLogClear.Size = new Size(459, 34);
            ButtonLogClear.TabIndex = 37;
            ButtonLogClear.Text = "Clear";
            ButtonLogClear.UseVisualStyleBackColor = true;
            ButtonLogClear.Click += ButtonLogClear_Click;
            // 
            // TextBoxPeriod
            // 
            TextBoxPeriod.Location = new Point(15, 53);
            TextBoxPeriod.Name = "TextBoxPeriod";
            TextBoxPeriod.Size = new Size(100, 23);
            TextBoxPeriod.TabIndex = 38;
            // 
            // TextBoxOnTime
            // 
            TextBoxOnTime.Location = new Point(134, 53);
            TextBoxOnTime.Name = "TextBoxOnTime";
            TextBoxOnTime.Size = new Size(100, 23);
            TextBoxOnTime.TabIndex = 39;
            // 
            // LabelPeriod
            // 
            LabelPeriod.AutoSize = true;
            LabelPeriod.Location = new Point(36, 26);
            LabelPeriod.Name = "LabelPeriod";
            LabelPeriod.Size = new Size(58, 15);
            LabelPeriod.TabIndex = 40;
            LabelPeriod.Text = "Period (s)";
            // 
            // LabelOnTime
            // 
            LabelOnTime.AutoSize = true;
            LabelOnTime.Location = new Point(151, 26);
            LabelOnTime.Name = "LabelOnTime";
            LabelOnTime.Size = new Size(66, 15);
            LabelOnTime.TabIndex = 41;
            LabelOnTime.Text = "OnTime (s)";
            // 
            // ButtonStart
            // 
            ButtonStart.Location = new Point(250, 26);
            ButtonStart.Name = "ButtonStart";
            ButtonStart.Size = new Size(195, 50);
            ButtonStart.TabIndex = 42;
            ButtonStart.Text = "Start";
            ButtonStart.UseVisualStyleBackColor = true;
            ButtonStart.Click += ButtonStart_Click;
            // 
            // TestPanel
            // 
            TestPanel.Controls.Add(TextBoxPeriod);
            TestPanel.Controls.Add(ButtonStart);
            TestPanel.Controls.Add(TextBoxOnTime);
            TestPanel.Controls.Add(LabelOnTime);
            TestPanel.Controls.Add(LabelPeriod);
            TestPanel.Location = new Point(34, 552);
            TestPanel.Name = "TestPanel";
            TestPanel.Size = new Size(459, 100);
            TestPanel.TabIndex = 43;
            // 
            // AnhuaEngineController
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 682);
            Controls.Add(TestPanel);
            Controls.Add(ButtonLogClear);
            Controls.Add(TextBoxFanSpeed3);
            Controls.Add(ButtonSetFanSpeed3);
            Controls.Add(ComboBoxPorts);
            Controls.Add(ButtonConnect);
            Controls.Add(LabelPort);
            Controls.Add(RichTextBoxLog);
            Controls.Add(TextBoxRotation);
            Controls.Add(ButtonSetRotation);
            Controls.Add(TextBoxFanSpeed2);
            Controls.Add(TextBoxFanSpeed1);
            Controls.Add(ButtonSetFanSpeed2);
            Controls.Add(ButtonSetFanSpeed1);
            Controls.Add(TextBoxCurrent);
            Controls.Add(ButtonSetCurrent);
            Controls.Add(ButtonLEDOff);
            Controls.Add(ButtonLEDOn);
            Controls.Add(ButtonPowerOff);
            Controls.Add(ButtonPowerOn);
            Controls.Add(ButtonAutoConnect);
            Name = "AnhuaEngineController";
            Text = "AnhuaEngineController";
            TestPanel.ResumeLayout(false);
            TestPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button ButtonAutoConnect;
        private Button ButtonPowerOn;
        private Button ButtonPowerOff;
        private Button ButtonLEDOff;
        private Button ButtonLEDOn;
        private Button ButtonSetCurrent;
        private TextBox TextBoxCurrent;
        private Button ButtonSetFanSpeed1;
        private Button ButtonSetFanSpeed2;
        private TextBox TextBoxFanSpeed1;
        private TextBox TextBoxFanSpeed2;
        private TextBox TextBoxRotation;
        private Button ButtonSetRotation;
        private RichTextBox RichTextBoxLog;
        private Label LabelPort;
        private Button ButtonConnect;
        private ComboBox ComboBoxPorts;
        private TextBox TextBoxFanSpeed3;
        private Button ButtonSetFanSpeed3;
        private Button ButtonLogClear;
        private TextBox TextBoxPeriod;
        private TextBox TextBoxOnTime;
        private Label LabelPeriod;
        private Label LabelOnTime;
        private Button ButtonStart;
        private Panel TestPanel;
    }
}
