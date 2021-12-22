namespace TesteBalancaAlfa
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPortCOM1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.receiveTextBox = new System.Windows.Forms.TextBox();
            this.pesoTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.desconectarButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pesoEstabilizadoCheckBox = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.m0 = new System.Windows.Forms.RadioButton();
            this.m1 = new System.Windows.Forms.RadioButton();
            this.m2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m4 = new System.Windows.Forms.RadioButton();
            this.m3 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.taraTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbSimulaEnvio = new System.Windows.Forms.GroupBox();
            this.pesoBrutoCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hexadecimalcheckBox = new System.Windows.Forms.CheckBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbBelM4102 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbToledo9091 = new System.Windows.Forms.RadioButton();
            this.rbAlfaSerie3101 = new System.Windows.Forms.RadioButton();
            this.rbAlfaSerie3102c = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbTcpIp = new System.Windows.Forms.RadioButton();
            this.rbSerial = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gbSimulaEnvio.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortCOM1
            // 
            this.serialPortCOM1.ReadTimeout = 0;
            this.serialPortCOM1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPortCOM1_ErrorReceived);
            this.serialPortCOM1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortCOM1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.Location = new System.Drawing.Point(5, 17);
            this.receiveTextBox.Multiline = true;
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.Size = new System.Drawing.Size(268, 27);
            this.receiveTextBox.TabIndex = 0;
            // 
            // pesoTextBox
            // 
            this.pesoTextBox.Location = new System.Drawing.Point(67, 19);
            this.pesoTextBox.MaxLength = 8;
            this.pesoTextBox.Name = "pesoTextBox";
            this.pesoTextBox.Size = new System.Drawing.Size(81, 20);
            this.pesoTextBox.TabIndex = 1;
            this.pesoTextBox.Text = "1";
            this.pesoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(254, 168);
            this.connectButton.Name = "connectButton";
            this.connectButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.connectButton.Size = new System.Drawing.Size(89, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connectar";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // desconectarButton
            // 
            this.desconectarButton.Location = new System.Drawing.Point(254, 211);
            this.desconectarButton.Name = "desconectarButton";
            this.desconectarButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.desconectarButton.Size = new System.Drawing.Size(90, 23);
            this.desconectarButton.TabIndex = 3;
            this.desconectarButton.Text = "Desconnectar";
            this.desconectarButton.UseVisualStyleBackColor = true;
            this.desconectarButton.Click += new System.EventHandler(this.desconectarButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 145);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(675, 285);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 122);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "00,000";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.Location = new System.Drawing.Point(5, 50);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(268, 348);
            this.textBox2.TabIndex = 8;
            // 
            // pesoEstabilizadoCheckBox
            // 
            this.pesoEstabilizadoCheckBox.AutoSize = true;
            this.pesoEstabilizadoCheckBox.Location = new System.Drawing.Point(17, 76);
            this.pesoEstabilizadoCheckBox.Name = "pesoEstabilizadoCheckBox";
            this.pesoEstabilizadoCheckBox.Size = new System.Drawing.Size(109, 17);
            this.pesoEstabilizadoCheckBox.TabIndex = 9;
            this.pesoEstabilizadoCheckBox.Text = "Peso Estabilizado";
            this.pesoEstabilizadoCheckBox.UseVisualStyleBackColor = true;
            this.pesoEstabilizadoCheckBox.CheckedChanged += new System.EventHandler(this.pesoEstabilizadoCheckBox_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(141, 145);
            this.button4.Name = "button4";
            this.button4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button4.Size = new System.Drawing.Size(113, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Iniciar Envio";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // m0
            // 
            this.m0.AutoSize = true;
            this.m0.Location = new System.Drawing.Point(6, 19);
            this.m0.Name = "m0";
            this.m0.Size = new System.Drawing.Size(31, 17);
            this.m0.TabIndex = 10;
            this.m0.Text = "1";
            this.m0.UseVisualStyleBackColor = true;
            this.m0.CheckedChanged += new System.EventHandler(this.m1_CheckedChanged);
            // 
            // m1
            // 
            this.m1.AutoSize = true;
            this.m1.Location = new System.Drawing.Point(6, 37);
            this.m1.Name = "m1";
            this.m1.Size = new System.Drawing.Size(40, 17);
            this.m1.TabIndex = 10;
            this.m1.Text = "0.1";
            this.m1.UseVisualStyleBackColor = true;
            this.m1.CheckedChanged += new System.EventHandler(this.m1_CheckedChanged);
            // 
            // m2
            // 
            this.m2.AutoSize = true;
            this.m2.Location = new System.Drawing.Point(6, 58);
            this.m2.Name = "m2";
            this.m2.Size = new System.Drawing.Size(46, 17);
            this.m2.TabIndex = 10;
            this.m2.Text = "0.01";
            this.m2.UseVisualStyleBackColor = true;
            this.m2.CheckedChanged += new System.EventHandler(this.m1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m4);
            this.groupBox1.Controls.Add(this.m3);
            this.groupBox1.Controls.Add(this.m0);
            this.groupBox1.Controls.Add(this.m2);
            this.groupBox1.Controls.Add(this.m1);
            this.groupBox1.Location = new System.Drawing.Point(159, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 122);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mascara";
            // 
            // m4
            // 
            this.m4.AutoSize = true;
            this.m4.Checked = true;
            this.m4.Location = new System.Drawing.Point(6, 98);
            this.m4.Name = "m4";
            this.m4.Size = new System.Drawing.Size(58, 17);
            this.m4.TabIndex = 12;
            this.m4.TabStop = true;
            this.m4.Text = "0.0001";
            this.m4.UseVisualStyleBackColor = true;
            this.m4.CheckedChanged += new System.EventHandler(this.m1_CheckedChanged);
            // 
            // m3
            // 
            this.m3.AutoSize = true;
            this.m3.Location = new System.Drawing.Point(6, 77);
            this.m3.Name = "m3";
            this.m3.Size = new System.Drawing.Size(52, 17);
            this.m3.TabIndex = 11;
            this.m3.Text = "0.001";
            this.m3.UseVisualStyleBackColor = true;
            this.m3.CheckedChanged += new System.EventHandler(this.m1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 172);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Minimum = -50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(262, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // taraTextBox
            // 
            this.taraTextBox.Location = new System.Drawing.Point(68, 46);
            this.taraTextBox.MaxLength = 6;
            this.taraTextBox.Name = "taraTextBox";
            this.taraTextBox.Size = new System.Drawing.Size(81, 20);
            this.taraTextBox.TabIndex = 16;
            this.taraTextBox.Text = "0";
            this.taraTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Peso:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Tara:";
            // 
            // gbSimulaEnvio
            // 
            this.gbSimulaEnvio.Controls.Add(this.pesoBrutoCheckBox);
            this.gbSimulaEnvio.Controls.Add(this.pesoTextBox);
            this.gbSimulaEnvio.Controls.Add(this.label5);
            this.gbSimulaEnvio.Controls.Add(this.taraTextBox);
            this.gbSimulaEnvio.Controls.Add(this.textBox3);
            this.gbSimulaEnvio.Controls.Add(this.label4);
            this.gbSimulaEnvio.Controls.Add(this.listBox1);
            this.gbSimulaEnvio.Controls.Add(this.label2);
            this.gbSimulaEnvio.Controls.Add(this.trackBar1);
            this.gbSimulaEnvio.Controls.Add(this.groupBox1);
            this.gbSimulaEnvio.Controls.Add(this.pesoEstabilizadoCheckBox);
            this.gbSimulaEnvio.Controls.Add(this.button1);
            this.gbSimulaEnvio.Controls.Add(this.button4);
            this.gbSimulaEnvio.Location = new System.Drawing.Point(671, 11);
            this.gbSimulaEnvio.Name = "gbSimulaEnvio";
            this.gbSimulaEnvio.Size = new System.Drawing.Size(401, 225);
            this.gbSimulaEnvio.TabIndex = 19;
            this.gbSimulaEnvio.TabStop = false;
            this.gbSimulaEnvio.Text = "Simular envio de dados";
            // 
            // pesoBrutoCheckBox
            // 
            this.pesoBrutoCheckBox.AutoSize = true;
            this.pesoBrutoCheckBox.Location = new System.Drawing.Point(17, 102);
            this.pesoBrutoCheckBox.Name = "pesoBrutoCheckBox";
            this.pesoBrutoCheckBox.Size = new System.Drawing.Size(78, 17);
            this.pesoBrutoCheckBox.TabIndex = 24;
            this.pesoBrutoCheckBox.Text = "Peso Bruto";
            this.pesoBrutoCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(273, 145);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 23;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(271, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 108);
            this.listBox1.TabIndex = 21;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Portas Disponíveis:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(676, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 36);
            this.label1.TabIndex = 20;
            this.label1.Text = "Peso Balança";
            // 
            // hexadecimalcheckBox
            // 
            this.hexadecimalcheckBox.Location = new System.Drawing.Point(254, 257);
            this.hexadecimalcheckBox.Name = "hexadecimalcheckBox";
            this.hexadecimalcheckBox.Size = new System.Drawing.Size(128, 37);
            this.hexadecimalcheckBox.TabIndex = 19;
            this.hexadecimalcheckBox.Text = "Mostrar Valores em Hexadecimal";
            this.hexadecimalcheckBox.UseVisualStyleBackColor = true;
            this.hexadecimalcheckBox.CheckedChanged += new System.EventHandler(this.hexadecimalcheckBox_CheckedChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(5, 40);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(224, 376);
            this.propertyGrid1.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbBelM4102);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.rbToledo9091);
            this.groupBox3.Controls.Add(this.rbAlfaSerie3101);
            this.groupBox3.Controls.Add(this.rbAlfaSerie3102c);
            this.groupBox3.Location = new System.Drawing.Point(254, 11);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(119, 140);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Indicadores";
            // 
            // rbBelM4102
            // 
            this.rbBelM4102.AutoSize = true;
            this.rbBelM4102.Location = new System.Drawing.Point(5, 115);
            this.rbBelM4102.Margin = new System.Windows.Forms.Padding(2);
            this.rbBelM4102.Name = "rbBelM4102";
            this.rbBelM4102.Size = new System.Drawing.Size(76, 17);
            this.rbBelM4102.TabIndex = 1;
            this.rbBelM4102.Text = "Bel M4102";
            this.rbBelM4102.UseVisualStyleBackColor = true;
            this.rbBelM4102.CheckedChanged += new System.EventHandler(this.rbBelM4102_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(5, 93);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(80, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Painel RTU";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbToledo9091
            // 
            this.rbToledo9091.AutoSize = true;
            this.rbToledo9091.Location = new System.Drawing.Point(4, 72);
            this.rbToledo9091.Margin = new System.Windows.Forms.Padding(2);
            this.rbToledo9091.Name = "rbToledo9091";
            this.rbToledo9091.Size = new System.Drawing.Size(85, 17);
            this.rbToledo9091.TabIndex = 0;
            this.rbToledo9091.Text = "Toledo 9091";
            this.rbToledo9091.UseVisualStyleBackColor = true;
            this.rbToledo9091.CheckedChanged += new System.EventHandler(this.rbToledo9091_CheckedChanged);
            // 
            // rbAlfaSerie3101
            // 
            this.rbAlfaSerie3101.AutoSize = true;
            this.rbAlfaSerie3101.Location = new System.Drawing.Point(4, 50);
            this.rbAlfaSerie3101.Margin = new System.Windows.Forms.Padding(2);
            this.rbAlfaSerie3101.Name = "rbAlfaSerie3101";
            this.rbAlfaSerie3101.Size = new System.Drawing.Size(97, 17);
            this.rbAlfaSerie3101.TabIndex = 0;
            this.rbAlfaSerie3101.Text = "Alfa Serie 3101";
            this.rbAlfaSerie3101.UseVisualStyleBackColor = true;
            this.rbAlfaSerie3101.CheckedChanged += new System.EventHandler(this.rbAlfaSerie3101_CheckedChanged);
            // 
            // rbAlfaSerie3102c
            // 
            this.rbAlfaSerie3102c.AutoSize = true;
            this.rbAlfaSerie3102c.Checked = true;
            this.rbAlfaSerie3102c.Location = new System.Drawing.Point(5, 28);
            this.rbAlfaSerie3102c.Margin = new System.Windows.Forms.Padding(2);
            this.rbAlfaSerie3102c.Name = "rbAlfaSerie3102c";
            this.rbAlfaSerie3102c.Size = new System.Drawing.Size(103, 17);
            this.rbAlfaSerie3102c.TabIndex = 0;
            this.rbAlfaSerie3102c.TabStop = true;
            this.rbAlfaSerie3102c.Text = "Alfa Serie 3102c";
            this.rbAlfaSerie3102c.UseVisualStyleBackColor = true;
            this.rbAlfaSerie3102c.CheckedChanged += new System.EventHandler(this.rbAlfaSerie3102c_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbTcpIp);
            this.groupBox4.Controls.Add(this.rbSerial);
            this.groupBox4.Controls.Add(this.propertyGrid1);
            this.groupBox4.Location = new System.Drawing.Point(9, 10);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(240, 426);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Comunicação";
            // 
            // rbTcpIp
            // 
            this.rbTcpIp.AutoSize = true;
            this.rbTcpIp.Location = new System.Drawing.Point(149, 17);
            this.rbTcpIp.Margin = new System.Windows.Forms.Padding(2);
            this.rbTcpIp.Name = "rbTcpIp";
            this.rbTcpIp.Size = new System.Drawing.Size(58, 17);
            this.rbTcpIp.TabIndex = 0;
            this.rbTcpIp.Text = "Tcp/Ip";
            this.rbTcpIp.UseVisualStyleBackColor = true;
            this.rbTcpIp.CheckedChanged += new System.EventHandler(this.rbTcpIp_CheckedChanged);
            // 
            // rbSerial
            // 
            this.rbSerial.AutoSize = true;
            this.rbSerial.Checked = true;
            this.rbSerial.Location = new System.Drawing.Point(39, 17);
            this.rbSerial.Margin = new System.Windows.Forms.Padding(2);
            this.rbSerial.Name = "rbSerial";
            this.rbSerial.Size = new System.Drawing.Size(51, 17);
            this.rbSerial.TabIndex = 0;
            this.rbSerial.TabStop = true;
            this.rbSerial.Text = "Serial";
            this.rbSerial.UseVisualStyleBackColor = true;
            this.rbSerial.CheckedChanged += new System.EventHandler(this.rbSerial_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.receiveTextBox);
            this.groupBox5.Controls.Add(this.textBox2);
            this.groupBox5.Location = new System.Drawing.Point(387, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(279, 405);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dados da Balança";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 444);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.hexadecimalcheckBox);
            this.Controls.Add(this.desconectarButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbSimulaEnvio);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Teste de Balança Alfa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gbSimulaEnvio.ResumeLayout(false);
            this.gbSimulaEnvio.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortCOM1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox receiveTextBox;
        private System.Windows.Forms.TextBox pesoTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button desconectarButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox pesoEstabilizadoCheckBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton m0;
        private System.Windows.Forms.RadioButton m1;
        private System.Windows.Forms.RadioButton m2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox taraTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbSimulaEnvio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox hexadecimalcheckBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.RadioButton m4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbToledo9091;
        private System.Windows.Forms.RadioButton rbAlfaSerie3101;
        private System.Windows.Forms.RadioButton rbAlfaSerie3102c;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbTcpIp;
        private System.Windows.Forms.RadioButton rbSerial;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox pesoBrutoCheckBox;
        private System.Windows.Forms.RadioButton rbBelM4102;
    }
}

