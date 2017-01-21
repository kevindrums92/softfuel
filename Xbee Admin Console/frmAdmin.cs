using BusinessLayer;
using DataAccess;
using Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XbeeUtils;
using System.ServiceProcess;
using System.Threading;

namespace XbeeAdminConsole
{
    public partial class frmAdmin : Form
    {
        #region Variables Globales
        XbeeSingleton instancia = XbeeSingleton.Instance;
        private Main claseMain = new Main();
        DataTable dtLog;
        BindingSource bindingSource;
        List<ctrCara> ListadoObjetosCaras;
        #endregion

        #region Conocer estado del XAMPP


        private void bwEstadoXamp_DoWork(object sender, DoWorkEventArgs e)
        {
            SaberSiEstaIniciadoXamp();
        }

        private void bwEstadoXamp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Conectar();
            EstablecerPorcentajesProductosGasolina();
        }
        
        public void SaberSiEstaIniciadoXamp()
        {
            //mysql
            ServiceController sc = new ServiceController("mysql");

            var statusService = sc.Status;
            while (statusService != ServiceControllerStatus.Running)
            {
                sc.Refresh();
                statusService = sc.Status;
                
                //AsignarRegistrosRejilla d = new AsignarRegistrosRejilla(RefrescarRejilla);
                //this.Invoke(d, new LogPantalla() { Dispositivo = "", Fecha = DateTime.Now, Mensaje = "Mysql no está iniciado!" });
            }
        }
        #endregion

        #region Constructor
        public frmAdmin()
        {
            InitializeComponent();
            CargarConfiguracionAppConfig();
            EstablecerVersion();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ColocarFecha();
            instancia.NodoAgregadoEvent += NodoAgregadoEventHandler;
            claseMain.MonitoreoEvent += MonitoreoProceso_Main;
            cargarImagenes();
            bwEstadoXamp.RunWorkerAsync();
            

            //NodosXbee _nodoPrueba = new NodosXbee(null, "DISPENSADOR 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 3);
            //NodosXbee _nodoPrueba2 = new NodosXbee(null, "POS 1", "13A20040D29D35", "13A20040D29D35", 0, Enumeraciones.TipoDispositivo.moduloPOS, 2);
            //NodosXbee _nodoPrueba3 = new NodosXbee(null, "POS 2", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.moduloPOS, 4);
            //NodosXbee _nodoPrueba4 = new NodosXbee(null, "DISPENSADOR 2", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 5);
            //NodosXbee _nodoPrueba5 = new NodosXbee(null, "DISPENSADOR 3", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 7);
            //NodosXbee _nodoPrueba6 = new NodosXbee(null, "DISPENSADOR 4", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 8);
            //NodosXbee _nodoPrueba7 = new NodosXbee(null, "DISPENSADOR 5", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 9);

            //instancia.ListNodes = new List<NodosXbee>();
            //instancia.AgregarNodo(_nodoPrueba);
            //instancia.AgregarNodo(_nodoPrueba2);
            //instancia.AgregarNodo(_nodoPrueba3);
            //instancia.AgregarNodo(_nodoPrueba4);
            //instancia.AgregarNodo(_nodoPrueba5);
            //instancia.AgregarNodo(_nodoPrueba6);
            //instancia.AgregarNodo(_nodoPrueba7);


            //string tramaRecibida1 = "M:1:87";
            //string[] arrayTramaRecibida1 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida1);
            //claseMain.ProcesarTrama(arrayTramaRecibida1, _nodoPrueba2);

            //string tramaRecibida1 = "F:2:403b820d";
            //string[] arrayTramaRecibida1 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida1);
            //claseMain.ProcesarTrama(arrayTramaRecibida1, _nodoPrueba2);

            //string tramaRecibida3 = "C:2:04578cfa162280";
            //string[] arrayTramaRecibida3 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida3);
            //claseMain.ProcesarTrama(arrayTramaRecibida3, _nodoPrueba2);

            //string tramaRecibida2 = "ET:2:5139970:38994204:007560:1642918:13253948:7950:0:0:0:";
            //string[] arrayTramaRecibida2 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida2);
            //claseMain.ProcesarTrama(arrayTramaRecibida2, _nodoPrueba);

            //string tramaRecibida2 = "E:2";
            //string[] arrayTramaRecibida2 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida2);
            //claseMain.ProcesarTrama(arrayTramaRecibida2, _nodoPrueba,false);

            //string tramaRecibida4 = "I:2:asd654:654";
            //string[] arrayTramaRecibida4 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida4);
            //claseMain.ProcesarTrama(arrayTramaRecibida4, _nodoPrueba2);
        }
        #endregion

