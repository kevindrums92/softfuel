using BusinessLayer;
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
    class TramasPOS : IDisposable
    {
        XbeeSingleton instancia = XbeeSingleton.Instance;
        #region "Actualizar Dispensador"
        public List<byte[]> TramaAutorizarVentaDispensador(string idXbee)
        {
            List<byte[]> TramasDevolver = new List<byte[]> { };
            string TramaCara1 = "W0";
            string TramaCara2 = "Q0";
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                DataTable dtAutorizados = modPOS.EstaTurnoAbiertoPorIdXbee(idXbee);
                foreach (DataRow _row in dtAutorizados.Rows)
                {
                    //Cara1
                    if (_row["LetraTrama"].ToString() == "W")
                    {
                        TramaCara1 = "W1";
                    }
                    //Cara2
                    if (_row["LetraTrama"].ToString() == "Q")
                    {
                        TramaCara2 = "Q1";
                    }
                }

                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara1));
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara2));
                return TramasDevolver;
            }
        }
        #endregion

        #region "Procesos Tramas"

        public ResultadoTrama Credito(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                string serial = data[2];
                string NomApeUsuario = "";

                if (instancia.ListaFidelizadosCreditosPendientes.Count > 0)
                {
                    FidelizadoCreditoPendiente objCredito = instancia.ListaFidelizadosCreditosPendientes.Find(item => item.serial == serial && item.tipoSolicitud == ETipoSolicitudSerial.Credito);
                    if (objCredito != null)
                    {
                        if (objCredito.cara != cara)
                        {
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "hay credito pendiente", "en la cara: " + objCredito.cara}), "Hay un credito pendiente en cara " + objCredito.cara);
                        }
                        else
                        {
                            instancia.ListaFidelizadosCreditosPendientes.Remove(objCredito);
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Credito Cancelado!!!"}), "Crédito cancelado");
                        }
                    }
                }


                DataTable dtCredito;
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    dtCredito = modPOS.ObtenerCreditoPorSerial(serial);
                }

                if (dtCredito.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se encontro credito", "con codigo: " + serial }), "No se encontro credito con serial " + serial);
                
                DataTable dtUsuario;
                int idXbee = 0;
                using (Generales modGenerales = new Generales())
                {
                    dtUsuario = modGenerales.ObtenerUsuario(dtCredito.Rows[0]["propietario"].ToString());
                    if (dtUsuario.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no existe o incorrecto" }), "Usuario no existe o incorrecto");
                    NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                    using (ModeloPOS modPOS = new ModeloPOS())
                    {
                        DataTable dtPosicion = modPOS.ObtenerPosicionesPorCara(cara);
                        idXbee = (int)dtPosicion.Rows[0]["idXbee"];
                        if (dtPosicion.Rows.Count == 0) return new ResultadoTrama(false, null, "No se pudo obtener la posición.");
                        var DatosTurno = modPOS.ObtenerTurnoPorPosicionyEstado(dtPosicion.Rows[0]["idPosicion"].ToString());
                        if (DatosTurno.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno en la cara " + cara }), "No hay turno en la cara " + cara);
                        if (modPOS.AumentoNumeroVentaCredito(Convert.ToInt32(dtCredito.Rows[0]["id"])) == false)
                        {
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "el usuario excedio el maximo","de tanqueos por dia"}), "El usuario excedió el máximo de tanqueos por día");
                        }
                        string cupo = "";
                        string saldo = "";
                        int descuento = 0;

                        if (object.Equals(dtCredito.Rows[0]["cupo"], DBNull.Value) == false && dtCredito.Rows[0]["cupo"].ToString().Trim() != "")
                        {
                            cupo = dtCredito.Rows[0]["cupo"].ToString();
                        }
                        else
                        {
                            cupo = "0";
                        }

                        if (object.Equals(dtCredito.Rows[0]["saldo"], DBNull.Value) == false && dtCredito.Rows[0]["saldo"].ToString().Trim() != "")
                        {
                            saldo = dtCredito.Rows[0]["saldo"].ToString();
                        }
                        else
                        {
                            saldo = "0";
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El usuario tiene saldo", "saldo en 0" }), "El usuario tiene saldo en 0");
                        }

                        if (object.Equals(dtCredito.Rows[0]["descuento"], DBNull.Value) == false && dtCredito.Rows[0]["descuento"].ToString().Trim() != "")
                        {
                            descuento = Convert.ToInt32(dtCredito.Rows[0]["descuento"]);
                        }
                        else
                        {
                            descuento = 0;
                        }

                        ResultadoTrama _resultado = new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Credito al usuario:", NomApeUsuario, "Cupo: $" + cupo, "Saldo: $" + saldo, "en cara " + cara }), "Crédito al usuario: " + NomApeUsuario + ", cupo: $" + cupo + ", salgo: $" + saldo + ", en la cara " + cara, idXbee, true, _descuentoCredito: descuento);

                        return _resultado;
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama Fidelizado(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                string serial = data[2];
                string NomApeUsuario = "";
                DataTable dtFidelizado;
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    dtFidelizado = modPOS.ObtenerFidelizadoPorSerial(serial);
                }

                if (dtFidelizado.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se encontro fidelizado", "con codigo: " + serial }), "No se encontro fidelizado con serial " + serial);
                if (object.Equals(dtFidelizado.Rows[0]["nomPlan"], DBNull.Value) == true)
                {
                    return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no tiene", "parametrizado puntos" }), "Usuario No Tiene Parametrizado Puntos");
                }


                DataTable dtUsuario;
                int idXbee = 0;
                using (Generales modGenerales = new Generales())
                {
                    dtUsuario = modGenerales.ObtenerUsuario(dtFidelizado.Rows[0]["propietario"].ToString());
                    if (dtUsuario.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no existe o incorrecto" }), "Usuario no existe o incorrecto");
                    NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                    using (ModeloPOS modPOS = new ModeloPOS())
                    {
                        DataTable dtPosicion = modPOS.ObtenerPosicionesPorCara(cara);
                        idXbee = (int)dtPosicion.Rows[0]["idXbee"];
                        if (dtPosicion.Rows.Count == 0) return new ResultadoTrama(false, null, "No se pudo obtener la posición.");
                        var DatosTurno = modPOS.ObtenerTurnoPorPosicionyEstado(dtPosicion.Rows[0]["idPosicion"].ToString());
                        if (DatosTurno.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno en la cara " + cara }), "No hay turno en la cara " + cara);

                        ResultadoTrama _resultado = new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Venta al usuario:", NomApeUsuario }), "Venta fidelizado al usuario: " + NomApeUsuario, idXbee, true);

                        return _resultado;
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        /// <summary>
        /// Resuelve la peticion de consignacion en efectivo, H:1075227951:1075227951
        /// </summary>
        /// <param name="data">recivo un array de strings con la informacion de la trama recibida
        /// donde 0 es el tipo de petición, 1 es la identificación del usuario, y 2 es el dinero que consigna</param>
        /// <returns></returns>
        public ResultadoTrama ConsignacionEfectivo(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string Identificacion = data[1];
                int Dinero = Convert.ToInt32(data[2]);
                string NomApeUsuario = "";
                bool encontroUsuario = false;
                DataTable dtUsuario = null;
                using (Generales modGenerales = new Generales())
                {
                    //Buscar el usuario y validar que sea islero.
                    dtUsuario = modGenerales.ObtenerUsuario(Identificacion);
                    if (dtUsuario != null && dtUsuario.Rows.Count > 0)
                    {
                        if (dtUsuario.Rows[0]["tipoPerfil"].ToString().Trim() == "3")
                        {
                            encontroUsuario = true;
                        }
                    }
                }
                if (encontroUsuario == true)
                {
                    using (ModeloPOS modPOS = new ModeloPOS())
                    {
                        if (modPOS.GuardaConsignacion(Identificacion,Dinero.ToString()) == true) //mando a guardar la consignación en la base de datos
                        {
                            NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                            //Devuelvo trama exitosa
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("CONSIGNACION",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, '-'));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(NomApeUsuario,
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("$" + Dinero.ToString(""),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, '-'));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                        }
                        else
                        {
                            return new ResultadoTrama(false, null,"No se pudo guardar la consignación en la base de datos");
                        }
                    }
                    
                }
                else
                {
                    //Devuelvo trama que el usuario no es islero
                    return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no existe o incorrecto"}), "Usuario no existe o incorrecto para consignación en efectivo");
                }
                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(mensajeTrama),"");
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null,e.Message);
            }
        }

        public ResultadoTrama AbrirTurno(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string Identificacion = data[1];
                string cara = data[2];
                string NomApeUsuario = "";
                bool encontroUsuario = false;
                DataTable dtUsuario;
                int idXbee = 0;
                using (Generales modGenerales = new Generales())
                {
                    //Buscar el usuario y validar que sea islero.
                    dtUsuario = modGenerales.ObtenerUsuario(Identificacion);
                    if (dtUsuario != null && dtUsuario.Rows.Count > 0)
                    {
                        if (dtUsuario.Rows[0]["tipoPerfil"].ToString().Trim() == "3")
                        {
                            encontroUsuario = true;
                        }
                    }
                    if (encontroUsuario == true)
                    {
                        NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                        using (ModeloPOS modPOS = new ModeloPOS())
                        {
                            DataTable dtPosicion = modPOS.ObtenerPosicionesPorCara(cara);
                            idXbee = (int)dtPosicion.Rows[0]["idXbee"];

                            if (dtPosicion != null && dtPosicion.Rows.Count > 0)
                            {
                                //Valido si ya hay turno abierto en la cara
                                var DatosTurno = modPOS.ObtenerTurnoPorPosicionyEstado(dtPosicion.Rows[0]["idPosicion"].ToString());
                                if (DatosTurno == null)
                                {
                                    return new ResultadoTrama(false, null, "Error Sql al momento de obbtener los datos del turno");
                                }
                                if (DatosTurno.Rows.Count == 0)
                                {
                                    DataTable dtVentas = modPOS.ObtenerTotalesVentaPorCara(cara);
                                    if (dtVentas != null && dtVentas.Rows.Count > 0)
                                    {
                                        var resultGuardar = modPOS.GuardarAperturaTurno(Identificacion, dtPosicion.Rows[0]["idPosicion"].ToString(), _FechaActual, (int)(dtVentas.Rows[0][0]));
                                        if (resultGuardar >0)
                                        {
                                            mensajeTrama = ArmarMensajeAperturaTurno(resultGuardar.ToString());
                                        }
                                        else
                                        {
                                            return new ResultadoTrama(false, null, "No se pudo guardar la apertura del turno");
                                        }
                                    }
                                    else
                                    {
                                        return new ResultadoTrama(false, null, "No se encontraron totales de ventas para la cara " + cara + ".");
                                    }
                                }
                                else
                                {
                                    return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Ya se abrio turno","en la cara: " + cara}), "Usuario no existe o incorrecto para consignación en efectivo");
                                }
                            }
                            else
                            {
                                return new ResultadoTrama(false, null, "No se pudo obtener la posición.");
                            }
                        }
                    }
                    else
                    {
                        //Devuelvo trama que el usuario no es islero
                        return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no existe o incorrecto" }), "Usuario no existe o incorrecto para apertura de turno");
                    }
                }
                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(mensajeTrama), "",idXbee);
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama VentaCanasta(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string idProducto = data[1];
                int cantidad = Convert.ToInt32(data[2]);
                string cara = data[3];
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    //Validamos que el producto exista, ademas que tenga existencias, y validar tambien que haya turno abierto
                    DataTable producto = modPOS.ObtenerProductoPorId(idProducto);
                    DataTable turno = modPOS.ObtenerTurnoPorCara(cara);
                    if (producto.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se encontro producto ",  idProducto.ToString() }), "Usuario no existe o incorrecto para consignación en efectivo");
                    if (Convert.ToInt32(producto.Rows[0]["existenciaProducto"]) < cantidad)
                    {
                        List<Byte[]> tramaAlerta = AsistenteMensajes.GenerarMensajeAlerta(new string[] {"La cantidad a vender","es mayor a la existente"});
                        return new ResultadoTrama(true, tramaAlerta, "El producto con id " + idProducto + " no cuenta con las cantidades solicitadas: " + cantidad);
                    }
                    if (turno.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno abierto ", "en cara " + cara }), "No hay turno abierto en cara " + cara + "para venta de canasta");

                    int valorVenta = Convert.ToInt32(producto.Rows[0]["precioventaProducto"]) * cantidad;
                    var result = modPOS.GuardaVentaCanasta(idProducto, cara, valorVenta.ToString(), _FechaActual, turno.Rows[0]["idUSuario"].ToString(), turno.Rows[0]["idXbee"].ToString(), cantidad);
                    if (result == true)
                    {
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("CANASTA VENTA",
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
                        mensajeTrama.Add("C SE VENDIO EL PRODUCTO:");
                        mensajeTrama.Add("C " + producto.Rows[0]["nomProducto"].ToString().Trim());
                        mensajeTrama.Add("C CANTIDAD: " + cantidad);
                        mensajeTrama.Add("C TOTAL: $" + valorVenta.ToString());
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                           Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, '-'));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                    }
                    else
                    {
                        return new ResultadoTrama(false, null, "No se pudo guardar la venta, porfavor contacte al administrador");
                    }
                }


                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(mensajeTrama), "");
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama CerrarTurno(string[] data)
        {
            Generales modGenerales = new Generales();
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string identificacion = data[1];
                string cara = data[2];
                int idXbee = 0;
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    DataTable dtPosicion = modPOS.ObtieneInformacionCara(cara);
                    if (dtPosicion.Rows.Count == 0) return new ResultadoTrama(false, null, "La cara " + cara + "no existe!");
                    DataTable dtUsuario = modGenerales.ObtenerUsuario(identificacion);
                    if (dtUsuario.Rows.Count == 0) return new ResultadoTrama(false, null, "El usuario con código " + identificacion + " no existe!");
                    DataTable dtTurno = modPOS.ObtenerTurnoPorCara(cara);
                    if (dtTurno.Rows.Count == 0) return new ResultadoTrama(false, null, "No hay turno en la cara " + cara);

                    if (dtTurno.Rows[0]["idUsuario"].ToString().Trim() != identificacion) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El usuario " + identificacion, "no tiene turno abierto"}), "El usuario " + identificacion + " no tiene turno abierto");

                    string idTurno = dtTurno.Rows[0][0].ToString();
                    string idPosicion = dtPosicion.Rows[0][0].ToString();
                    idXbee = Convert.ToInt32(dtPosicion.Rows[0]["idXbee"]);
                    var resCierre = modPOS.GuardaCerrarTurno(idTurno,idPosicion,cara,_FechaActual);
                    if (resCierre == false) return new ResultadoTrama(false, null, "No se pudo guardar el cierre de turno" + cara);
                    VentasPorTurno datosVenta = modPOS.ObtenerDatosVentaPorIdTurno(idTurno);
                    if (object.Equals(datosVenta,null)) return new ResultadoTrama(false, null, "No se pudo obtener información de las ventas del turno!");
                    return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVentasTurno(datosVenta)), "",idXbee);
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
            finally 
            {
                modGenerales.Dispose();
            }
        }

        /// <summary>
        /// Devuelve información de ventas de un turno por consecutivo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ResultadoTrama ConsecutivoCierre_AperturaTurno(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string tipoTurno = data[1]; //1-> apertura, 2->Cierre
                string idTurno = data[2];
                if (tipoTurno == "1")
                {
                    return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeAperturaTurno(idTurno)), "");
                }
                else
                {
                    using (ModeloPOS modPOS = new ModeloPOS())
                    {
                        VentasPorTurno datosVenta = modPOS.ObtenerDatosVentaPorIdTurno(idTurno);
                        if (object.Equals(datosVenta, null)) return new ResultadoTrama(false, null, "No se pudo obtener información de las ventas del turno!");
                        return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVentasTurno(datosVenta)), "");
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama UltimaVenta(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                string placa = data[2];
                string km = data[3];

                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    DataTable dtTurno = modPOS.ObtenerTurnoPorCara(cara);
                    if (dtTurno.Rows.Count == 0) return new ResultadoTrama(false, null, "No hay turno abierto en la cara:" + cara);
                    DataTable dtUltimaVenta = modPOS.ObtenerUltimaVentaPorCara(cara);
                    if (dtUltimaVenta.Rows.Count == 0) return new ResultadoTrama(false, null, "No se pudo obtener ultima venta en cara:" + cara);
                    if (modPOS.ActualizarPlacaKmUltimaVenta(placa, km, dtUltimaVenta.Rows[0]["idVenta"].ToString()) == false)
                    {
                        return new ResultadoTrama(false, null, "No se pudo actualizar los datos de última venta en la cara :" + cara);
                    }
                    return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVenta(true, dtUltimaVenta.Rows[0]["idVenta"].ToString())), "");
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama ConsecutivoUltimaVenta(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string idVenta = data[1];

                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVenta(false,idVenta)), "");
                
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        #endregion 

        #region "Armar Impresiones"
        /// <summary>
        /// Arma el mensaje de información de la venta
        /// </summary>
        /// <param name="inforVenta"></param>
        /// <returns></returns>
        public List<string> ArmarMensajeVentasTurno(VentasPorTurno infoVenta)
        {
            List<string> mensajeTrama = new List<string>();
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("CIERRE DE TURNO",
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CCara: " + infoVenta.Cara);
            mensajeTrama.Add("CIslero: " + infoVenta.Usuario);
            mensajeTrama.Add("CNum de Turno: " + infoVenta.NumTurno);
            mensajeTrama.Add("CFecha: " + infoVenta.Fecha.ToString("yyyy-MM-dd") + " " + infoVenta.Fecha.ToString("H:mm:ss"));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL MANGUERAS",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            
            mensajeTrama.Add("CMANGUERA1");
            mensajeTrama.Add("C$: " + infoVenta.TotalDineroMang1.ToString() + " | G: " + infoVenta.TotalGalonesMang1.ToString());

            mensajeTrama.Add("CMANGUERA2");
            mensajeTrama.Add("C$: " + infoVenta.TotalDineroMang2.ToString() + " | G: " + infoVenta.TotalGalonesMang2.ToString());


            if (infoVenta.TotalDineroMang3 > 0)
            {
                mensajeTrama.Add("CMANGUERA3");
                mensajeTrama.Add("C$: " + infoVenta.TotalDineroMang3.ToString() + " | G: " + infoVenta.TotalGalonesMang3.ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL CARA",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("C$: " + infoVenta.TotalCaraDin.ToString() + " | G: " + infoVenta.TotalCaraGal.ToString());

            if (infoVenta.TotalCredTran != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL CREDITO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
                mensajeTrama.Add("CTransacciones: " + infoVenta.TotalCredTran.ToString());
                mensajeTrama.Add("C$: " + infoVenta.TotalCredDin.ToString() + " | G: " + infoVenta.TotalCredGal.ToString());
            }

            if (infoVenta.TotalProdTran != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL PRODUCTO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
                mensajeTrama.Add("CTransacciones: " + infoVenta.TotalProdTran.ToString());
                mensajeTrama.Add("C$ :" + infoVenta.TotalProdDin.ToString() + " | G: " + infoVenta.TotalProdCant.ToString());
            }
            
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL EFECTIVO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("C$: " + infoVenta.TotalEfectivo.ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL ELECTRONICOS INICIALES",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CMANGUERA1");
            mensajeTrama.Add("C$: " + infoVenta.IniDineroMang1.ToString() + " | G: " + infoVenta.IniGalMang1.ToString());

            mensajeTrama.Add("CMANGUERA2");
            mensajeTrama.Add("C$: " + infoVenta.IniDineroMang2.ToString() + " | G:" + infoVenta.IniGalMang2.ToString());

            if (infoVenta.IniDineroMang3 > 0)
            {
                mensajeTrama.Add("CMANGUERA3");
                mensajeTrama.Add("C$: " + infoVenta.IniDineroMang3.ToString() + " | G: " + infoVenta.IniGalMang3.ToString());
            }

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL ELECTRONICOS FINALES",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CMANGUERA1");
            mensajeTrama.Add("C$: " + infoVenta.FinDineroMang1.ToString() + " | G: " + infoVenta.FinGalMang1.ToString());

            mensajeTrama.Add("CMANGUERA2");
            mensajeTrama.Add("C$: " + infoVenta.FinDineroMang2.ToString() + " | G: " + infoVenta.FinGalMang2.ToString());
            if (infoVenta.FinDineroMang3 > 0)
            {
                mensajeTrama.Add("CMANGUERA3");
                mensajeTrama.Add("C$: " + infoVenta.FinDineroMang3.ToString() + " | G: " + infoVenta.FinGalMang3.ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' '));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' '));
            return mensajeTrama;
        }

        public List<string> ArmarMensajeAperturaTurno(string idConsecutivo)
        {
            DataTable dtDatosApertura;
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                dtDatosApertura = modPOS.ObtenerAperturaTurnoPorId(idConsecutivo);
            }
            List<string> mensajeTrama = new List<string>();
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("ABRIR TURNO",
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));

            mensajeTrama.Add("C Consecutivo: " + idConsecutivo);

            mensajeTrama.Add("C " + dtDatosApertura.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtDatosApertura.Rows[0]["apeUsuario"].ToString().Trim());

            mensajeTrama.Add("C CARA: " + dtDatosApertura.Rows[0]["cara"].ToString().Trim());

            mensajeTrama.Add("C " + dtDatosApertura.Rows[0]["abrirTurno"].ToString());

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("INICIO DE TURNO",
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));

            //MANGERA 1
            if ((int)dtDatosApertura.Rows[0]["p1"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p1"];
                mensajeTrama.Add("C MANGERA 1");
                mensajeTrama.Add("C $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g1"].ToString());
            }
            //MANGERA 2
            if ((int)dtDatosApertura.Rows[0]["p2"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p2"];
                mensajeTrama.Add("C MANGERA 2");
                mensajeTrama.Add("C $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g2"].ToString());
            }
            //MANGERA 3
            if ((int)dtDatosApertura.Rows[0]["p3"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p3"];
                mensajeTrama.Add("C MANGERA 3");
                mensajeTrama.Add("C $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g3"].ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' '));

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' '));

            return mensajeTrama;
        }

        public List<string> ArmarMensajeVenta(bool esOriginal, string idVenta)
        {
            DataTable dtInfo;
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                dtInfo = modPOS.ObtenerUltimaVentaPorId(idVenta);
            }
            if (dtInfo.Rows.Count == 0) throw new Exception("No se pudo obtener información de la venta con consecutivo " + idVenta);
            List<string> mensajeTrama = new List<string>();
            string textoEncabezado;
            if (esOriginal == true)
            {
                textoEncabezado = "ORIGINAL";
            }
            else
            {
                textoEncabezado = "COPIA";
            }
            string fecha = Convert.ToDateTime(dtInfo.Rows[0]["fecha"]).ToString("yyyy-MM-dd");
            string hora = Convert.ToDateTime(dtInfo.Rows[0]["fecha"]).ToString(" H:mm:ss");
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(textoEncabezado,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CFecha: " + fecha + " " + hora);
            mensajeTrama.Add("CTiquete: " + idVenta.ToString() + " Placa: " + dtInfo.Rows[0]["placa"].ToString());
            mensajeTrama.Add("CKilometraje: " + dtInfo.Rows[0]["kilometraje"].ToString());
            mensajeTrama.Add("CCara: " + dtInfo.Rows[0]["cara"].ToString() + " Mang: " + dtInfo.Rows[0]["manguera"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CProd: " + dtInfo.Rows[0]["nomProducto"].ToString());
            mensajeTrama.Add("CCant: " + dtInfo.Rows[0]["galones"].ToString() + " PPG: $" + dtInfo.Rows[0]["ppu"].ToString() + "");
            mensajeTrama.Add("CTotal: $" + dtInfo.Rows[0]["precio"].ToString());

            if (object.Equals(dtInfo.Rows[0]["tipoCuenta"],DBNull.Value) == false && dtInfo.Rows[0]["tipoCuenta"].ToString() == "1")
            {
                mensajeTrama.Add("CForma de Pago: Credito");
                string descuento = "0";
                if (object.Equals(dtInfo.Rows[0]["descuento"], DBNull.Value) == false && dtInfo.Rows[0]["descuento"].ToString().Trim() != "")
                {
                    descuento = dtInfo.Rows[0]["descuento"].ToString();
                }
                decimal DineroDescontar = Convert.ToDecimal(dtInfo.Rows[0]["precio"]) - (Convert.ToDecimal(dtInfo.Rows[0]["precio"]) * Convert.ToDecimal(descuento) / 100);
                mensajeTrama.Add("CDescuento: %" + descuento + "");
                mensajeTrama.Add("CTotal acreditado: $" + DineroDescontar + "");
            }
            else
            {
                mensajeTrama.Add("CForma de Pago: Contado");
            }
            mensajeTrama.Add("CCliente: " + dtInfo.Rows[0]["cliente"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                                           Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            mensajeTrama.Add("CAtendido: " + dtInfo.Rows[0]["nomUsuario"].ToString() + " " + dtInfo.Rows[0]["apeUsuario"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("-",
                                                                       Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, '-'));
            string puntosVenta = "0";
            string puntosTotal = "0";
            if (object.Equals(dtInfo.Rows[0]["puntosEnVenta"], DBNull.Value) == false && dtInfo.Rows[0]["puntosEnVenta"].ToString().Trim() != "")
            {
                puntosVenta = dtInfo.Rows[0]["puntosEnVenta"].ToString();
            }
            if (object.Equals(dtInfo.Rows[0]["puntosTotal"], DBNull.Value) == false && dtInfo.Rows[0]["puntosTotal"].ToString().Trim() != "")
            {
                puntosTotal = dtInfo.Rows[0]["puntosTotal"].ToString();
            }
            mensajeTrama.Add("CPts C: " + puntosVenta + " Pts T: " + puntosTotal + "");
            
            return mensajeTrama;
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
