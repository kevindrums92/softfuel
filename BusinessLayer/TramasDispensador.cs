using DataAccess;
using Singleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XbeeUtils;

namespace BusinessLayer
{
    class TramasDispensador : IDisposable
    {
        XbeeSingleton instancia = XbeeSingleton.Instance;
        #region "Procesos Tramas"
        public ResultadoTrama EnvioTotales(string[] data)
        {
            try
            {
                //desglosar mensaje
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                decimal galon_m1 = (Convert.ToDecimal(data[2])/1000);
                int dinero_m1 = Convert.ToInt32(data[3]);
                int ppu_m1 = Convert.ToInt32(data[4]);
                decimal galon_m2 = (Convert.ToDecimal(data[5])/1000);
                int dinero_m2 = Convert.ToInt32(data[6]);
                int ppu_m2 = Convert.ToInt32(data[7]);
                decimal galon_m3 = (Convert.ToDecimal(data[8])/1000);
                int dinero_m3 = Convert.ToInt32(data[9]);
                int ppu_m3 = Convert.ToInt32(data[10]);
                string idProducto;
                string usuarioIslero;
                string idXbeeDispensador;
                DataTable dtPosicion;
                DataTable dtVentasTotales;
                bool RealizoVentaTotal = false;
                string ventaGalones = "";
                string ventaDinero = "";
                bool esCredito = false;

                string serialFidelizado = "";
                string serialCredito = "";
                int descuentoCredito = 0;
                //Capturo si es venta fidelizado 
                if (instancia.ListaFidelizadosCreditosPendientes.Count > 0)
                {
                    FidelizadoCreditoPendiente objFidelizado = instancia.ListaFidelizadosCreditosPendientes.Find(item => item.cara == cara && item.tipoSolicitud == ETipoSolicitudSerial.Fidelizado);
                    if (objFidelizado != null)
                    {
                        serialFidelizado = objFidelizado.serial;
                        instancia.ListaFidelizadosCreditosPendientes.Remove(objFidelizado);
                    }
                    FidelizadoCreditoPendiente objCredito = instancia.ListaFidelizadosCreditosPendientes.Find(item => item.cara == cara && item.tipoSolicitud == ETipoSolicitudSerial.Credito);
                    if (objCredito != null)
                    {
                        esCredito = true;
                        serialCredito = objCredito.serial;
                        descuentoCredito = objCredito.descuento;
                        instancia.ListaFidelizadosCreditosPendientes.Remove(objCredito);
                    }
                }

                //obtengo el id de la posición por la cara, y traigo el idProducto tambien
                //Obtengo el ultimo registro de ventas en la cara
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    dtPosicion = modPOS.ObtenerPosicionesPorCara(cara);
                    
                    if (dtPosicion != null && dtPosicion.Rows.Count > 0)
                    {
                        idProducto = dtPosicion.Rows[0]["idProducto"].ToString();
                        idXbeeDispensador = dtPosicion.Rows[0]["idXbee"].ToString();
                        var DatosTurno = modPOS.ObtenerTurnoPorPosicionyEstado(dtPosicion.Rows[0]["idPosicion"].ToString());
                        if (DatosTurno != null && DatosTurno.Rows.Count>0)
                        {
                            usuarioIslero = DatosTurno.Rows[0]["idUsuario"].ToString();
                        }
                        else
                        {
                            return new ResultadoTrama(false, null, "No se pudo obtener los datos de un turno abierto para esta cara");
                        }
                    }
                    else
                    {
                        return new ResultadoTrama(false, null, "No se pudo obtener el id del producto de la posición");
                    }
                    dtVentasTotales = modPOS.ObtenerTotalesVentaPorCara(cara);
                    
                    if (dtVentasTotales == null) return new ResultadoTrama(false, null, "Hubo error obteniendo los valores de las ventas totales");
                    if (dtVentasTotales.Rows.Count > 0)
                    { 
                        //detectamos si en alguna de las mangueras hubo cambios para insertar en la tabla de ventas
                        if (Convert.ToDecimal(dtVentasTotales.Rows[0]["g1"]) != galon_m1)
                        {
                            RealizoVentaTotal = true;
                            decimal difGalon = (galon_m1 - Convert.ToDecimal(dtVentasTotales.Rows[0]["g1"]));
                            int difDinero = (dinero_m1 - Convert.ToInt32(dtVentasTotales.Rows[0]["p1"]));
                            ventaGalones = difGalon.ToString();
                            ventaDinero = difDinero.ToString();
                            using (ModeloDispensador modDIS = new ModeloDispensador())
                            {
                                DataTable dtPosicionProductoCorrecto;
                                dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara,"1");
                                if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 1");
                                var resultGuardar = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "1", difDinero.ToString(), difGalon.ToString(), ppu_m1.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado,serialCredito,descuentoCredito);
                            }
                        }
                        if (Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]) != galon_m2)
                        {
                            DataTable dtPosicionProductoCorrecto;
                            dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara, "2");
                            if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 2");
                            RealizoVentaTotal = true;
                            decimal difGalon = (galon_m2 - Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]));
                            int difDinero = (dinero_m2 - Convert.ToInt32(dtVentasTotales.Rows[0]["p2"]));
                            ventaGalones = difGalon.ToString();
                            ventaDinero = difDinero.ToString();
                            using (ModeloDispensador modDIS = new ModeloDispensador())
                            {
                                var resultGuardar = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "2", difDinero.ToString(), difGalon.ToString(), ppu_m2.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado, serialCredito, descuentoCredito);
                            }
                        }
                        if (Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]) != galon_m3)
                        {
                            DataTable dtPosicionProductoCorrecto;
                            dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara, "3");
                            if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 3");
                            RealizoVentaTotal = true;
                            decimal difGalon = (galon_m3 - Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]));
                            int difDinero = (dinero_m3 - Convert.ToInt32(dtVentasTotales.Rows[0]["p3"]));
                            ventaGalones = difGalon.ToString();
                            ventaDinero = difDinero.ToString();
                            using (ModeloDispensador modDIS = new ModeloDispensador())
                            {
                                var resultGuardar = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "3", difDinero.ToString(), difGalon.ToString(), ppu_m3.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado, serialCredito, descuentoCredito);
                            }
                        }
                    }
                    if (RealizoVentaTotal)
                    {
                        using (ModeloDispensador modDIS = new ModeloDispensador())
                        {
                            //Por ultimo guardamos en ventas totales
                            var resultGuardarTotales = modDIS.GuardaVentasTotales(data, _FechaActual, idXbeeDispensador);
                        }
                    }
                    else
                    {
                        return new ResultadoTrama(false, null, "No se guardo venta por que no se detectaron diferencias en galones ni dinero");
                    }
                }

                return new ResultadoTrama(true, null,"",_ventaGalones:ventaGalones,_ventaDinero:ventaDinero,_esCredito: esCredito);
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public List<Byte[]> CambiodePrecio(string idXbee)
        {
            try
            {
                List<byte[]> TramasDevolver = new List<byte[]> { };
                DataTable dtPrecios;
                using (ModeloDispensador modDIS = new ModeloDispensador())
                {
                    dtPrecios = modDIS.ObtenerPreciosActualizados();
                }
                int precioAcpm = Convert.ToInt32(dtPrecios.Rows[0][0]);
                int precioGasolina = Convert.ToInt32(dtPrecios.Rows[1][0]);
                int precioExtra = Convert.ToInt32(dtPrecios.Rows[2][0]);
                int precioGas = Convert.ToInt32(0);
                string TramaDevolver = "MM:" + UtilidadesTramas.ConcatenarCerosIzquiera(precioAcpm.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioGasolina.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioExtra.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioGas.ToString()) + "";
                //string TramaDevolver = "XP:" + UtilidadesTramas.ConcatenarCerosIzquiera(precioAcpm.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioGasolina.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioExtra.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precioGas.ToString()) + "";
                //string TramaDevolver = "XXX";
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaDevolver));

                return TramasDevolver;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return null;
            }
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
