using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XBee;
using XbeeUtils;

namespace Singleton
{
    public class XbeeSingleton
    {
        #region "Instancia"
        private static XbeeSingleton instance;

        private XbeeSingleton() { }

        public static XbeeSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XbeeSingleton();
                }
                return instance;
            }
        }
        #endregion

        #region "Propiedades"
        public List<NodosXbee> ListNodes { get; set; }
        public XBeeController Controller { get; set; }
        public string SqlServidor { get; set; }
        public string SqlUsuario { get; set; }
        public string SqlPassword { get; set; }
        public string SqlBaseDatos { get; set; }
        public List<FidelizadoCreditoPendiente> _listaFidelizadosCreditosPendientes;
        public List<FidelizadoCreditoPendiente> ListaFidelizadosCreditosPendientes 
        {
            get
            { 
                if (_listaFidelizadosCreditosPendientes == null) _listaFidelizadosCreditosPendientes = new List<FidelizadoCreditoPendiente>();
                return _listaFidelizadosCreditosPendientes;
            }
            set
            { 
                _listaFidelizadosCreditosPendientes = value;
            }
        }

        public delegate void NodoAgregadoEventHandler(NodosXbee e);
        public event NodoAgregadoEventHandler NodoAgregadoEvent;
        #endregion

        #region "Metodos"
        public void AgregarNodo(NodosXbee _nodo)
        {
            ListNodes.Add(_nodo);
            NodoAgregadoEvent(_nodo);
        }
        #endregion
    }


    /// <summary>
    /// Clase que contiene los datos necesarios para un nodo
    /// </summary>
    public class NodosXbee
    {
        #region "Variables y propiedades"
        public XBee.XBeeNode Nodo { get; set; }
        public string Nombre { get; set; }
        public string Mac { get; set; }
        public string MacImpresion { get; set; }
        public int TiempoEspera { get; set; }
        public Enumeraciones.TipoDispositivo TipoDispositivo { get; set; }
        public int IdXbee { get; set; }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_nodo"></param>
        /// <param name="_nombre"></param>
        /// <param name="_mac"></param>
        public NodosXbee(XBee.XBeeNode _nodo, string _nombre, string _mac, string _macImpresion,int _tiempoEspera,Enumeraciones.TipoDispositivo _tipDisp,int _idXbee)
        {
            this.Nodo = _nodo;
            this.Nombre = _nombre;
            this.Mac = _mac;
            this.MacImpresion = _macImpresion;
            this.TiempoEspera = _tiempoEspera;
            this.TipoDispositivo = _tipDisp;
            this.IdXbee = _idXbee;
        }
        #endregion

        #region "Eventos"
        /// <summary>
        /// Envia una trama
        /// </summary>
        /// <param name="data"></param>
        public void EnviarTrama(byte[] data)
        {
            try
            {
                Nodo.TransmitDataAsync(data);
                Thread.Sleep(TiempoEspera);
            }
            catch (Exception ex)
            {
                LocalLogManager.EscribeLog("Falló envió de trama a nodo con mac: " + Mac + "\n" + ex.Message,LocalLogManager.TipoImagen.TipoError);
            }
            
        }
        #endregion
    }
    public class FidelizadoCreditoPendiente
    {
        public string serial { get; set; }
        public string cara { get; set; }
        public int descuento { get; set; }
        public ETipoSolicitudSerial tipoSolicitud { get; set; }
    }
    public enum ETipoSolicitudSerial
    { 
        Fidelizado,
        Credito
    }
}
