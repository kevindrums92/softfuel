﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using XbeeUtils;

namespace DataAccess
{
    public class ModeloPOS: Connection, IDisposable
    {

        #region "Consignaciones"
        public bool GuardaConsignacion(string idUsuario, string valorConsig )
        {
            string _FechaActual = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            return ExecuteQuery("insert into consignaciones (valorConsig, fechaConsig, idUsuario) values ('" + valorConsig + "','" + _FechaActual + "'," +
                idUsuario +")");

        }       
        #endregion

        #region "Abrir Turnos"

        public DataTable EstaTurnoAbiertoPorIdXbee(string idXbee)
        {
            return GetTable("select * from posicion where idXbee = " + idXbee + " and estadoPosicion = 'activo'");
        }

        public DataTable ObtieneInformacionCara(string cara)
        {
            return GetTable("select idPosicion,numPosicion,idProducto,mangPosicion,idXbee,estadoPosicion from posicion where numPosicion = " + cara + "");
        }

        /// <summary>
        /// Obtiene un turno por cara
        /// </summary>
        /// <param name="cara"></param>
        /// <returns></returns>
        public DataTable ObtenerTurnoPorCara(string cara)
        {
            DataTable posicion = GetTable("select idPosicion, x.macXbee,p.idProducto, p.idXbee from posicion as p inner join xbee as x on p.idXbee = x.idXbee where p.numPosicion = " + cara + " limit 1");
            if (posicion.Rows.Count > 0)
            {
                DataTable turno = GetTable("select idTurno, idUsuario, abrirTurno, cerrarTurno, idPosicion, estadoTurno, '' as idXbee from turno where idPosicion = " + posicion.Rows[0]["idPosicion"].ToString() + " and estadoTurno = 'activo'");
                if (turno.Rows.Count>0)
                {
                    turno.Rows[0]["idXbee"] = posicion.Rows[0]["idXbee"];
                    return turno;
                }
                else
                {
                    return new DataTable();
                }   
            }
            else
            {
                return new DataTable();
            }
            
        }

        /// <summary>
        /// Obtiene turno por posicion y estado
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns></returns>
        public DataTable ObtenerTurnoPorPosicionyEstado(string posicion)
        {
            return GetTable("select * from turno where idPosicion = " + posicion + " and estadoTurno = 'activo'");
        }

        /// <summary>
        /// Obtiene una de las posiciones del dispensador por cara
        /// </summary>
        /// <param name="cara"></param>
        /// <returns></returns>
        public DataTable ObtenerPosicionesPorCara(string cara)
        {
            return GetTable("select idPosicion, x.macXbee,p.idProducto, p.idXbee from posicion as p inner join xbee as x on p.idXbee = x.idXbee where p.numPosicion = " + cara + " limit 1");
        }

        /// <summary>
        /// Obtiene una de las posiciones del dispensador por cara
        /// </summary>
        /// <param name="cara"></param>
        /// <returns></returns>
        public DataTable ObtenerPosicionesPorCarayManguera(string cara,string manguera)
        {
            return GetTable("select idPosicion, x.macXbee,p.idProducto, p.idXbee from posicion as p inner join xbee as x on p.idXbee = x.idXbee where p.numPosicion = " + cara + " AND p.mangPosicion = '" + manguera + "'  limit 1");
        }
        /// <summary>
        /// Obtiene los totales de ventas por cara
        /// </summary>
        /// <param name="cara"></param>
        /// <returns></returns>
        public DataTable ObtenerTotalesVentaPorCara(string cara)
        {
            return GetTable("SELECT id, m1,g1,p1,m2,g2,p2,m3,g3,p3,idXbee FROM ventatotal WHERE cara = " + cara + " order by id desc limit 1");
        }

