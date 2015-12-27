using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbeeAdminConsole.Controles
{
    class VerticalProgressBar : ProgressBar 
    {
        //public VerticalProgressBar()
        //{
        //    this.SetStyle(ControlStyles.UserPaint, true);
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Rectangle rec = e.ClipRectangle;

        //    rec.Height = (int)(rec.Height * ((double)Value / Maximum)) - 4;
        //    if(ProgressBarRenderer.IsSupported)
        //        ProgressBarRenderer.DrawVerticalChunks(e.Graphics, e.ClipRectangle);
        //    rec.Width = rec.Width - 4;
        //    e.Graphics.FillRectangle(Brushes.Red, 2, 2, rec.Width, rec.Height);
        //}
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | 0x4;
                return cp;
            }
        }

    }
}

