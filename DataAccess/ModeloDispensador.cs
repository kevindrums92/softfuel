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

    public class ModeloDispensador : Connection, IDisposable
    {

        #region Enviar Totales

        public int GuardaVenta(string idProducto, string cara, string manguera, string dinero, string galones, string ppu, string fecha, string islero, string xbee, string serialFidelizado, string serialCredito, int descuento)
        {
            int ImprimeTiquete = 0;
            string idVehiculo = "NULL";
            int tipoVenta = 2; //tipo de cuenta 1-> credito, 2-> Contado,3->Prepago, 4->tarjetaCredito

            decimal descuentoporGalonPrepago = 0;
            decimal valPrepago = 0;

            decimal descuentoporGalon = 0;
            decimal valCredito = 0;
            decimal descuentoReal = 0;

            string puntos = "NULL";

            try
            {
                var dtDatafonoCara = GetTable("select count(*) from datafono where cara = " + cara + " and pendiente = 1");
                if(Convert.ToInt32(dtDatafonoCara.Rows[0][0])>0)
                {
                    tipoVenta = 4;
                    ExecuteQuery("update datafono set pendiente = 0 where cara = " + cara);
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog("Ocurrio un error guardando venta tarjeta crédito: " + e.Message.ToString() + "\n" + e.StackTrace.ToString(), LocalLogManager.TipoImagen.TipoError);
            }

            try
            {
                var dtPrepagoEnCara = GetTable("select idPrepago,idUsuario,idVehiculo,cara,cast(descuento as signed) as descuento,cupo,saldo,pendiente from prepago where cara = " + cara + " and pendiente = 1");
                //Parte para Prepago
                
                if (dtPrepagoEnCara.Rows.Count > 0)
                {
                    ImprimeTiquete = 1;
                    if (Convert.ToInt32(dtPrepagoEnCara.Rows[0]["descuento"].ToString()) != 0)
                    {
                        descuentoporGalonPrepago = Convert.ToInt32(dtPrepagoEnCara.Rows[0]["descuento"].ToString());
                        var valGalonDescuento = Convert.ToDecimal(ppu) + descuentoporGalonPrepago;
                        valPrepago = Convert.ToDecimal(galones) * Convert.ToDecimal(valGalonDescuento);
                    }
                    else
                    {
                        valPrepago = Convert.ToInt32(dinero);
                    }

                    tipoVenta = 3;
                    idVehiculo = dtPrepagoEnCara.Rows[0]["idVehiculo"].ToString();
                    decimal DineroDescontar = valPrepago;
                    descuentoReal = Convert.ToDecimal(dinero) - valPrepago;
                    decimal saldoAntiguo = 0;
                    if (object.Equals(dtPrepagoEnCara.Rows[0]["saldo"], DBNull.Value) == false && dtPrepagoEnCara.Rows[0]["saldo"].ToString().Trim() != "")
                    {
                        saldoAntiguo = Convert.ToDecimal(dtPrepagoEnCara.Rows[0]["saldo"]);
                    }
                    decimal nuevoSaldo = saldoAntiguo - DineroDescontar;
                    ExecuteQuery("update prepago set pendiente = 0, saldo = " + nuevoSaldo.ToString().Replace(',', '.') + " where idPrepago = " + dtPrepagoEnCara.Rows[0]["idPrepago"].ToString());
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog("Ocurrio un error guardando venta prepago: " + e.Message.ToString() + "\n" + e.StackTrace.ToString(), LocalLogManager.TipoImagen.TipoError);
            }


            try
            {
                var dtCreditosEnCara = GetTable("select idVehiculo,idUsuario,idCredito,cara,cast(descuento as signed) as descuento,cupo,saldo,dia,pendiente,estadoCredito from credito where cara = " + cara + " and pendiente = 1");
                //Parte para crédito
                
                if (dtCreditosEnCara.Rows.Count > 0)
                {
                    ImprimeTiquete = 1;

                    if (Convert.ToInt32(dtCreditosEnCara.Rows[0]["descuento"].ToString()) != 0)
                    {
                        descuentoporGalon = Convert.ToInt32(dtCreditosEnCara.Rows[0]["descuento"].ToString());
                        var valGalonDescuento = Convert.ToDecimal(ppu) + descuentoporGalon;
                        valCredito = Convert.ToDecimal(galones) * Convert.ToDecimal(valGalonDescuento);
                    }
                    else
                    {
                        valCredito = Convert.ToInt32(dinero);
                    }

                    tipoVenta = 1;
                    idVehiculo = dtCreditosEnCara.Rows[0]["idVehiculo"].ToString();
                    decimal DineroDescontar = valCredito;
                    descuentoReal = Convert.ToDecimal(dinero) - valCredito;
                    decimal saldoAntiguo = 0;
                    if (object.Equals(dtCreditosEnCara.Rows[0]["saldo"], DBNull.Value) == false && dtCreditosEnCara.Rows[0]["saldo"].ToString().Trim() != "")
                    {
                        saldoAntiguo = Convert.ToDecimal(dtCreditosEnCara.Rows[0]["saldo"]);
                    }
                    decimal nuevoSaldo = saldoAntiguo - DineroDescontar;
                    ExecuteQuery("update credito set pendiente = 0, saldo = " + nuevoSaldo.ToString().Replace(',', '.') + " where idCredito = " + dtCreditosEnCara.Rows[0]["idCredito"].ToString());
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog("Ocurrio un error guardando venta crédito: " + e.Message.ToString() + "\n" + e.StackTrace.ToString(), LocalLogManager.TipoImagen.TipoError);
            }
            


            var dtFidelizadoEnCara = GetTable("select * from puntos where cara = " + cara + " and pendiente = 1");

            try
            {
                //Parte para fidelizado
                
                if (dtFidelizadoEnCara.Rows.Count > 0)
                {
                    DataTable dtFidelizado = ObtenerFidelizadoPorIdUsuario(dtFidelizadoEnCara.Rows[0]["idUsuario"].ToString());
                    if (dtFidelizado.Rows.Count > 0)
                    {
                        idVehiculo = dtFidelizado.Rows[0]["idVehiculo"].ToString();
                        int valorDinero = Convert.ToInt32(dtFidelizado.Rows[0]["valorDinero"]);
                        int valorPuntos = Convert.ToInt32(dtFidelizado.Rows[0]["valorPuntos"]);

                        puntos = ((Convert.ToInt32(dinero) / valorDinero) * valorPuntos).ToString();
                        ExecuteQuery("update puntos set puntos = puntos + " + puntos + " where idPuntos = " + dtFidelizadoEnCara.Rows[0]["idPuntos"] + "");
                        ExecuteQuery("update puntos set pendiente = 0 where idPuntos = " + dtFidelizadoEnCara.Rows[0]["idPuntos"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog("Ocurrio un error guardando venta fidelizado: " + e.Message.ToString() + "\n" + e.StackTrace.ToString(), LocalLogManager.TipoImagen.TipoError);
            }
            

            var placa = "";
            //saco la placa
            if (idVehiculo != "NULL")
            {
                var dtVehiculo = GetTable("select placa from vehiculo where id=" + idVehiculo);
                if (dtVehiculo.Rows[0][0] != DBNull.Value)
                {
                    placa = dtVehiculo.Rows[0][0].ToString();
                }

            }

            string sqlInsertInto = "insert into ventas (idProducto, cara, manguera, precio, galones, ppu, fecha, islero, idXbee,puntos,idVehiculo,tipoCuenta,descuento,placa) values";
            ExecuteQuery(sqlInsertInto + "(" + idProducto + "," + cara + "," + manguera + "," + dinero + "," + galones.ToString().Replace(',', '.') + "," + ppu + ",'" + fecha + "','" + islero + "'," + xbee + "," + puntos + "," + idVehiculo + "," + tipoVenta + "," + descuentoReal.ToString().Replace(',', '.') + ",'" + placa + "')");
            string sqlUpdate = "update producto set existenciaProducto = existenciaProducto - " + galones.ToString().Replace(',', '.') + " where idProducto = " + idProducto;
            ExecuteQuery(sqlUpdate);
            return ImprimeTiquete;
            
        }

        public bool GuardaVentasTotales(string[] datos, string fecha, string idXbee)
        {
            string cara = datos[1];
            decimal galon_m1 = (Convert.ToDecimal(datos[2]) / 1000);

            decimal galon_m2 = (Convert.ToDecimal(datos[5]) / 1000);

            decimal galon_m3 = (Convert.ToDecimal(datos[8]) / 1000);
            string stringgalon_m1 = galon_m1.ToString().Replace(',', '.');
            string stringgalon_m2 = galon_m2.ToString().Replace(',', '.');
            string stringgalon_m3 = galon_m3.ToString().Replace(',', '.');

            string sqlInsertInto = "insert into ventatotal (cara, m1, g1, p1, v1, m2, g2, p2, v2, m3, g3, p3, v3, fecha, idXbee) values";
            return ExecuteQuery(sqlInsertInto + "(" + cara + ",1," + stringgalon_m1 + "," + datos[3] + "," + datos[4]
                + ",2," + stringgalon_m2 + "," + datos[6] + "," + datos[7] + ",3," + stringgalon_m3 + "," + datos[9] + "," + datos[10] + ",'" + fecha
                + "'," + idXbee + ")");
        }
        #endregion

        #region Fidelizado
        public DataTable ObtenerFidelizadoPorIdUsuario(string idUsuario)
        {
            return GetTable("SELECT V.id AS idVehiculo, V.placa, P.idPuntos, P.puntos, P.idPlan,PP.nomPlan, TP.valorPuntos, TP.valorDinero, V.propietario FROM vehiculo V LEFT OUTER JOIN puntos P ON P.idVehiculo = V.id LEFT OUTER JOIN parametrizapuntos PP ON PP.idPlan = P.idPlan LEFT OUTER JOIN tipoplan TP ON TP.idTipoplan = PP.tipoPlan WHERE V.propietario = '" + idUsuario + "'");
        }
        #endregion
        #region Credito
        public DataTable ObtenerCreditoPorSerial(string serial)
        {
            return GetTable("select V.id, V.placa, C.idCredito, C.descuento, C.cupo, C.saldo, C.dia, V.propietario from vehiculo V LEFT OUTER JOIN credito C ON C.idVehiculo = V.id WHERE C.estadoCredito = 'activo' and serial  = '" + serial + "'");
        }
        #endregion
        #region CambioPrecio
        public DataTable ObtenerPreciosActualizados()
        {
            return GetTable("select precioventaProducto from producto where idProducto IN(1,2,3,4)");
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
