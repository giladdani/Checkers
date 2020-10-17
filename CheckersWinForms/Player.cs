using System;
using System.Drawing;
using System.Collections.Generic;

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
        // Initialize array of pieces and their location on the board according to it's size
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
            foreach (Piece piece in m_Pieces)
            {
                if (piece.HasPossibleMoves(this, i_CurrentBoard))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns a randomly selected move
        public Move GenerateRandomMove(Board i_Board)
        {
            Move chosenMove = null;
            bool capturePossible = false;
            List<List<Move>> allPiecesMoves = new List<List<Move>>();

            // for every piece- make a list of it's possible moves
            foreach (Piece piece in m_Pieces)
            {
                List<Move> pieceMoves = piece.GetPossibleMovesList(this, i_Board, ref capturePossible);
                if (pieceMoves.Count > 0)
                {
                    allPiecesMoves.Add(pieceMoves);
                }
            }

            // if there is at least one possible capture move possible- we choose capture over simple move
            if (capturePossible)
            {
                chosenMove = findCaptureMove(i_Board, allPiecesMoves);
            }
            // otherwise- randomly select any of the possible simple moves
            else
            {
                Random randomGenerator = new Random();
                int listIndex = randomGenerator.Next(allPiecesMoves.Count);
                int moveIndex = randomGenerator.Next(allPiecesMoves[listIndex].Count);
                chosenMove = allPiecesMoves[listIndex][moveIndex];
            }

            return chosenMove;
        }

        // Returns a capture move from all possible moves of all pieces
        private Move findCaptureMove(Board i_Board, List<List<Move>> i_AllMovesList)
        {
            foreach (List<Move> list in i_AllMovesList)
            {
                foreach (Move move in list)
                {
                    if (MoveValidator.IsCaptureMovePossible(this, i_Board, move))
                    {
                        return move;
                    }
                }
            }

            return null;
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
