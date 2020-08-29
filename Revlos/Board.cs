using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Revlos
{
    
    public class Board
    {
        private readonly BoardSquare[,] _board;

        public Board(IReadOnlyList<string> rows)
        {
            _board = BuildBoard(rows);
        }

        private static BoardSquare[,] BuildBoard(IReadOnlyList<string> rows)
        {
            Debug.Assert(rows.Count == 9);

            var board = new BoardSquare[9, 9];
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                Debug.Assert(row.Length == 9);

                for (var j = 0; j < row.Length; j++)
                {
                    board[i, j] = row[j] == 0
                        ? new BoardSquare(0)
                        : new BoardSquare((int) char.GetNumericValue(row[j]));

                    board[i, j].SetLocation(i, j);
                }
            }

            return board;
        }

        public BoardSquare GetBoardSquare(int row, int column) => _board[row, column];

        public void SetBoardSquare(int row, int column, int value) => _board[row, column].SetValue(value);

        public int GetSize() => _board.GetLength(0);

        public void PrintBoard()
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                if (i % 3 == 0 && i > 0)
                    Console.WriteLine(" - - - + - - - + - - - ");

                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j % 3 == 0 && j > 0)
                        Console.Write(" |");

                    Console.Write(" " + _board[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
