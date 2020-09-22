using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public class Square : Button
    {
        public Square()
        {
            this.SetStyle(ControlStyles.Selectable, false);     //  remove focus border
        }

        //protected override void OnClick(EventArgs e)
        //{
        //    //base.OnClick(e);
        //    if (this.Enabled)
        //    {
        //        this.BackColor = Color.LightBlue;
        //    }
        //}
    }
}