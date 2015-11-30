using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using XbeeUtils;

namespace DataAccess
{
    public class Generales : Connection, IDisposable
    {


        public DataTable ObtenerUsuario(string Identificacion)
        {
            return GetTable("select idUsuario, nomUsuario, apeUsuario, P.tipoPerfil from Usuario AS U INNER JOIN perfil AS P ON U.perfilUsuario = P.idPerfil where idUsuario = '" + Identificacion + "'");
        }

        public DataTable ObtenerXbeePorMac(string Mac) 
        {
            return GetTable("select idXbee, nomXbee, macXbee, tipoXbee, tiempoEspXbee, impresoraXbee from xbee where macXbee = '" + Mac + "'");
        }

        public DataTable ObtenerXbeeCoordinador()
        {
            return GetTable("select idXbee, nomXbee, macXbee, puertoXbee, velocidadXbee from xbee where tipoXbee = " + (int)Enumeraciones.TipoDispositivo.Cordinador + "");
        }

        public DataTable ObtenerTodosLosXbee()
        {
            return GetTable("select * from Xbee where tipoXbee <> 1");
        }

        #region "IDisposable"
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
        }
        #endregion
        
    }
}
