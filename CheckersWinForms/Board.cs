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
        // First initialize of the game's board according to it's given size
        public void Build()
        {
            m_GameBoard = new Square[m_Size, m_Size];

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_GameBoard[i, j] = new Square(i, j);
                }
            }
        }

        // Places the given player's pieces on the board according to their given location
        public void SetPiecesPosition(Player i_Player)
        {
            foreach (Piece piece in i_Player.Pieces)
            {
                int x = piece.Location.X;
                int y = piece.Location.Y;
                m_GameBoard[x, y].PiecePointer = piece;
            }
        }

        // Update the board of the given piece's new location and set it's old position to be empty
        public void MovePiece(Player i_Player, Piece i_Piece, Point i_To)
        {
            // if the piece reaches top/bottom line- make it king
            if (i_To.X == 0 || i_To.X == Size - 1)                       
            {
                i_Piece.IsKing = true;
            }

            m_GameBoard[i_Piece.Location.X, i_Piece.Location.Y].PiecePointer = null;    // clear piece's old location
            i_Piece.Location = i_To;                                                    // set piece's new location
            m_GameBoard[i_To.X, i_To.Y].PiecePointer = i_Piece;                         // update the board of the piece's new location
        }

        // Remove captured piece and update the board of the given capturing piece's new location and
        public void MoveCapturingPiece(Player i_CurrentPlayer, Player i_EnemyPlayer, Piece i_Piece, Point i_NewLocation)
        {
            Piece pieceToBeCaptured = m_GameBoard[(i_Piece.Location.X + i_NewLocation.X) / 2,
                (i_Piece.Location.Y + i_NewLocation.Y) / 2].PiecePointer;

            if (pieceToBeCaptured != null)
            {
                // clear captured piece's old location
                m_GameBoard[(i_Piece.Location.X + i_NewLocation.X) / 2, (i_Piece.Location.Y + i_NewLocation.Y) / 2].PiecePointer = null;
                // remove captured enemy piece from it's owner's pieces array
                i_EnemyPlayer.Pieces.Remove(pieceToBeCaptured);
            }

            MovePiece(i_CurrentPlayer, i_Piece, i_NewLocation);
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