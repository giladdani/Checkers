
using System;
using System.Drawing;

namespace CheckersWinForms
{
    public class Board
    {
        // Private Members
        private int m_Size;
        private Square[,] m_GameBoard;

        // Constructors
        public Board(int i_Size)
        {
            m_Size = i_Size;
            Build();
        }

        // Public Methods
        // Initialize a new empty board
        public void Build()
        {
            m_GameBoard = new Square[m_Size, m_Size];

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_GameBoard[i, j] = new Square();
                }
            }
        }

        // Places the given player's pieces on the board
        public void SetPiecesPosition(Player i_Player)
        {
            foreach (Piece piece in i_Player.Pieces)
            {
                int x = piece.Location.X;
                int y = piece.Location.Y;
                m_GameBoard[x, y].PiecePointer = piece;
            }
        }

        // Update the board of moving piece
        public void makeMove(Player i_Player, Piece i_Piece, Point i_To)
        {
            m_GameBoard[i_Piece.Location.X, i_Piece.Location.Y].PiecePointer = null;
            i_Piece.Location = i_To;
            m_GameBoard[i_To.X, i_To.Y].PiecePointer = i_Piece;
            if (i_To.X == 0 || i_To.X == Size - 1)
            {
                i_Piece.IsKing = true;
            }
        }

        // Make a capture move and update enemy player pieces and board
        public void MakeCaptureMove(Player i_EnemyPlayer, Piece i_Piece, Point i_To)
        {
            i_EnemyPlayer.Pieces.Remove(m_GameBoard[(i_Piece.Location.X + i_To.X) / 2, (i_Piece.Location.Y + i_To.Y) / 2].PiecePointer);
            m_GameBoard[i_Piece.Location.X, i_Piece.Location.Y].PiecePointer = null;
            m_GameBoard[(i_Piece.Location.X + i_To.X) / 2, (i_Piece.Location.Y + i_To.Y) / 2].PiecePointer.IsCaptured = true;
            m_GameBoard[(i_Piece.Location.X + i_To.X) / 2, (i_Piece.Location.Y + i_To.Y) / 2].PiecePointer = null;
            i_Piece.Location = i_To;
            m_GameBoard[i_To.X, i_To.Y].PiecePointer = i_Piece;
            if (i_To.X == 0 || i_To.X == Size - 1)
            {
                i_Piece.IsKing = true;
            }
        }

        // Properties
        public int Size
        {
            get
            {
                return m_Size;
            }
        }

        public Square[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }
    }
}