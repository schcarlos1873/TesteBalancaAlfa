using System;
using System.Collections;
using System.Collections.Generic;

namespace TesteBalancaAlfa
{
    public abstract class BalancaBase : IProtocoloIndicador
    {
        protected double _peso = 0;
        protected double _tara = 0;
        protected double _convertPeso = 0;
        protected List<byte> data = new List<byte>();
        public BalancaStatus SWA { get; protected set; }
        public BalancaStatus SWB { get; protected set; }
        public BalancaStatus SWC { get; protected set; }
        public string Message { get; protected set; }
        public double Peso { get { return _peso;} }
        public double Tara { get { return _tara; } }
        public abstract bool ReadBytes(byte[] dataBytes);
        public abstract string GetPesoFormated();
        public bool Sobrecarga { get; set; }
        public bool Saturacao { get; set; }
        public bool Instavel { get; set; }
        public bool PesoNegativo { get; set; }
        public bool PesoLiquido { get; set; }
        public double CasasDecimais { get; set; }

        public virtual double ConvertPesoCasaDecimal()
        {
            return 1.0 / Math.Pow(10, this.CasasDecimais);
        }

        public virtual BalancaStatus GetStatus(byte status)
        {
            BalancaStatus statusWord = new BalancaStatus();
            statusWord.Status = status;
            byte[] aByte = new byte[] { status };
            BitArray bits = new BitArray(aByte);

            for (int i = bits.Count - 1; i >= 0; i--)
            {
                switch (i)
                {
                    case 0:
                        statusWord.BIT0 = bits[i];
                        break;
                    case 1:
                        statusWord.BIT1 = bits[i];
                        break;
                    case 2:
                        statusWord.BIT2 = bits[i];
                        break;
                    case 3:
                        statusWord.BIT3 = bits[i];
                        break;
                    case 4:
                        statusWord.BIT4 = bits[i];
                        break;
                    case 5:
                        statusWord.BIT5 = bits[i];
                        break;
                    case 6:
                        statusWord.BIT6 = bits[i];
                        break;
                    default:
                        break;
                }
            }
            return statusWord;
        }

        public virtual byte CheckSumBCC()
        {
            int bcc = 0;
            int dec = 0;
            if (data.Count > 0)
            {
                foreach (byte msg in data)
                {
                    dec = msg;
                    bcc ^= dec++;
                }
            }
            return Convert.ToByte(bcc);
        }
    }
}
