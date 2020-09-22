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
        private Piece m_PiecePointer;

        public Square()
        {
            this.SetStyle(ControlStyles.Selectable, false);     //  remove focus border
            m_PiecePointer = null;
        }

        public Piece PiecePointer
        {
            get
            {
                return m_PiecePointer;
            }

            set
            {
                m_PiecePointer = value;
            }
        }
        //protected override void OnClick(EventArgs e)
        //{
        //    base.OnClick(e);
        //    if (this.Enabled)
        //    {
        //        this.BackColor = Color.LightBlue;
        //    }
        //}
    }
}