        /// <summary>
        /// Guardo una apertura de turno
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="posicion"></param>
        /// <param name="fecha"></param>
        /// <param name="idVentas"></param>
        /// <returns></returns>
        public long GuardarAperturaTurno(string usuario, string posicion,string fecha,int idVentas)
        {
            long Autonumerico = ExecuteQueryLastInsert("insert into turno (idUsuario,abrirTurno,cerrarTurno,idPosicion,estadoTurno) values(" + usuario + ",'" + fecha + "','" + fecha + "'," + posicion + ",'activo')");
            if (Autonumerico > 0)
            {
                ExecuteQuery("insert into ventaturno (idTurno,idVentaAbrir) values(" + Autonumerico + "," + idVentas + ")");
                ExecuteQuery("update posicion set estadoPosicion = 'activo' where idPosicion = " + posicion + "");
                return Autonumerico;
            }
            else
            {
                return 0;
            }
            
        }


        #endregion

        #region "Cerrar Turno"
        public bool GuardaCerrarTurno(string idTurno, string idPosicion, string cara, string fechaActual)
        {
            DataTable dtUltimaVenta = ObtenerTotalesVentaPorCara(cara);
            if (dtUltimaVenta.Rows.Count == 0) return false;
            string sqlUpdateVentaTurno = "update ventaturno set idVentaCerrar = " + Convert.ToInt32(dtUltimaVenta.Rows[0][0]) + " where idTurno = " + idTurno + "";
            string sqlUpdateTurno = "update turno set cerrarTurno = '" + fechaActual + "' , estadoTurno = 'inactivo' where idTurno = " + idTurno + "";
            string sqlUpdatePosicion = "update posicion set estadoPosicion = 'inactivo' where idPosicion = " + idPosicion;

            ExecuteQuery(sqlUpdateVentaTurno);
            ExecuteQuery(sqlUpdateTurno);
            ExecuteQuery(sqlUpdatePosicion);
            return true;
        }

