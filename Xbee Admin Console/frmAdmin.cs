using Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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
        #endregion

        #region Constructor
        public frmAdmin()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ColocarFecha();
            instancia.NodoAgregadoEvent += NodoAgregadoEventHandler;
            NodosXbee _nodoPrueba = new NodosXbee(null, "DISPENSADOR 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 1);
            NodosXbee _nodoPrueba2 = new NodosXbee(null, "DISPENSADOR 2", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 2);
            instancia.ListNodes = new List<NodosXbee>();
            instancia.AgregarNodo(_nodoPrueba);
            instancia.AgregarNodo(_nodoPrueba2);
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

        void CargarConfiguracionMysql()
        {
            instancia.SqlServidor = ConfigurationManager.AppSettings["servidor"];
            instancia.SqlUsuario = ConfigurationManager.AppSettings["usuario"];
            instancia.SqlPassword = ConfigurationManager.AppSettings["password"];
            instancia.SqlBaseDatos = ConfigurationManager.AppSettings["bd"];
        }
        #endregion

        #region Metodos

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

                ctrCara newCara1 = new ctrCara();
                newCara1.Cara = 1;
                newCara1.EstadoCara = EnumEstadoCara.Normal;
                newCara1.NombreCara = "Cara 1";
                newCara1.idXbee = e.IdXbee;
                newCara1.NombreNodo = e.Nombre;
                PanelCara1.Controls.Add(newCara1);
                newCara1.Dock = DockStyle.Fill;

                ctrCara newCara2 = new ctrCara();
                newCara1.Cara = 2;
                newCara2.EstadoCara = EnumEstadoCara.Normal;
                newCara2.NombreCara = "Cara 2";
                newCara2.idXbee = e.IdXbee;
                newCara2.NombreNodo = e.Nombre;
                PanelCara2.Controls.Add(newCara2);
                newCara2.Dock = DockStyle.Fill;
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
        #endregion

    }
}
