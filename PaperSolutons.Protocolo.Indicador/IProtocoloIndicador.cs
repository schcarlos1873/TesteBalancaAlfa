using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaperSolutions.Protocolo.Indicador
{
    public interface IProtocoloIndicador
    {
        bool ReadBytes(byte[] dataBytes);
        byte CheckSumBCC();
        string GetPesoFormated();
    }

}
