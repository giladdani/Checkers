using System;
using System.Text;

namespace CheckersWinForms
{
    public class MoveValidator
    {
        // Returns a string of format 'COLrow' describing a location on the board
        public static string ConvertLocationToString(int i_Row, int i_Col)
        {
            StringBuilder locationString = new StringBuilder();
            locationString.Append((char)(i_Col + 'A'));
            locationString.Append((char)(i_Row + 'a'));

            return locationString.ToString();
        }

        // Returns a string describing the given move
        public static string ConvertMoveToString(Move i_Move)
        {
            char fromColumn = (char)(i_Move.FromCol + 'A');
            char fromRow = (char)(i_Move.FromRow + 'a');
            char toColumn = (char)(i_Move.ToCol + 'A');
            char toRow = (char)(i_Move.ToRow + 'a');

            StringBuilder moveString = new StringBuilder();
            moveString.Append(fromColumn);
            moveString.Append(fromRow);
            moveString.Append('>');
            moveString.Append(toColumn);
            moveString.Append(toRow);

            return moveString.ToString();
        }

        // Returns a move using given string that describes it
        public static Move ConvertStringToMove(string i_MoveAsString)
        {
            int fromColumn = i_MoveAsString[0] - 'A';
            int fromRow = i_MoveAsString[1] - 'a';
            int toColumn = i_MoveAsString[3] - 'A';
            int toRow = i_MoveAsString[4] - 'a';
            Move move = new Move { FromCol = fromColumn, FromRow = fromRow, ToCol = toColumn, ToRow = toRow };

            return move;
        }

        // Returns true if the given piece can capture
        public static bool CanPieceCapture(Board i_Board, Piece i_Piece)
        {
            bool isCaptureMovePossible = false;
            int moveDirection = i_Piece.Side == ePlayerSide.Down ? -1 : 1;


            isCaptureMovePossible = IsCaptureMovePossiblePerSide(i_Piece.Side, i_Board, moveDirection, i_Piece);

            // if the piece is king- also look for captures in opposite direction
            if (i_Piece.IsKing)
            {
                isCaptureMovePossible = isCaptureMovePossible || IsCaptureMovePossiblePerSide(i_Piece.Side, i_Board, (moveDirection * -1), i_Piece);
            }

            return isCaptureMovePossible;
        }

