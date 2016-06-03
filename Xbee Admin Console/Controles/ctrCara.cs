using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbeeAdminConsole
{
    public partial class ctrCara : UserControl
    {
        public ctrCara()
        {
            InitializeComponent();
            ColocarImagen();
            
        }

        public void ColocarImagen()
        {
            System.Drawing.Bitmap imagenDis = Properties.Resources.dispensador;
            Bitmap objimagenDis = new Bitmap(imagenDis, SFpcImageDispensador.Size);
            SFpcImageDispensador.Image = objimagenDis;
        }

        #region Propiedades
        public Color ColorCara { get; set; }
        public Color ColorCaraHead { get; set; }
        public int idXbee { get; set; }
        private EnumEstadoCara _estadoCara = EnumEstadoCara.Normal;

        public EnumEstadoCara EstadoCara
        {
            get {
                return _estadoCara;
            }
            set {
                _estadoCara = value;
                EstablecerComportamientoEstadoCara();
            }
        }
        public string NombreCara
        {
            get
            {
                return SFlbNombreCara.Text;
            }
            set
            {
                SFlbNombreCara.Text = value;
            }
        }

        public string Galones
        {
            set
            {
                SFlbGalonesValor.Text = value;
            }
        }

        public string Dinero
        {
            set
            {
                SFlbDineroValor.Text = value;
            }
        }

        public string NombreNodo { get; set; }
        int _numCara;
        public int NumCara {
            get
            {
                return _numCara;
            }
            set 
            {
                _numCara = value;
            }
        }

        #endregion

        #region Metodos
        void EstablecerComportamientoEstadoCara()
        {
            switch (EstadoCara)
            { 
                case EnumEstadoCara.Normal:
                    this.BackColor = ColorCara;
                    this.SFlbNombreCara.BackColor = ColorCaraHead;
                    break;

                case EnumEstadoCara.Atendiendo:
                    this.BackColor = Color.FromArgb(75, 175, 79);
                    break;

                default:
                    break;
            }
        }

        void MangueraArriba()
        {
            EstadoCara = EnumEstadoCara.Atendiendo;
        }

        void MangueraAbajo()
        {
            EstadoCara = EnumEstadoCara.Normal;
        }
        #endregion

        private void SFPictureBox_Resize(object sender, EventArgs e)
        {
            ColocarImagen();
        }
    }
    #region Enumeraciones
    public enum EnumEstadoCara
    {
        Atendiendo,
        Normal
    }
    #endregion
    
}
