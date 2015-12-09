using DataAccess;
using Singleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XBee;
using XbeeUtils;

namespace BusinessLayer
{
    public  class Main : IDisposable
    {
        #region "Variables"
        XbeeSingleton instancia = XbeeSingleton.Instance;
        TramasPOS _tramasPOS = new TramasPOS();
        TramasDispensador _tramaDIS = new TramasDispensador();
        public delegate void MonitoreoEventHandler(object sender, MonitoreoEventArgs e);
        public event MonitoreoEventHandler MonitoreoEvent;
        private string puerto { get; set; }
        private int velocidadTrasmision { get; set; }
        private List<FidelizadoCreditoPendiente> ListaFidelizadosPendientes= new List<FidelizadoCreditoPendiente>();
        #endregion
                
        #region "Conexión y procesos Xbee"
        /// <summary>
        /// Metodo para abrir conexion a xbee con el puerto predefinido
        /// </summary>
        private void Conectar()
        {
            try
            {
                using (Generales modGenerales = new Generales())
                {
                    var XbeeCoordinador = modGenerales.ObtenerXbeeCoordinador();
                    if (XbeeCoordinador == null)
                    {
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Error Sql al momento de obtener el xbee coordinador",ETipoEvento.Error,0,""));
                    }
                    if (XbeeCoordinador != null && XbeeCoordinador.Rows.Count > 0)
                    {
                        puerto = XbeeCoordinador.Rows[0]["puertoXbee"].ToString().Trim();
                        velocidadTrasmision = (int)XbeeCoordinador.Rows[0]["velocidadXbee"];
                    }
                    else
                    {
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("No se encontró Xbee Coordinador en base de datos!", ETipoEvento.Error, 0, ""));
                        return;
                    }
                }
                if (instancia.Controller == null) instancia.Controller = new XBeeController();
                //Configuro el manejador para escuchar los datos recibidos
                instancia.Controller.DataReceived += DataReceivedXbee;
                //Configuro manejador para escuchar el metodo que descubre los xbee en red
                instancia.Controller.NodeDiscovered += NodeDiscovered_controller;
                instancia.Controller.OpenAsync(puerto, velocidadTrasmision);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se abrió conexión en el puerto " + puerto + " con velocidad de trasmisión " + velocidadTrasmision.ToString() + " ", ETipoEvento.Exitoso, 0, ""));
                //instancia.Controller.Dispose();
            }
            catch (Exception e)
            {
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + e.Message, ETipoEvento.Error, 0, ""));
                LocalLogManager.EscribeLog(e.Message,LocalLogManager.TipoImagen.TipoError);
            }
        }

        /// <summary>
        /// Metodo para descubrir red, en caso de no estar conectado o cerrada la conexion, vuelve y ejecuta el metodo de conectar
        /// </summary>
        public void ConectaryDescubrirRed()
        {
            if (instancia.Controller == null || instancia.Controller.IsOpen == false)
            {
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Realizando Conexión...", ETipoEvento.Exitoso, 0, ""));
                Conectar();
            }
            if (instancia.Controller != null)
            {
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se va a escanear Red!",ETipoEvento.Exitoso,0,""));
                instancia.Controller.DiscoverNetwork();
                //EscanearRed();
            }
            
        }

        /// <summary>
        /// Evento que escanea la red en busqueda de los xbee para conexión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NodeDiscovered_controller(object sender, NodeDiscoveredEventArgs args)
        {
            try
            {
                if (instancia.ListNodes == null)
                {
                    if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Escaneando Red...", ETipoEvento.Exitoso, 0, ""));
                    instancia.ListNodes = new List<NodosXbee>();
                }

                string _macNodoEncontrado = args.Node.Address.LongAddress.Value.ToString("X");
                string _macImpresora = "";
                int _tiempoEspera = 0;
                int _idXbee = 0;
                Enumeraciones.TipoDispositivo _tipDisp;
                using (Generales modGenerales = new Generales())
                {
                    DataTable XbeeConsultado = modGenerales.ObtenerXbeePorMac(_macNodoEncontrado);
                    if (XbeeConsultado != null && XbeeConsultado.Rows.Count > 0)
                    {
                        _macImpresora = (string)XbeeConsultado.Rows[0]["impresoraXbee"];
                        _tiempoEspera = (int)XbeeConsultado.Rows[0]["tiempoEspXbee"];
                        _idXbee = (int)XbeeConsultado.Rows[0]["idXbee"];
                        _tipDisp = (Enumeraciones.TipoDispositivo)XbeeConsultado.Rows[0]["tipoXbee"];
                        NodosXbee newDispositivo = new NodosXbee(args.Node, args.Name, _macNodoEncontrado, _macImpresora, _tiempoEspera, _tipDisp, _idXbee);
                        instancia.AgregarNodo(newDispositivo);
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se conectó el dispositivo: " + args.Name + " MAC:" + _macNodoEncontrado + "", ETipoEvento.Exitoso, 0, ""));
                        if (_tipDisp == Enumeraciones.TipoDispositivo.Dispensador)
                        {
                            ActualizarDatosDispensador(newDispositivo.IdXbee);
                        }
                    }
                    else
                    {
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se encontro dispositivo pero no esta registrado en base de datos: " + args.Name + " MAC: " + _macNodoEncontrado + "", ETipoEvento.Exitoso, 0, ""));
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + e.Message, ETipoEvento.Error, 0, ""));
            }
            
        }

        /// <summary>
        /// Evento que escucha los datos recibidos desde otros Xbee en red
        /// </summary>
        /// <param name="sender">Xbee que envia</param>
        /// <param name="e">Parametros que envia en el mensaje</param>
        private void DataReceivedXbee(object sender, SourcedDataReceivedEventArgs e)
        {
            try
            {
                string data = System.Text.Encoding.UTF8.GetString(e.Data);
                data = data.ToString().Replace('\0',' ');

                if (instancia.ListNodes != null && instancia.ListNodes.Count > 0)
                {
                    NodosXbee nodeXbee = instancia.ListNodes.Find(x => object.Equals(x.Nodo.Address.LongAddress.Value, e.Address.LongAddress.Value));
                    if (nodeXbee != null)
                    {
                        string[] arrayTramaRecibida = UtilidadesTramas.ObtieneArrayTrama(data);
                        if (nodeXbee.TipoDispositivo == Enumeraciones.TipoDispositivo.moduloPOS) //Valido si es modulo pos para sacar el nodo de impresion
                        {
                            NodosXbee nodoImpresion = instancia.ListNodes.Find(item => item.Mac == nodeXbee.MacImpresion);
                            if (nodoImpresion != null)
                            {
                                ProcesarTrama(arrayTramaRecibida, nodoImpresion);
                            }
                            else {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("No se encontró el dispositivo con mac " + nodeXbee.MacImpresion + " para impresión", ETipoEvento.Error, nodeXbee.IdXbee, ""));
                            }
                        }
                        else {
                            ProcesarTrama(arrayTramaRecibida, nodeXbee);
                        }
                    }
                }
                else
                {
                    LocalLogManager.EscribeLog("Llegó un paquete, pero no se detectaron dispositivos para devolver el mensaje!", LocalLogManager.TipoImagen.Advertencia);
                }
            }
            catch (Exception ex)
            {
                LocalLogManager.EscribeLog(ex.Message, LocalLogManager.TipoImagen.TipoError);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + ex.Message, ETipoEvento.Error, 0, ""));
            }
            
        }

        /// <summary>
        /// Metodo para desconectar el xbee controlador
        /// </summary>
        public void Desconectar()
        {
            try
            {
                if (instancia.Controller != null)
                {
                    instancia.Controller.Dispose();
                    instancia.Controller.Close();
                    instancia.Controller = null;
                    if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se ha cerrado la conexión!", ETipoEvento.Exitoso, 0, ""));
                }
                if (instancia.ListNodes != null && instancia.ListNodes.Count > 0)
                {
                    instancia.ListNodes = null;
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + e.Message, ETipoEvento.Error, 0, ""));
            }
        }

        public async void EscanearRed()
        {
            try
            {
                if (instancia.ListNodes == null)
                {
                    if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Escaneando Red...", ETipoEvento.Exitoso, 0, ""));
                    instancia.ListNodes = new List<NodosXbee>();
                }
                string _macNodoEncontrado;
                string _macImpresora = "";
                int _tiempoEspera = 0;
                int _idXbee = 0;
                Enumeraciones.TipoDispositivo _tipDisp;
                DataTable dtXbees;
                using (Generales modGEN = new Generales())
                {
                    dtXbees = modGEN.ObtenerTodosLosXbee();
                }
                foreach (DataRow _row in dtXbees.Rows)
                {
                    _macNodoEncontrado = (string)_row["macXbee"];
                    _macImpresora = (string)_row["impresoraXbee"];
                    _tiempoEspera = (int)_row["tiempoEspXbee"];
                    _idXbee = (int)_row["idXbee"];
                    _tipDisp = (Enumeraciones.TipoDispositivo)_row["tipoXbee"];
                    NodosXbee newDispositivo = new NodosXbee(null, (string)_row["nomXbee"].ToString(), _macNodoEncontrado, _macImpresora, _tiempoEspera, _tipDisp, _idXbee);

                    //Buscar nodo en red
                    ulong _ulong = Convert.ToUInt64(_row["macXbee"].ToString(), 16);
                    NodeAddress Address = new NodeAddress(new LongAddress(_ulong));
                    XBeeNode nodoXbee = null;
                    nodoXbee = await instancia.Controller.GetRemoteAsync(Address);
                    if (nodoXbee != null)
                    {
                        newDispositivo.Nodo = nodoXbee;
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se conectó el dispositivo: " + newDispositivo.Nombre + " MAC:" + _macNodoEncontrado + "", ETipoEvento.Exitoso, newDispositivo.IdXbee, ""));
                        if (_tipDisp == Enumeraciones.TipoDispositivo.Dispensador)
                        {
                            ActualizarDatosDispensador(newDispositivo.IdXbee);
                        }
                    }
                    instancia.AgregarNodo(newDispositivo);
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + e.Message, ETipoEvento.Error, 0, ""));
            }
        }

        /// <summary>
        /// Metodo que autoriza ventas en dispensador
        /// </summary>
        /// <param name="IdXbee"></param>
        public void ActualizarDatosDispensador(int IdXbee)
        {
            NodosXbee nodo = instancia.ListNodes.Find(item => item.IdXbee == IdXbee);
            try
            {
                if (nodo != null)
                {
                    var result = _tramasPOS.TramaAutorizarVentaDispensador(nodo.IdXbee.ToString());
                    foreach (Byte[] data in result)
                    {
                        nodo.EnviarTrama(data);
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.ObtenerStringDeBytes(data), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                    }
                }
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se detectó un error:\n" + e.Message, ETipoEvento.Error, 0, "",nodo.Nombre));
            }
        }

        public void CambioPrecioDispensador(int IdXbee)
        {
            NodosXbee nodo = instancia.ListNodes.Find(item => item.IdXbee == IdXbee);
            if (nodo != null)
            {
                var result = _tramaDIS.CambiodePrecio(nodo.IdXbee.ToString());
                if (result != null)
                {
                    foreach (Byte[] data in result)
                    {
                        nodo.EnviarTrama(data);
                        if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Cambio de precio", ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                    }
                }
            }
        }
        
        #endregion

        #region "Procesos Tramas"
        public void ProcesarTrama(string[] arrayTramaRecibida,NodosXbee nodo)
        {
            if (arrayTramaRecibida.Count() > 1)
            {
                //if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Llegó un paquete de " + nodo.Nombre + "", ETipoEvento.Exitoso, nodo.IdXbee, ""));
                if (nodo.TipoDispositivo == Enumeraciones.TipoDispositivo.moduloPOS)
                {//Recibo petición de MOD POS
                    switch (arrayTramaRecibida[0])
                    {
                            //Credito
                        case "C":
                            var resultCre = _tramasPOS.Credito(arrayTramaRecibida);
                            if (resultCre.Resultado == true)
                            {
                                foreach (Byte[] data in resultCre.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultCre.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "", nodo.Nombre));
                                if (resultCre.Fidelizado_o_Credito == true)
                                {
                                    FidelizadoCreditoPendiente _newCre = new FidelizadoCreditoPendiente();
                                    _newCre.cara = arrayTramaRecibida[1];
                                    _newCre.serial = arrayTramaRecibida[2];
                                    _newCre.descuento = resultCre.DescuentoCredito;
                                    _newCre.tipoSolicitud = ETipoSolicitudSerial.Credito;
                                    instancia.ListaFidelizadosCreditosPendientes.Add(_newCre);
                                    if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Preparando Crédito en cara " + _newCre.cara, ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                                }
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultCre.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;
                        case "F":
                            var resultFid = _tramasPOS.Fidelizado(arrayTramaRecibida);
                            if (resultFid.Resultado == true)
                            {
                                foreach (Byte[] data in resultFid.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultFid.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                                if (resultFid.Fidelizado_o_Credito == true)
                                {
                                    FidelizadoCreditoPendiente _newFid = new FidelizadoCreditoPendiente();
                                    _newFid.cara = arrayTramaRecibida[1];
                                    _newFid.serial = arrayTramaRecibida[2];
                                    _newFid.tipoSolicitud = ETipoSolicitudSerial.Fidelizado;
                                    instancia.ListaFidelizadosCreditosPendientes.Add(_newFid);
                                    if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Preparando Fidelizado en cara " + _newFid.cara, ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                                }
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultFid.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;
                        ///Petición Consignación en efectivo
                        case "H":
                            var result = _tramasPOS.ConsignacionEfectivo(arrayTramaRecibida);
                            if (result.Resultado == true)
                            {
                                foreach (Byte[] data in result.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(result.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(result.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;

                            //Petición abrir turno
                        case "T":
                            var resultAbrirTurno = _tramasPOS.AbrirTurno(arrayTramaRecibida);
                            if (resultAbrirTurno.Resultado == true)
                            {
                                foreach (Byte[] data in resultAbrirTurno.TramaResultado)
                                {

                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultAbrirTurno.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultAbrirTurno.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            if (resultAbrirTurno.IdXbee > 0) ActualizarDatosDispensador(resultAbrirTurno.IdXbee);
                            
                            _tramasPOS.Dispose();
                            break;

                            //Peticion de guardar venta canasta
                        case "P":
                            var resultVentaCanasta = _tramasPOS.VentaCanasta(arrayTramaRecibida);
                            if (resultVentaCanasta.Resultado == true)
                            {
                                foreach (Byte[] data in resultVentaCanasta.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultVentaCanasta.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultVentaCanasta.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;
                            //Cerrar Turno
                        case "E":
                            var resultCerrarTurno = _tramasPOS.CerrarTurno(arrayTramaRecibida);
                            if (resultCerrarTurno.Resultado == true)
                            {
                                foreach (Byte[] data in resultCerrarTurno.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultCerrarTurno.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultCerrarTurno.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            if (resultCerrarTurno.IdXbee > 0) ActualizarDatosDispensador(resultCerrarTurno.IdXbee);
                            _tramasPOS.Dispose();
                            break;
                            //Consecutivo de cerrar Turno
                        case "M":
                            var resultConsecutivoTurno = _tramasPOS.ConsecutivoCierre_AperturaTurno(arrayTramaRecibida);
                            if (resultConsecutivoTurno.Resultado == true)
                            {
                                foreach (Byte[] data in resultConsecutivoTurno.TramaResultado)
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultConsecutivoTurno.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultConsecutivoTurno.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;

                            //Imprimir Ultima venta
                        case "I":
                            var resultUltimaVenta = _tramasPOS.UltimaVenta(arrayTramaRecibida);
                            if (resultUltimaVenta.Resultado == true)
                            {
                                foreach (Byte[] data in AsistenteMensajes.CocarEncabezadoAListadosDeTramas(resultUltimaVenta.TramaResultado))
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultUltimaVenta.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultUltimaVenta.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;

                        case "N":
                            var resultUltimaVentaConsecutivo = _tramasPOS.ConsecutivoUltimaVenta(arrayTramaRecibida);
                            if (resultUltimaVentaConsecutivo.Resultado == true)
                            {
                                foreach (Byte[] data in AsistenteMensajes.CocarEncabezadoAListadosDeTramas(resultUltimaVentaConsecutivo.TramaResultado))
                                {
                                    nodo.EnviarTrama(data);
                                }
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(UtilidadesTramas.MensajeQueEnvióTrama(resultUltimaVentaConsecutivo.TramaResultado), ETipoEvento.Exitoso, nodo.IdXbee, "",nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultUltimaVentaConsecutivo.Mensaje, ETipoEvento.Error, nodo.IdXbee, "",nodo.Nombre));
                            }
                            _tramasPOS.Dispose();
                            break;

                        default:
                            if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Trama sin objetivo", ETipoEvento.Exitoso, 0, "",nodo.Nombre));
                            break;
                    }
                }
                else 
                {//recibo tramas de dispesadores
                    switch (arrayTramaRecibida[0])
                    { 
                            //Abrir Venta
                        case "E":
                            string cara = arrayTramaRecibida[1].ToString();
                            if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("LEVANTAMANGUERA",ETipoEvento.Exitoso,nodo.IdXbee,cara));
                            break;
                            //Totales de venta
                        case "ET":
                            var resultEnvioTotales = _tramaDIS.EnvioTotales(arrayTramaRecibida);
                            if (resultEnvioTotales.Resultado == true)
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Se guardó ventas totales en cara " + arrayTramaRecibida[1], ETipoEvento.Exitoso, nodo.IdXbee, arrayTramaRecibida[1],nodo.Nombre));
                            }
                            else
                            {
                                if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs(resultEnvioTotales.Mensaje, ETipoEvento.Exitoso, nodo.IdXbee, arrayTramaRecibida[1],nodo.Nombre));
                            }
                            if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("BAJAMANGUERA:" + resultEnvioTotales.VentaGalones + ":" + resultEnvioTotales.VentaDinero, ETipoEvento.Exitoso, nodo.IdXbee, arrayTramaRecibida[1]));
                            _tramaDIS.Dispose();
                            break;

                        default:
                            if (MonitoreoEvent != null) MonitoreoEvent(this, new MonitoreoEventArgs("Trama sin objetivo", ETipoEvento.Exitoso, 0, "",nodo.Nombre));
                            break;
                    }
                }
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

    #region Evento Log
    public class MonitoreoEventArgs
    {
        public string Texto { get; private set; }
        public ETipoEvento TipoEvento { get; private set; }
        public int IdXbee { get; private set; }
        public string Cara { get; private set; }
        public string Dispositivo { get; set; }
        public MonitoreoEventArgs(string s, ETipoEvento t, int idXbee, string cara,string dispositivo = "") 
        {
            Texto = s; TipoEvento = t; IdXbee = idXbee; Cara = cara;
            this.Dispositivo = dispositivo;
        }
    }
    /// <summary>
    /// Para identificar si un tipo de evento se realizó satisfactoriamente o erroneo
    /// </summary>
    public enum ETipoEvento
    { 
        Exitoso,
        Error
    }
    #endregion
    
}
