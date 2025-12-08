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
            TextBoxR = new TextBox();
            TextBoxG = new TextBox();
            TextBoxB = new TextBox();
            ButtonSetFanSpeed1 = new Button();
            ButtonSetFanSpeed2 = new Button();
            TextBoxFanSpeed1 = new TextBox();
            TextBoxFanSpeed2 = new TextBox();
            TextBoxRotation = new TextBox();
            ButtonSetRotation = new Button();
            ButtonGetTemperature = new Button();
            LabelTemperature = new Label();
            LabelTime = new Label();
            ButtonGetTime = new Button();
            LabelPWM = new Label();
            ButtonGetPWM = new Button();
            LabelVersion = new Label();
            ButtonGetVersion = new Button();
            RichTextBoxLog = new RichTextBox();
            LabelPort = new Label();
            ButtonConnect = new Button();
            ComboBoxPorts = new ComboBox();
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
            ButtonPowerOn.Size = new Size(84, 34);
            ButtonPowerOn.TabIndex = 7;
            ButtonPowerOn.Text = "Power On";
            ButtonPowerOn.UseVisualStyleBackColor = true;
            ButtonPowerOn.Click += ButtonPowerOn_Click;
            // 
            // ButtonPowerOff
            // 
            ButtonPowerOff.Location = new Point(277, 54);
            ButtonPowerOff.Name = "ButtonPowerOff";
            ButtonPowerOff.Size = new Size(84, 34);
            ButtonPowerOff.TabIndex = 8;
            ButtonPowerOff.Text = "Power Off";
            ButtonPowerOff.UseVisualStyleBackColor = true;
            ButtonPowerOff.Click += ButtonPowerOff_Click;
            // 
            // ButtonLEDOff
            // 
            ButtonLEDOff.Location = new Point(378, 54);
            ButtonLEDOff.Name = "ButtonLEDOff";
            ButtonLEDOff.Size = new Size(84, 34);
            ButtonLEDOff.TabIndex = 10;
            ButtonLEDOff.Text = "LED Off";
            ButtonLEDOff.UseVisualStyleBackColor = true;
            ButtonLEDOff.Click += ButtonLEDOff_Click;
            // 
            // ButtonLEDOn
            // 
            ButtonLEDOn.Location = new Point(378, 14);
            ButtonLEDOn.Name = "ButtonLEDOn";
            ButtonLEDOn.Size = new Size(84, 34);
            ButtonLEDOn.TabIndex = 9;
            ButtonLEDOn.Text = "LED On";
            ButtonLEDOn.UseVisualStyleBackColor = true;
            ButtonLEDOn.Click += ButtonLEDOn_Click;
            // 
            // ButtonSetCurrent
            // 
            ButtonSetCurrent.Location = new Point(30, 113);
            ButtonSetCurrent.Name = "ButtonSetCurrent";
            ButtonSetCurrent.Size = new Size(104, 104);
            ButtonSetCurrent.TabIndex = 11;
            ButtonSetCurrent.Text = "Set Current\r\n(R,G,B)\r\n0~874";
            ButtonSetCurrent.UseVisualStyleBackColor = true;
            ButtonSetCurrent.Click += ButtonSetCurrent_Click;
            // 
            // TextBoxR
            // 
            TextBoxR.Location = new Point(150, 113);
            TextBoxR.Name = "TextBoxR";
            TextBoxR.Size = new Size(65, 23);
            TextBoxR.TabIndex = 12;
            // 
            // TextBoxG
            // 
            TextBoxG.Location = new Point(150, 155);
            TextBoxG.Name = "TextBoxG";
            TextBoxG.Size = new Size(65, 23);
            TextBoxG.TabIndex = 13;
            // 
            // TextBoxB
            // 
            TextBoxB.Location = new Point(150, 194);
            TextBoxB.Name = "TextBoxB";
            TextBoxB.Size = new Size(65, 23);
            TextBoxB.TabIndex = 14;
            // 
            // ButtonSetFanSpeed1
            // 
            ButtonSetFanSpeed1.Location = new Point(277, 113);
            ButtonSetFanSpeed1.Name = "ButtonSetFanSpeed1";
            ButtonSetFanSpeed1.Size = new Size(114, 46);
            ButtonSetFanSpeed1.TabIndex = 15;
            ButtonSetFanSpeed1.Text = "Set Fan Speed 1\r\n0~100";
            ButtonSetFanSpeed1.UseVisualStyleBackColor = true;
            ButtonSetFanSpeed1.Click += ButtonSetFanSpeed1_Click;
            // 
            // ButtonSetFanSpeed2
            // 
            ButtonSetFanSpeed2.Location = new Point(277, 171);
            ButtonSetFanSpeed2.Name = "ButtonSetFanSpeed2";
            ButtonSetFanSpeed2.Size = new Size(114, 46);
            ButtonSetFanSpeed2.TabIndex = 16;
            ButtonSetFanSpeed2.Text = "Set Fan Speed 2\r\n0~100";
            ButtonSetFanSpeed2.UseVisualStyleBackColor = true;
            ButtonSetFanSpeed2.Click += ButtonSetFanSpeed2_Click;
            // 
            // TextBoxFanSpeed1
            // 
            TextBoxFanSpeed1.Location = new Point(397, 126);
            TextBoxFanSpeed1.Name = "TextBoxFanSpeed1";
            TextBoxFanSpeed1.Size = new Size(65, 23);
            TextBoxFanSpeed1.TabIndex = 17;
            // 
            // TextBoxFanSpeed2
            // 
            TextBoxFanSpeed2.Location = new Point(397, 184);
            TextBoxFanSpeed2.Name = "TextBoxFanSpeed2";
            TextBoxFanSpeed2.Size = new Size(65, 23);
            TextBoxFanSpeed2.TabIndex = 18;
            // 
            // TextBoxRotation
            // 
            TextBoxRotation.Location = new Point(397, 254);
            TextBoxRotation.Name = "TextBoxRotation";
            TextBoxRotation.Size = new Size(65, 23);
            TextBoxRotation.TabIndex = 20;
            // 
            // ButtonSetRotation
            // 
            ButtonSetRotation.Location = new Point(30, 241);
            ButtonSetRotation.Name = "ButtonSetRotation";
            ButtonSetRotation.Size = new Size(361, 46);
            ButtonSetRotation.TabIndex = 19;
            ButtonSetRotation.Text = "Set Rotation\r\n0 (0), 1 (90), 2 (180), 3 (270)";
            ButtonSetRotation.UseVisualStyleBackColor = true;
            ButtonSetRotation.Click += ButtonSetRotation_Click;
            // 
            // ButtonGetTemperature
            // 
            ButtonGetTemperature.Location = new Point(30, 303);
            ButtonGetTemperature.Name = "ButtonGetTemperature";
            ButtonGetTemperature.Size = new Size(114, 46);
            ButtonGetTemperature.TabIndex = 21;
            ButtonGetTemperature.Text = "Get Temperature";
            ButtonGetTemperature.UseVisualStyleBackColor = true;
            ButtonGetTemperature.Click += ButtonGetTemperature_Click;
            // 
            // LabelTemperature
            // 
            LabelTemperature.AutoSize = true;
            LabelTemperature.Location = new Point(150, 319);
            LabelTemperature.Name = "LabelTemperature";
            LabelTemperature.Size = new Size(74, 15);
            LabelTemperature.TabIndex = 22;
            LabelTemperature.Text = "Temperature";
            // 
            // LabelTime
            // 
            LabelTime.AutoSize = true;
            LabelTime.Location = new Point(397, 319);
            LabelTime.Name = "LabelTime";
            LabelTime.Size = new Size(33, 15);
            LabelTime.TabIndex = 24;
            LabelTime.Text = "Time";
            // 
            // ButtonGetTime
            // 
            ButtonGetTime.Location = new Point(277, 303);
            ButtonGetTime.Name = "ButtonGetTime";
            ButtonGetTime.Size = new Size(114, 46);
            ButtonGetTime.TabIndex = 23;
            ButtonGetTime.Text = "Get Time";
            ButtonGetTime.UseVisualStyleBackColor = true;
            ButtonGetTime.Click += ButtonGetTime_Click;
            // 
            // LabelPWM
            // 
            LabelPWM.AutoSize = true;
            LabelPWM.Location = new Point(397, 382);
            LabelPWM.Name = "LabelPWM";
            LabelPWM.Size = new Size(36, 15);
            LabelPWM.TabIndex = 28;
            LabelPWM.Text = "PWM";
            // 
            // ButtonGetPWM
            // 
            ButtonGetPWM.Location = new Point(277, 366);
            ButtonGetPWM.Name = "ButtonGetPWM";
            ButtonGetPWM.Size = new Size(114, 46);
            ButtonGetPWM.TabIndex = 27;
            ButtonGetPWM.Text = "Get PWM";
            ButtonGetPWM.UseVisualStyleBackColor = true;
            ButtonGetPWM.Click += ButtonGetPWM_Click;
            // 
            // LabelVersion
            // 
            LabelVersion.AutoSize = true;
            LabelVersion.Location = new Point(150, 382);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(47, 15);
            LabelVersion.TabIndex = 26;
            LabelVersion.Text = "Version";
            // 
            // ButtonGetVersion
            // 
            ButtonGetVersion.Location = new Point(30, 366);
            ButtonGetVersion.Name = "ButtonGetVersion";
            ButtonGetVersion.Size = new Size(114, 46);
            ButtonGetVersion.TabIndex = 25;
            ButtonGetVersion.Text = "Get Version";
            ButtonGetVersion.UseVisualStyleBackColor = true;
            ButtonGetVersion.Click += ButtonGetVersion_Click;
            // 
            // RichTextBoxLog
            // 
            RichTextBoxLog.Location = new Point(34, 442);
            RichTextBoxLog.Name = "RichTextBoxLog";
            RichTextBoxLog.Size = new Size(428, 187);
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
            // AnhuaEngineController
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 653);
            Controls.Add(ComboBoxPorts);
            Controls.Add(ButtonConnect);
            Controls.Add(LabelPort);
            Controls.Add(RichTextBoxLog);
            Controls.Add(LabelPWM);
            Controls.Add(ButtonGetPWM);
            Controls.Add(LabelVersion);
            Controls.Add(ButtonGetVersion);
            Controls.Add(LabelTime);
            Controls.Add(ButtonGetTime);
            Controls.Add(LabelTemperature);
            Controls.Add(ButtonGetTemperature);
            Controls.Add(TextBoxRotation);
            Controls.Add(ButtonSetRotation);
            Controls.Add(TextBoxFanSpeed2);
            Controls.Add(TextBoxFanSpeed1);
            Controls.Add(ButtonSetFanSpeed2);
            Controls.Add(ButtonSetFanSpeed1);
            Controls.Add(TextBoxB);
            Controls.Add(TextBoxG);
            Controls.Add(TextBoxR);
            Controls.Add(ButtonSetCurrent);
            Controls.Add(ButtonLEDOff);
            Controls.Add(ButtonLEDOn);
            Controls.Add(ButtonPowerOff);
            Controls.Add(ButtonPowerOn);
            Controls.Add(ButtonAutoConnect);
            Name = "AnhuaEngineController";
            Text = "AnhuaEngineController";
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
        private TextBox TextBoxR;
        private TextBox TextBoxG;
        private TextBox TextBoxB;
        private Button ButtonSetFanSpeed1;
        private Button ButtonSetFanSpeed2;
        private TextBox TextBoxFanSpeed1;
        private TextBox TextBoxFanSpeed2;
        private TextBox TextBoxRotation;
        private Button ButtonSetRotation;
        private Button ButtonGetTemperature;
        private Label LabelTemperature;
        private Label LabelTime;
        private Button ButtonGetTime;
        private Label LabelPWM;
        private Button ButtonGetPWM;
        private Label LabelVersion;
        private Button ButtonGetVersion;
        private RichTextBox RichTextBoxLog;
        private Label LabelPort;
        private Button ButtonConnect;
        private ComboBox ComboBoxPorts;
    }
}
