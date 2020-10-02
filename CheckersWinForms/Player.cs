using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

namespace CheckersWinForms
{
    public class Player
    {
        // Private Members
        private string m_Name;
        private ePlayerSide m_Side;
        private List<Piece> m_Pieces;
        private int m_TotalScore;
        private bool m_IsAi;

        // Constructors
        public Player(string i_Name, ePlayerSide i_Side, int i_BoardSize, bool i_IsAi)
        {
            m_Name = i_Name;
            m_Side = i_Side;
            m_IsAi = i_IsAi;
            m_TotalScore = 0;
            InitPieceArr(i_Side, i_BoardSize);
        }

        // Public Methods
        // Initialize array of pieces
        public void InitPieceArr(ePlayerSide i_Side, int i_BoardSize)
        {
            int numOfPieces = (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
            m_Pieces = new List<Piece>();
            int endRow, startRow, piecesIndex = 0;
            if (i_Side == ePlayerSide.Up)
            {
                startRow = 0;
                endRow = (i_BoardSize / 2) - 1;
            }
            else
            {
                startRow = (i_BoardSize / 2) + 1;
                endRow = i_BoardSize;
            }

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if (piecesIndex < numOfPieces)
                    {
                        if ((j % 2 == 1) && (i % 2 == 0))
                        {
                            Point locationPoint = new Point(i, j);
                            Piece boardPiece = new Piece(locationPoint, i_Side);
                            m_Pieces.Add(boardPiece);
                        }

                        if ((j % 2 == 0) && (i % 2 == 1))
                        {
                            Point locationPoint = new Point(i, j);
                            Piece boardPiece = new Piece(locationPoint, i_Side);
                            m_Pieces.Add(boardPiece);
                        }
                    }
                }
            }
        }

        // Returns true if the player has any possible move to play
        public bool HasPossibleMoves(Board i_CurrentBoard)
        {
            bool hasMove = false;

            foreach (Piece piece in m_Pieces)
            {
                // if piece can move/capture
                if (piece.HasPossibleMoves(this, i_CurrentBoard))
                {
                    hasMove = true;
                }
            }

            return hasMove;
        }

        // Returns a random selected move for player 2
        public Move GenerateRandomMove(Board i_Board)
        {
            Move chosenMove = null;
            bool capturePossible = false;
            List<List<Move>> allPiecesMoves = new List<List<Move>>();

            foreach (Piece piece in m_Pieces)
            {
                List<Move> pieceMoves = piece.GetAvailableMovesList(this, i_Board, ref capturePossible);
                if (pieceMoves.Count > 0)
                {
                    allPiecesMoves.Add(pieceMoves);
                }
            }

            if (capturePossible)
            {
                chosenMove = findCaptureMove(i_Board, allPiecesMoves);
            }
            else
            {
                Random randomGenerator = new Random();
                int listIndex = randomGenerator.Next(allPiecesMoves.Count - 1);
                int moveIndex = randomGenerator.Next(allPiecesMoves[listIndex].Count - 1);
                chosenMove = allPiecesMoves[listIndex][moveIndex];
            }

            return chosenMove;
        }

        private Move findCaptureMove(Board i_Board, List<List<Move>> i_AllMovesList)
        {
            Move captureMove = null;

            foreach (List<Move> list in i_AllMovesList)
            {
                foreach (Move move in list)
                {
                    if (MoveValidator.IsCaptureMovePossible(this, i_Board, move))
                    {
                        captureMove = move;
                    }
                }
            }

            return captureMove;
        }

        // Properties
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public ePlayerSide Side
        {
            get
            {
                return m_Side;
            }
        }

        public List<Piece> Pieces
        {
            get
            {
                return m_Pieces;
            }
        }

        public int TotalScore
        {
            get
            {
                return m_TotalScore;
            }

            set
            {
                if (value >= 0)
                {
                    m_TotalScore = value;
                }
            }
        }

        public bool IsAi
        {
            get
            {
                return m_IsAi;
            }

            set
            {
                m_IsAi = value;
            }
        }
    }
}
