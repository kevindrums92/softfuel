using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class ResultadoTrama
    {

        public bool Resultado { get; set; }
        public List<byte[]> TramaResultado { get; set; }
        public string Mensaje { get; set; }
        public int IdXbee { get; set; }
        public bool Fidelizado_o_Credito { get; set; }
        public int DescuentoCredito { get; set; }
        public string VentaGalones { get; set; }
        public string VentaDinero { get; set; }

        public ResultadoTrama(bool _resultado, List<byte[]> _tramaResultado,string _mensaje,int _idXbee = 0, bool _esCredito = false,string _ventaGalones  = "",string _ventaDinero = "",int _descuentoCredito = 0)
        {
            this.Resultado = _resultado;
            this.TramaResultado = _tramaResultado;
            this.Mensaje = _mensaje;
            this.IdXbee = _idXbee;
            this.Fidelizado_o_Credito = _esCredito;
            this.VentaGalones = _ventaGalones;
            this.VentaDinero = _ventaDinero;
            this.DescuentoCredito = _descuentoCredito;
        }
    }
}
