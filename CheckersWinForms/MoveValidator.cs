using System;
using System.Globalization;
using System.Text;

namespace CheckersWinForms
{
    public class MoveValidator
    {
        public static string ConvertLocationToString(int i_Row, int i_Col)
        {
            StringBuilder locationString = new StringBuilder();
            locationString.Append((char)(i_Col + 'A'));
            locationString.Append((char)(i_Row + 'a'));

            return locationString.ToString();
        }

        public static string ConvertMoveToString(Move i_Move)
        {
            char fromColumn = (char)(i_Move.YFrom + 'A');
            char fromRow = (char)(i_Move.XFrom + 'a');
            char toColumn = (char)(i_Move.YTo + 'A');
            char toRow = (char)(i_Move.XTo + 'a');
            StringBuilder moveString = new StringBuilder();
            moveString.Append(fromColumn);
            moveString.Append(fromRow);
            moveString.Append('>');
            moveString.Append(toColumn);
            moveString.Append(toRow);

            return moveString.ToString();
        }

        public static Move ConvertStringToMove(string i_MoveAsString)
        {
            int fromColumn = i_MoveAsString[0] - 'A';
            int fromRow = i_MoveAsString[1] - 'a';
            int toColumn = i_MoveAsString[3] - 'A';
            int toRow = i_MoveAsString[4] - 'a';
            Move move = new Move { YFrom = fromColumn, XFrom = fromRow, YTo = toColumn, XTo = toRow };

            return move;
        }

        public static bool isCapturePossiblePerPiece(Player i_Player, Board i_Board, Piece i_Piece)
        {
            bool captureMovePossible = false;

            if (i_Player.Side == ePlayerSide.Down)
            {
                captureMovePossible = IsCaptureMovePossiblePerSide(ePlayerSide.Down, i_Board, (int)ePlayerMoves.MoveUp, i_Piece);
            }
            else
            {
                captureMovePossible = IsCaptureMovePossiblePerSide(ePlayerSide.Up, i_Board, (int)ePlayerMoves.MoveDown, i_Piece);
            }

            if (i_Piece.IsKing)
            {
                if (i_Player.Side == ePlayerSide.Down)
                {
                    captureMovePossible = captureMovePossible || IsCaptureMovePossiblePerSide(ePlayerSide.Down, i_Board, (int)ePlayerMoves.MoveDown, i_Piece);
                }
                else
                {
                    captureMovePossible = captureMovePossible || IsCaptureMovePossiblePerSide(ePlayerSide.Up, i_Board, (int)ePlayerMoves.MoveUp, i_Piece);
                }
            }

            return captureMovePossible;
        }

        public static bool IsPlayerHasCapture(Player i_Player, Board i_Board)
        {
            bool canCapture = false;

            foreach (Piece piece in i_Player.Pieces)
            {
                if (piece.IsKing)
                {
                    canCapture = canCapture || IsKingCapturePossible(i_Player, i_Board, piece);
                }
                else
                {
                    canCapture = canCapture || isCapturePossiblePerPiece(i_Player, i_Board, piece);
                }
            }

            return canCapture;
        }

