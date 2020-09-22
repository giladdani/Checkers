using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
//using System.Windows;

namespace CheckersWinForms
{
    public class Piece
    {
        // Private Members
        private readonly ePlayerSide r_Side;
        private bool m_IsKing;
        private bool m_IsCaptured;
        private Point m_Location;

        // Constructors
        public Piece(Point i_Location, ePlayerSide i_Side)
        {
            m_IsKing = false;
            m_IsCaptured = false;
            m_Location = i_Location;
            r_Side = i_Side;
        }

        // Public Methods
        public bool HasPossibleMoves(Player i_Player, Board i_CurrentBoard)
        {
            return MoveValidator.IsPieceHavePossibleMove(i_Player, i_CurrentBoard, this);
        }

        public List<Move> GetAvailableMovesList(Player i_Player, Board i_Board, ref bool capturePossible)
        {
            List<Move> movesList = new List<Move>();

            if (MoveValidator.IsPieceHavePossibleMove(i_Player, i_Board, this))
            {
                if (!this.IsKing)
                {
                    addNotKingsMovesToList(i_Player, i_Board, movesList, ref capturePossible);
                }
                else
                {
                    addKingsMovesToList(i_Player, i_Board, movesList, ref capturePossible);
                }
            }

            return movesList;
        }

        private void addNotKingsMovesToList(Player i_Player, Board i_Board, List<Move> i_MovesList, ref bool capturePossible)
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

        private void addKingsMovesToList(Player i_Player, Board i_Board, List<Move> i_MovesList, ref bool capturePossible)
        {
            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X + 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 1, this.Location.X - 1));
            }

            if (MoveValidator.IsSimpleMove(i_Player, i_Board, new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X + 1)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 1, this.Location.X - 1));
            }

            if (checkKingCapture(i_Board, -1, new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X + 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y - 2, this.Location.X - 2));
                capturePossible = true;
            }

            if (checkKingCapture(i_Board, 1, new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X + 2)))
            {
                i_MovesList.Add(new Move(this.Location.Y, this.Location.X, this.Location.Y + 2, this.Location.X - 2));
                capturePossible = true;
            }
        }

        private bool checkCapture(Board i_Board, int i_Direction, Move i_Move)
        {
            bool captureMove = false;
            if (MoveValidator.IsInBorders(i_Board, i_Move.XTo, i_Move.YTo))
            {
                if (i_Board.GameBoard[i_Move.XTo, i_Move.YTo].PiecePointer == null)
                {
                    if (MoveValidator.IsInBorders(i_Board, i_Move.XFrom - 1, i_Move.YFrom + i_Direction))
                    {
                        if (i_Board.GameBoard[i_Move.XFrom - 1, i_Move.YFrom + i_Direction].PiecePointer != null)
                        {
                            if (i_Board.GameBoard[i_Move.XFrom - 1, i_Move.YFrom + i_Direction].PiecePointer.Side == MoveValidator.GetOtherSide(this.Side))
                            {
                                captureMove = true;
                            }
                        }
                    }
                }
            }

            return captureMove;
        }

        private bool checkKingCapture(Board i_Board, int i_Direction, Move i_Move)
        {
            bool captureMove = false;
            if (MoveValidator.IsInBorders(i_Board, i_Move.XTo, i_Move.YTo))
            {
                if (i_Board.GameBoard[i_Move.XTo, i_Move.YTo].PiecePointer == null)
                {
                    if (i_Board.GameBoard[i_Move.XFrom + 1, i_Move.YFrom + i_Direction].PiecePointer.Side == MoveValidator.GetOtherSide(this.Side))
                    {
                        captureMove = true;
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

        public bool IsCaptured
        {
            get
            {
                return m_IsCaptured;
            }

            set
            {
                m_IsCaptured = true;
            }
        }
    }
}
