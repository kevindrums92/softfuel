using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace XbeeController
{
    public partial class ServiceXbeeController : ServiceBase
    {
        #region "Constructor"
        public ServiceXbeeController()
        {
            InitializeComponent();
        }
        #endregion

        #region "Procedimientos Servicio"
        protected override void OnStart(string[] args)
        {
            // Agregue el código aquí para iniciar el servicio. Este método debería poner
            // en movimiento los elementos para que el servicio pueda funcionar.
          
        }

        // Pausa del servicio
        protected override void OnPause()
        {
            
        }

        // cuando se continua el servicio
        protected override void OnContinue()
        {
           
        }
        protected override void OnStop()
        {
            // Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
           
        }
        #endregion
        
    }
}
