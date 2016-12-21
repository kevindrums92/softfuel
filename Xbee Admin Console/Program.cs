using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbeeAdminConsole
{
    static class Program
    {
        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Ticket Soft Console Application ya está corriendo!");
                    return;
                }

                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);

                // Set the unhandled exception mode to force all Windows Forms errors to go through
                // our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(UnhandledException);

                //Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmAdmin());
            }
            
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            XbeeUtils.LocalLogManager.EscribeLog("Ha ocurrido un error UnhandledException: " + e.ExceptionObject.GetType().ToString(), XbeeUtils.LocalLogManager.TipoImagen.TipoError);
            
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            XbeeUtils.LocalLogManager.EscribeLog("Ha ocurrido un error UIThreadException: " + t.Exception.Message, XbeeUtils.LocalLogManager.TipoImagen.TipoError);
            //Application.Exit();
        }
       
    }
}
