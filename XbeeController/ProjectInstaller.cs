
namespace XbeeController
{
    using System.ComponentModel;
    using System.Configuration;

    [System.ComponentModel.RunInstaller(true)]
    partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }


        //Installer overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Component Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Component Designer
        //It can be modified using the Component Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.ServiceProccessXbeeController = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceXbeeController = new System.ServiceProcess.ServiceInstaller();
            //
            //IndigoServicesProcessSMTPPOP3
            //
            this.ServiceProccessXbeeController.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServiceProccessXbeeController.Password = null;
            this.ServiceProccessXbeeController.Username = null;
            //
            //IndigoServicioSMTPPOP3Res3047
            //
            this.ServiceXbeeController.Description = "Xbee Controller .NET - Servicio encargado de la comunicación con los dispositivos Xbee";
            this.ServiceXbeeController.DisplayName = "Xbee Controller .NET ";
            this.ServiceXbeeController.ServiceName = "Xbee Controller .NET ";
            this.ServiceXbeeController.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            //
            //ProjectInstaller
            //
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceProccessXbeeController,
            this.ServiceXbeeController
        });

        }
        internal System.ServiceProcess.ServiceProcessInstaller ServiceProccessXbeeController;

        internal System.ServiceProcess.ServiceInstaller ServiceXbeeController;
    }
}
