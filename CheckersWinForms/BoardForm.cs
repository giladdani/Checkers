using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersWinForms
{
    public partial class BoardForm : Form
    {
        public BoardForm(int i_Size)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // hide window resizing
            generateButtonMatrix(i_Size);
        }

        private void generateButtonMatrix(int i_Size)
        {
            Square[,] squareMatrix = new Square[i_Size, i_Size];

            for (int y = 0; y < i_Size; y++)
            {
                for (int x = 0; x < i_Size; x++)
                {
                    squareMatrix[y, x] = new Square() { Width = 60, Height = 60, Left = y * 60, Top = (x + 1) * 60 };
                    if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0))
                    {
                        //squareMatrix[y, x].Click += SquareClicked;
                        squareMatrix[y, x].BackColor = Color.DimGray;
                    }
                    else
                    {
                        squareMatrix[y, x].Enabled = false;
                        squareMatrix[y, x].BackColor = Color.White;
                    }

                    this.Controls.Add(squareMatrix[y, x]);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.label2.Left = this.Width - 175;
        }

        //private void SquareClicked(object sender, EventArgs e)
        //{
        //    Button clickedSquare = sender as Button;
        //    if(clickedSquare.Enabled == true) // playable square
        //    {
        //        // if has a piece and its the players turn
        //        clickedSquare.BackColor = Color.Cyan;
        //    }
        //}
    }
}
