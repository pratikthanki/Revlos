using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Revlos
{
    public class Board
    {
        private readonly BoardSquare[,] _board;

        public Board(IReadOnlyList<string> rows)
        {
            _board = BuildBoard(rows);
            PrintBoard();
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
                    board[i, j] = int.Parse(row[j].ToString()) == 0
                        ? new BoardSquare(0)
                        : new BoardSquare((int) char.GetNumericValue(row[j]));
                    board[i, j].SetLocation(i, j);
                }
            }

            return board;
        }

        public BoardSquare GetBoardSquare(int row, int column)
        {
            return _board[row, column];
        }

        public void PrintBoard()
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                if (i % 3 == 0 && i > 0) Console.WriteLine(" - - - + - - - + - - - ");

                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j % 3 == 0 && j > 0) Console.Write(" |");

                    Console.Write(" " + _board[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public IEnumerable<BoardSquare> GetRow(int row)
        {
            var result = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                if (i != row) continue;

                for (var j = 0; j < _board.GetLength(1); j++) result.Add(_board[i, j]);

            }

            return result;
        }

        public IEnumerable<BoardSquare> GetColumn(int column)
        {
            var result = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j != column) continue;

                    result.Add(_board[i, j]);
                }
            }

            return result;
        }

        public IEnumerable<BoardSquare> GetSubBoard(SubBoard subBoard)
        {
            var subBoardSquares = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j].GetSubBoard() == subBoard) subBoardSquares.Add(_board[i, j]);
                }
            }

            return subBoardSquares;
        }

        public bool IsSolved()
        {
            return SquaresRemaining() == 0;
        }

        private int SquaresRemaining()
        {
            return _board.Cast<BoardSquare>().Count(square => square.IsEmpty());
        }
    }
}
