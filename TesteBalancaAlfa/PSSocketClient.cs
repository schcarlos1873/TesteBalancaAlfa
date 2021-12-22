using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.Timers;


namespace TesteBalancaAlfa
{
    public class PSSocketClient
    {
        private int remotePort;
        private IPAddress remoteAdress;
        private IPEndPoint remoteEP;
        public Socket ClientSocket;
        private Timer timer;
        public StateObject State;
        private Boolean stopped = false;
        
        private Timer reconectTimer;
        private DateTime lastDataReceive;

        public delegate void ReceiveDataHandler(Socket sender, string data);
        public event ReceiveDataHandler ReceiveData;

        public PSSocketClient(string remote_ip, int remote_port)
        {
            remoteAdress = IPAddress.Parse(remote_ip);
            remoteEP = new IPEndPoint(remoteAdress, remote_port);
            remotePort = remote_port;
            timer = new Timer();
            timer.Interval = 5 * 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            timer.Enabled = false;

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create the state object.
            State = new StateObject(client);
            ConnectClient(client);


        }


        public void Start()
        {
            timer.Start();
            stopped = false;
        }

        public void Stop()
        {
            timer.Stop();
            timer.Enabled = false;
            stopped = true;
            try
            {
                closeClient(State.workSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Stop() {0}", e.Message));
            }
        }


        private void Receive(Socket _client)
        {
            try
            {

                // Begin receiving the data from the remote device.
                _client.BeginReceive(State.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), State);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Receive() {0}", e.Message));
                timer.Enabled = !stopped;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            lastDataReceive = DateTime.Now;
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
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    string plc_data = state.sb.ToString();
                    int startStr = plc_data.IndexOf(Convert.ToChar(PSHexadecimal.STX));
                    int endStr = plc_data.IndexOf(Convert.ToChar(PSHexadecimal.ETX));

                    if (startStr != -1 && endStr != -1)
                    {
                        state.sb.Clear();
                        string strResult = plc_data.Substring(startStr, (endStr - startStr) + 1);
                        if (ReceiveData != null) ReceiveData(_client, strResult);

                    }
                    // Get the rest of the data.
                    _client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(string.Format("ReceiveCallback() {0}", e.Message));
                closeClient(_client);
            }
        }

        private void ConnectClient(Socket _client)
        {
            try
            {
                if (_client != null)
                {
                    if (!_client.Connected)
                    {
                        _client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _client);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("ConnectClient() {0}", e.Message));
                timer.Enabled = !stopped;
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

                Console.WriteLine(string.Format("Cliente conectado {0}", _client.RemoteEndPoint.ToString()));

                Receive(_client);

            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("ConnectCallback() {0}", e.Message));
                closeClient(_client);
            }
        }


        public void Send(Socket client, byte[] byteData)
        {
            try
            {
                // Begin sending the data to the remote device.
                client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket _client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = _client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("SendCallback() {0}", e.Message));
                timer.Enabled = !stopped;
            }
        }


        private void closeClient(Socket p_client)
        {
            timer.Enabled = false;
            if (p_client != null)
            {
                if (p_client.Connected)
                {
                    p_client.Shutdown(SocketShutdown.Both);
                    p_client.Close();
                }
                p_client.Dispose();
            }
            timer.Enabled = !stopped;
        }
    }
}
