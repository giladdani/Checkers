using System;
using System.Drawing;

namespace CheckersWinForms
{
    public class Game
    {
        // Events
        public event Action<eMoveFeedback> MoveExecuted;

        // Private Members
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Board m_Board;
        private bool m_AiMode;
        private int m_TurnCount;

        // Constructors
        public Game(string i_PlayerOneName, string i_PlayerTwoName, int i_BoardSize, bool i_AiMode)
        {
            if(i_AiMode)
            {
                i_PlayerTwoName = "Computer";
            }

            m_PlayerOne = new Player(i_PlayerOneName, ePlayerSide.Up, i_BoardSize, false);
            m_PlayerTwo = new Player(i_PlayerTwoName, ePlayerSide.Down, i_BoardSize, i_AiMode);
            m_Board = new Board(i_BoardSize);
            m_AiMode = i_AiMode;
        }

        // Public Methods
        // Setup each player's piece's position
        public void SetPiecesStartingPositions()
        {
            m_PlayerOne.InitPieceArr(PlayerOne.Side, m_Board.Size);
            m_PlayerTwo.InitPieceArr(PlayerTwo.Side, m_Board.Size);
            m_Board.SetPiecesPosition(m_PlayerOne);
            m_Board.SetPiecesPosition(m_PlayerTwo);
        }

        // Execute the given move, returns true if it was executed successfully
        public void ExecuteMove(Move i_Move)
        {
            eMoveFeedback moveFeedback = eMoveFeedback.Failed;

            if (m_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer != null)
            {
                // simple move
                if (MoveValidator.IsSimpleMove(CurrentPlayer, m_Board, i_Move))
                {
                    if (MoveValidator.IsPlayerHasCapture(CurrentPlayer, m_Board))
                    {
                        moveFeedback = eMoveFeedback.FailedCouldCapture;
                    }
                    else
                    {
                        if (m_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer.IsKing == false)
                        {
                            if ((CurrentPlayer.Side == ePlayerSide.Down && i_Move.XFrom < i_Move.XTo) || (CurrentPlayer.Side == ePlayerSide.Up && i_Move.XFrom > i_Move.XTo))
                            {
                                moveFeedback = eMoveFeedback.Failed;
                            }
                            else
                            {
                                m_Board.makeMove(CurrentPlayer, m_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer, new Point(i_Move.XTo, i_Move.YTo));
                                moveFeedback = eMoveFeedback.Success;
                            }
                        }
                        else
                        {
                            m_Board.makeMove(CurrentPlayer, m_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer, new Point(i_Move.XTo, i_Move.YTo));
                            moveFeedback = eMoveFeedback.Success;
                        }
                    }
                }
                else if (MoveValidator.IsCaptureMovePossible(CurrentPlayer, m_Board, i_Move))
                {
                    Player enemyPlayer = CurrentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
                    m_Board.MakeCaptureMove(enemyPlayer, m_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer, new Point(i_Move.XTo, i_Move.YTo));
                    moveFeedback = eMoveFeedback.Success;

                    // check double capture option
                    if (MoveValidator.isCapturePossiblePerPiece(CurrentPlayer, m_Board, m_Board.GameBoard[i_Move.XTo, i_Move.YTo].PiecePointer))
                    {
                        moveFeedback = eMoveFeedback.CanDoubleCapture;
                    }
                }
            }

            if(moveFeedback == eMoveFeedback.Success)
            {
                m_TurnCount++;
            }

            MoveExecuted?.Invoke(moveFeedback);
        }

        // Returns true if the game is over
        public bool IsOver()
        {
            // If a player has no pieces left
            bool isOver = (m_PlayerOne.Pieces.Count == 0 || m_PlayerTwo.Pieces.Count == 0) && m_TurnCount > 0;

            // If current player has no moves to play
            if (!CurrentPlayer.HasPossibleMoves(m_Board))
            {
                isOver = true;
            }

            return isOver;
        }

        // Calculate current score of players based of pieces left difference
        public eRoundResult CalculateScores()
        {
            eRoundResult roundResult;
            int piecesDifference = Math.Abs(m_PlayerOne.Pieces.Count - m_PlayerTwo.Pieces.Count);

            if (m_PlayerOne.Pieces.Count > m_PlayerTwo.Pieces.Count)
            {
                roundResult = eRoundResult.playerOneVictory;
                m_PlayerOne.TotalScore += piecesDifference;
            }
            else if (m_PlayerOne.Pieces.Count < m_PlayerTwo.Pieces.Count)
            {
                roundResult = eRoundResult.playerTwoVictroy;
                m_PlayerTwo.TotalScore += piecesDifference;
            }
            else
            {
                roundResult = eRoundResult.Tie;
            }

            return roundResult;
        }

        // Properties
        public Player PlayerOne
        {
            get
            {
                return m_PlayerOne;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                Player player;
                if (m_TurnCount % 2 == 0)
                {
                    player = m_PlayerOne;
                }
                else
                {
                    player = m_PlayerTwo;
                }

                return player;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public int TurnCount
        {
            get
            {
                return m_TurnCount;
            }

            set
            {
                if (value > 0)
                {
                    m_TurnCount = value;
                }
            }
        }

        public bool AiMode
        {
            get
            {
                return m_AiMode;
            }

            set
            {
                m_AiMode = value;
            }
        }
    }
}