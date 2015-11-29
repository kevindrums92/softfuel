using MySql.Data.MySqlClient;
using Singleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using XbeeUtils;

namespace DataAccess
{
    public class Connection : IDisposable
    {
        #region "Variables"
        XbeeSingleton instancia = XbeeSingleton.Instance;
        public MySqlConnection myconn;
        #endregion

        #region "Builder"
        internal Connection()
        {
            try
            {
                string connMysql = "DataSource=" + instancia.SqlServidor + "; Database=" + instancia.SqlBaseDatos + "; Uid=" + instancia.SqlUsuario
                + "; Pwd=" + instancia.SqlPassword + ";Convert Zero Datetime=True";
                myconn = new MySqlConnection(connMysql);
            }
            catch (Exception ex)
            {
                LocalLogManager.EscribeLog(ex.Message, LocalLogManager.TipoImagen.TipoError);
                throw ex;
            }
        }
        #endregion

        #region "Procedures"
        public DataTable GetTable(string sql)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                if (myconn.State != System.Data.ConnectionState.Open)
                {
                    myconn.Open();
                }

                MySqlCommand _sqlCommand = new MySqlCommand(sql, myconn);
                _sqlCommand.ExecuteNonQuery();

                MySqlDataAdapter _da = new MySqlDataAdapter(_sqlCommand);
                _da.Fill(dtReturn);

                MySqlCommandBuilder _cb = new MySqlCommandBuilder(_da);

                return dtReturn;
            }
            catch (MySqlException ex)
            {
                //Capturo el error en caso de haberlo
                LocalLogManager.EscribeLog(ex.Message, LocalLogManager.TipoImagen.TipoError);
                throw ex;
            }
            finally
            {
                if (myconn != null) myconn.Close();
            }
        }

        /// <summary>
        /// Ejecuta un Query por ejemplo insert, update, delete
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteQuery(string sql)
        {
            try
            {
                if (myconn.State != System.Data.ConnectionState.Open)
                {
                    myconn.Open();
                }
                MySqlCommand _sqlCommand = new MySqlCommand(sql, myconn);
                MySqlDataReader _sqlReader;
                _sqlReader = _sqlCommand.ExecuteReader();

                return true;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                throw e;
            }
            finally
            {
                if (myconn != null) myconn.Close();
            }
        }

        /// <summary>
        /// Ejecuta un Query por ejemplo insert, update, delete en transacción
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteQuery(string[] sql)
        {
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    if (myconn.State != System.Data.ConnectionState.Open)
                    {
                        myconn.Open();
                    }
                    foreach (string _cadaSql in sql)
                    {
                        MySqlCommand _sqlCommand = new MySqlCommand(_cadaSql, myconn);
                        MySqlDataReader _sqlReader;
                        _sqlReader = _sqlCommand.ExecuteReader();
                    }
                    tran.Complete();
                    return true;
                }
                
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                throw e;
            }
            finally
            {
                if (myconn != null) myconn.Close();
            }
        }

        public long ExecuteQueryLastInsert(string sql)
        {
            try
            {
                if (myconn.State != System.Data.ConnectionState.Open)
                {
                    myconn.Open();
                }
                MySqlCommand _sqlCommand = new MySqlCommand(sql, myconn);
                MySqlDataReader _sqlReader;
                _sqlReader = _sqlCommand.ExecuteReader();
                return _sqlCommand.LastInsertedId;
            }
            catch (Exception e)
            {
                LocalLogManager.EscribeLog(e.Message, LocalLogManager.TipoImagen.TipoError);
                throw e;
            }
            finally
            {
                if (myconn != null) myconn.Close();
            }
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
}
