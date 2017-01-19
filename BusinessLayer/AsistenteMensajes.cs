using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using XbeeUtils;

namespace BusinessLayer
{
    public static class AsistenteMensajes
    {
        const char _CARACTERDIVISOR = '*';
        public static List<Byte[]> CocarEncabezadoAListadosDeTramas(List<Byte[]> _listTramas)
        {
            List<Byte[]> newListTramas = new List<Byte[]>();
            EstructuraTiquete estructuraTiquete;
            using (ModeloPOS modPOS = new ModeloPOS())
            {
                estructuraTiquete = modPOS.ObtenerDefinicionTiquete();
            }
            if (object.Equals( estructuraTiquete,null) == false)
            {
                if (estructuraTiquete.Cab1 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab1,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Cab2 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab2,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Cab3 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab3,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Cab4 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab4,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Cab5 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab5,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Cab6 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Cab6,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                foreach (byte[] _byte in _listTramas)
                {
                    newListTramas.Add(_byte);
                }
                if (estructuraTiquete.Pie1 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie1,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Pie2 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie2,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Pie3 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie3,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Pie4 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie4,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Pie5 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie5,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                if (estructuraTiquete.Pie6 != "") newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(estructuraTiquete.Pie6,
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama("",
                                                Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos,_CARACTERDIVISOR).TrimEnd()));

                newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                    Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));

                newListTramas.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                    Enumeraciones.TipodeMensaje.SinAlerta, Enumeraciones.Direccion.ambos, ' ').TrimEnd()));
                return newListTramas;
            }
            else
            {
                return _listTramas;
            }
        }

        /// <summary>
        /// Recibe un array de string para generar una alerta
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static List<Byte[]> GenerarMensajeAlerta(string[] texto)
        {
            List<Byte[]> newListado = new List<byte[]>() { };
            newListado.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama("ALERTA",
                                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR)));
            foreach(string _txt in texto)
            {
                newListado.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(_txt,
                                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' ')));
            }
            newListado.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama("",
                                                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, _CARACTERDIVISOR)));

            newListado.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' ')));

            newListado.Add(UtilidadesTramas.ObtenerByteDeString(UtilidadesTramas.CentrarConcatenarMensajeTrama(" ",
                Enumeraciones.TipodeMensaje.ConAlerta, Enumeraciones.Direccion.ambos, ' ')));
            return newListado;
        }
    }
}
