using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Revlos
{
    
    public class Board
    {
        private readonly BoardSquare[,] _board;
        public Candidate[,] cellConstraint;
        public Candidate[] rowConstraint;
        public Candidate[] columnConstraint;
        public Candidate[,] subBoardConstraint;

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
                    if (_board[row, col].GetSubBoard() != subBoard)
                        continue;

                    if (!_board[row, col].IsEmpty())
                        result.Add(_board[row, col]);
                }
            }

            return result;
        }

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
        
        private void InitialiseConstraints()
        {
            for (var row = 0; row < 9; row++)
            for (var col = 0; col < 9; col++)
                cellConstraint[row, col] = new Candidate(9, true);

            for (var row = 0; row < 3; row++)
            for (var col = 0; col < 3; col++)
                subBoardConstraint[row, col] = new Candidate(9, false);


            for (var i = 0; i < 9; i++)
            {
                rowConstraint[i] = new Candidate(9, false);
                columnConstraint[i] = new Candidate(9, false);
            }

            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    if (_board[row, col].GetValue() <= 0)
                        continue;

                    var candidate = _board[row, col].GetValue();
                    rowConstraint[row][candidate] = true;
                    columnConstraint[col][candidate] = true;
                    subBoardConstraint[row / 3, col / 3][candidate] = true;
                }
            }
        }
    }
}