        public VentasPorTurno ObtenerDatosVentaPorIdTurno(string idTurno)
        {
            VentasPorTurno newVentas = new VentasPorTurno();
            DataTable dtVentas = GetTable("select U.nomUsuario,U.apeUsuario, VT.idVentaAbrir, VT.idVentaCerrar,T.abrirTurno, T.cerrarTurno from ventaturno as VT INNER JOIN turno AS T ON VT.idTurno = T.idTurno INNER JOIN usuario AS U ON U.idUsuario = T.idUsuario where VT.idTurno = " + idTurno + "");
            if (dtVentas.Rows.Count == 0) throw new Exception("No se pudo obtener información del turno con consecutivo " + idTurno);
            DataTable dtVentasTotales = GetTable("SELECT * FROM ventatotal WHERE id = " + Convert.ToInt32(dtVentas.Rows[0]["idVentaAbrir"]) + " union all SELECT * FROM ventatotal WHERE id = " + Convert.ToInt32(dtVentas.Rows[0]["idVentaCerrar"]) + "");
            newVentas.Cara = dtVentasTotales.Rows[0]["cara"].ToString();
            string fechaInicioTurno = Convert.ToDateTime(dtVentas.Rows[0]["abrirTurno"]).ToString("yyyy-MM-dd H:mm:ss");
            string fechaFinTurno = Convert.ToDateTime(dtVentas.Rows[0]["cerrarTurno"]).ToString("yyyy-MM-dd H:mm:ss");
            DataTable dtVentasProductos = GetTable("select V.idVenta, V.idProducto, V.precio, P.precioventaProducto,   V.galones as Cantidad from ventas as V inner join producto AS P on P.idProducto = V.idProducto " +
                            "where V.cara = '" + newVentas.Cara + "' and V.fecha between '" + fechaInicioTurno + "' and '" + fechaFinTurno + "' and manguera is NULL ");
            newVentas.Usuario = dtVentas.Rows[0]["nomUsuario"].ToString() + ' ' + dtVentas.Rows[0]["apeUsuario"].ToString();
            newVentas.NumTurno = idTurno;
            newVentas.Fecha = Convert.ToDateTime(fechaFinTurno);

            newVentas.TotalDineroMang1 = Convert.ToInt32(dtVentasTotales.Rows[1]["p1"]) - Convert.ToInt32(dtVentasTotales.Rows[0]["p1"]);
            newVentas.TotalDineroMang2 = Convert.ToInt32(dtVentasTotales.Rows[1]["p2"]) - Convert.ToInt32(dtVentasTotales.Rows[0]["p2"]);
            newVentas.TotalDineroMang3 = Convert.ToInt32(dtVentasTotales.Rows[1]["p3"]) - Convert.ToInt32(dtVentasTotales.Rows[0]["p3"]);
            newVentas.TotalGalonesMang1 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g1"]) - Convert.ToDecimal(dtVentasTotales.Rows[0]["g1"]);
            newVentas.TotalGalonesMang2 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g2"]) - Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]);
            newVentas.TotalGalonesMang3 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g3"]) - Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]);

            newVentas.TotalCaraDin = (newVentas.TotalDineroMang1 + newVentas.TotalDineroMang2 + newVentas.TotalDineroMang3).ToString();
            newVentas.TotalCaraGal = (newVentas.TotalGalonesMang1 + newVentas.TotalGalonesMang2 + newVentas.TotalGalonesMang3).ToString();
            newVentas.TotalCredTran = "0";
            newVentas.TotalCredDin = "";
            newVentas.TotalCredGal = "";

            newVentas.TotalProdTran = "0";
            newVentas.TotalProdDin = "";
            newVentas.TotalProdCant = "";
            int sumaCantidadesVentaProductos = 0;
            int sumaValorVentaProductos = 0;

            if (dtVentasProductos.Rows.Count>0)
            {
                newVentas.TotalProdTran = dtVentasProductos.Rows.Count.ToString();
                sumaCantidadesVentaProductos = Convert.ToInt32(dtVentasProductos.Compute("Sum(Cantidad)",""));
                newVentas.TotalProdCant = sumaCantidadesVentaProductos.ToString();
                sumaValorVentaProductos = Convert.ToInt32(dtVentasProductos.Compute("Sum(precio)", ""));
                newVentas.TotalProdDin = sumaValorVentaProductos.ToString();
            }

            newVentas.TotalEfectivo = (Convert.ToInt32(newVentas.TotalCaraDin) + sumaValorVentaProductos).ToString();

            newVentas.IniDineroMang1 = Convert.ToInt32(dtVentasTotales.Rows[0]["p1"]);
            newVentas.IniDineroMang2 = Convert.ToInt32(dtVentasTotales.Rows[0]["p2"]);
            newVentas.IniDineroMang3 = Convert.ToInt32(dtVentasTotales.Rows[0]["p3"]);
            newVentas.IniGalMang1 = Convert.ToDecimal(dtVentasTotales.Rows[0]["g1"]);
            newVentas.IniGalMang2 = Convert.ToDecimal(dtVentasTotales.Rows[0]["g2"]);
            newVentas.IniGalMang3 = Convert.ToDecimal(dtVentasTotales.Rows[0]["g3"]);
            newVentas.FinDineroMang1 = Convert.ToInt32(dtVentasTotales.Rows[1]["p1"]);
            newVentas.FinDineroMang2 = Convert.ToInt32(dtVentasTotales.Rows[1]["p2"]);
            newVentas.FinDineroMang3 = Convert.ToInt32(dtVentasTotales.Rows[1]["p3"]);
            newVentas.FinGalMang1 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g1"]);
            newVentas.FinGalMang2 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g2"]);
            newVentas.FinGalMang3 = Convert.ToDecimal(dtVentasTotales.Rows[1]["g3"]);

            return newVentas;
        }
        #endregion

