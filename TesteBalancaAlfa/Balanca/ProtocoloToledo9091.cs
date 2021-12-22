using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TesteBalancaAlfa
{
    public class ProtocoloToledo9091 : BalancaBase
    {
        #region Constantes
        private const int STX = 0x02;  //Start Transmission
        private const int ETX = 0x03;  //End of Transmission
        private const int NUL = 0x00;  //Nulo
        private const int CR = 0x0d;   //Return
        private const int LF = 0x0a;   //Line Feet
        #endregion

        public ProtocoloToledo9091()
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
                    case CR:
                        data.Add(item);
                        eof = true;
                        break;
                    default:
                        if(data.Count > 0)  data.Add(item);
                        break;
                }
                if (eof)
                {
                    msgComplete = true;
                    break;
                }
            }

             //Retonar se existirem dados com mais de 15 bytes.
            if (data.Count > 17)
            {
                data.Clear();
                return false;
            }

            if (msgComplete && data.Count == 17)
            {
                _peso = _tara = 0;
                this.SWA = this.GetStatus(data[1]); //Convert para Binário
                this.SWB = this.GetStatus(data[2]); //Convert para Binário
                this.SWC = this.GetStatus(data[3]); //Convert para Binário
                SetStatus();
                double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { data[4], data[5], data[6], data[7], data[8], data[9] }), out _peso);
                double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { data[10], data[11], data[12], data[13], data[14], data[15] }), out _tara);
                if (this.PesoNegativo) _peso *= -1;
                _peso *= this.ConvertPesoCasaDecimal();
                _tara *= this.ConvertPesoCasaDecimal();
                success = true;
            }
            return success;
        }

        #endregion
        private void SetStatus()
        {
            string str = string.Format("{0}{1}{2}", Convert.ToInt16(this.SWA.BIT2), Convert.ToInt16(this.SWA.BIT1), Convert.ToInt16(this.SWA.BIT0));
            this.CasasDecimais = Convert.ToInt32(str, 2) -2; //Convert Binary String to Int.
            this.PesoNegativo = this.SWB.BIT1;
            this.Instavel = this.SWB.BIT3;
            this.Saturacao = this.SWB.BIT5;
            this.Sobrecarga = this.SWB.BIT2;
        }
      

    }
}
