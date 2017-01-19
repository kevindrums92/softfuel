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
    public class ActionResult
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public List<Byte[]> ResultadoDevolver { get; set; }
    }
    class TramasDispensador : IDisposable
    {
        XbeeSingleton instancia = XbeeSingleton.Instance;


        #region "Procesos Tramas"
        public ActionResult ValidacionAutorizarVenta(string cara, string manguera, int idXbee)
        {
            try
            {
                ActionResult resultado = new ActionResult();
                resultado.Estado = false;
                List<byte[]> TramasDevolver = new List<byte[]> { };
                resultado.ResultadoDevolver = TramasDevolver;

                //obtener credito pendiente
                var credito = instancia.ListaCreditosPendientes.Find(x => x.cara == cara);
                using (var modPOS = new ModeloPOS())
                {
                    //Validar si el dispensador requiere autorización para vender
                    DataTable dtXbee = modPOS.InformacionXbee(idXbee.ToString());
                    if (dtXbee.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtXbee.Rows[0]["requiereAutVenta"]) == true)
                        {
                            DataTable dtAutorizados = modPOS.EstaTurnoAbiertoPorIdXbee(idXbee.ToString());
                            if (dtAutorizados.Select("numPosicion = " + cara).Length > 0)
                            {
                                if (credito != null && credito.exigeRestriccion == true)
                                {
                                    var dtInfoProducto = modPOS.ObtenerPosicionesPorCarayManguera(cara, manguera);
                                    var limTanqueo = credito.datos.Rows[0]["limiteTanqueo"];
                                    //Limite de tanqueo por galones
                                    if (limTanqueo != DBNull.Value && limTanqueo.ToString() == "1")
                                    {
                                        var numGalones = credito.datos.Rows[0]["limiteGalones"];
                                        if (numGalones != DBNull.Value)
                                        {
                                            decimal galonesATanquear = Convert.ToDecimal(credito.valor) / Convert.ToDecimal(dtInfoProducto.Rows[0]["precioVentaProducto"]);
                                            if (galonesATanquear > Convert.ToDecimal(numGalones))
                                            {
                                                resultado.ResultadoDevolver = AsistenteMensajes.GenerarMensajeAlerta(
                                                new string[] { "El numero de galones", "es superior al permitido", "num gal max: " + numGalones.ToString() });
                                                return resultado;
                                            }
                                        }
                                    }

                                    if (credito.datos.Rows[0]["restriccionProd"] != DBNull.Value &&
                                        Convert.ToBoolean(credito.datos.Rows[0]["restriccionProd"]) == true)
                                    {

                                        if (!modPOS.ConocerSiProductoEsValidoParaTanqueo(dtInfoProducto.Rows[0]["idProducto"].ToString(), credito.datos.Rows[0]["idVehiculo"].ToString()))
                                        {
                                            resultado.ResultadoDevolver = AsistenteMensajes.GenerarMensajeAlerta(
                                                new string[] { "El producto ", dtInfoProducto.Rows[0]["nomProducto"].ToString(), "no es permitido" });
                                            return resultado;
                                        }
                                    }
                                }
                            }
                            else {
                                resultado.ResultadoDevolver = AsistenteMensajes.GenerarMensajeAlerta(
                                                new string[] { "No hay turno en la cara", cara});
                                return resultado;
                            }
                        }
                    }
                }

                //Validar si hay credito y si puede tanquear el producto que contiene la maguera
                resultado.Estado = true;
                if (credito != null && credito.exigeRestriccion == true)
                {
                    TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("VC:" + cara + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(credito.valor,7)));
                }
                else {
                    TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("OK" + cara));
                }
                    
                return resultado;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return null;
            }
        }

        public ResultadoTrama EnvioTotales(string[] data)
        {
            try
            {
                //desglosar mensaje
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                decimal galon_m1 = (Convert.ToDecimal(data[2])/1000);
                int dinero_m1 = Convert.ToInt32(data[3]);
                int ppu_m1 = 0;
                ppu_m1 = (data[4].ToString().Trim() != "") ? Convert.ToInt32(data[4]) : 0;
                decimal galon_m2 = (Convert.ToDecimal(data[5])/1000);
                int dinero_m2 = Convert.ToInt32(data[6]);
                int ppu_m2 = 0;
                ppu_m2 = (data[7].ToString().Trim() != "") ? Convert.ToInt32(data[7]) : 0;
                decimal galon_m3 = (Convert.ToDecimal(data[8])/1000);
                int dinero_m3 = Convert.ToInt32(data[9]);
                int ppu_m3 = Convert.ToInt32(data[10]);
                ppu_m3 = (data[10].ToString().Trim() != "") ? Convert.ToInt32(data[10]) : 0;
                string idProducto;
                string usuarioIslero;
                string idXbeeDispensador;
                DataTable dtPosicion;
                DataTable dtVentasTotales;
                bool RealizoVentaTotal = false;
                bool VentaMayorA9999999 = false;
                string ventaGalones = "";
                string ventaDinero = "";
                bool esCredito = false;
                int ImprimeTiquete = 0;

                string serialFidelizado = "";
                string serialCredito = "";
                int descuentoCredito = 0;
                
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
                            //validamos si es negativo
                            if (Convert.ToInt32(ventaDinero)> Convert.ToInt32(9999999))
                            {
                                VentaMayorA9999999 = true;
                            }
                            else {
                                using (ModeloDispensador modDIS = new ModeloDispensador())
                                {
                                    DataTable dtPosicionProductoCorrecto;
                                    dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara, "1");
                                    if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 1");

                                    if(ppu_m1 != Convert.ToInt32(dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"])){
                                        return new ResultadoTrama(false, null, "ET_002 El valor del producto " 
                                            + dtPosicionProductoCorrecto.Rows[0]["nomProducto"].ToString() + " en la cara " + cara
                                            + " es diferente al que llego en la venta\nppu venta: "+ ppu_m1 + "\nppu parametrizado:" + dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"].ToString());
                                    }

                                    ImprimeTiquete = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "1", difDinero.ToString(), difGalon.ToString(), ppu_m1.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado, serialCredito, descuentoCredito);
                                }
                            }
                            
                        }
                        if (Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]) != galon_m2)
                        {
                            DataTable dtPosicionProductoCorrecto;
                            dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara, "2");
                            if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 2");
                            if (ppu_m2 != Convert.ToInt32(dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"]))
                            {
                                return new ResultadoTrama(false, null, "ET_002 El valor del producto "
                                    + dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"].ToString()
                                    + " es diferente al que llego en la venta\nppu venta: " + ppu_m2 + "\nppu parametrizado:" + dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"].ToString());
                            }
                            RealizoVentaTotal = true;
                            decimal difGalon = (galon_m2 - Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]));
                            int difDinero = (dinero_m2 - Convert.ToInt32(dtVentasTotales.Rows[0]["p2"]));
                            ventaGalones = difGalon.ToString();
                            ventaDinero = difDinero.ToString();
                            //validamos si es negativo
                            if (Convert.ToInt32(ventaDinero) > Convert.ToInt32(9999999))
                            {
                                VentaMayorA9999999 = true;
                            }
                            else {
                                using (ModeloDispensador modDIS = new ModeloDispensador())
                                {
                                    ImprimeTiquete = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "2", difDinero.ToString(), difGalon.ToString(), ppu_m2.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado, serialCredito, descuentoCredito);
                                }
                            }
                            
                        }
                        if (Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]) != galon_m3)
                        {
                            DataTable dtPosicionProductoCorrecto;
                            dtPosicionProductoCorrecto = modPOS.ObtenerPosicionesPorCarayManguera(cara, "3");
                            if (dtPosicionProductoCorrecto.Rows.Count == 0) return new ResultadoTrama(false, null, "No se encontro producto en la cara " + cara + " manguera 3");
                            if (ppu_m3 != Convert.ToInt32(dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"]))
                            {
                                return new ResultadoTrama(false, null, "ET_002 El valor del producto "
                                    + dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"].ToString()
                                    + " es diferente al que llego en la venta\nppu venta: " + ppu_m3 + "\nppu parametrizado:" + dtPosicionProductoCorrecto.Rows[0]["precioVentaProducto"].ToString());
                            }
                            RealizoVentaTotal = true;
                            decimal difGalon = (galon_m3 - Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]));
                            int difDinero = (dinero_m3 - Convert.ToInt32(dtVentasTotales.Rows[0]["p3"]));
                            ventaGalones = difGalon.ToString();
                            ventaDinero = difDinero.ToString();
                            //validamos si es negativo
                            if (Convert.ToInt32(ventaDinero) > Convert.ToInt32(9999999))
                            {
                                VentaMayorA9999999 = true;
                            }
                            else
                            {
                                using (ModeloDispensador modDIS = new ModeloDispensador())
                                {
                                    ImprimeTiquete = modDIS.GuardaVenta(dtPosicionProductoCorrecto.Rows[0]["idProducto"].ToString(), cara, "3", difDinero.ToString(), difGalon.ToString(), ppu_m3.ToString(), _FechaActual, usuarioIslero, idXbeeDispensador, serialFidelizado, serialCredito, descuentoCredito);
                                }
                            }
                            
                        }
                    }
                    if (RealizoVentaTotal == true && VentaMayorA9999999 == false)
                    {
                        using (ModeloDispensador modDIS = new ModeloDispensador())
                        {
                            //Por ultimo guardamos en ventas totales
                            var resultGuardarTotales = modDIS.GuardaVentasTotales(data, _FechaActual, idXbeeDispensador);
                        }
                    }
                    else
                    {
                        if (VentaMayorA9999999 == true)
                        {
                            return new ResultadoTrama(false, null, "ET_001 Venta mayor a 9999999");
                        }
                        else {
                            return new ResultadoTrama(false, null, "No se guardo venta por que no se detectaron diferencias en galones ni dinero");
                        }
                        
                    }
                }

                return new ResultadoTrama(true, null,"",_ventaGalones:ventaGalones,_ventaDinero:ventaDinero,_esCredito: esCredito,_imprimeTiquete:ImprimeTiquete);
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                    dtPrecios = modDIS.ObtenerProductosDispensador(idXbee);
                }

                int precio1 = 0;
                int precio2 = 0;
                int precio3 = 0;
                int precio4 = 0;

                precio1 = (dtPrecios.Rows.Count > 0) ? getPrecioProducto(dtPrecios.Rows[0][0].ToString()) : 0;
                precio2 = (dtPrecios.Rows.Count > 1) ? getPrecioProducto(dtPrecios.Rows[1][0].ToString()) : 0;
                precio3 = (dtPrecios.Rows.Count > 2) ? getPrecioProducto(dtPrecios.Rows[2][0].ToString()) : 0;
                precio4 = (dtPrecios.Rows.Count > 3) ? getPrecioProducto(dtPrecios.Rows[3][0].ToString()) : 0;
                string TramaDevolver = "MM:" + UtilidadesTramas.ConcatenarCerosIzquiera(precio1.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precio2.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precio3.ToString()) + ":" + UtilidadesTramas.ConcatenarCerosIzquiera(precio4.ToString()) + "";
                
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaDevolver));

                return TramasDevolver;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return null;
            }
        }

        public int getPrecioProducto(string idProducto)
        {
            using (ModeloDispensador modDIS = new ModeloDispensador())
            {
                var res = modDIS.ObtenerPreciosActualizadosProducto(idProducto);
                if (res.Rows.Count > 0)
                {
                    return Convert.ToInt32(res.Rows[0][0]);
                }
                else {
                    return 0;
                }
            }
        }

        public List<Byte[]> SolicitudTotales(string cara) {
            try
            {
                List<byte[]> TramasDevolver = new List<byte[]> { };

                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("ST" + cara));
                return TramasDevolver;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