        public static bool IsCaptureMovePossiblePerSide(ePlayerSide i_Side, Board i_Board, int i_Direction, Piece i_Piece)
        {
            bool captureMovePossible = false;

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction) && i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer != null)
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer.Side == GetOtherSide(i_Side))
                {
                    if (IsInBorders(i_Board, i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y - (2 * i_Direction)))
                    {
                        if (i_Board.GameBoard[i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y - (2 * i_Direction)].PiecePointer == null)
                        {
                            captureMovePossible = true;
                        }
                    }
                }
            }

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction) && i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer != null)
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer.Side == GetOtherSide(i_Side))
                {
                    if (IsInBorders(i_Board, i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y + (2 * i_Direction)))
                    {
                        if (i_Board.GameBoard[i_Piece.Location.X + (2 * i_Direction), i_Piece.Location.Y + (2 * i_Direction)].PiecePointer == null)
                        {
                            captureMovePossible = true;
                        }
                    }
                }
            }

            return captureMovePossible;
        }

        public static bool IsKingCapturePossible(Player i_Player, Board i_Board, Piece i_Piece)
        {
            bool kingCapturePossible = false;

            if (i_Player.Side == ePlayerSide.Down)
            {
                kingCapturePossible = IsCaptureMovePossiblePerSide(ePlayerSide.Down, i_Board, (int)ePlayerMoves.MoveDown, i_Piece);
            }
            else
            {
                kingCapturePossible = IsCaptureMovePossiblePerSide(ePlayerSide.Up, i_Board, (int)ePlayerMoves.MoveUp, i_Piece);
            }

            return kingCapturePossible;
        }

        // Returns true if the the move without capture is possible
        public static bool IsSimpleMove(Player i_Player, Board i_Board, Move i_Move)
        {
            bool simpleMove = false;

            // if destination is empty
            if (IsInBorders(i_Board, i_Move.XTo, i_Move.YTo) && i_Board.GameBoard[i_Move.XTo, i_Move.YTo].PiecePointer == null)
            {
                // if row diff is 1
                if (Math.Abs(i_Move.XTo - i_Move.XFrom) == 1)
                {
                    simpleMove = IsKingMoveDiagonalLine(i_Player, i_Move) || IsSimpleMovePossible(i_Player, i_Board, i_Move);
                }
            }

            return simpleMove;
        }

        public static bool IsSimpleMovePossible(Player i_Player, Board i_Board, Move i_Move)
        {
            bool possible = false;

            // check valid move for regular piece
            if (i_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer != null)
            {
                if (i_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer.IsKing == false)
                {
                    if (i_Player.Side == ePlayerSide.Down)
                    {
                        possible = IsMoveDiagonalLine(ePlayerSide.Down, i_Move, (int)ePlayerMoves.MoveUp);
                    }

                    if (i_Player.Side == ePlayerSide.Up)
                    {
                        possible = IsMoveDiagonalLine(ePlayerSide.Up, i_Move, (int)ePlayerMoves.MoveDown);
                    }
                }

                if (i_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer.IsKing)
                {
                    if (IsKingMoveDiagonalLine(i_Player, i_Move))
                    {
                        possible = true;
                    }
                }
            }

            return possible;
        }

        public static bool IsMoveDiagonalLine(ePlayerSide i_Side, Move i_Move, int i_Direction)
        {
            bool diagonalLine = false;

            if ((i_Move.XTo == i_Move.XFrom + i_Direction) && (Math.Abs(i_Move.YFrom - i_Move.YTo) == 1))
            {
                diagonalLine = true;
            }

            return diagonalLine;
        }

        public static bool IsKingMoveDiagonalLine(Player i_Player, Move i_Move)
        {
            bool diagonalLine = (Math.Abs(i_Move.XTo - i_Move.XFrom) == 1) &&
                                (Math.Abs(i_Move.YTo - i_Move.YFrom) == 1);

            return diagonalLine;
        }

        // Returns true if the the move with capture is possible
        public static bool IsCaptureMovePossible(Player i_Player, Board i_Board, Move i_Move)
        {
            bool captureMove = false;
            Piece currentPiece = i_Board.GameBoard[i_Move.XFrom, i_Move.YFrom].PiecePointer;
            Piece pieceToBeEaten = i_Board.GameBoard[(i_Move.XFrom + i_Move.XTo) / 2, (i_Move.YFrom + i_Move.YTo) / 2].PiecePointer;

            if (currentPiece != null && IsInBorders(i_Board, i_Move.XTo, i_Move.YTo))
            {
                // is destination empty
                if (i_Board.GameBoard[i_Move.XTo, i_Move.YTo].PiecePointer == null)
                {
                    if (i_Player.Side == ePlayerSide.Down)
                    {
                        if(pieceToBeEaten != null && pieceToBeEaten.Side == ePlayerSide.Up)
                        {
                            if (i_Move.XFrom - 1 == i_Move.XTo + 1 || (currentPiece.IsKing && i_Move.XFrom + 1 == i_Move.XTo - 1))
                            {
                                if (((i_Move.YFrom + 1) == (i_Move.YTo - 1)) || ((i_Move.YFrom - 1) == (i_Move.YTo + 1)))
                                {
                                    captureMove = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(pieceToBeEaten != null && pieceToBeEaten.Side == ePlayerSide.Down)
                        {
                            if (i_Move.XFrom + 1 == i_Move.XTo - 1 || (currentPiece.IsKing && i_Move.XFrom - 1 == i_Move.XTo + 1))
                            {
                                if (((i_Move.YFrom + 1) == (i_Move.YTo - 1)) || ((i_Move.YFrom - 1) == (i_Move.YTo + 1)))
                                {
                                    captureMove = true;
                                }
                            }
                        }
                    }
                }
            }

            return captureMove;
        }

        public static bool IsPieceHavePossibleMove(Player i_Player, Board i_Board, Piece i_Piece)
        {
            bool pieceHavePossibleMove = false;

            pieceHavePossibleMove = isSimpleMovePossiblePerPiece(i_Player, i_Board, i_Piece);
            pieceHavePossibleMove = pieceHavePossibleMove || isCapturePossiblePerPiece(i_Player, i_Board, i_Piece);

            return pieceHavePossibleMove;
        }

        private static bool isSimpleMovePossiblePerPiece(Player i_Player, Board i_Board, Piece i_Piece)
        {
            bool pieceHavePossibleMove = false;

            if (i_Piece.IsKing == false)
            {
                if (i_Player.Side == ePlayerSide.Down)
                {
                    pieceHavePossibleMove = isSimpleMovePossiblePerPieceBySide(i_Board, i_Piece, (int)ePlayerMoves.MoveUp);
                }
                else
                {
                    pieceHavePossibleMove = isSimpleMovePossiblePerPieceBySide(i_Board, i_Piece, (int)ePlayerMoves.MoveDown);
                }
            }
            else
            {
                if (i_Player.Side == ePlayerSide.Down)
                {
                    pieceHavePossibleMove = isSimpleMovePossiblePerPieceBySide(i_Board, i_Piece, (int)ePlayerMoves.MoveDown);
                }
                else
                {
                    pieceHavePossibleMove = isSimpleMovePossiblePerPieceBySide(i_Board, i_Piece, (int)ePlayerMoves.MoveUp);
                }
            }

            return pieceHavePossibleMove;
        }

        private static bool isSimpleMovePossiblePerPieceBySide(Board i_Board, Piece i_Piece, int i_Direction)
        {
            bool pieceHavePossibleMove = false;

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction))
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y + i_Direction].PiecePointer == null)
                {
                    pieceHavePossibleMove = true;
                }
            }

            if (IsInBorders(i_Board, i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction))
            {
                if (i_Board.GameBoard[i_Piece.Location.X + i_Direction, i_Piece.Location.Y - i_Direction].PiecePointer == null)
                {
                    pieceHavePossibleMove = true;
                }
            }

            return pieceHavePossibleMove;
        }

        public static ePlayerSide GetOtherSide(ePlayerSide i_Side)
        {
            ePlayerSide side = (i_Side == ePlayerSide.Down) ? ePlayerSide.Up : ePlayerSide.Down;

            return side;
        }

        public static bool IsInBorders(Board i_Board, int i_X, int i_Y)
        {
            bool inBorders = false;
            if (i_X >= 0 && i_X < i_Board.Size && i_Y >= 0 && i_Y < i_Board.Size)
            {
                inBorders = true;
            }

            return inBorders;
        }
    }
}
