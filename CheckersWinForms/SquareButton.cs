using System;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWinForms
{
    public class SquareButton : Button
    {
        // Events
        public event Action<SquareButton> SquareClicked;

        // Private Members
        private int m_MatrixRowIndex;
        private int m_MatrixColIndex;
        private string m_LocationChars;

        // Constructors
        public SquareButton(int i_Row, int i_Col)
        {
            m_MatrixRowIndex = i_Row;
            m_MatrixColIndex = i_Col;
            m_LocationChars = MoveValidator.ConvertLocationToString(i_Row, i_Col);
        }

        // Event Handlers
        protected override void OnClick(EventArgs e)
        {
            SquareClicked.Invoke(this);
        }

        // Properties
        public int RowIndex
        {
            get
            {
                return m_MatrixRowIndex;
            }

            set
            {
                m_MatrixRowIndex = value;
            }
        }

        public int ColIndex
        {
            get
            {
                return m_MatrixColIndex;
            }

            set
            {
                m_MatrixColIndex = value;
            }
        }

        public string LocationChars
        {
            get
            {
                return m_LocationChars;
            }

            set
            {
                m_LocationChars = value;
            }
        }
    }
}
