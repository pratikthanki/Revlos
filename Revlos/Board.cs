using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Revlos
{
    public class Board
    {
        private readonly BoardSquare[,] _board;
        private readonly int initialEmptySquares;

        public Board(string rows)
        {
            _board = BuildBoard(rows);
            initialEmptySquares = Regex.Matches(rows, "0").Count;
        }

        private static BoardSquare[,] BuildBoard(string str)
        {
            Debug.Assert(str.Length == 81);

            const int chunkSize = 9;
            var boardRows = Enumerable
                .Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToList();

            var board = new BoardSquare[9, 9];
            for (var r = 0; r < boardRows.Count; r++)
            {
                var row = boardRows[r];
                Debug.Assert(row.Length == 9);

                for (var c = 0; c < row.Length; c++) 
                    board[r,c] = new BoardSquare(r, c, (int) char.GetNumericValue(row[c]));
            }

            return board;
        }

        public BoardSquare GetBoardSquare(int row, int column) => _board[row, column];

        public void SetBoardSquare(int row, int column, int value) => _board[row, column].SetValue(value);

        public int GetSize() => _board.GetLength(0);

        public int CountInitialEmptySquares() => initialEmptySquares;

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
        }
    }
}
