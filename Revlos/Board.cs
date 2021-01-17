using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Revlos
{
    public class Board
    {
        private readonly BoardSquare[,] _board;

        public Board(string rows)
        {
            _board = BuildBoard(rows);
        }

        private static BoardSquare[,] BuildBoard(string str)
        {
            Debug.Assert(str.Length == 81);

            const int chunkSize = 9;
            var boardRows = Enumerable
                .Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize))
                .ToList();

            var board = new BoardSquare[9, 9];
            for (var r = 0; r < boardRows.Count; r++)
            {
                var row = boardRows[r];
                Debug.Assert(row.Length == 9);

                for (var c = 0; c < row.Length; c++)
                    board[r, c] = new BoardSquare(r, c, (int) char.GetNumericValue(row[c]));
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
        }
        
        public IEnumerable<BoardSquare> GetRow(int rowIndex)
        {
            var result = new List<BoardSquare>(9);
            for (var row = 0; row < _board.GetLength(0); row++)
            {
                if (row != rowIndex)
                    continue;

                for (var col = 0; col < _board.GetLength(1); col++)
                {
                    if (!_board[row, col].IsEmpty())
                        result.Add(_board[row, col]);
                }

            }

            return result;
        }

        public IEnumerable<BoardSquare> GetColumn(int colIndex)
        {
            var result = new List<BoardSquare>(9);
            for (var row = 0; row < _board.GetLength(0); row++)
            {
                for (var col = 0; col < _board.GetLength(1); col++)
                {
                    if (col != colIndex)
                        continue;

                    if (!_board[row, col].IsEmpty())
                        result.Add(_board[row, col]);
                }
            }

            return result;
        }

        public IEnumerable<BoardSquare> GetSubBoard(SubBoard subBoard)
        {
            var result = new List<BoardSquare>(9);
            for (var row = 0; row < _board.GetLength(0); row++)
            {
                for (var col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col].GetSubBoard(col, row) != subBoard)
                        continue;

                    if (!_board[row, col].IsEmpty())
                        result.Add(_board[row, col]);
                }
            }

            return result;
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
