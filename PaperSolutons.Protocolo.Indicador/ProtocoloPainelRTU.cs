using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public class ProtocoloPainelRTU : BalancaBase
    {
        #region Constantes
        private const int STX = 0x02;
        private const int ETX = 0x03;
        private const int NUL = 0x00;
        private const int CR = 0x0d;   //Return
        private const int LF = 0x0a;   //Line Feet
        private const int AAA = 0x41;   //letra (A)
        private const int INST = 0x30;

        #endregion

        #region private fiels and lists
        private int BCC = -1;
        #endregion

        public ProtocoloPainelRTU()
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
            for (int i = 0; i < dataBytes.Length; i++) 
            {
                byte item = dataBytes[i];
                Debug.Write(string.Format("{0} ", item));
                switch (item)
                {
                   
                    case AAA:
                        if(i == 0) data.Clear();
                        data.Add(item);
                        break;
                    case LF:
                        data.Add(item);
                        eof = true;
                        break;
                    default:
                        if(data.Count > 0) data.Add(item);
                        break;
                }

                if (eof)
                {
                    msgComplete = true;
                    break;
                }
            }
            Debug.WriteLine(string.Format("\n Count: {0} ", dataBytes.Length));
            //Retonar se existirem dados com mais de 15 bytes.
            if (data.Count > 10)
            {
                data.Clear();
                return false;
            }

            if (msgComplete && data.Count == 10)
            {
                _peso = _tara = 0;
                this.Instavel = (data[2] == INST);
                this.PesoNegativo = false;
                this.PesoLiquido = true;

                byte[] _weight = new byte[] { data[3], data[4], data[5], data[6], data[7] };

                double.TryParse(UnicodeEncoding.ASCII.GetString(_weight), out _peso);
                _peso = _peso / 100;
               // double.TryParse(UnicodeEncoding.ASCII.GetString(new byte[] { data[14], data[15], data[16], data[17], data[18],data[19] }), out _tara);
                if (this.PesoNegativo) _peso *= -1;
                success = true;
            }
            return success;
        }

        #endregion

    }
}
