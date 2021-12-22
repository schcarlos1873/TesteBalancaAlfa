using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public class ProtocoloIndicador
    {
        public BalancaBase Indicador;
        public BalancaType ProtocoloType { get; private set;}

        public ProtocoloIndicador(BalancaType type)
        {
            this.ProtocoloType = type;
            switch (type)
            {
                case BalancaType.ALFA3102:
                case BalancaType.ALFA3104:
                case BalancaType.ALFA3105:
                case BalancaType.ALFA3107:
                    Indicador = new ProtocoloAlpha3100c();
                    break;
                case BalancaType.ALFA3101:
                case BalancaType.ALFA3103:
                    Indicador = new ProtocoloAlpha3103c();
                    break;
                case BalancaType.TOLEDO9091:
                    Indicador = new ProtocoloToledo9091();
                    break;
                case BalancaType.PAINEL_RTU:
                    Indicador = new ProtocoloPainelRTU();
                    break;
                case BalancaType.BELM4102:
                    Indicador = new ProtocoloBelM4102();
                    break;
                default:
                    break;
            }
            
        }
    }
}
