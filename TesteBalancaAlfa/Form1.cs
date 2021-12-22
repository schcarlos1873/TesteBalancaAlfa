using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using PaperSolutions.Protocolo.Indicador;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace TesteBalancaAlfa
{
    public partial class Form1 : Form
    {
        //frmHexaDecimal _frmHexadecimal;
        private int peso_estabilizado = 0;
        private double mascaraFator = 1;
        private string mascaraBin = "000";
        private int mascaraLen = 2;
        private static bool EnviadoOk = true;
        private static ManualResetEvent _resetEvent;
        StringBuilder sb = new StringBuilder();

        private const int DLE = 0x10;
        private const int STX = 0x02;
        private const int DST = 0x10;
        private const int SRC = 0x00;
        private const int CMD = 0x08;
        private const int ETX = 0x03;
        private const int CR = 0x0d;   //Return
        private const int LF = 0x0a;   //Line Feet

        private const int NAK = 0x15;
        private const int ACK = 0x06;
        private const int EOT = 0x04;
        private const int ENQ = 0x05;
        private const int WAK = 0x14;
        private const int BCC = 0x0F;
        private const int PESOESTAVEL = 5;
        private ProtocoloIndicador Balanca;
        delegate void ReceberDadosByte(byte[] bytesReceived);

        //used for TCP/IP CONNECTION
        private IPAddress remoteAdress;
        private IPEndPoint remoteEP;
        public Socket ClientSocket;
        public StateObject State;

        public delegate void OnReceiveData(string text);
        public event OnReceiveData onReceiveData;

        public Form1()
        {
            InitializeComponent();
            SetFontDigital();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SetConfig();
            connectButton.Enabled = true;
            desconectarButton.Enabled = false;
            MudarMascara();
            trackBar1.Value = 1;


            Balanca = new ProtocoloIndicador(BalancaType.ALFA3102);

        }

        private void SetFontDigital()
        {
            try
            {
                PrivateFontCollection collection = new PrivateFontCollection();
                collection.AddFontFile(string.Format(@"{0}\Font\DS-DIGIB.ttf", Environment.CurrentDirectory));
                FontFamily fontFamily = new FontFamily("DS-Digital", collection);
                Font font = new Font(fontFamily, 75F);
                textBox1.Font = font;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void SetConfig()
        {
            if (rbSerial.Checked)
            {
                foreach (String str in SerialPort.GetPortNames())
                {
                    listBox1.Items.Add(str);
                }
                propertyGrid1.SelectedObject = serialPortCOM1;
                gbSimulaEnvio.Enabled = true;
            }
            else
            {
                DesconectarSerial();
                propertyGrid1.SelectedObject = new TcpIPConnect();
                gbSimulaEnvio.Enabled = false;
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            if (rbSerial.Checked)
            {
                ConectarSerial();
            }
            else
            {
                ConectarTcpIp();
            }
            desconectarButton.Enabled = true;

        }

        private void desconectarButton_Click(object sender, EventArgs e)
        {
            desconectarButton.Enabled = false;
            if (rbSerial.Checked)
            {
                DesconectarSerial();
            }
            else
            {
                DesconectarTcpIp();
            }
            connectButton.Enabled = true;
        }

        private void InitialConfigButton()
        {
            this.BeginInvoke((Action)(() =>
            {
                connectButton.Enabled = true;
                desconectarButton.Enabled = false;
            })
            );
        }

        //###########################################################################################
        // Serial port comunication
        //###########################################################################################
        #region Serial Comunication 

        private void ConectarSerial()
        {
            serialPortCOM1 = (SerialPort)propertyGrid1.SelectedObject;
            try
            {
                if (!serialPortCOM1.IsOpen)
                {
                    serialPortCOM1.Open();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao se connectar: [ " + ex.Message + "\n" + ex.StackTrace + " ]");
                InitialConfigButton();
            }
        }

        private void DesconectarSerial()
        {
            try
            {
                if (serialPortCOM1.IsOpen)
                {
                    serialPortCOM1.DiscardInBuffer();
                    serialPortCOM1.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao se desconnectar: [ " + ex.Message + "\n" + ex.StackTrace + " ]");
            }
        }

        private void serialPortCOM1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Debug.WriteLine("Recebendo dados...");
                //this.Invoke(new EventHandler(DataReceive));
                byte[] byteReceived = new byte[serialPortCOM1.BytesToRead];
                serialPortCOM1.Read(byteReceived, 0, byteReceived.Length);
                this.BeginInvoke(new ReceberDadosByte(ReadDataReceive), new object[] { byteReceived });
            }
            catch (TimeoutException tx)
            {
                MessageBox.Show("Timeout serialPortCOM1_DataReceived()[ " + tx.Message + "\n" + tx.StackTrace + " ]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro serialPortCOM1_DataReceived() [ " + ex.Message + "\n" + ex.StackTrace + " ]");
            }
        }


        private void serialPortCOM1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialError er = e.EventType;
            MessageBox.Show("Erro ao receber mensagem da balança [ " + er.ToString() + " ] ");
        }

        #endregion



        //###########################################################################################
        // TCP/IP comunication
        //###########################################################################################
        #region Tcp/ip comunication

        //Todo Implementar configuração tcpip
        private void ConectarTcpIp()
        {
            try
            {
                TcpIPConnect address = (TcpIPConnect)propertyGrid1.SelectedObject;
                remoteAdress = IPAddress.Parse(address.TcpIP);
                remoteEP = new IPEndPoint(remoteAdress, address.Port);
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(remoteEP);
                //ClientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), ClientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
                InitialConfigButton();
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            Socket _client = null;
            try
            {
                // Retrieve the socket from the state object.
                _client = (Socket)ar.AsyncState;
                // Complete the connection.
                _client.EndConnect(ar);
                Debug.WriteLine(string.Format("Cliente conectado {0}", _client.RemoteEndPoint.ToString()));
                Receive(_client);
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível conectar");
                Debug.WriteLine(string.Format("ConnectCallback() {0}", e.Message));
                InitialConfigButton();
                DesconectarTcpIp();
            }
        }

        private void DesconectarTcpIp()
        {
            if (ClientSocket != null)
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Disconnect(false);
                    ClientSocket.Close();
                }
            }
        }

        private void Receive(Socket _client)
        {
            try
            {
                // Begin receiving the data from the remote device.
                _client.BeginReceive(State.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), State);
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Receive() {0}", e.Message));
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket _client = null;
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                _client = state.workSocket;
                // Read data from the remote device.
                int bytesRead = _client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    ReadDataReceive(state.buffer);
                    // Get the rest of the data.
                    _client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("ReceiveCallback() {0}", e.Message));
                DesconectarTcpIp();
            }
        }



        #endregion

        public virtual void OnReceiveDataEvent(string text)
        {
            try
            {
                onReceiveData?.Invoke(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        //###########################################################################################
        // Interpreta os dados recebidos
        //###########################################################################################
        #region READ DATA RECEIVE
        private void ReadDataReceive(byte[] byteReceived)
        {
            if (hexadecimalcheckBox.Checked)
            {
                sb.Clear();
                foreach (byte b in byteReceived)
                {
                    // _frmHexadecimal.AdicionaTexto(b.ToString("X"));
                    sb.Append(string.Format("{0} ", b.ToString("X")));
                }
                //_frmHexadecimal.AdicionaTexto(Environment.NewLine);
               // sb.Append(Environment.NewLine);
                OnReceiveDataEvent(sb.ToString());
                
            }

            if (Balanca != null)
            {
                if (Balanca.Indicador.ReadBytes(byteReceived))
                {
                    textBox1.Text = Balanca.Indicador.Peso.ToString();
                    textBox2.Text = string.Format("STATUS 1 : {0}\r\n", Balanca.Indicador.SWA.Status);
                    textBox2.Text += string.Format(" Casas Decimais\t =\t {0}\r\n", Balanca.Indicador.CasasDecimais.ToString());
                    textBox2.Text += string.Format(" Peso Negativo\t =\t {0}\r\n", Balanca.Indicador.PesoNegativo.ToString());
                    textBox2.Text += string.Format(" Sobre Carga\t =\t {0}\r\n", Balanca.Indicador.Sobrecarga.ToString());
                    textBox2.Text += string.Format(" Saturação\t =\t {0}\r\n", Balanca.Indicador.Saturacao.ToString());
                    textBox2.Text += string.Format(" Peso Líquido\t =\t {0}\r\n", Balanca.Indicador.PesoLiquido.ToString());
                    textBox2.Text += string.Format(" Sensor Movimento = \t {0}\r\n\r\n", Balanca.Indicador.Instavel.ToString());

                    textBox2.Text += string.Format("\r\nSTATUS 2 : {0}\r\n", Balanca.Indicador.SWB.Status);
                    textBox2.Text += string.Format(" TRAVA 0 =\t {0}\r\n", Balanca.Indicador.SWB.BIT0.ToString());
                    textBox2.Text += string.Format(" TRAVA 1 =\t {0}\r\n", Balanca.Indicador.SWB.BIT1.ToString());
                    textBox2.Text += string.Format(" TRAVA 2 =\t {0}\r\n", Balanca.Indicador.SWB.BIT2.ToString());
                    textBox2.Text += string.Format(" TRAVA 3 =\t {0}\r\n", Balanca.Indicador.SWB.BIT3.ToString());
                    textBox2.Text += string.Format(" NIVEL 0 =\t {0}\r\n", Balanca.Indicador.SWB.BIT4.ToString());
                    textBox2.Text += string.Format(" NIVEL 1 =\t {0}\r\n", Balanca.Indicador.SWB.BIT5.ToString());
                    textBox2.Text += string.Format(" NIVEL 2 =\t {0}\r\n", Balanca.Indicador.SWB.BIT6.ToString());
                    textBox2.Text += string.Format(" NIVEL 3 =\t {0}\r\n", Balanca.Indicador.SWB.BIT7.ToString());

                    textBox2.Text += string.Format("\r\n Peso: {0}\r\n", Balanca.Indicador.Peso.ToString());
                    textBox2.Text += string.Format(" Tara: {0}\r\n", Balanca.Indicador.Tara.ToString());


                    if (!Balanca.Indicador.Instavel)
                    {
                        peso_estabilizado++;
                    }
                    else
                    {
                        peso_estabilizado = 0;
                        label1.Text = "";
                        textBox1.BackColor = Color.Red;

                    }

                    if (peso_estabilizado >= PESOESTAVEL)
                    {
                        button4.Text = "Iniciar Envio";
                        timer1.Enabled = false;
                        textBox1.BackColor = Color.Green;
                        label1.Text = "PESO IDEAL!!";
                        peso_estabilizado = 0;
                    }
                    receiveTextBox.Text = UTF8Encoding.ASCII.GetString(byteReceived);

                }
            }
        }


        #endregion







        private void button1_Click(object sender, EventArgs e)
        {
            if (!EnviadoOk)
                return;

            EnviadoOk = false;

            List<byte> mensagem = new List<byte>();
            double pesoDouble = 0;
            double.TryParse(pesoTextBox.Text, out pesoDouble);
            double taraDouble = 0;
            double.TryParse(taraTextBox.Text, out taraDouble);

            switch (Balanca.ProtocoloType)
            {
                case BalancaType.ALFA3102:
                case BalancaType.ALFA3104:
                case BalancaType.ALFA3105:
                case BalancaType.ALFA3107:
                    mensagem = GeraAlfa3100cMessage(pesoDouble, taraDouble);
                    break;
                case BalancaType.TOLEDO9091:
                    mensagem = GeraToledo9191Message(pesoDouble, taraDouble);
                    break;
                case BalancaType.ALFA3103:
                case BalancaType.ALFA3101:
                    mensagem = GerarAlfaTRCMessage(pesoDouble, taraDouble);
                    break;
                case BalancaType.BELM4102:
                    mensagem = GerarBelM4102Message(pesoDouble);
                    break;
                default:
                    break;
            }


            try
            {
                if (mensagem.Count == 0)
                    return;

                _resetEvent = new ManualResetEvent(false);
                string strMsg = UTF8Encoding.Default.GetString(mensagem.ToArray());
                ParameterizedThreadStart ps1 = new ParameterizedThreadStart(Enviar);
                Thread t1 = new Thread(ps1);
                t1.Start(strMsg);
                EnviadoOk = true;
                //ParameterizedThreadStart ps2 = new ParameterizedThreadStart(Enviar2);
                //Thread t2 = new Thread(ps2);
                //t2.Start(strMsg);

            }
            catch
            {
                pesoEstabilizadoCheckBox.Checked = false;
                timer1.Enabled = false;
                MessageBox.Show("Nenhum serviço escutanto");
            }
            mensagem.Clear();

        }


        private List<byte> GeraAlfa3100cMessage(double pesoDouble, double taraDouble)
        {
            byte bcc = 0;
            List<byte> mensagem = new List<byte>();
            //adicionado inicio de mensagem.
            mensagem.Add(STX);

            // Gera os status 1 e 2.
            string statusBinario = string.Format("100{0}{1}{2}", (!pesoEstabilizadoCheckBox.Checked ? 1 : 0), (pesoDouble < 0 ? 1 : 0), mascaraBin);
            int status1 = Convert.ToInt32(statusBinario, 2);
            int status2 = Convert.ToInt32("00001000", 2);

            mensagem.Add(Convert.ToByte(status1));
            mensagem.Add(Convert.ToByte(status2));

            string peso = Convert.ToInt32(Math.Abs(pesoDouble) * mascaraFator).ToString().PadLeft(5, '0');
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(peso));

            string tara = Convert.ToInt32(Math.Abs(taraDouble) * mascaraFator).ToString().PadLeft(5, '0');
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(tara));

            //Adicionar final Da mensagem
            mensagem.Add(ETX);
            bcc = ProtocoloAlfa.CheckSumBCC(mensagem.ToArray(), 0, mensagem.Count);
            //adicionadndo o CheckSum da Mensagem
            mensagem.Add(bcc);
            return mensagem;

        }


        private List<byte> GeraToledo9191Message(double pesoDouble, double taraDouble)
        {
            List<byte> mensagem = new List<byte>();
            int autozerado = 0;
            int pesoliquido = 1;
            int sobrecarga = 0;
            int emMovimento = (pesoEstabilizadoCheckBox.Checked ? 0 : 1);
            int paridade = 0;
            //adicionado inicio de mensagem.
            mensagem.Add(STX);

            byte display = 2;

            if (m0.Checked) display = 2;
            if (m1.Checked) display = 3;
            if (m2.Checked) display = 4;
            if (m3.Checked) display = 5;
            if (m4.Checked) display = 6;
            int pesoNegativo = (pesoDouble < 0 ? 1 : 0);

            byte[] aByte = new byte[] { display };
            BitArray bits = new BitArray(aByte);
            string dispString = string.Format("{0}{1}{2}", Convert.ToInt16(bits[2]), Convert.ToInt16(bits[1]), Convert.ToInt16(bits[0]));
            // Gera os status 1 e 2.
            string statusBinario = string.Format("10101{0}", dispString);
            //, (!pesoEstabilizadoCheckBox.Checked ? 1 : 0), (pesoDouble < 0 ? 1 : 0), mascaraBin);
            int status1 = Convert.ToInt32(statusBinario, 2);
            int status2 = Convert.ToInt32(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", paridade, autozerado, 1, 1, emMovimento, sobrecarga, pesoNegativo, pesoliquido), 2);
            int status3 = Convert.ToInt32("00000000", 2);

            mensagem.Add(Convert.ToByte(status1));
            mensagem.Add(Convert.ToByte(status2));
            mensagem.Add(Convert.ToByte(status3));

            string peso = Convert.ToInt32(Math.Abs(pesoDouble) * mascaraFator).ToString().PadLeft(6, '0');
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(peso));

            string tara = Convert.ToInt32(Math.Abs(taraDouble) * mascaraFator).ToString().PadLeft(6, '0');
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(tara));

            //Adicionar final Da mensagem
            mensagem.Add(CR);
            return mensagem;

        }


        private List<byte> GerarAlfaTRCMessage(double pesoDouble, double taraDouble)
        {
            try
            {
                List<byte> mensagem = new List<byte>();
                string pesoNegativo = (pesoDouble < 0 ? "-" : " ");
                string taraNegativo = (taraDouble < 0 ? "-" : " ");
                string prefixPeso = pesoEstabilizadoCheckBox.Checked ? (pesoBrutoCheckBox.Checked ? "PB:" : "PL:") : "**:";
                string prefixTara = pesoEstabilizadoCheckBox.Checked ? "T:" : "*:";
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(prefixPeso));
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(pesoNegativo));
                string peso = string.Format("{0:00.000}", Math.Abs(pesoDouble)).Replace(".", ","); // Convert.ToInt32(Math.Abs(pesoDouble) * mascaraFator).ToString().PadLeft(6, '0');
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(peso));
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(" "));
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(prefixTara));
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(taraNegativo));
                string tara = string.Format("{0:00.000}", Math.Abs(taraDouble)).Replace(".", ","); //Convert.ToInt32(Math.Abs(taraDouble) * mascaraFator).ToString().PadLeft(6, '0');
                mensagem.AddRange(UTF8Encoding.Default.GetBytes(tara));
                //Adicionar final Da mensagem
                mensagem.Add(CR);
                mensagem.Add(LF);
                return mensagem;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        private List<byte> GerarBelM4102Message(double pesoDouble)
        {
            List<byte> mensagem = new List<byte>();
            string pesoNegativo = (pesoDouble < 0 ? "-" : " ");
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(pesoNegativo));
            string peso = string.Format("{0:00000.00}", Math.Abs(pesoDouble)).Replace(",", ".");
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(peso));
            mensagem.AddRange(UTF8Encoding.Default.GetBytes("g  "));
            mensagem.AddRange(UTF8Encoding.Default.GetBytes(pesoEstabilizadoCheckBox.Checked ? "S" : " "));
            //Adicionar final Da mensagem
            mensagem.Add(CR);
            mensagem.Add(LF);
            return mensagem;
        }



        //Envia a primeira linha
        private void Enviar(object o)
        {
            string msgString = (string)o;
            List<byte> _dados = UTF8Encoding.Default.GetBytes(msgString).ToList();
            int sizeBuffer = _dados.Count;


            byte[] data1 = new byte[sizeBuffer];

            if (_dados.Count == 0)
                return;
            _dados.CopyTo(0, data1, 0, sizeBuffer);
            try
            {
                using (SerialPort COM2 = new SerialPort(textBox3.Text, 9600, Parity.None, 8, StopBits.One))
                {
                    COM2.WriteTimeout = 300;
                    COM2.Handshake = Handshake.XOnXOff;
                    COM2.Open();
                    COM2.Write(data1, 0, data1.Length);
                    Thread.Sleep(250);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("private void Enviar(object o)");
                Debug.WriteLine(ex);
            }
            _resetEvent.Set();
        }

        //Envia a segunda linha.
        private void Enviar2(object o)
        {
            _resetEvent.WaitOne();
            string msgString = (string)o;
            List<byte> _dados = UTF8Encoding.Default.GetBytes(msgString).ToList();

            byte[] data1 = new byte[10];

            if (_dados.Count == 0)
                return;

            _dados.CopyTo(10, data1, 0, 10);

            try
            {

                using (SerialPort COM2 = new SerialPort(textBox3.Text, 9600, Parity.None, 8, StopBits.One))
                {
                    COM2.WriteTimeout = 300;
                    COM2.Handshake = Handshake.XOnXOff;
                    COM2.Open();
                    COM2.Write(data1, 0, data1.Length);
                }
            }
            catch { }
            EnviadoOk = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            byte[] msg = new byte[] { DLE, STX, DST, SRC, CMD, DLE, ETX, 0x84 };
            //msg[msg.Length - 1] = pa.ReturnBCC(msg, 1, msg.Length - 1);

            for (int i = 0; i < msg.Length; i++)
            {
                Console.Write(msg[i]);
                Console.Write(" | ");
            }
            Console.WriteLine();

            try
            {
                if (serialPortCOM1.IsOpen)
                {
                    serialPortCOM1.Write(msg, 0, msg.Length);

                    byte[] data = new byte[15];
                    serialPortCOM1.Read(data, 0, data.Length);

                    for (int i = 0; i < data.Length; i++)
                    {
                        Console.Write(data[i]);
                        Console.Write(" , ");
                    }
                    Console.WriteLine();
                    receiveTextBox.Text = UTF8Encoding.ASCII.GetString(data);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao se desconnectar: [ " + ex.Message + " ]");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!pesoEstabilizadoCheckBox.Checked)
            {
                double fator = Convert.ToDouble(trackBar1.Value);
                Random rnd = new Random();
                double dec = rnd.Next(-50, 50) * (fator / mascaraFator);
                double value = 0;
                double.TryParse(pesoTextBox.Text, out value);
                value += dec;
                value = Math.Round(value, mascaraLen);
                pesoTextBox.Text = value.ToString();

            }

            //Envia dados Para a balanca
            button1_Click(this, null);
        }

        private void pesoEstabilizadoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (pesoEstabilizadoCheckBox.Checked ? 0 : 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                button4.Text = "Iniciar Envio";
                timer1.Enabled = false;
            }
            else
            {
                button4.Text = "Para Envio";
                timer1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MudarMascara();
        }


        private void MudarMascara()
        {
            if (m0.Checked)
            {
                mascaraFator = 1;
                mascaraBin = "000";
                mascaraLen = 0;
            }
            if (m1.Checked)
            {
                mascaraFator = 10;
                mascaraBin = "001";
                mascaraLen = 1;
            }

            if (m2.Checked)
            {
                mascaraFator = 100;
                mascaraBin = "010";
                mascaraLen = 2;
            }
            if (m3.Checked)
            {
                mascaraFator = 1000;
                mascaraBin = "011";
                mascaraLen = 3;
            }

            if (m4.Checked)
            {
                mascaraFator = 10000;
                mascaraBin = "100";
                mascaraLen = 4;
            }
        }

        private void m1_CheckedChanged(object sender, EventArgs e)
        {
            MudarMascara();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pesoEstabilizadoCheckBox.Checked = (trackBar1.Value == 0 ? true : false);

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPortCOM1.IsOpen)
            {
                //serialPortCOM1.Dispose();
                serialPortCOM1.Close();
            }
            GC.SuppressFinalize(this);
        }

        private void Uncheck()
        {
            if(hexadecimalcheckBox.InvokeRequired)
            {
               hexadecimalcheckBox.Invoke((MethodInvoker)(()=>Uncheck()));
               
            }
            else
            {
                hexadecimalcheckBox.Checked = false;
                hexadecimalcheckBox.Enabled = true;
            }
        }


        private void hexadecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hexadecimalcheckBox.Checked)
            {
                hexadecimalcheckBox.Enabled = false;
                hexadecimalcheckBox.Refresh();
                Task.Factory.StartNew(() =>
                {
                    Application.Run(new frmHexaDecimal(this));
                    Uncheck();
                });
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = listBox1.SelectedItem.ToString();
        }

        private void rbAlfaSerie3102c_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlfaSerie3101.Checked)
            {
                Balanca = new ProtocoloIndicador(BalancaType.ALFA3102);
            }
        }

        private void rbAlfaSerie3101_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlfaSerie3101.Checked)
            {
                Balanca = new ProtocoloIndicador(BalancaType.ALFA3103);
            }
        }

        private void rbToledo9091_CheckedChanged(object sender, EventArgs e)
        {

            if (rbToledo9091.Checked)
            {
                Balanca = new ProtocoloIndicador(BalancaType.TOLEDO9091);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Balanca = new ProtocoloIndicador(BalancaType.PAINEL_RTU);
            }
        }

        private void rbSerial_CheckedChanged(object sender, EventArgs e)
        {
            SetConfig();
        }

        private void rbTcpIp_CheckedChanged(object sender, EventArgs e)
        {
            SetConfig();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rbBelM4102_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBelM4102.Checked)
            {
                Balanca = new ProtocoloIndicador(BalancaType.BELM4102);
            }
        }
    }
}
