﻿using System;
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
        public bool Fidelizado { get; set; }
        public string VentaGalones { get; set; }
        public string VentaDinero { get; set; }

        public ResultadoTrama(bool _resultado, List<byte[]> _tramaResultado,string _mensaje,int _idXbee = 0, bool _fidelizado = false,string _ventaGalones  = "",string _ventaDinero = "")
        {
            this.Resultado = _resultado;
            this.TramaResultado = _tramaResultado;
            this.Mensaje = _mensaje;
            this.IdXbee = _idXbee;
            this.Fidelizado = _fidelizado;
            this.VentaGalones = _ventaGalones;
            this.VentaDinero = _ventaDinero;
        }
    }
}
