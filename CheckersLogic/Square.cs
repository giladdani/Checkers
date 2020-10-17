using System;

namespace CheckersLogic
{
    public class Square
    {
        // Events
        public event Action<Square> CurrentPieceChanged;

        // Private Members
        private Piece m_CurrentPiece;
        private int m_MatrixRowIndex;
        private int m_MatrixColIndex;

        // Constructors
        public Square(int i_Row, int i_Col)
        {
            m_MatrixRowIndex = i_Row;
            m_MatrixColIndex = i_Col;
            CurrentPiece = null;
        }

        // Properties
        public Piece CurrentPiece
        {
            get
            {
                return m_CurrentPiece;
            }

            set
            {
                m_CurrentPiece = value;
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