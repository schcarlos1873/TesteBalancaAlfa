using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public static class BalancaAlfa
    {

        #region Constantes

        private const int STX = 0x02;

        #endregion

        #region Estruturas
        public struct STATUS1
        {
            public string Status;
            public string Display;
            public bool Sobrecarga;
            public bool Saturacao;
            public bool Movimento;
            public bool PesoNegativo;
            public double PontoDecimal;
        }

        public struct STATUS2
        {
            public string Status;
            public bool TR0;
            public bool TR3;
            public bool TR2;
            public bool TR1;
            public bool NV3;
            public bool NV2;
            public bool NV1;
            public bool NV0;
        }

        #endregion

        #region Fields

        static private STATUS1 _status1;
        static private STATUS2 _status2;
        static private double _peso = 0;
        static private double _tara = 0;

        #endregion

        #region Propriedades

        static public double Peso
        {
            get
            {
                return _peso;
            }
        }

        static public double Tara
        {
            get
            {
                return _tara;
            }
        }

        static public STATUS1 Status1
        {
            get
            {
                return _status1;
            }
        }

        static public STATUS2 Status2
        {
            get
            {
                return _status2;
            }
        }

        #endregion


        static private void Initialize()
        {
            //inicializa o Status 1
            _status1 = new STATUS1()
            {
                Status = "",
                Display = "",
                Movimento = false,
                PesoNegativo = false,
                Saturacao = false,
                Sobrecarga = false,
                PontoDecimal = 0
            };

            //Inicializa o status 2
            _status2 = new STATUS2()
            {
                Status = "",
                TR0 = false,
                TR3 = false,
                TR2 = false,
                TR1 = false,
                NV3 = false,
                NV2 = false,
                NV1 = false,
                NV0 = false
            };
        }



        #region Métodos Publicos

        static public bool TratarMensagem(byte[] msg)
        {
            Initialize();
            bool success = false;
            _peso = _tara = 0;
            int status1 = 0;
            int status2 = 0;

            if (msg.Length >= 13)
            {

                char[] chars = UnicodeEncoding.ASCII.GetChars(msg);

                if (msg[0] == STX)
                {
                    byte bcc = CheckSumBCC(msg, 0, msg.Length - 1);

                    if (msg[msg.Length - 1] == bcc)
                    {

                        string strMessage = new string(chars);
                        int.TryParse(msg[1].ToString(), out status1);
                        _status1.Status = Convert.ToString(status1, 2).PadLeft(8, '0'); //Convert para Binário
                        TrataStatus1();

                        int.TryParse(msg[2].ToString(), out status2);
                        _status2.Status = Convert.ToString(status2, 2).PadLeft(8, '0');
                        TrataStatus2();

                        double.TryParse(strMessage.Substring(3, 5), out _peso);
                        double.TryParse(strMessage.Substring(8, 5), out _tara);
                        if (_status1.PesoNegativo) _peso *= -1;
                        
                        _peso *= _status1.PontoDecimal;
                        _tara *= _status1.PontoDecimal;



                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
                else
                {
                    _peso = _tara = 0;
                }
            }

            return success;
        }

        #endregion

        #region Métodos privados

        //Trata o Binário do status 1
        static private void TrataStatus1()
        {
            if (_status1.Status.Length == 8)
            {
                bool stat = false;
                for (int i = 1; i < _status1.Status.Length - 3; i++)
                {
                    stat = (_status1.Status[i] == '1' ? true : false);
                    switch (i)
                    {
                        case 1:
                            _status1.Sobrecarga = stat;
                            break;

                        case 2:
                            _status1.Saturacao = stat;
                            break;

                        case 3:
                            _status1.Movimento = stat;
                            break;

                        case 4:
                            _status1.PesoNegativo = stat;
                            break;
                        default:
                            break;
                    }
                }

                _status1.Display = _status1.Status.Substring(5, 3);

                switch (_status1.Display)
                {
                    case "000":
                        _status1.PontoDecimal = 1;
                        break;
                    case "001":
                        _status1.PontoDecimal = 0.1;
                        break;                        
                    case "010":
                        _status1.PontoDecimal = 0.01;
                        break;
                    case "011":
                        _status1.PontoDecimal = 0.001;
                        break;
                    default:
                        break;
                }

            }
        }

        //Trata o binario do Status 2
        static private void TrataStatus2()
        {
            if (_status2.Status.Length == 8)
            {
                bool stat = false;
                for (int i = 1; i < _status2.Status.Length - 3; i++)
                {
                    stat = (_status2.Status[i] == '1' ? true : false);
                    switch (i)
                    {
                        case 0:
                            _status2.TR0 = stat;
                            break;

                        case 1:
                            _status2.TR3 = stat;
                            break;

                        case 2:
                            _status2.TR2 = stat;
                            break;

                        case 3:
                            _status2.TR1 = stat;
                            break;

                        case 4:
                            _status2.NV3 = stat;
                            break;

                        case 5:
                            _status2.NV2 = stat;
                            break;

                        case 6:
                            _status2.NV1 = stat;
                            break;

                        case 7:
                            _status2.NV0 = stat;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static private bool ConvertToBoolean(string str)
        {
            bool result = false;
            bool.TryParse(str, out result);
            return result;
        }


        static public byte CheckSumBCC(byte[] msg, int startIndex, int endIndex)
        {
            int bcc = 0;
            int dec = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                dec = msg[i];
                bcc = bcc ^ dec; //Operação Xor
                //bcc = bcc << 1; //Rotate shift left
            }
            return Convert.ToByte(bcc);
        }

        #endregion

    
    }
}