        public static bool IsCaptureMovePossiblePerSide(ePlayerSide i_Side, Board i_Board, int i_Direction, Piece i_Piece)
        {
            bool isCaptureMovePossible = false;
            ePlayerSide opponentSide = GetOtherSide(i_Side);

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction) && i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer != null)
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer.Side == opponentSide)
                {
                    if (IsInBorders(i_Board, i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y - (2 * i_Direction)))
                    {
                        if (i_Board.GameBoard[i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y - (2 * i_Direction)].PiecePointer == null)
                        {
                            isCaptureMovePossible = true;
                        }
                    }
                }
            }

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction) && i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer != null)
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer.Side == opponentSide)
                {
                    if (IsInBorders(i_Board, i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y + (2 * i_Direction)))
                    {
                        if (i_Board.GameBoard[i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y + (2 * i_Direction)].PiecePointer == null)
                        {
                            isCaptureMovePossible = true;
                        }
                    }
                }
            }

            return isCaptureMovePossible;
        }

        // Returns true if the player has any possible captures to make
        public static bool IsPlayerHasCapture(Player i_Player, Board i_Board)
        {
            foreach (Piece piece in i_Player.Pieces)
            {
                if(CanPieceCapture(i_Board, piece))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns true if the given move is considered simple
        public static bool IsSimpleMove(Player i_Player, Board i_Board, Move i_Move)
        {
            bool isDestinationInBorders = IsInBorders(i_Board, i_Move.ToRow, i_Move.ToCol);

            if (isDestinationInBorders)
            {
                bool isDestinationEmpty = i_Board.GameBoard[i_Move.ToRow, i_Move.ToCol].PiecePointer == null;
                if(isDestinationEmpty)
                {
                    return isSimpleMovePossible(i_Player, i_Board, i_Move);
                }
            }

            return false;
        }

        private static bool isSimpleMovePossible(Player i_Player, Board i_Board, Move i_Move)
        {
            bool isPossible = false;
            Piece movingPiece = i_Board.GameBoard[i_Move.FromRow, i_Move.FromCol].PiecePointer;

            if (movingPiece != null)
            {
                isPossible = isMoveSimpleDiagonalLine(i_Player.Side, i_Move);

                // if king- also check opposite direction
                if(movingPiece.IsKing)
                {
                    ePlayerSide oppositeSide = MoveValidator.GetOtherSide(i_Player.Side);
                    isPossible = isPossible || isMoveSimpleDiagonalLine(oppositeSide, i_Move);
                }
            }

            return isPossible;
        }

        private static bool isMoveSimpleDiagonalLine(ePlayerSide i_Side, Move i_Move)
        {
            int rowDifference = Math.Abs(i_Move.ToRow - i_Move.FromRow);
            int colDifference = Math.Abs(i_Move.FromCol - i_Move.ToCol);
            if (rowDifference == 1 && colDifference == 1)
            {
                return true;
            }

            return false;
        }

        // Returns true if the the move with capture is possible
        public static bool IsCaptureMovePossible(Player i_Player, Board i_Board, Move i_Move)
        {
            bool isCaptureMovePossible = false;
            Piece currentPiece = i_Board.GameBoard[i_Move.FromRow, i_Move.FromCol].PiecePointer;
            Piece pieceToBeCaptured = i_Board.GameBoard[(i_Move.FromRow + i_Move.ToRow) / 2, (i_Move.FromCol + i_Move.ToCol) / 2].PiecePointer;

            if (currentPiece != null && IsInBorders(i_Board, i_Move.ToRow, i_Move.ToCol))
            {
                // is destination empty
                if (i_Board.GameBoard[i_Move.ToRow, i_Move.ToCol].PiecePointer == null)
                {
                    if (i_Player.Side == ePlayerSide.Down)
                    {
                        if(pieceToBeCaptured != null && pieceToBeCaptured.Side == ePlayerSide.Up)
                        {
                            if (i_Move.FromRow - 1 == i_Move.ToRow + 1 || (currentPiece.IsKing && i_Move.FromRow + 1 == i_Move.ToRow - 1))
                            {
                                if (((i_Move.FromCol + 1) == (i_Move.ToCol - 1)) || ((i_Move.FromCol - 1) == (i_Move.ToCol + 1)))
                                {
                                    isCaptureMovePossible = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(pieceToBeCaptured != null && pieceToBeCaptured.Side == ePlayerSide.Down)
                        {
                            if (i_Move.FromRow + 1 == i_Move.ToRow - 1 || (currentPiece.IsKing && i_Move.FromRow - 1 == i_Move.ToRow + 1))
                            {
                                if (((i_Move.FromCol + 1) == (i_Move.ToCol - 1)) || ((i_Move.FromCol - 1) == (i_Move.ToCol + 1)))
                                {
                                    isCaptureMovePossible = true;
                                }
                            }
                        }
                    }
                }
            }

            return isCaptureMovePossible;
        }

        // Returns true if the given piece has any possible moves to execute
        public static bool IsPieceHaveMoves(Board i_Board, Piece i_Piece)
        {
            bool hasPossibleMoves = false;

            hasPossibleMoves = CanPieceSimpleMove(i_Board, i_Piece);
            hasPossibleMoves = hasPossibleMoves || CanPieceCapture(i_Board, i_Piece);

            return hasPossibleMoves;
        }

        public static bool CanPieceSimpleMove(Board i_Board, Piece i_Piece)
        {
            bool canSimpleMove = false;
            int moveDirection = i_Piece.Side == ePlayerSide.Down ? -1 : 1;


            canSimpleMove = canPieceSimpleMoveInDirection(i_Board, i_Piece, moveDirection);

            // if piece is king- also check opposite direction
            if(i_Piece.IsKing)
            {
                canSimpleMove = canSimpleMove || canPieceSimpleMoveInDirection(i_Board, i_Piece, (moveDirection * -1));
            }

            return canSimpleMove;
        }

        // returns true if the given piece can simple move by given side
        private static bool canPieceSimpleMoveInDirection(Board i_Board, Piece i_Piece, int i_Direction)
        {
            bool isPieceHavePossibleMove = false;

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction))
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer == null)
                {
                    isPieceHavePossibleMove = true;
                }
            }

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction))
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer == null)
                {
                    isPieceHavePossibleMove = true;
                }
            }

            return isPieceHavePossibleMove;
        }

        // Returns the opposite side of the given side
        public static ePlayerSide GetOtherSide(ePlayerSide i_Side)
        {
            ePlayerSide side = (i_Side == ePlayerSide.Down) ? ePlayerSide.Up : ePlayerSide.Down;
            return side;
        }

        // Returns true if the given new location is in the board's borders
        public static bool IsInBorders(Board i_Board, int i_X, int i_Y)
        {
            bool inRowBorders = i_X >= 0 && i_X < i_Board.Size;
            bool inColBorders = i_Y >= 0 && i_Y < i_Board.Size;
            if (inRowBorders && inColBorders)
            {
                return true;
            }

            return false;
        }

        public static bool IsMovingInValidDirection(Player i_CurrentPlayer, Move i_Move, Piece i_Piece)
        {
            bool isDirectionValid = false;

            if (i_Piece.IsKing)
            {
                isDirectionValid = true;
            }
            else
            {
                // bottom player tries to move up
                if(i_CurrentPlayer.Side == ePlayerSide.Down && i_Move.FromRow > i_Move.ToRow)
                {
                    isDirectionValid = true;
                }

                // top player tries to move down
                else if(i_CurrentPlayer.Side == ePlayerSide.Up && i_Move.FromRow < i_Move.ToRow)
                {
                    isDirectionValid = true;
                }
            }

            return isDirectionValid;
        }
    }
}
