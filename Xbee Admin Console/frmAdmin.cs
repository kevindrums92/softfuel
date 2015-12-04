using BusinessLayer;
using DataAccess;
using Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XbeeUtils;

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

        #region Constructor
        public frmAdmin()
        {
            InitializeComponent();
            CargarConfiguracionMysql();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ColocarFecha();
            instancia.NodoAgregadoEvent += NodoAgregadoEventHandler;
            claseMain.MonitoreoEvent += MonitoreoProceso_Main;
            cargarImagenes();

            NodosXbee _nodoPrueba = new NodosXbee(null, "DISPENSADOR 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 3);
            NodosXbee _nodoPrueba2 = new NodosXbee(null, "DISPENSADOR 2", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.moduloPOS, 5);
            instancia.ListNodes = new List<NodosXbee>();
            instancia.AgregarNodo(_nodoPrueba);
            instancia.AgregarNodo(_nodoPrueba2);


            //string tramaRecibida1 = "C:2:30f98b0d";
            //string[] arrayTramaRecibida1 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida1);
            //claseMain.ProcesarTrama(arrayTramaRecibida1, _nodoPrueba2);

            //string tramaRecibida2 = "ET:2:5129970:38974204:007560:1642918:13253948:7950:0:0:0:";
            //string[] arrayTramaRecibida2 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida2);
            //claseMain.ProcesarTrama(arrayTramaRecibida2, _nodoPrueba);

            string tramaRecibida1 = "I:2:asd654:654";
            string[] arrayTramaRecibida1 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida1);
            claseMain.ProcesarTrama(arrayTramaRecibida1, _nodoPrueba2);
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
            e.Graphics.FillRectangle(Brushes.DarkGray, Top);
            e.Graphics.FillRectangle(Brushes.DarkGray, Left);
            e.Graphics.FillRectangle(Brushes.DarkGray, Right);
            e.Graphics.FillRectangle(Brushes.DarkGray, Bottom);
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
            DialogResult result = MessageBox.Show("Esta seguro que desea salir?", "SOFTFUEL .NET", MessageBoxButtons.YesNo);
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
            SFbtnConectar.Enabled = false;
            SFbtnEscanearRed.Enabled = true;
            SFbtnDesconectar.Enabled = true;
            claseMain.ConectaryDescubrirRed();
            SFTimerCambioPrecios.Enabled = true;
            SFTimerCambioPrecios.Start();
        }
        private void SFbtnEscanearRed_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea escanear de nuevo la red?", "SOFTFUEL .NET", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                claseMain.Desconectar();
                claseMain.ConectaryDescubrirRed();
            }
        }

        private void SFbtnDesconectar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea desconectar la red?", "SOFTFUEL .NET", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                SFbtnConectar.Enabled = true;
                SFbtnEscanearRed.Enabled = false;
                SFbtnDesconectar.Enabled = false;
                claseMain.Desconectar();
                SFTimerCambioPrecios.Enabled = false;
                SFTimerCambioPrecios.Stop();
                LimpiarDispositivos();
            }
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
            }
            DataRow NewRow = dtLog.NewRow();
            NewRow["Fecha"] = _log.Fecha;
            NewRow["Mensaje"] = _log.Mensaje;
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

        void LimpiarDispositivos()
        {
            ListadoObjetosCaras.Clear();
            SFPanelCara1.Controls.Clear();
            SFPanelCara2.Controls.Clear();
            SFPanelCara3.Controls.Clear();
            SFPanelCara4.Controls.Clear();
            SFPanelCara5.Controls.Clear();
            SFPanelCara6.Controls.Clear();
            SFPanelCara7.Controls.Clear();
            SFPanelCara8.Controls.Clear();
            SFPanelPOS1.Controls.Clear();
            SFPanelPOS2.Controls.Clear();
            SFPanelPOS3.Controls.Clear();
            SFPanelPOS4.Controls.Clear();
        }

        void NodoAgregadoEventHandler(NodosXbee e)
        {
            if (e.TipoDispositivo == XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador)
            {
                string cara1 = "";
                string cara2 = "";
                if (instancia.ListNodes.FindAll(item => item.TipoDispositivo ==
                    XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador).Count == 1)
                {
                    cara1 = "SFPanelCara1";
                    cara2 = "SFPanelCara2";
                }
                if (instancia.ListNodes.FindAll(item => item.TipoDispositivo ==
                    XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador).Count == 2)
                {
                    cara1 = "SFPanelCara3";
                    cara2 = "SFPanelCara4";
                }
                if (instancia.ListNodes.FindAll(item => item.TipoDispositivo ==
                    XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador).Count == 3)
                {
                    cara1 = "SFPanelCara5";
                    cara2 = "SFPanelCara6";
                }
                if (instancia.ListNodes.FindAll(item => item.TipoDispositivo ==
                    XbeeUtils.Enumeraciones.TipoDispositivo.Dispensador).Count == 4)
                {
                    cara1 = "SFPanelCara7";
                    cara2 = "SFPanelCara8";
                }

                Panel PanelCara1 = FindPanel(SFLayoutContainer, cara1);
                Panel PanelCara2 = FindPanel(SFLayoutContainer, cara2);

                DataTable dtCaras;
                using (Generales modGEN = new Generales())
                {
                    dtCaras = modGEN.GetTable("select DISTINCT numPosicion FROM posicion WHERE idXbee = " + e.IdXbee);
                }
                if (dtCaras != null && dtCaras.Rows.Count > 1)
                {
                    if (ListadoObjetosCaras == null) ListadoObjetosCaras = new List<ctrCara>();
                    ctrCara newCara1 = new ctrCara();
                    newCara1.NumCara = Convert.ToInt32(dtCaras.Rows[0][0]);
                    newCara1.EstadoCara = EnumEstadoCara.Normal;
                    newCara1.NombreCara = "Cara " + newCara1.NumCara.ToString();
                    newCara1.idXbee = e.IdXbee;
                    newCara1.NombreNodo = e.Nombre;
                    PanelCara1.Controls.Add(newCara1);
                    newCara1.Dock = DockStyle.Fill;

                    ctrCara newCara2 = new ctrCara();
                    newCara2.NumCara = Convert.ToInt32(dtCaras.Rows[1][0]);
                    newCara2.EstadoCara = EnumEstadoCara.Normal;
                    newCara2.NombreCara = "Cara " + newCara2.NumCara.ToString();
                    newCara2.idXbee = e.IdXbee;
                    newCara2.NombreNodo = e.Nombre;
                    PanelCara2.Controls.Add(newCara2);
                    newCara2.Dock = DockStyle.Fill;

                    ListadoObjetosCaras.Add(newCara1);
                    ListadoObjetosCaras.Add(newCara2);
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

        void CargarConfiguracionMysql()
        {
            instancia.SqlServidor = ConfigurationManager.AppSettings["servidor"];
            instancia.SqlUsuario = ConfigurationManager.AppSettings["usuario"];
            instancia.SqlPassword = ConfigurationManager.AppSettings["password"];
            instancia.SqlBaseDatos = ConfigurationManager.AppSettings["bd"];
        }

        void ExpandirPanelLogs(bool expandir)
        {
            if (expandir == true)
            {
                this.SFLyContainer.RowStyles[0].Height = 0F;
                this.SFLyContainer.RowStyles[1].Height = 100F;
                SFbtnMaximizarMinimizar.Text = "Minimizar";
            }
            else
            {
                this.SFLyContainer.RowStyles[0].Height = 60.98039F;
                this.SFLyContainer.RowStyles[1].Height = 39.01961F;
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
                    Ctrcara.Dinero = "$0";
                }
                else
                {
                    Ctrcara.EstadoCara = EnumEstadoCara.Normal;
                    Ctrcara.Galones = galones;
                    Ctrcara.Dinero = "$" + dinero;
                }
            }
        }

        void cargarImagenes()
        {
            System.Drawing.Bitmap imagenConexion = Properties.Resources.conexion;
            Bitmap objBitmapConexion = new Bitmap(imagenConexion, new Size(32, 32));
            SFbtnConectar.Image = objBitmapConexion;

            System.Drawing.Bitmap imagenRed = Properties.Resources.network;
            Bitmap objimagenRed = new Bitmap(imagenRed, new Size(32, 32));
            SFbtnEscanearRed.Image = objimagenRed;
        }
        #endregion

        #region BackgroundWorkerCambioPrecio
        private void INDbwCambioPrecio_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string cambioPrecio = System.IO.File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + "/cambioPrecio.txt");
                if (cambioPrecio == "1")
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
                LocalLogManager.EscribeLog("Ocurrio Un Error Con Cambio de Precio" + ex.Message, LocalLogManager.TipoImagen.TipoError);
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

       
    }
 
}
