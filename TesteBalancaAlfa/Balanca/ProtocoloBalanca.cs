using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

namespace SPPA
{
    public static class ProtocoloBalanca
    {

        #region Constantes

        public const int STX = 0x02;
        static public string configProtocolo = ConfigurationManager.AppSettings["BALANCA"];
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

        static public bool TratarMensagem(byte[] dados)
        {
            if (configProtocolo == "TOLEDO")
                TratarMensagemToledo(dados);
            else if(configProtocolo == "ALFA")
                TratarMensagemAlfa(dados);
            else if(configProtocolo == "3102")
                TratarMensagem3102(dados);
            else
                return false;

            return true;                                                                              
        }

        #endregion

        #region Métodos privados

        static private bool TratarMensagemToledo(byte[] dados)
        {
            if (dados.Length >= 13)
            {
                char[] chars = UnicodeEncoding.ASCII.GetChars(dados);

                string peso = new string(chars).Replace("\r", "").Replace("\u0002", "").Substring(3, 6);
                string tara = new string(chars).Replace("\r", "").Replace("\u0002", "").Substring(9, 6);

                if (chars[2].ToString() == "0") //Peso Estabilizado.
                {
                    SetDadosBalanca(peso, tara, false, false);
                }
                else if (chars[2].ToString() == "2") //Peso Negativo.
                {
                    SetDadosBalanca(peso, tara, true, true);
                }
                else if (chars[2].ToString() == "1") //Função tara estabilizada
                {
                    SetDadosBalanca(peso, tara, false, false);
                }
                else // Peso ainda não estabilizou char == 8 ou 9
                {
                    SetDadosBalanca(peso, tara, true, false);
                }

                return true;
            }
            return false;
        }

        static private bool TratarMensagemAlfa(byte[] dados)
        {
            if (dados.Length >= 13)
            {
                char[] chars = UnicodeEncoding.ASCII.GetChars(dados);

                string peso = new string(chars).Substring(4, 6);
                string tara = new string(chars).Substring(13, 6);
                string estabilizado = new string(chars).Substring(0, 2);

                if (estabilizado == "PB" && peso != "00,000") //Peso Estabilizado.
                {
                    SetDadosBalanca(peso, tara, false, false);
                }
                else if (estabilizado == "PB" && peso == "00,000") //Peso Estabilizado.
                {
                    SetDadosBalanca(peso, tara, true, false);
                }
                else if (estabilizado == "PL")  //Peso Estabilizado tara
                {
                    SetDadosBalanca(peso, tara, false, false);
                }
                else if (estabilizado == "**") // Ainda 
                {
                    SetDadosBalanca(peso, tara, true, false);
                }
                else if (chars[3].ToString() == "-") //Peso Negativo.
                {
                    SetDadosBalanca(peso, tara, false, true);
                }

                return true;
            }
            return false;
        }

        static public bool TratarMensagem3102(byte[] msg)
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

        static private void SetDadosBalanca(string peso, string tara, bool movimento, bool negativo)
        {
            double.TryParse(peso, out _peso);
            double.TryParse(tara, out _tara);
            _status1.Movimento = movimento;
            _status1.PesoNegativo = negativo;

            if (configProtocolo != "ALFA")
            {
                _peso *= 0.001;
                _tara *= 0.001;
            }
        }

        #endregion
    }

}







