using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteBalancaAlfa
{
    public class TcpIPConnect
    {
        public string TcpIP { get; set; }
        public int Port { get; set; }


        public TcpIPConnect()
        {
            this.TcpIP = "192.168.0.80";
            this.Port = 10001;
        }
    }

    
}
