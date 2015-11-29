using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeeUtils
{
    public static class LocalLogManager
    {
        #region "Shared Methods"
        public static void EscribeLog(string Mensaje, TipoImagen Imagen)
        {
            try
            {
                switch (Imagen)
                {
                    case TipoImagen.Informacion:
                        LogManager.WriteEntry(Mensaje, EventLogEntryType.Information);
                        break;
                    case TipoImagen.Advertencia:
                        LogManager.WriteEntry(Mensaje, EventLogEntryType.Warning);
                        break;
                    case TipoImagen.TipoError:
                        LogManager.WriteEntry(Mensaje, EventLogEntryType.Error);
                        break;
                }

                if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\XbeeManagementLog"))
                {
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\XbeeManagementLog");
                }
                string NombreLog = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\XbeeManagementLog\\logXbee" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString();
                //pregunta si existe
                if (System.IO.File.Exists(NombreLog + ".xml") == false)
                {
                    using (DataTable dtLogRegistrados = new DataTable())
                    {
                        dtLogRegistrados.TableName = "RegistrosLog";
                        dtLogRegistrados.Columns.Add("Fecha");
                        dtLogRegistrados.Columns.Add("Mensaje");
                        dtLogRegistrados.Columns.Add("Imagen");
                        DataRow dtRow = default(DataRow);
                        dtRow = dtLogRegistrados.NewRow();
                        dtRow[0] = System.DateTime.Now;
                        dtRow[1] = Mensaje;
                        dtRow[2] = Imagen;
                        dtLogRegistrados.Rows.Add(dtRow);
                        dtLogRegistrados.WriteXmlSchema(NombreLog + ".xsd");
                        dtLogRegistrados.WriteXml(NombreLog + ".xml");
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(NombreLog + ".xml");
                    using (DataTable dtLogRegistrados = new DataTable())
                    {
                        dtLogRegistrados.TableName = "RegistrosLog";
                        dtLogRegistrados.Columns.Add("Fecha");
                        dtLogRegistrados.Columns.Add("Mensaje");
                        dtLogRegistrados.Columns.Add("Imagen");
                        foreach (DataRow _item in ds.Tables[0].Rows)
                        {
                            DataRow _dtRow = default(DataRow);
                            _dtRow = dtLogRegistrados.NewRow();
                            _dtRow[0] = _item["Fecha"];
                            _dtRow[1] = _item["Mensaje"];
                            _dtRow[2] = _item["Imagen"];
                            dtLogRegistrados.Rows.Add(_dtRow);
                        }
                        DataRow dtRow = default(DataRow);
                        dtRow = dtLogRegistrados.NewRow();
                        dtRow[0] = System.DateTime.Now;
                        dtRow[1] = Mensaje;
                        dtRow[2] = Imagen;
                        dtLogRegistrados.Rows.Add(dtRow);
                        dtLogRegistrados.WriteXmlSchema(NombreLog + ".xsd");
                        dtLogRegistrados.WriteXml(NombreLog + ".xml");
                    }
                }
            }
            catch (Exception ex)
            {
                //   Diagnostics.EventLog.WriteEntry(EventLogIndigo.Source, ex.Message)
            }
        }
        #endregion
        

        #region "Enums"
        public enum TipoImagen
        {
            Informacion = 1, Advertencia = 2, TipoError = 3
        }
        #endregion
        
    }
}
