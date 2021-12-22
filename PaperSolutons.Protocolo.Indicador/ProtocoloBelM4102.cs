using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public class ProtocoloBelM4102 : BalancaBase
    {
        #region Constantes
        private const int STX = 0x02;  //Start Transmission
        private const int ETX = 0x03;  //End of Transmission
        private const int NUL = 0x00;  //Nulo
        private const int CR = 0x0d;   //Return
        private const int LF = 0x0a;   //Line Feet
        private const int NEG = 0x2D; //Negativo
        private const int STA = 0x53; //Stabilidade

        #endregion

        public ProtocoloBelM4102()
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

            if (dataBytes.Length != 15) return false;

            if (dataBytes.Length == 15 && dataBytes[14]==LF)
            {
                _peso = _tara = 0;
                this.PesoNegativo = (dataBytes[0] == NEG);
                this.Instavel = !(dataBytes[12]==STA);

                string val = UnicodeEncoding.ASCII.GetString(new byte[] { dataBytes[1], dataBytes[2], dataBytes[3], dataBytes[4], dataBytes[5], dataBytes[6], dataBytes[7], dataBytes[8] });

                double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { dataBytes[1], dataBytes[2], dataBytes[3], dataBytes[4], dataBytes[5], dataBytes[6], dataBytes[7], dataBytes[8] }), NumberStyles.Any, CultureInfo.InvariantCulture, out _peso);
                if (this.PesoNegativo) _peso *= -1;
                _peso *= this.ConvertPesoCasaDecimal();
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
