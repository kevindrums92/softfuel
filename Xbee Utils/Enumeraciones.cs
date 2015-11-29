using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeeUtils
{
    public static class Enumeraciones
    {
        #region "Enums"
        public enum TipodeMensaje { ConAlerta, SinAlerta };
        public enum Direccion { izquierda, derecha, ambos };
        public enum TipoDispositivo:sbyte { Cordinador = 1,Dispensador = 2, moduloPOS =3 };

        #endregion
    }
}
