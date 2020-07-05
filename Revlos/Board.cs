using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Board
    {
        private BoardSquare[,] _board;
        
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
                    board[i,j].SetLocation(i, j);
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
            return _board.Cast<BoardSquare>().Any(cell => cell.IsSolved());
        }
    }
}
