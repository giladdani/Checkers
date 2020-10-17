using System;
using System.Drawing;

namespace CheckersLogic
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
            Piece piece = m_Board.GameBoard[i_Move.FromRow, i_Move.FromCol].CurrentPiece;

            if (piece != null)
            {
                // User chose simple move
                if (MoveValidator.IsSimpleMove(CurrentPlayer, m_Board, i_Move))
                {
                    // If the player can capture- the move fails (he must choose to capture)
                    if (MoveValidator.IsPlayerHasCapture(CurrentPlayer, m_Board))
                    {
                        moveFeedback = eMoveFeedback.FailedCouldCapture;
                    }
                    else
                    {
                        // If the moving piece is NOT a king
                        if (piece.IsKing == false)
                        {
                            // If tries to move in the opposite direction- the move fails
                            if (MoveValidator.IsMovingInValidDirection(CurrentPlayer, i_Move, piece) == false)
                            {
                                moveFeedback = eMoveFeedback.Failed;
                            }
                            else
                            {
                                m_Board.MovePiece(CurrentPlayer, piece, new Point(i_Move.ToRow, i_Move.ToCol));
                                moveFeedback = eMoveFeedback.Success;
                            }
                        }
                        // moving piece is a king
                        else
                        {
                            m_Board.MovePiece(CurrentPlayer, piece, new Point(i_Move.ToRow, i_Move.ToCol));
                            moveFeedback = eMoveFeedback.Success;
                        }
                    }
                }
                // User chose capture move
                else if (MoveValidator.IsCaptureMovePossible(CurrentPlayer, m_Board, i_Move))
                {
                    Player enemyPlayer = CurrentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
                    m_Board.MoveCapturingPiece(CurrentPlayer, enemyPlayer, piece, new Point(i_Move.ToRow, i_Move.ToCol));
                    moveFeedback = eMoveFeedback.Success;

                    // check double capture option
                    if (MoveValidator.CanPieceCapture(m_Board, m_Board.GameBoard[i_Move.ToRow, i_Move.ToCol].CurrentPiece))
                    {
                        moveFeedback = eMoveFeedback.CanDoubleCapture;
                    }
                }
            }

            if (moveFeedback == eMoveFeedback.Success)
            {
                m_TurnCount++;
            }

            // Notify listeners of move execution completed
            MoveExecuted?.Invoke(moveFeedback);
        }

        // Returns true if the game is over
        public bool IsOver()
        {
            // If a player has no pieces left
            bool isPlayerOutOfPieces = (m_PlayerOne.Pieces.Count == 0 || m_PlayerTwo.Pieces.Count == 0) && m_TurnCount > 0;

            // If current player has no moves to play
            bool isPlayerOutOfMoves = CurrentPlayer.HasPossibleMoves(m_Board) == false;

            if (isPlayerOutOfMoves || isPlayerOutOfPieces)
            {
                return true;
            }

            return false;
        }

        // Resets the turn count, rebuilds the board and resets pieces starting positions
        public void ResetBasicSettings()
        {
            m_TurnCount = 0;
            m_Board.Build();
            SetPiecesStartingPositions();
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
                return m_TurnCount % 2 == 0 ? m_PlayerOne : m_PlayerTwo;
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
        }

        public bool AiEnabled
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