using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeeUtils
{
    public static class UtilidadesTramas
    {
        #region "Functions"
        /// <summary>
        /// funcion que sirve para centrar el texto de todos los items de array
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static string[] CentrarMensajedeTrama(string[] mensaje, Enumeraciones.TipodeMensaje tipoMensaje)
        {
            string LetraTipoMensaje;
            if (tipoMensaje == Enumeraciones.TipodeMensaje.ConAlerta)
            {
                LetraTipoMensaje = "$";
            }
            else {
                LetraTipoMensaje = "C";
            }

            List<String> nuevoArrayTexto = new List<string>();
            foreach (string _texto in mensaje)
            {
                char pad = ' ';
                string nuevoTexto = "";
                int longitud = _texto.Trim().Length;
                int longRestante = 33 - longitud;
                int longConcatInicial = longRestante / 2;
                nuevoTexto = LetraTipoMensaje + _texto.PadLeft((longConcatInicial + longitud) - 1, pad);
                if (nuevoTexto.Length > 33) nuevoTexto = nuevoTexto.Substring(0, (Convert.ToInt16(nuevoTexto.Length) - (nuevoTexto.Length - 33)));
                nuevoArrayTexto.Add(nuevoTexto);
            }
            return nuevoArrayTexto.ToArray();
        }

        /// <summary>
        /// Centra y concatena cualquier caracter a un texto, en cualquier direccion o ambas, y con la primera letra como tipo de mensaje
        /// </summary>
        /// <param name="mensaje">mensaje o texto</param>
        /// <param name="tipoMensaje">si es de alerta o no</param>
        /// <param name="direccion">si es izquiera derecha o ambas</param>
        /// <param name="caracter">que caracter sera utilizado para concatenar</param>
        /// <returns></returns>
        public static string CentrarConcatenarMensajeTrama(string mensaje, Enumeraciones.TipodeMensaje tipoMensaje, Enumeraciones.Direccion direccion, char caracter)
        {
            string LetraTipoMensaje;
            if (tipoMensaje == Enumeraciones.TipodeMensaje.ConAlerta)
            {
                LetraTipoMensaje = "$";
            }
            else
            {
                LetraTipoMensaje = "C";
            }

            char pad = caracter;
            string nuevoTexto = "";
            int longitud = mensaje.Trim().Length;
            int longRestante = 33 - longitud;
            int longConcatInicial = longRestante / 2;
            switch (direccion)
            {
                case Enumeraciones.Direccion.izquierda:
                    nuevoTexto = LetraTipoMensaje + mensaje.PadLeft((longConcatInicial + longitud) - 1, pad);
                    break;
                case Enumeraciones.Direccion.derecha:
                    nuevoTexto = LetraTipoMensaje + mensaje.PadRight((longConcatInicial + longitud) - 1, pad);
                    break;
                case Enumeraciones.Direccion.ambos:
                   nuevoTexto = LetraTipoMensaje + mensaje.PadLeft((longConcatInicial + longitud) - 1, pad);
                   nuevoTexto = nuevoTexto.PadRight(33, pad);
                   break;
            }
            return nuevoTexto;
        }

        /// <summary>
        /// Obtiene el código en bytes de un string
        /// </summary>
        /// <param name="data">texto</param>
        /// <returns></returns>
        public static byte[] ObtenerByteDeString(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// Obtiene el string de un array de bytes
        /// </summary>
        /// <param name="data">data en bytes</param>
        /// <returns></returns>
        public static string ObtenerStringDeBytes(byte[] data) 
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// Esta funcion recibe un listado de tramas a enviar en string, y devuelve un listdo de tramas en array de bytes, para ser enviadas
        /// </summary>
        /// <param name="Listado">listado de strings</param>
        /// <returns></returns>
        public static List<Byte[]> ConvertirListadoStringaByte(List<String> Listado)
        {
            List<Byte[]> NuevoListado = new List<byte[]>();
            foreach(string texto in Listado)
            {
                NuevoListado.Add(ObtenerByteDeString(texto));
            }

            return NuevoListado;
        }

        /// <summary>
        /// Obtiene el array de string de una trama definida por un protocolo de comunicación
        /// </summary>
        /// <param name="trama"></param>
        /// <returns></returns>
        public static string[] ObtieneArrayTrama(string trama)
        { 
            string[] resultado = trama.Split(':');
            for (int i = 0; i <= resultado.Count() - 1; i++)
            {
                resultado[i] = resultado[i].Trim();
            }
            return resultado;
        }

        public static string MensajeQueEnvióTrama(List<Byte[]> listBytes)
        {
            string mensaje = "";
            foreach(Byte[] _item in listBytes)
            {
                mensaje = mensaje + ObtenerStringDeBytes(_item) + " ";
            }
            return mensaje;
        }
        #endregion
    }
}
