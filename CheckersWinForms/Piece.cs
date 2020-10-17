using System.Collections.Generic;
using System.Drawing;

namespace CheckersWinForms
{
    public class Piece
    {
        // Private Members
        private readonly ePlayerSide r_Side;
        private bool m_IsKing;
        private Point m_Location;

        // Constructors
        public Piece(Point i_Location, ePlayerSide i_Side)
        {
            m_IsKing = false;
            m_Location = i_Location;
            r_Side = i_Side;
        }

        // Public Methods
        public bool HasPossibleMoves(Player i_Player, Board i_CurrentBoard)
        {
            return MoveValidator.IsPieceHaveMoves(i_CurrentBoard, this);
        }

        // Returns a list of all possible moves
        public List<Move> GetPossibleMovesList(Player i_Player, Board i_Board, ref bool capturePossible)
        {
            List<Move> movesList = new List<Move>();

            if (MoveValidator.IsPieceHaveMoves(i_Board, this))
            {
                addNonKingsMovesToList(i_Player, i_Board, movesList, ref capturePossible);
                if (this.IsKing)
                {
                    addKingsMovesToList(i_Player, i_Board, movesList, ref capturePossible);
                }
            }

            return movesList;
        }

        // Adds all possible non-king moves to the given moves list
        private void addNonKingsMovesToList(Player i_Player, Board i_Board, List<Move> i_MovesList, ref bool capturePossible)
        {
            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X - 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X - 1));
            }

            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X - 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X - 1));
            }

            if (checkCapture(i_Board, -1, new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X - 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X - 2));
                capturePossible = true;
            }

            if (checkCapture(i_Board, 1, new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X - 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X - 2));
                capturePossible = true;
            }
        }

        // Adds all possible king moves to the given moves list
        private void addKingsMovesToList(Player i_Player, Board i_Board, List<Move> i_MovesList, ref bool capturePossible)
        {
            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X + 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X + 1));
            }

            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X + 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X + 1));
            }

            if (checkKingCapture(i_Board, -1, new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X + 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X + 2));
                capturePossible = true;
            }

            if (checkKingCapture(i_Board, 1, new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X + 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X + 2));
                capturePossible = true;
            }
        }

        // Returns true if the given move is a capture move
        private bool checkCapture(Board i_Board, int i_Direction, Move i_Move)
        {
            // check if destinations is in board's borders
            if (MoveValidator.IsInBorders(i_Board, i_Move.ToRow, i_Move.ToCol))
            {
                Piece pieceToBeCaptured =
                    i_Board.GameBoard[(i_Move.FromRow + i_Move.ToRow) / 2, (i_Move.FromCol + i_Move.ToCol) / 2].PiecePointer;
                // check destination is empty
                if (i_Board.GameBoard[i_Move.ToRow, i_Move.ToCol].PiecePointer == null)
                {
                    if (pieceToBeCaptured != null)
                    {
                        if (pieceToBeCaptured.r_Side == MoveValidator.GetOtherSide(this.Side))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool checkKingCapture(Board i_Board, int i_Direction, Move i_Move)
        {
            bool captureMove = false;
            if (MoveValidator.IsInBorders(i_Board, i_Move.ToRow, i_Move.ToCol))
            {
                if (i_Board.GameBoard[i_Move.ToRow, i_Move.ToCol].PiecePointer == null)
                {
                    if(i_Board.GameBoard[i_Move.FromRow + 1, i_Move.FromCol + i_Direction].PiecePointer != null)
                    {
                        if (i_Board.GameBoard[i_Move.FromRow + 1, i_Move.FromCol + i_Direction].PiecePointer.Side == MoveValidator.GetOtherSide(this.Side))
                        {
                            captureMove = true;
                        }
                    }
                }
            }

            return captureMove;
        }

        // Private Methods
        private void addSimpleMovesUpside(List<Move> i_MovesList, Board i_Board)
        {
            if (MoveValidator.IsInBorders(i_Board, this.Location.X + 1, this.Location.Y + 1) && i_Board.GameBoard[this.Location.X + 1, this.Location.Y + 1].PiecePointer == null)
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X + 1));
            }

            if (MoveValidator.IsInBorders(i_Board, this.Location.X + 1, this.Location.Y - 1) && i_Board.GameBoard[this.Location.X + 1, this.Location.Y - 1].PiecePointer == null)
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X + 1));
            }

            // Capture moves
            if (MoveValidator.IsInBorders(i_Board, this.Location.X + 2, this.Location.Y + 2)
               && i_Board.GameBoard[this.Location.X + 2, this.Location.Y + 2].PiecePointer == null)
            {
                if (i_Board.GameBoard[this.Location.X + 1, this.Location.Y + 1].PiecePointer != null
                   && i_Board.GameBoard[this.Location.X + 1, this.Location.Y + 1].PiecePointer.Side
                   == ePlayerSide.Down)
                {
                    i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X + 2));
                }
            }

            if (MoveValidator.IsInBorders(i_Board, this.Location.X + 2, this.Location.Y - 2)
               && i_Board.GameBoard[this.Location.X + 2, this.Location.Y - 2].PiecePointer == null)
            {
                if (i_Board.GameBoard[this.Location.X + 1, this.Location.Y - 1].PiecePointer != null
                   && i_Board.GameBoard[this.Location.X + 1, this.Location.Y - 1].PiecePointer.Side
                   == ePlayerSide.Down)
                {
                    i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X + 2));
                }
            }
        }

        // Properties
        public ePlayerSide Side
        {
            get
            {
                return r_Side;
            }
        }

        public Point Location
        {
            get
            {
                return m_Location;
            }

            set
            {
                m_Location = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = true;
            }
        }
    }
}
