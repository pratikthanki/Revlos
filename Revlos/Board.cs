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
    }
    
    internal enum SubBoards
    {
        UpperLeft,
        UpperMiddle,
        UpperRight,
        MiddleLeft,
        Middle,
        MiddleRight,
        LowerLeft,
        LowerMiddle,
        LowerRight
    }
}
