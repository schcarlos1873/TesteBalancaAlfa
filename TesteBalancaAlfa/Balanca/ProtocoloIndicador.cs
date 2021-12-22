using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteBalancaAlfa
{
    public class ProtocoloIndicador
    {
        public BalancaBase Indicador;

        public ProtocoloIndicador(BalancaType type)
        {
            switch (type)
            {
                case BalancaType.ALFA3102:
                case BalancaType.ALFA3104:
                case BalancaType.ALFA3105:
                case BalancaType.ALFA3107:
                    Indicador = new ProtocoloAlpha3100c();
                    break;
                case BalancaType.TOLEDO9091:
                    Indicador = new ProtocoloToledo9091();
                    break;
                default:
                    break;
            }
            
        }
    }
}
