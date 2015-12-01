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
    public class ModeloDispensador: Connection, IDisposable
    {

        #region Enviar Totales
        
        public bool GuardaVenta(string idProducto, string cara, string manguera, string dinero, string galones, string ppu, string fecha, string islero, string xbee)
        {
            string sqlInsertInto = "insert into ventas (idProducto, cara, manguera, precio, galones, ppu, fecha, islero, idXbee) values";
            return ExecuteQuery(sqlInsertInto + "(" + idProducto + "," + cara + "," + manguera + "," + dinero + "," + galones.ToString().Replace(',','.') + "," + ppu + ",'" + fecha + "','" + islero + "'," + xbee + ")");
        }

        public bool GuardaVentasTotales(string[] datos,string fecha, string idXbee)
        {
            string cara = datos[1];
            decimal galon_m1 = (Convert.ToDecimal(datos[2]) / 1000);

            decimal galon_m2 = (Convert.ToDecimal(datos[5]) / 1000);

            decimal galon_m3 = (Convert.ToDecimal(datos[8]) / 1000);
            string stringgalon_m1 = galon_m1.ToString().Replace(',','.');
            string stringgalon_m2 = galon_m2.ToString().Replace(',', '.');
            string stringgalon_m3 = galon_m3.ToString().Replace(',', '.');

            string sqlInsertInto = "insert into ventatotal (cara, m1, g1, p1, v1, m2, g2, p2, v2, m3, g3, p3, v3, fecha, idXbee) values";
            return ExecuteQuery(sqlInsertInto + "(" + cara + ",1," + stringgalon_m1 + "," + datos[3] + "," + datos[4]
                + ",2," + stringgalon_m2 + "," + datos[6] + "," + datos[7] + ",3," + stringgalon_m3 + "," + datos[9] + "," + datos[10] + ",'" + fecha 
                + "'," + idXbee + ")");
        }
        #endregion

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