        #region Eventos Mover Ventana

        int ex;

        int ey;

        bool Arrastre;
        //Estas tres subrutinas permiten desplazar el formulario. 

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            ex = e.X;

            ey = e.Y;

            Arrastre = true;

        }


        private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Arrastre = false;

        }


        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Si el formulario no tiene borde (FormBorderStyle = none) la siguiente linea funciona bien


            if (Arrastre)
                this.Location = this.PointToScreen(new Point(Cursor.Position.X - this.Location.X - ex, Cursor.Position.Y - this.Location.Y - ey));

            //pero si quieres dejar los bordes y la barra de titulo entonces es necesario descomentar la siguiente linea y comentar la anterior, osea ponerle el apostrofe

            //If Arrastre Then Me.Location = Me.PointToScreen(New Point(Me.MousePosition.X - Me.Location.X - ex - 8, Me.MousePosition.Y - Me.Location.Y - ey - 60))

        }


        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            var _color = Color.FromArgb(38, 50, 56);
            SolidBrush myBrush = new SolidBrush(_color);
            e.Graphics.FillRectangle(myBrush, Top);
            e.Graphics.FillRectangle(myBrush, Left);
            e.Graphics.FillRectangle(myBrush, Right);
            e.Graphics.FillRectangle(myBrush, Bottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 10; // you can rename this variable if you like

        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        #endregion

        #region Eventos Formulario
        private void SFbtnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea salir?", "TICKETSOFT .NET", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                claseMain.Desconectar();
                Application.Exit();
            }
            
        }
        private void SFbtnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void SFbtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;            
        }
        private void TimerFecha_Tick(object sender, EventArgs e)
        {
            ColocarFecha();
          
        }
        private void SFTimerCambioPrecios_Tick(object sender, EventArgs e)
        {
            if (SFbwCambioPrecio.IsBusy == false)
            {
                SFbwCambioPrecio.RunWorkerAsync();
            }
        }
        private void SFbtnConectar_Click(object sender, EventArgs e)
        {
            Conectar();
        }
        private void SFbtnEscanearRed_Click(object sender, EventArgs e)
        {
            Reconectar();
        }
        private void INDbtnEscanRed_Click(object sender, EventArgs e)
        {
            Reconectar();
        }
        private void SFbtnDesconectar_Click(object sender, EventArgs e)
        {
            Desconectar();
        }
        private void SFbtnMaximizarMinimizar_Click(object sender, EventArgs e)
        {
            if (SFbtnMaximizarMinimizar.Text == "Maximizar")
            {
                ExpandirPanelLogs(true);
            }
            else
            {
                ExpandirPanelLogs(false);
            }
        }
        private void SFbtnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltroRejilla();
        }
        private void SFtxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) AplicarFiltroRejilla();
        }
        private void SFtxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
         {
             if(e.KeyChar == 13)
             {
                 e.Handled = true;
             }
         }
        private void frmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            XbeeUtils.LocalLogManager.EscribeLog("Se cerro la aplicación!", LocalLogManager.TipoImagen.Informacion);
        }
        #endregion

        #region "Eventos Log Tramas"
         public delegate void AsignarRegistrosRejilla(LogPantalla _log);
         public AsignarRegistrosRejilla delegadoRegistrosRejilla;

         public delegate void LevantaBajaMangueraEvent(string cara, int idXbee, bool levanta,string galones, string dinero);
         public LevantaBajaMangueraEvent LevantaoBajaMangueraEvent;

        void RefrescarRejilla(LogPantalla _log)
        {
            if (dtLog == null)
            {
                dtLog = new DataTable();
                dtLog.Columns.Add("Fecha", typeof(DateTime));
                dtLog.Columns.Add("Mensaje", typeof(string));
                dtLog.Columns.Add("Dispositivo", typeof(string));
            }
            else
            {
                if (dtLog.Rows.Count > 600)
                {
                    dtLog.Rows.Clear();
                }
            }
                    

            DataRow NewRow = dtLog.NewRow();
            NewRow["Fecha"] = _log.Fecha;
            NewRow["Mensaje"] = _log.Mensaje;
            NewRow["Dispositivo"] = _log.Dispositivo;
            dtLog.Rows.Add(NewRow);
            if(bindingSource == null)
            {
                bindingSource = new BindingSource();
                bindingSource.DataSource = dtLog;
                bindingSource.Sort = "Fecha Desc";
                this.SFGridLog.DataSource = bindingSource;
            }
            else
	        {
                bindingSource.ResetBindings(false);
                if (this.SFGridLog.Rows.Count > 0)
                {
                    this.SFGridLog.Rows[0].Selected = true;
                    this.SFGridLog.CurrentCell = this.SFGridLog.Rows[0].Cells[0];
                }
	        }
        }

        void MonitoreoProceso_Main(object sender, MonitoreoEventArgs e)
        {
            bool TramaVentaManguera = false;
            bool Levanta = false;
            string galones = "";
            string dinero = "";

            if (e.Texto == "LEVANTAMANGUERA")
            {
                TramaVentaManguera = true;
                Levanta = true;
            }
            if (e.Texto.StartsWith("BAJAMANGUERA"))
            {
                string[] arrayMensaje = UtilidadesTramas.ObtieneArrayTrama(e.Texto);
                if (arrayMensaje.Count() == 3)
                {
                    galones = arrayMensaje[1];
                    dinero = arrayMensaje[2];
                }
                TramaVentaManguera = true;
                Levanta = false;
            }
            if (TramaVentaManguera == true)
            {
                if (this.InvokeRequired == true)
                {
                    LevantaBajaMangueraEvent d = new LevantaBajaMangueraEvent(LevantaoBajaManguera);
                    this.Invoke(d, e.Cara, e.IdXbee, Levanta, galones, dinero);
                }
                else
                {
                    LevantaoBajaManguera(e.Cara, e.IdXbee, Levanta,galones,dinero);
                }
                return;
            }
            

            LogPantalla newLog = new LogPantalla();
            newLog.Mensaje = e.Texto;
            newLog.Fecha = DateTime.Now;
            newLog.Dispositivo = e.Dispositivo;
            if (this.SFGridLog.InvokeRequired == true)
            {
                AsignarRegistrosRejilla d = new AsignarRegistrosRejilla(RefrescarRejilla);
                this.Invoke(d, newLog);
            }
            else
            {
                RefrescarRejilla(newLog);
            }
        }
        #endregion 

        #region Metodos
        
        void Conectar()
        {
            claseMain.ConectaryDescubrirRed();
            SFTimerCambioPrecios.Enabled = true;
            SFTimerCambioPrecios.Start();
        }

        void Desconectar()
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea desconectar la red?", "SOFTFUEL .NET", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                claseMain.Desconectar();
                SFTimerCambioPrecios.Enabled = false;
                SFTimerCambioPrecios.Stop();
                LimpiarDispositivos();
            }
        }

        void Reconectar()
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea escanear de nuevo la red?", "SOFTFUEL .NET", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                claseMain.Desconectar();
                claseMain.ConectaryDescubrirRed();
            }
        }

        void LimpiarDispositivos()
        {
            if (ListadoObjetosCaras != null) ListadoObjetosCaras.Clear();
            //SFPanelCara1.Controls.Clear();
            //SFPanelCara2.Controls.Clear();
            //SFPanelCara3.Controls.Clear();
            //SFPanelCara4.Controls.Clear();
            //SFPanelCara5.Controls.Clear();
            //SFPanelCara6.Controls.Clear();
            //SFPanelCara7.Controls.Clear();
            //SFPanelCara8.Controls.Clear();
            //SFPanelPOS1.Controls.Clear();
            //SFPanelPOS2.Controls.Clear();
            //SFPanelPOS3.Controls.Clear();
            //SFPanelPOS4.Controls.Clear();
        }

        void NodoAgregadoEventHandler(NodosXbee e)
        {
            if (e.TipoDispositivo == XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador)
            {
                DataTable dtCaras;
                using (Generales modGEN = new Generales())
                {
                    dtCaras = modGEN.GetTable("select DISTINCT numPosicion FROM posicion WHERE idXbee = " + e.IdXbee);
                }
                if (dtCaras != null && dtCaras.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCaras.Rows) {
                        if (ListadoObjetosCaras == null) ListadoObjetosCaras = new List<ctrCara>();
                        ctrCara newCara1 = new ctrCara();
                        newCara1.NumCara = Convert.ToInt32(row[0]);
                        newCara1.EstadoCara = EnumEstadoCara.Normal;
                        newCara1.NombreCara = "Cara " + newCara1.NumCara.ToString();
                        newCara1.idXbee = e.IdXbee;
                        newCara1.NombreNodo = e.Nombre;
                        FloatPanelDispositivos.Controls.Add(newCara1);
                        ListadoObjetosCaras.Add(newCara1);
                    }
                }
            }
            else if (e.TipoDispositivo == XbeeUtils.Enumeraciones.TipoDispositivo.moduloPOS)
            {
                using (Generales modGEN = new Generales())
                {
                    DataTable dtPOS = modGEN.GetTable("select nomXbee FROM xbee WHERE idXbee = " + e.IdXbee);
                    if (dtPOS != null && dtPOS.Rows.Count > 0)
                    {
                        ctrPOS newPOS = new ctrPOS();
                        newPOS.NombrePOS = dtPOS.Rows[0][0].ToString();
                        newPOS.idXbee = e.IdXbee;
                        FloatPanelDispositivos.Controls.Add(newPOS);
                        newPOS.Dock = DockStyle.Fill;
                        newPOS.EstableceColor();

                    }
                }
            }
        }

        private Panel FindPanel(Panel parent, string ctlName)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl.Name.Equals(ctlName))
                {
                    return (Panel)ctl;
                }

                //FindPanel((Panel)ctl, ctlName);
            }
            return null;
        }
        
        void ColocarFecha()
        {
            SFlbHora.Text = DateTime.Now.ToLongTimeString();
            SFlbFechaHora.Text = DateTime.Now.ToLongDateString();
        }

        void CargarConfiguracionAppConfig()
        {
            instancia.SqlServidor = ConfigurationManager.AppSettings["servidor"];
            instancia.SqlUsuario = ConfigurationManager.AppSettings["usuario"];
            instancia.SqlPassword = ConfigurationManager.AppSettings["password"];
            instancia.SqlBaseDatos = ConfigurationManager.AppSettings["bd"];
            instancia.TiempoSegundosDescubriendoRed = Convert.ToDouble(ConfigurationManager.AppSettings["tiempoSegundosDescubriendoRed"]);
            if (instancia.TiempoSegundosDescubriendoRed == 0) instancia.TiempoSegundosDescubriendoRed = 5;
            instancia.ImpresionTramaMaxima = Convert.ToBoolean(ConfigurationManager.AppSettings["impresionTramaMaxima"]);
            instancia.DelayAntesDeEnviarTrama = Convert.ToInt32(ConfigurationManager.AppSettings["delayAntesDeEnviarTrama"]);
            
        }

        void ExpandirPanelLogs(bool expandir)
        {
            if (expandir == true)
            {
                this.TypDispensadoresRejilla.RowStyles[0].Height = 0F;
                this.TypDispensadoresRejilla.RowStyles[1].SizeType = SizeType.Percent;
                this.TypDispensadoresRejilla.RowStyles[1].Height = 100F;
                SFbtnMaximizarMinimizar.Text = "Minimizar";
            }
            else
            {
                this.TypDispensadoresRejilla.RowStyles[0].Height = 100F;
                this.TypDispensadoresRejilla.RowStyles[1].SizeType = SizeType.Absolute;
                this.TypDispensadoresRejilla.RowStyles[1].Height = 40;
                SFbtnMaximizarMinimizar.Text = "Maximizar";
            }
        }

        void AplicarFiltroRejilla()
        {
            if (dtLog != null)
            {
                string searchValue = SFtxtBuscar.Text;
                dtLog.DefaultView.RowFilter = "Mensaje like '%" + searchValue + "%'";
                SFGridLog.Refresh();
            }
        }

        void LevantaoBajaManguera(string cara, int idXbee,bool levanta,string galones,string dinero)
        {
            ctrCara Ctrcara = ListadoObjetosCaras.Find(x => x.NumCara == Convert.ToInt32(cara) && x.idXbee == idXbee);
            if (Ctrcara != null)
            {
                if (levanta == true)
                {
                    Ctrcara.EstadoCara = EnumEstadoCara.Atendiendo;
                    Ctrcara.Galones = "0";
                    Ctrcara.Dinero = "0";
                }
                else
                {
                    Ctrcara.EstadoCara = EnumEstadoCara.Normal;
                    if (galones != "" && galones != "0")
                    {
                        Ctrcara.Galones = galones;
                        Ctrcara.Dinero = dinero;
                    }
                    EstablecerPorcentajesProductosGasolina();
                }
            }
        }

        

        void cargarImagenes()
        {
           
            System.Drawing.Bitmap imagenIcono = Properties.Resources.logo_ticket_soft;
            Bitmap objimagenIcono = new Bitmap(imagenIcono, SFLogo.Size);
            SFLogo.Image = objimagenIcono;
        }

        void EstablecerVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = "Versión: "+  fvi.FileVersion;
            SFVersion.Text = version;
        }

        /// <summary>
        /// Metodo que establece el porcentaje de existencia de gasolina segun capacidad y dibuja los valores en los controles de la vista
        /// </summary>
        void EstablecerPorcentajesProductosGasolina()
        {
            if (SFbwConsultaPorcentajesGasolina.IsBusy == false)
            {
                SFbwConsultaPorcentajesGasolina.RunWorkerAsync();
            }
        }

        #endregion

        #region BackgroundWorkerCambioPrecio
        private void INDbwCambioPrecio_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var dtTramasManuales = claseMain.GetTramasManuales();

                foreach(DataRow _row in dtTramasManuales.Rows)
                {
                    //Obtener Id de xbee y trama
                    string[] LineRecibida = _row[1].ToString().Split('|');

                    var _trama = LineRecibida[0];
                    var _idXbeeImprimir = LineRecibida[1];

                    NodosXbee nodeXbee = instancia.ListNodes.FindAll(x => x.IdXbee == Convert.ToInt32(_idXbeeImprimir)).FirstOrDefault();
                    if (nodeXbee != null)
                    {
                        string[] arrayTramaRecibida = UtilidadesTramas.ObtieneArrayTrama(_trama);
                        claseMain.ProcesarTrama(arrayTramaRecibida, nodeXbee, true);
                        //NodosXbee nodoImpresion = instancia.ListNodes.Find(item => item.Mac == nodeXbee.MacImpresion);
                        //if (nodoImpresion != null)
                        //{
                        //    claseMain.ProcesarTrama(arrayTramaRecibida, nodoImpresion);
                        //}
                    }
                }
                

                string cambioPrecio = System.IO.File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + "/cambioPrecio.txt");
                if (cambioPrecio.StartsWith("1"))
                {
                    if (ListadoObjetosCaras != null)
                    {
                        List<int> ListaXbess = new List<int>();
                        foreach (ctrCara _cara in ListadoObjetosCaras)
                        {
                            if (ListaXbess.Find(item => item == _cara.idXbee) == 0)
                            {
                                ListaXbess.Add(_cara.idXbee);
                                claseMain.CambioPrecioDispensador(_cara.idXbee);
                            }
                        }
                        e.Result = true;
                    }
                    else
                    {
                        e.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LocalLogManager.EscribeLog("Ocurrio Un Error Con Cambio de Precio o con tramas POS manuales" + ex.Message, LocalLogManager.TipoImagen.TipoError);
                e.Result = false;
            }
        }

        private void INDbwCambioPrecio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            if (Convert.ToBoolean(e.Result) == true)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "/cambioPrecio.txt");
                sw.WriteLine("0");
                sw.Close();
            }
            else
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "/cambioPrecio.txt");
                sw.WriteLine("1");
                sw.Close();
            }
        }
        #endregion

        #region BackgroundWorker Porcentaje Gasolina
        private void SFbwConsultaPorcentajesGasolina_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dtProductos;
            using (Generales modGen = new Generales())
            {
                dtProductos = modGen.ObtenerProductosGasolina();
            }
            e.Result = dtProductos;
        }

        private void SFbwConsultaPorcentajesGasolina_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                try
                {
                    DataTable dtProductos = (DataTable)e.Result;
                    if (dtProductos.Rows.Count > 0)
                    {
                        //ACPM
                        if (dtProductos.Select("tipoProducto = 1").Count() > 0)
                        {
                            DataRow _rowAcpm = dtProductos.Select("tipoProducto = 1")[0];
                            SFpgACPM.Maximum = Convert.ToInt32(_rowAcpm["capacidad"]);
                            if (Convert.ToInt32(Convert.ToDecimal(_rowAcpm["existenciaProducto"])) >= 0)
                            {
                                SFpgACPM.Value = Convert.ToInt32(Convert.ToDecimal(_rowAcpm["existenciaProducto"]));
                            }
                        }

                        //Corriente
                        if (dtProductos.Select("tipoProducto = 2").Count() > 0)
                        {
                            DataRow _rowCorriente = dtProductos.Select("tipoProducto = 2")[0];
                            SFpgCorriente.Maximum = Convert.ToInt32(_rowCorriente["capacidad"]);
                            if (Convert.ToInt32(Convert.ToDecimal(_rowCorriente["existenciaProducto"])) >= 0)
                            {
                                SFpgCorriente.Value = Convert.ToInt32(Convert.ToDecimal(_rowCorriente["existenciaProducto"]));
                            }
                        }

                        //Extra
                        if (dtProductos.Select("tipoProducto = 3").Count() > 0)
                        {
                            DataRow _rowExtra = dtProductos.Select("tipoProducto = 3")[0];
                            SFpgExtra.Maximum = Convert.ToInt32(_rowExtra["capacidad"]);
                            SFpgExtra.Value = Convert.ToInt32(_rowExtra["existenciaProducto"]);
                        }

                    }
                }
                catch (Exception)
                {
                    
                }
                
            }
        }
        #endregion 

    }

    public struct LogPantalla
    {
        public string Mensaje { get; set; }
        public string Dispositivo { get; set; }
        public DateTime Fecha { get; set; }
    }
 
}
