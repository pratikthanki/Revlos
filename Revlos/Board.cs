using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Revlos
{
    public class Board
    {
        private BoardSquare[,] _board;
        public const int Size = 9;
        public Board(IReadOnlyList<string> rows)
        {
            _board = new BoardSquare[9, 9];
            _board = BuildBoard(rows);
        }
        
        private static BoardSquare[,] BuildBoard(IReadOnlyList<string> rows)
        {
            var board = new BoardSquare[9,9];
            for (var i = 0; i < rows.Count;i++)
            {
                var row = rows[i];
                for (var j = 0; j < row.Length; j++)
                {
                    if (row[j] == '-')
                    {
                        board[i,j] = new BoardSquare();
                    }
                    else
                    {
                        board[i,j] = new BoardSquare((int)char.GetNumericValue(row[j]));
                    }
                }
            }
            return board;
        }
        
        public void PrintBoard()
        {
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                if (i % 3 == 0 && i > 0)
                {
                    Console.WriteLine(" - - - + - - - + - - - ");
                }

                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j % 3 == 0 && j > 0)
                    {
                        Console.Write(" |");
                    }
                    Console.Write(" " + _board[i, j].ToString());
                }

                Console.WriteLine();
            }
        }

        public BoardSquare[] GetRow(int row)
        {
            BoardSquare[] result = new BoardSquare[9];
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                if (i != row)
                    continue;

                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    result[j] = _board[i,j];
                }
            }
            return result;
        }
        
        public BoardSquare[] GetColumn(int column)
        {
            BoardSquare[] result = new BoardSquare[9];
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j != column)
                        continue;

                    result[i] = _board[i,j];
                }
            }
            return result;
        }

        public SubBoard GetSubBoard(int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8)
                throw new ArgumentOutOfRangeException();

            if (x < 3 && y < 3)
                return SubBoard.TopLeft;

            if (x > 2 && x < 6 && y < 3)
                return SubBoard.TopMiddle;

            if (x > 5 && y < 3)
                return SubBoard.TopRight;

            if (x < 3 && y > 2 && y < 6)
                return SubBoard.MiddleLeft;

            if (x > 2 && x < 6 && y > 2 && y < 6)
                return SubBoard.Middle;

            if (x > 5 && y > 2 && y < 6)
                return SubBoard.MiddleRight;

            if (x < 3 && y > 5)
                return SubBoard.BottomLeft;

            if (x > 2 && x < 6 && y > 5)
                return SubBoard.BottomMiddle;

            return SubBoard.BottomRight;
        }

        public bool IsBoardSolved()
        {
            return _board.Cast<BoardSquare>().Any(cell => cell.IsSolved());
        }
        
        public bool IsPossible(BoardSquare square)
        {
            
            return false;
        }
    }

    public enum SubBoard
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Middle,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight
    }
}
