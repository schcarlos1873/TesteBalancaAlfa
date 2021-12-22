using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteBalancaAlfa
{
    public class BalancaStatus
    {
        public byte Status { get;  set; }
        public bool BIT0 { get;  set; }
        public bool BIT1 { get; set; }
        public bool BIT2 { get; set; }
        public bool BIT3 { get; set; }
        public bool BIT4 { get; set; }
        public bool BIT5 { get; set; }
        public bool BIT6 { get; set; }
        public bool BIT7 { get; set; }

        public BalancaStatus()
        {
            Status = 0x00;
            this.BIT0 = false;
            this.BIT1 = false;
            this.BIT2 = false;
            this.BIT3 = false;
            this.BIT4 = false;
            this.BIT5 = false;
            this.BIT6 = false;
            this.BIT7 = false;

        }
    }
}