        #region "Ultima Venta"
        /// <summary>
        /// Obtengo datos de ultima venta por cara
        /// </summary>
        /// <param name="cara"></param>
        /// <returns></returns>
        public DataTable ObtenerUltimaVentaPorCara(string cara)
        {
            return GetTable("select idVenta from ventas where cara = '" + cara + "' ORDER by fecha desc limit 1");
        }

        public DataTable ObtenerUltimaVentaPorId(string id)
        {
            return GetTable("select idVenta, V.idProducto, cara, manguera, precio, galones,ppu, fecha, islero, P.nomProducto,U.nomUsuario,U.apeUsuario,IF(V.placa IS NULL,'',V.placa) AS placa, IF(V.kilometraje IS NULL,'',V.kilometraje) AS kilometraje from ventas AS V INNER JOIN producto AS P ON P.idProducto = V.idProducto INNER JOIN usuario AS U ON V.islero = U.idUsuario where V.idVenta = " + id + "");
        }

        /// <summary>
        /// Actualizo placa y kilometraje a ultima venta por idVenta
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="km"></param>
        /// <param name="idVenta"></param>
        /// <returns></returns>
        public bool ActualizarPlacaKmUltimaVenta(string placa, string km,string idVenta)
        {
            return ExecuteQuery("update ventas set placa = '" + placa + "', kilometraje = '" + km + "' where idVenta = " + idVenta);
        }
        #endregion

        #region Productos
        public DataTable ObtenerProductoPorId(string idProducto)
        {
            string campos = "idProducto, codProducto, nomProducto, idTipoProducto, idProveedor, existenciaProducto, preciocostoProducto," +
            " precioventaProducto, estadoProducto";
            return GetTable("select " + campos + " from producto where idProducto = " + idProducto + "");
        }

        public bool GuardaVentaCanasta(string idProducto, string cara, string dinero, string fecha, string islero, string xbee,int cantidadProducto)
        {

            string sqlInsertIntoCanasta = "insert into ventas (idProducto, cara,  precio, fecha, islero, idXbee, galones) values (" + idProducto + "," + cara + "," + dinero + ",'" + fecha + "','" + islero + "'," + xbee + "," + cantidadProducto + ")";
            string sqlUpdate = "update producto set existenciaProducto = existenciaProducto - " + cantidadProducto + " where idProducto = " + idProducto;
            //string[] sqlInserts = new string[] { sqlInsertIntoCanasta, sqlUpdate };
            //return ExecuteQuery(sqlInserts);
            if (ExecuteQuery(sqlInsertIntoCanasta) == true)
            {
                return ExecuteQuery(sqlUpdate);
            }
            else
            {
                return false;
            }
        }

        
        #endregion

