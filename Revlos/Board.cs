using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Board
    {
        private readonly BoardSquare[,] _board;
        private HashSet<BoardSquare> _solved;
        private HashSet<BoardSquare> _unsolved;
        private Stack<HashSet<BoardSquare>> _changed;
        
        public Board(IReadOnlyList<string> rows)
        {
            _board = BuildBoard(rows);

            _solved = new HashSet<BoardSquare>();
            _unsolved = new HashSet<BoardSquare>();
            _changed = new Stack<HashSet<BoardSquare>>();
        }
        
        private static BoardSquare[,] BuildBoard(IReadOnlyList<string> rows)
        {
            var board = new BoardSquare[9,9];
            for (var i = 0; i < rows.Count;i++)
            {
                var row = rows[i];
                for (var j = 0; j < row.Length; j++)
                {
                    board[i,j] = row[j] == '-' ? new BoardSquare(0) : new BoardSquare((int)char.GetNumericValue(row[j]));
                    board[i,j].SetLocation(i, j);
                }
            }
            return board;
        }

        public BoardSquare GetBoardSquare(int row, int column)
        {
            return _board[column, row];
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

        public List<BoardSquare> GetRow(int row)
        {
            var result = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                if (i != row)
                    continue;

                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    result.Add(_board[i,j]);
                }
            }
            return result;
        }
        
        public List<BoardSquare> GetColumn(int column)
        {
            var result = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (j != column)
                        continue;

                    result.Add(_board[i,j]);
                }
            }
            return result;
        }
        
        public List<BoardSquare> GetSubBoard(SubBoard subBoard)
        {
            var subBoardSquares = new List<BoardSquare>(9);
            for (var i = 0; i < _board.GetLength(0);i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j].GetSubBoard() == subBoard)
                    {
                        subBoardSquares.Add(_board[i, j]);
                    }
                }
            }
            return subBoardSquares;
        }
        
        public bool IsBoardSolved()
        {
            return _board.Cast<BoardSquare>().Any(cell => cell.IsEmpty());
        }
    }
}
