using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TesteBalancaAlfa
{
    public class ProtocoloAlpha3100c : BalancaBase
    {
        #region Constantes
        private const int STX = 0x02;
        private const int ETX = 0x03;
        private const int NUL = 0x00;
        #endregion

        #region private fiels and lists
        private int BCC = -1;
        #endregion

        public ProtocoloAlpha3100c()
        {
            this.SWA = new BalancaStatus();
            this.SWB = new BalancaStatus();
            this.SWC = new BalancaStatus();
        }
        public override string GetPesoFormated()
        {
            string formatString = string.Concat("{0:n", this.CasasDecimais, "}");
            return string.Format(formatString, this.Peso);
        }

        #region Métodos Publicos

        public override bool ReadBytes(byte[] dataBytes)
        {
            bool success = false;
            bool msgComplete = false;
            bool eof = false;
            BCC = -1;

            //Verifica Se a mensagem está ok.
            foreach (byte item in dataBytes)
            {
                switch (item)
                {
                    case STX:
                        data.Clear();
                        data.Add(item);
                        break;
                    case ETX:
                        data.Add(item);
                        BCC = CheckSumBCC();
                        eof = true;
                        break;
                    default:
                        if(data.Count > 0) data.Add(item);
                        break;
                }

                if (eof && item == BCC)
                {
                    msgComplete = true;
                }
            }
            //Retonar se existirem dados com mais de 15 bytes.
            if (data.Count > 15)
            {
                data.Clear();
                return false;
            }

            if (msgComplete && data.Count == 15)
            {
                _peso = _tara = 0;
                this.SWA = this.GetStatus(data[1]); //Convert para Binário
                SetStatus();
                this.SWB = this.GetStatus(data[2]); //Convert para Binário
                double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { data[3], data[4], data[5], data[6], data[7]}), out _peso);
                double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { data[8], data[9], data[10], data[11], data[12] }), out _tara);
                if (this.PesoNegativo) _peso *= -1;
                _peso *= this.ConvertPesoCasaDecimal();
                _tara *= this.ConvertPesoCasaDecimal();
                success = true;
            }
            return success;
        }

        #endregion

        private  void SetStatus()
        {
            string str = string.Format("{0}{1}{2}", Convert.ToInt16(this.SWA.BIT2) ,Convert.ToInt16(this.SWA.BIT1) ,Convert.ToInt16(this.SWA.BIT0)) ;
            this.CasasDecimais = Convert.ToInt32(str, 2); //Convert Binary String to Int.
            this.PesoNegativo = this.SWA.BIT3;
            this.Instavel = this.SWA.BIT4;
            this.Saturacao = this.SWA.BIT5;
            this.Sobrecarga = this.SWA.BIT6;
        }

    }
}