        #region "Cabecera y pie de pagina reportes"
        public EstructuraTiquete ObtenerDefinicionTiquete()
        {
            EstructuraTiquete tiquete = new EstructuraTiquete();
            tiquete.Cab1 = "";
            tiquete.Cab2 = "";
            tiquete.Cab3 = "";
            tiquete.Cab4 = "";
            tiquete.Cab5 = "";
            tiquete.Cab6 = "";
            tiquete.Pie1 = "";
            tiquete.Pie2 = "";
            tiquete.Pie3 = "";
            tiquete.Pie4 = "";
            tiquete.Pie5 = "";
            tiquete.Pie6 = "";
            DataTable dtTiquete = GetTable("select idTiquete, encTiquete1, encTiquete2, encTiquete3, encTiquete4, encTiquete5, encTiquete6, pieTiquete1, pieTiquete2, pieTiquete3, pieTiquete4, pieTiquete5, pieTiquete6 from tiquete where estadoTiquete = 'activo'");
            if (dtTiquete.Rows.Count > 0)
            {
                DataRow _row = dtTiquete.Rows[0];
                if (object.Equals(_row["encTiquete1"], DBNull.Value) == false) tiquete.Cab1 = _row["encTiquete1"].ToString();
                if (object.Equals(_row["encTiquete2"], DBNull.Value) == false) tiquete.Cab2 = _row["encTiquete2"].ToString();
                if (object.Equals(_row["encTiquete3"], DBNull.Value) == false) tiquete.Cab3 = _row["encTiquete3"].ToString();
                if (object.Equals(_row["encTiquete4"], DBNull.Value) == false) tiquete.Cab4 = _row["encTiquete4"].ToString();
                if (object.Equals(_row["encTiquete5"], DBNull.Value) == false) tiquete.Cab5 = _row["encTiquete5"].ToString();
                if (object.Equals(_row["encTiquete6"], DBNull.Value) == false) tiquete.Cab6 = _row["encTiquete6"].ToString();
                if (object.Equals(_row["pieTiquete1"], DBNull.Value) == false) tiquete.Pie1 = _row["pieTiquete1"].ToString();
                if (object.Equals(_row["pieTiquete2"], DBNull.Value) == false) tiquete.Pie2 = _row["pieTiquete2"].ToString();
                if (object.Equals(_row["pieTiquete3"], DBNull.Value) == false) tiquete.Pie3 = _row["pieTiquete3"].ToString();
                if (object.Equals(_row["pieTiquete4"], DBNull.Value) == false) tiquete.Pie4 = _row["pieTiquete4"].ToString();
                if (object.Equals(_row["pieTiquete5"], DBNull.Value) == false) tiquete.Pie5 = _row["pieTiquete5"].ToString();
                if (object.Equals(_row["pieTiquete6"], DBNull.Value) == false) tiquete.Pie6 = _row["pieTiquete6"].ToString();
            }

            return tiquete;
        }
        #endregion

        #region "IDisposable"
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
        }
        #endregion
        
    }

    #region Estructuras
    /// <summary>
    /// Estructura de las ventas de un turno
    /// </summary>
    public struct VentasPorTurno
    {
        public string Cara { get; set; }
        public string Usuario { get; set; }
        public string NumTurno { get; set; }
        public DateTime Fecha { get; set; }

        public int TotalDineroMang1 { get; set; }
        public decimal TotalGalonesMang1 { get; set; }
        public int TotalDineroMang2 { get; set; }
        public decimal TotalGalonesMang2 { get; set; }
        public int TotalDineroMang3 { get; set; }
        public decimal TotalGalonesMang3 { get; set; }

        public string TotalCaraDin { get; set; }
        public string TotalCaraGal { get; set; }
        public string TotalCredTran { get; set; }
        public string TotalCredDin { get; set; }
        public string TotalCredGal { get; set; }
        public string TotalProdTran { get; set; }
        public string TotalProdDin { get; set; }
        public string TotalProdCant { get; set; }
        public string TotalEfectivo { get; set; }

        public int IniDineroMang1 { get; set; }
        public decimal IniGalMang1 { get; set; }
        public int IniDineroMang2 { get; set; }
        public decimal IniGalMang2 { get; set; }
        public int IniDineroMang3 { get; set; }
        public decimal IniGalMang3 { get; set; }

        public int FinDineroMang1 { get; set; }
        public decimal FinGalMang1 { get; set; }
        public int FinDineroMang2 { get; set; }
        public decimal FinGalMang2 { get; set; }
        public int FinDineroMang3 { get; set; }
        public decimal FinGalMang3 { get; set; }


    }

    /// <summary>
    /// Estructura de un tiquete
    /// </summary>
    public struct EstructuraTiquete
    {
        public string Cab1 { get; set; }
        public string Cab2 { get; set; }
        public string Cab3 { get; set; }
        public string Cab4 { get; set; }
        public string Cab5 { get; set; }
        public string Cab6 { get; set; }
        public string Pie1 { get; set; }
        public string Pie2 { get; set; }
        public string Pie3 { get; set; }
        public string Pie4 { get; set; }
        public string Pie5 { get; set; }
        public string Pie6 { get; set; }
    }
    #endregion

    
}
