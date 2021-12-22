using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public class ProtocoloAlpha3103c : BalancaBase
    {
        #region Constantes
        private const int STX = 0x02;
        private const int ETX = 0x03;
        private const int NUL = 0x00;
        private const int CR = 0x0d;   //Return
        private const int LF = 0x0a;   //Line Feet
        private const int PES = 0x50;   //letra (P)eso
        private const int NEG = 0x2D; //Negativo
        private const int AST = 0x2A; // Asterisco
        private const int VIR = 0x2C; //Virgula
        private const int LIQ = 0x4C; //Letra - (L)iquido
        private const int DPO = 0x3A; // dois pontos
        private byte[] _weight = new byte[6];
        private byte[] _wgt_tare = new byte[6];

        #endregion

        #region private fiels and lists
        private int BCC = -1;
        #endregion

        public ProtocoloAlpha3103c()
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
            _peso = _tara = 0;
            bool success = false;
            bool msgComplete = false;
            bool eof = false;
            BCC = -1;

            this.Saturacao = (dataBytes[0] == 0x53 && dataBytes[1] == 0x41);
            this.Sobrecarga = (dataBytes[0] == 0x53 && dataBytes[1] == 0x4F);
            
            //Verifica Se a mensagem está ok.
            for (int i = 0; i < dataBytes.Length; i++) 
            {
                byte item = dataBytes[i];
                Debug.Write(string.Format("{0} ", item));
                switch (item)
                {
                    case PES:
                    case AST:
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
            if (data.Count > 25)
            {
                data.Clear();
                return false;
            }

            if (msgComplete && data.Count > 20)
            {
               
                this.Instavel = (data[0] == AST);
                this.PesoLiquido = (data[1] == LIQ);
                this.PesoNegativo = (data[3] == NEG);

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == DPO)
                    {
                        i = i + 2;
                        if (i == 4) //Se a posição 4 então é o peso liquido ou bruto
                        {
                            for (int p = 0; p < 6 && i < data.Count; p++)
                            {
                                if (data[i] == VIR) this.CasasDecimais = 5 - p;
                                _weight[p] = data[i++];
                            }
                        }
                        else //tara
                        {
                            for (int t = 0; t < 6 && i < data.Count; t++) _wgt_tare[t] = data[i++];
                        }
                    }
                }

                double.TryParse(UnicodeEncoding.ASCII.GetString(_weight), out _peso);
                double.TryParse(UnicodeEncoding.ASCII.GetString(_wgt_tare), out _tara);
                if (this.PesoNegativo) _peso *= -1;
                success = true;
            }
            return success;
        }

        #endregion

    }
}
