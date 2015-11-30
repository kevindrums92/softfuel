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
        }

        #region Propiedades
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

        public string NombreNodo
        {
            get
            {
                return SFlbNombreNodo.Text;
            }
            set
            {
                SFlbNombreNodo.Text = value;
            }
        }
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
                    this.BackColor = Color.FromArgb(66, 73, 92);
                    break;

                case EnumEstadoCara.Atendiendo:
                    this.BackColor = Color.FromArgb(75, 175, 79);
                    break;

                default:
                    break;
            }
        }
        #endregion


        private void SFlbNombreCara_Click_1(object sender, EventArgs e)
        {
            EstadoCara = EnumEstadoCara.Atendiendo;
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
