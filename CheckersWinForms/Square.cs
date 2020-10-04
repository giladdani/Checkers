using System;

namespace CheckersWinForms
{
    public class Square
    {
        // Events
        public event Action<Square> CurrentPieceChanged;

        // Private Members
        private Piece m_PiecePointer;
        private int m_MatrixRowIndex;
        private int m_MatrixColIndex;

        // Constructors
        public Square(int i_Row, int i_Col)
        {
            m_MatrixRowIndex = i_Row;
            m_MatrixColIndex = i_Col;
            PiecePointer = null;
        }

        // Properties
        public Piece PiecePointer
        {
            get
            {
                return m_PiecePointer;
            }

            set
            {
                m_PiecePointer = value;
                if(CurrentPieceChanged != null)
                {
                    CurrentPieceChanged.Invoke(this);
                }
            }
        }

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
    }
}