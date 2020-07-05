using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Solver
    {
        private Board _board;

        public Solver(Board board)
        {
            _board = board;
        }

        public Board DancingLink(Board board)
        {
            Board finalBoard = null;

            return finalBoard;
        }
        
        public bool IsPossible(BoardSquare square, int value)
        {
            var row = square.GetRowIndex();
            var column = square.GetColumnIndex();
            var subBoard = square.GetSubBoard();

            return IsValueAvailable(_board.GetRow(row), value) ||
                   IsValueAvailable(_board.GetColumn(column), value) ||
                   IsValueAvailable(_board.GetSubBoard(subBoard), value);
        }

        private static bool IsValueAvailable(List<BoardSquare> squares, int value)
        {
            return squares == null || squares.Any(option => option.GetValue() != value);
        }
    }
}
