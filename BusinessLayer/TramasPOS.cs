﻿using BusinessLayer;
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

        const char _CARACTERDIVISOR = '*';
        const string _CARACTERINICIALIMPRESION = "{";

        #region "Actualizar Dispensador"
        /// <summary>
        /// esta funcion se usa para definir si los turnos estan abiertos en las caras,
        /// en caso de estar abierto el turno en la cara 1, se envía W1, de lo contrario W0, lo mismo para la car 2
        /// con la letra Q, 
        /// </summary>
        /// <param name="idXbee"></param>
        /// <returns></returns>
        public List<byte[]> TramaAutorizarVentaDispensador(string idXbee)
        {
            List<byte[]> TramasDevolver = new List<byte[]> { };
            string TramaCara1 = "_CARA1_0";
            string TramaCara2 = "_CARA2_0";
            string TramaCara3 = "_CARA3_0";
            string TramaCara4 = "_CARA4_0";
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                DataTable dtAutorizados = modPOS.EstaTurnoAbiertoPorIdXbee(idXbee);
                foreach (DataRow _row in dtAutorizados.Rows)
                {
                    //Cara1
                    if (_row["LetraTrama"].ToString() == "CARA1")
                    {
                        TramaCara1 = "_CARA1_1";
                    }
                    //Cara2
                    if (_row["LetraTrama"].ToString() == "CARA2")
                    {
                        TramaCara2 = "_CARA2_1";
                    }

                    //Cara3
                    if (_row["LetraTrama"].ToString() == "CARA3")
                    {
                        TramaCara3 = "_CARA3_1";
                    }
                    //Cara4
                    if (_row["LetraTrama"].ToString() == "CARA4")
                    {
                        TramaCara4 = "_CARA4_1";
                    }
                }

                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara1));
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara2));
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara3));
                TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString(TramaCara4));
                return TramasDevolver;
            }
        }

        public List<byte[]> EnviarMododeTrabajo(string idXbee)
        {
            List<byte[]> TramasDevolver = new List<byte[]> { };
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                DataTable dtXbee = modPOS.InformacionXbee(idXbee);
                if (dtXbee.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtXbee.Rows[0]["requiereAutVenta"]) == true)
                    {
                        TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("RA1"));
                    }
                    else {
                        TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("RA0"));
                    }
                }
                else {
                    TramasDevolver.Add(UtilidadesTramas.ObtenerByteDeString("RA0"));
                }
                
                return TramasDevolver;
            }
        }
        #endregion

        #region "Procesos Tramas"

        public ResultadoTrama BloqueoCara(string[] data, int idXbee)
        {
            try
            {
                var cara = data[2];
                var usuario = data[1];
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string dato = data[1];

                using (var modPOS = new ModeloPOS())
                {
                    DataTable dtAutorizados = modPOS.EstaTurnoAbiertoPorIdXbee(idXbee.ToString());
                    if (dtAutorizados.Select("numPosicion = " + cara).Length > 0)
                    {
                        var dtPosicion = dtAutorizados.Select("numPosicion = " + cara);
                        //Validar si el usuario es el del turno
                        var turnoUsuario = modPOS.ObtenerTurnoUsuarioXPosicion(usuario, dtPosicion[0]["idPosicion"].ToString());
                        if(turnoUsuario.Rows.Count == 0)
                        {
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Este Usuario" + usuario,"No tiene turno abierto" }), "Este usuario " + usuario + " No tiene turno abierto");
                        }
                        modPOS.BloqueoCara(dtPosicion[0]["idPosicion"].ToString());
                        return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Se bloqueo la cara " + cara }), "Se bloqueo la cara " + cara);
                    }
                    else {
                        return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno abierto", "en la cara " + cara }), "No hay turno abierto en cara " + cara);
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama DesbloqueoCara(string[] data, int idXbee)
        {
            try
            {
                var cara = data[2];
                var usuario = data[1];
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string dato = data[1];

                using (var modPOS = new ModeloPOS())
                {
                    DataTable dtAutorizados = modPOS.EstaTurnoAbiertoPorIdXbee(idXbee.ToString());
                    if (dtAutorizados.Select("numPosicion = " + cara).Length > 0)
                    {
                        var dtPosicion = dtAutorizados.Select("numPosicion = " + cara);
                        //Validar si el usuario es el del turno
                        var turnoUsuario = modPOS.ObtenerTurnoUsuarioXPosicion(usuario, dtPosicion[0]["idPosicion"].ToString());
                        if (turnoUsuario.Rows.Count == 0)
                        {
                            return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Este Usuario" + usuario, "No tiene turno abierto" }), "Este usuario " + usuario + " No tiene turno abierto");
                        }
                        modPOS.DesbloqueoCara(dtPosicion[0]["idPosicion"].ToString());
                        return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Se desbloqueo la cara " + cara }), "Se desbloqueó la cara " + cara);
                    }
                    else {
                        return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno abierto", "en la cara " + cara }), "No hay turno abierto en cara " + cara);
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }


        public ResultadoTrama GuardarTramaCTD(string[] data, int idXbee)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string dato = data[1];

                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    modPOS.GuardaTramaCTD(dato, idXbee);
                }

                return new ResultadoTrama(true, null, "Trama CTD recibida");
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama PrepararTiquete(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                string placa = data[2];
                string km = data[3];
                int idXbeeImprimir = Convert.ToInt32(data[4]);

                if (instancia.ListaTiquetesPorImprimir == null) instancia.ListaTiquetesPorImprimir = new List<TiquetesPorImprimir>();
                var _TiquetePorImprimir = new TiquetesPorImprimir() {
                    cara = cara,
                    placa = placa,
                    km = km,
                    idXbeeImprimir = idXbeeImprimir
                };
                instancia.ListaTiquetesPorImprimir.Add(_TiquetePorImprimir);
                

                return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "preparando tiquete en cara " + cara }),"preparando tiquete en cara " + cara);
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }
        
        /// <summary>
        /// Trama de fidelizado; ej= F:{cara}:{serial}
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        /// <summary>
        /// Función para cancelar crédito
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ResultadoTrama CancelarCredito(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string cara = data[1];

                var item = instancia.ListaCreditosPendientes.Find(x => x.cara == cara);
                if (item != null)
                {
                    instancia.ListaCreditosPendientes.Remove(item);
                    return new ResultadoTrama(true,
                                    AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Credito cancelado en cara", cara }),
                                    "Credito cancelado en la cara " + cara);
                }
                else {
                    return new ResultadoTrama(true,
                                    AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay credito", "pendiente en cara", cara }),
                                    "No hay credito pendiente en la cara " + cara);
                }

            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        /// <summary>
        /// Función para preparar crédito
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ResultadoTrama PrepararCredito(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string cara = data[1];
                string serial = data[2];
                string valor = data[3];

                //se debe anular el credito en la cara cuando el valor sea cero
                if (valor == "0" || Convert.ToInt32(valor) == 0)
                {
                    var item = instancia.ListaCreditosPendientes.Find(x => x.cara == cara);
                    if(item != null)
                    {
                        instancia.ListaCreditosPendientes.Remove(item);
                        return new ResultadoTrama(true,
                                        AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Credito cancelado en cara", cara }),
                                        "Credito cancelado en la cara " + cara);
                    }
                }

                string NomApeUsuario = "";
                DataTable dtCredito;
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    dtCredito = modPOS.ObtenerCreditoPorSerial(serial);
                }

                if (dtCredito.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se encontro credito", "con serial: " + serial }), "No se encontro credito con serial " + serial);
               
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


                        //validaciones de credito
                        var idVehiculo = dtCredito.Rows[0]["idVehiculo"].ToString();
                        if (dtCredito.Rows[0]["idVP"] != DBNull.Value && Convert.ToBoolean(dtCredito.Rows[0]["exigeRestricciones"]) == true)
                        {
                            //validación de ventas por semana
                            if (dtCredito.Rows[0]["semanal"] != DBNull.Value && dtCredito.Rows[0]["semanal"].ToString() != "0")
                            {
                                var fechaHoy = DateTime.Now;
                                while (fechaHoy.DayOfWeek != DayOfWeek.Monday)
                                {
                                    fechaHoy = fechaHoy.AddDays(-1);
                                }

                                var fechaInicial = fechaHoy.ToString("yyyy-MM-dd"); 
                                var fechaFinal = fechaHoy.AddDays(7).ToString("yyyy-MM-dd");

                                if (modPOS.ObtenerVentasVechiculoXRangoFechas(fechaInicial, fechaFinal, idVehiculo) >=
                                    Convert.ToInt32(dtCredito.Rows[0]["semanal"].ToString()))
                                {
                                    return new ResultadoTrama(true,
                                        AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El vehiculo ya ocupo", "el máximo de tanqueos", "permitidos por semana" }),
                                        "El vehículo ya ocupó el máximo de tanqueos por semana");
                                }
                            }

                            //validación de ventas por día
                            if (dtCredito.Rows[0]["diario"] != DBNull.Value && dtCredito.Rows[0]["diario"].ToString() != "0")
                            {
                                var fechaHoy = DateTime.Now;
                               

                                var fechaInicial = fechaHoy.ToString("yyyy-MM-dd"); ;
                                var fechaFinal = fechaHoy.AddDays(1).ToString("yyyy-MM-dd");

                                if (modPOS.ObtenerVentasVechiculoXRangoFechas(fechaInicial, fechaFinal, idVehiculo) >=
                                    Convert.ToInt32(dtCredito.Rows[0]["diario"].ToString()))
                                {
                                    return new ResultadoTrama(true,
                                        AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El vehiculo ya ocupo", "el máximo de tanqueos", "permitidos por dia" }),
                                        "El vehículo ya ocupó el máximo de tanqueos por día");
                                }
                            }

                            var limTanqueo = dtCredito.Rows[0]["limiteTanqueo"];
                            
                            //Limite de tanqueo por dinero
                            if (limTanqueo != DBNull.Value && limTanqueo.ToString() == "2")
                            {
                                if(dtCredito.Rows[0]["limitePrecio"] != DBNull.Value)
                                {
                                    var limPrecio = Convert.ToDecimal(dtCredito.Rows[0]["limitePrecio"]);
                                    if(Convert.ToDecimal(valor)> limPrecio)
                                    {
                                        return new ResultadoTrama(true,
                                        AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El valor del tanqueo", "es superior al permitido", "valor max: $" + limPrecio.ToString() }),
                                        "El valor a tanquear es superior al permitido para este vechiculo, valor máximo: $" + limPrecio.ToString());
                                    }
                                }
                            }

                            //validación por saldo
                            if (dtCredito.Rows[0]["saldo"] != DBNull.Value &&
                                Convert.ToDecimal(valor) > Convert.ToDecimal(dtCredito.Rows[0]["saldo"]))
                            {
                                return new ResultadoTrama(true,
                                        AsistenteMensajes.GenerarMensajeAlerta(new string[] { "El valor del tanqueo", "es superior saldo", "valor saldo: $" + dtCredito.Rows[0]["saldo"].ToString() }),
                                        "El valor a tanquear es superior al saldo, valor saldo: $" + dtCredito.Rows[0]["saldo"].ToString());
                            }
                        }

                        var descuento = 0;
                        if (dtCredito.Rows[0]["descuentoUsuario"] != DBNull.Value)
                        {
                            descuento = Convert.ToInt32(dtCredito.Rows[0]["descuentoUsuario"]);
                        }

                        if (dtCredito.Rows[0]["descuentoVehiculo"] != DBNull.Value)
                        {
                            descuento = Convert.ToInt32(dtCredito.Rows[0]["descuentoVehiculo"]);
                        }

                        instancia.ListaCreditosPendientes.Add(new CreditoPendiente() {
                            cara = cara,
                            datos = dtCredito,
                            descuento = descuento,
                            serial = serial,
                            valor = valor,
                            exigeRestriccion = Convert.ToBoolean(dtCredito.Rows[0]["exigeRestricciones"])
                        });
                        ResultadoTrama _resultado = new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Venta autorizada ","en la cara:" + cara }), "Venta autorizada en la cara: " + cara, idXbee, true);

                        return _resultado;
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                        var consecutivo = modPOS.GuardaConsignacion(Identificacion, Dinero.ToString());
                        if (consecutivo > 0) //mando a guardar la consignación en la base de datos
                        {
                            NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                            //Devuelvo trama exitosa
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("CONSIGNACION #" + consecutivo.ToString(),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(NomApeUsuario,
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("$" + Dinero.ToString(""),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                        }
                        else
                        {
                            return new ResultadoTrama(false, null, "No se pudo guardar la consignación en la base de datos");
                        }
                    }

                }
                else
                {
                    //Devuelvo trama que el usuario no es islero
                    return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "Usuario no existe o incorrecto" }), "Usuario no existe o incorrecto para consignación en efectivo");
                }
                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(mensajeTrama), "");
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        /// <summary>
        /// Resuelve la peticion de consecutivo consignacion en efectivo, HC:100
        /// </summary>
        /// <param name="data">recivo un array de strings con la informacion de la trama recibida
        /// donde 0 es el tipo de petición, 1 es la identificación del usuario, y 2 es el dinero que consigna</param>
        /// <returns></returns>
        public ResultadoTrama ConsecutivoConsignacionEfectivo(string[] data)
        {
            try
            {
                List<string> mensajeTrama = new List<string>();
                int consecutivo = Convert.ToInt32(data[1]);

                DataTable dtConsignacion;
                using (var modPos = new ModeloPOS())
                {
                    dtConsignacion = modPos.GetDatosConsignacion(consecutivo);
                }
                if (dtConsignacion.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay consignacion con ", "Consecutivo " + consecutivo }), "No hay consignación con consecutivo " + consecutivo);

                double Dinero = Convert.ToDouble(dtConsignacion.Rows[0]["valorConsig"].ToString());
                string Identificacion = dtConsignacion.Rows[0]["idUsuario"].ToString();
                string NomApeUsuario = "";
                DataTable dtUsuario = null;
                using (Generales modGenerales = new Generales())
                {
                    //Buscar el usuario y validar que sea islero.
                    dtUsuario = modGenerales.ObtenerUsuario(Identificacion);
                    if (dtUsuario != null && dtUsuario.Rows.Count > 0)
                    {
                        NomApeUsuario = dtUsuario.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtUsuario.Rows[0]["apeUsuario"].ToString().Trim();
                        //Devuelvo trama exitosa
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("CONSIGNACION #" + consecutivo.ToString(),
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(NomApeUsuario,
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("$" + Dinero.ToString(""),
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.izquierda, ' '));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                        mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                            Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' '));
                    }
                }
                return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(mensajeTrama), "");
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
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
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                        serialCredito = objCredito.serial;
                        descuentoCredito = objCredito.descuento;
                        instancia.ListaFidelizadosCreditosPendientes.Remove(objCredito);
                    }
                }
                using (ModeloPOS modPOS = new ModeloPOS())
                {
                    //Validamos que el producto exista, ademas que tenga existencias, y validar tambien que haya turno abierto
                    DataTable producto = modPOS.ObtenerProductoPorId(idProducto);
                    DataTable turno = modPOS.ObtenerTurnoPorCara(cara);
                    if (producto.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se encontro producto ",  idProducto.ToString() }), "Usuario no existe o incorrecto para consignación en efectivo");
                    if (producto.Rows[0]["idTipoProducto"].ToString().Trim() == "1") return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se puede vender","productos tipo combustible"}), "No se puede vender productos tipo Combustible");

                    if (Convert.ToInt32(producto.Rows[0]["existenciaProducto"]) < cantidad)
                    {
                        List<Byte[]> tramaAlerta = AsistenteMensajes.GenerarMensajeAlerta(new string[] {"La cantidad a vender","es mayor a la existente"});
                        return new ResultadoTrama(true, tramaAlerta, "El producto con id " + idProducto + " no cuenta con las cantidades solicitadas: " + cantidad);
                    }
                    if (turno.Rows.Count == 0) return new ResultadoTrama(true, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno abierto ", "en cara " + cara }), "No hay turno abierto en cara " + cara + "para venta de canasta");

                    int valorVenta = Convert.ToInt32(producto.Rows[0]["precioventaProducto"]) * cantidad;
                    var result = modPOS.GuardaVentaCanasta(idProducto, cara, valorVenta.ToString(), _FechaActual, turno.Rows[0]["idUSuario"].ToString(), turno.Rows[0]["idXbee"].ToString(), cantidad, serialFidelizado, serialCredito, descuentoCredito);
                    if (result > 0)
                    {
                        return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVenta(true, result.ToString())), "");
                    }
                    else
                    {
                        return new ResultadoTrama(false, null, "No se pudo guardar la venta, porfavor contacte al administrador");
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                        if (object.Equals(datosVenta, null)) return new ResultadoTrama(false, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se obtuvo informacion", "de las ventas del turno " + idTurno }), "No se pudo obtener información de las ventas del turno!");
                        return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVentasTurno(datosVenta)), "");
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
                return new ResultadoTrama(false, null, e.Message);
            }
        }

        public ResultadoTrama UltimaVenta(string[] data, bool restriccionDeTiempo)
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
                    if (dtTurno.Rows.Count == 0) return new ResultadoTrama(false, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No hay turno abierto ", "en la cara: " + cara}), "No hay turno abierto en la cara:" + cara);
                    DataTable dtUltimaVenta = modPOS.ObtenerUltimaVentaPorCara(cara);
                    if (restriccionDeTiempo)
                    {
                        var tiempoSegundos = modPOS.TiempoSegundosParaImprimirUltimaVenta();
                        if(tiempoSegundos > 0)
                        {
                            var fechaAct = DateTime.Now;
                            var fechaVenta = Convert.ToDateTime(dtUltimaVenta.Rows[0]["fecha"]);
                            double segundosDiff = (fechaAct - fechaVenta).TotalSeconds;
                            if(segundosDiff > tiempoSegundos)
                            {
                                return new ResultadoTrama(false, AsistenteMensajes.GenerarMensajeAlerta(
                                    new string[] { "Se supero el tiempo limite", "de impresion de venta"}
                                    ), "Se supero el tiempo limite de impresion de venta");
                            }
                        }
                    }


                    if (dtUltimaVenta.Rows.Count == 0) return new ResultadoTrama(false, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se obtuvo ultima ", "venta en la cara: " + cara }), "No se pudo obtener ultima venta en cara:" + cara);
                    if (modPOS.ActualizarPlacaKmUltimaVenta(placa, km, dtUltimaVenta.Rows[0]["idVenta"].ToString()) == false)
                    {
                        return new ResultadoTrama(false, AsistenteMensajes.GenerarMensajeAlerta(new string[] { "No se pudo actualizar", "datos de ultima venta ","en la cara " + cara}), "No se pudo actualizar los datos de última venta en la cara :" + cara);
                    }
                    return new ResultadoTrama(true, UtilidadesTramas.ConvertirListadoStringaByte(ArmarMensajeVenta(true, dtUltimaVenta.Rows[0]["idVenta"].ToString())), "");
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                LocalLogManager.EscribeLog(e.Message + "\n\n" + e.StackTrace, LocalLogManager.TipoImagen.TipoError);
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
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Cara: " + infoVenta.Cara);
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Islero: " + infoVenta.Usuario);
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Num de Turno: " + infoVenta.NumTurno);
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Fecha: " + infoVenta.Fecha.ToString("yyyy-MM-dd") + " " + infoVenta.Fecha.ToString("H:mm:ss"));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL MANGUERAS",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA1");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalDineroMang1.ToString() + " | G: " + infoVenta.TotalGalonesMang1.ToString());

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA2");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalDineroMang2.ToString() + " | G: " + infoVenta.TotalGalonesMang2.ToString());


            if (infoVenta.TotalDineroMang3 > 0)
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA3");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalDineroMang3.ToString() + " | G: " + infoVenta.TotalGalonesMang3.ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL CARA",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalCaraDin.ToString() + " | G: " + infoVenta.TotalCaraGal.ToString());

            if (infoVenta.TotalCredTran != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL CREDITO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Transacciones: " + infoVenta.TotalCredTran.ToString());
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalCredDin.ToString() + " | G: " + infoVenta.TotalCredGal.ToString());
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalCredTran);
            }

            if (infoVenta.TotalPrepago != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL PREPAGO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Transacciones: " + infoVenta.TotalCredTran.ToString());
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalCredDin.ToString() + " | G: " + infoVenta.TotalCredGal.ToString());
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalPrepago);
            }

            if (infoVenta.TotalTarjetaCredito != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL DATAFONO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Transacciones: " + infoVenta.TotalCredTran.ToString());
                //mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalCredDin.ToString() + " | G: " + infoVenta.TotalCredGal.ToString());
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.TotalTarjetaCredito);
            }

            if (infoVenta.TotalProdTran != "0")
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL PRODUCTO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Transacciones: " + infoVenta.TotalProdTran.ToString());
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$ :" + infoVenta.TotalProdDin.ToString() + " | G: " + infoVenta.TotalProdCant.ToString());
            }

            if (infoVenta.TotalReversado != Convert.ToDouble(0)) 
            {
                mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL REVERSADO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Reversado: $" + infoVenta.TotalReversado.ToString());
            }

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL EFECTIVO",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            var totalEfectivo = Convert.ToDecimal(infoVenta.TotalEfectivo.ToString());
            var totalCredito = Convert.ToDecimal(infoVenta.TotalCredTran);
            var totalPrepago = Convert.ToDecimal(infoVenta.TotalPrepago);
            var totalTarjCredito = Convert.ToDecimal(infoVenta.TotalTarjetaCredito);
            var totalVendidoEfectivo = totalEfectivo - totalCredito - totalPrepago - totalTarjCredito;
            
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + totalVendidoEfectivo);
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL ELECTRONICOS INICIALES",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            if (infoVenta.IniDineroMang1 > 0) {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA1");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.IniDineroMang1.ToString() + " | G: " + infoVenta.IniGalMang1.ToString());
            }

            if (infoVenta.IniDineroMang2 > 0)
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA2");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.IniDineroMang2.ToString() + " | G:" + infoVenta.IniGalMang2.ToString());
            }
               

            if (infoVenta.IniDineroMang3 > 0)
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA3");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.IniDineroMang3.ToString() + " | G: " + infoVenta.IniGalMang3.ToString());
            }

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("TOTAL ELECTRONICOS FINALES",
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA1");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.FinDineroMang1.ToString() + " | G: " + infoVenta.FinGalMang1.ToString());

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA2");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.FinDineroMang2.ToString() + " | G: " + infoVenta.FinGalMang2.ToString());
            if (infoVenta.FinDineroMang3 > 0)
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "MANGUERA3");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$: " + infoVenta.FinDineroMang3.ToString() + " | G: " + infoVenta.FinGalMang3.ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
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
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + " Consecutivo: " + idConsecutivo);

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + " " + dtDatosApertura.Rows[0]["nomUsuario"].ToString().Trim() + " " + dtDatosApertura.Rows[0]["apeUsuario"].ToString().Trim());

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + " CARA: " + dtDatosApertura.Rows[0]["Cara"].ToString().Trim());

            mensajeTrama.Add(_CARACTERINICIALIMPRESION + " " + dtDatosApertura.Rows[0]["abrirTurno"].ToString());

            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama("INICIO DE TURNO",
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));

            //MANGERA 1
            if (dtDatosApertura.Rows[0]["p1"] != DBNull.Value && (int)dtDatosApertura.Rows[0]["p1"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p1"];
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " MANGERA 1");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g1"].ToString());
            }
            //MANGERA 2
            if (dtDatosApertura.Rows[0]["p2"] != DBNull.Value && (int)dtDatosApertura.Rows[0]["p2"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p2"];
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " MANGERA 2");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g2"].ToString());
            }
            //MANGERA 3
            if (dtDatosApertura.Rows[0]["p3"] != DBNull.Value && (int)dtDatosApertura.Rows[0]["p3"] != 0)
            {
                int dinero = (int)dtDatosApertura.Rows[0]["p3"];
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " MANGERA 3");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + " $: " + dinero.ToString()
                    + " | G: " + dtDatosApertura.Rows[0]["g3"].ToString());
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));

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
            if (dtInfo.Rows.Count == 0)
            {
                throw new Exception("No se pudo obtener información de la venta con consecutivo " + idVenta);
            }
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
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Fecha: " + fecha + " " + hora);
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Tiquete: " + idVenta.ToString() + " Placa: " + dtInfo.Rows[0]["placa"].ToString());
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Kilometraje: " + dtInfo.Rows[0]["kilometraje"].ToString());
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Cara: " + dtInfo.Rows[0]["cara"].ToString() + " Mang: " + dtInfo.Rows[0]["manguera"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                               Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Prod: " + dtInfo.Rows[0]["nomProducto"].ToString());
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Cant: " + dtInfo.Rows[0]["galones"].ToString() + " PPG: $" + dtInfo.Rows[0]["ppu"].ToString() + "");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Total: $" + dtInfo.Rows[0]["precio"].ToString());

            if (object.Equals(dtInfo.Rows[0]["tipoCuenta"],DBNull.Value) == false && dtInfo.Rows[0]["tipoCuenta"].ToString() == "1")
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Forma de Pago: Credito");
                string descuento = "0";
                if (object.Equals(dtInfo.Rows[0]["descuento"], DBNull.Value) == false && dtInfo.Rows[0]["descuento"].ToString().Trim() != "")
                {
                    descuento = dtInfo.Rows[0]["descuento"].ToString();
                }
                decimal DineroDescontar = Convert.ToDecimal(dtInfo.Rows[0]["precio"]) - Convert.ToDecimal(descuento);

                if (Convert.ToDecimal(descuento) != 0)
                {
                    if (Convert.ToDecimal(descuento) < 0)
                    {
                        mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Incremento: $" + Math.Abs(Convert.ToDecimal(descuento)) + "");
                    }
                    else
                    {
                        mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Descuento: $" + descuento + "");
                    }
                }
                
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Total acreditado: $" + DineroDescontar + "");
            }
            else if (object.Equals(dtInfo.Rows[0]["tipoCuenta"], DBNull.Value) == false && dtInfo.Rows[0]["tipoCuenta"].ToString() == "2")
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Forma de Pago: Contado");
            }
            else if (object.Equals(dtInfo.Rows[0]["tipoCuenta"], DBNull.Value) == false && dtInfo.Rows[0]["tipoCuenta"].ToString() == "3")
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Forma de Pago: Prepago");
                string descuento = "0";
                if (object.Equals(dtInfo.Rows[0]["descuento"], DBNull.Value) == false && dtInfo.Rows[0]["descuento"].ToString().Trim() != "")
                {
                    descuento = dtInfo.Rows[0]["descuento"].ToString();
                }
                decimal DineroDescontar = Convert.ToDecimal(dtInfo.Rows[0]["precio"]) - Convert.ToDecimal(descuento);

                if (Convert.ToDecimal(descuento) != 0)
                {
                    if (Convert.ToDecimal(descuento) < 0)
                    {
                        mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Incremento: $" + Math.Abs(Convert.ToDecimal(descuento)) + "");
                    }
                    else
                    {
                        mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Descuento: $" + descuento + "");
                    }
                }
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Total Venta Prepago:");
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "$" + DineroDescontar + "");
            }
            else
            {
                mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Forma de Pago: Datafono");
            }
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Cliente: " + dtInfo.Rows[0]["cliente"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                                           Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "Atendido por:");
            mensajeTrama.Add(_CARACTERINICIALIMPRESION + "" + dtInfo.Rows[0]["nomUsuario"].ToString() + " " + dtInfo.Rows[0]["apeUsuario"].ToString());
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                                                       Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));
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

            if(Convert.ToInt32(puntosTotal) != Convert.ToInt32(0))
            {
                mensajeTrama.Add("?     PUNTOS");
                mensajeTrama.Add("?COMPRA: " + puntosVenta);
                mensajeTrama.Add("?TOTAL: " + puntosTotal);
            }
            mensajeTrama.Add(UtilidadesTramas.CentrarConcatenarMensajeTrama(_CARACTERDIVISOR.ToString(),
                                                                       Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR));

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
