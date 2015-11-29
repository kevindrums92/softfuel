using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeeUtils
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Provee servicios para crear el log
    /// de eventos para el servicios, asi
    /// como para registrar entradas en él
    /// </summary>
    internal sealed class LogManager
    {
        #region "Consts"

        /// <summary>
        /// Nombre de la fuente
        /// </summary>

        private const string SOURCE = "XbeeController";
        #endregion

        #region "Methods"

        /// <summary>
        /// Escribe una entrada en el log de eventos
        /// para una excepción ocurrida
        /// </summary>
        /// <param name="ex">Excepción a registrar</param>
        public static void WriteEntry(Exception ex)
        {
            string message = GetInnerExceptionMessages(ex);
            WriteEntry(message, EventLogEntryType.Error);
        }

        /// <summary>
        /// Escribe una entrada en el log de eventos
        /// </summary>
        /// <param name="message">Mensaje a escribir</param>
        /// <param name="level">Nivel del evento</param>
        public static void WriteEntry(string message, EventLogEntryType level)
        {
            if (!EventLog.SourceExists(SOURCE))
            {
                CreateLog();
            }

            EventLog CustomEventLog = new EventLog();
            CustomEventLog.Source = SOURCE;
            CustomEventLog.Log = SOURCE;
            CustomEventLog.WriteEntry(message, level);
        }

        /// <summary>
        /// Crea el log para el registro de eventos del servicio
        /// </summary>
        private static void CreateLog()
        {
            EventLog.CreateEventSource(SOURCE, SOURCE);
            EventLog CustomEventLog = new EventLog();

            CustomEventLog.Source = SOURCE;
            CustomEventLog.Log = SOURCE;

            CustomEventLog.WriteEntry((Convert.ToString("The ") + SOURCE) + " was successfully initialize component.", EventLogEntryType.Information);
        }

        /// <summary>
        /// Obtiene la lista de mensajes de excepcion
        /// </summary>
        /// <param name="ex">Excepcion a obtener</param>
        /// <returns>Lista de mensajes en la excepción</returns>
        private static string GetInnerExceptionMessages(Exception ex)
        {
            StringBuilder list = new StringBuilder();
            GetInnerExceptionMessage(ex, ref list);
            return list.ToString();
        }

        /// <summary>
        /// Obtiene la recursivamente la secuencia de excepciones heredadas
        /// </summary>
        /// <param name="ex">Excepcion a obtener</param>
        /// <param name="list">Lista de mensajes</param>
        private static void GetInnerExceptionMessage(Exception ex, ref StringBuilder list)
        {
            if (ex.InnerException != null)
            {
                GetInnerExceptionMessage(ex, ref list);
            }
            list.AppendLine("Name: " + ex.GetType().Name + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace + Environment.NewLine + "<------------------------------------------------------------------>" + Environment.NewLine);
        }

        #endregion

    }
}
