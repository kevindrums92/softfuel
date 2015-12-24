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
    public partial class ctrPOS : UserControl
    {
        public ctrPOS()
        {
            InitializeComponent();
            ColocarImagen();
            
        }

        public void ColocarImagen()
        {
            System.Drawing.Bitmap imagenPOS = Properties.Resources.pos_pay;
            Bitmap objimagenPOS = new Bitmap(imagenPOS, SFpcImagePOS.Size);
            SFpcImagePOS.Image = objimagenPOS;
        }

        #region Propiedades
        public int idXbee { get; set; }
       
        public string NombrePOS
        {
            get
            {
                return SFlbNombrePOS.Text;
            }
            set
            {
                SFlbNombrePOS.Text = value;
            }
        }

        
        public string NombreNodo { get; set; }
      

        #endregion

        #region Metodos
       
        #endregion

        private void SFPictureBox_Resize(object sender, EventArgs e)
        {
            ColocarImagen();
        }
    }
    
}
