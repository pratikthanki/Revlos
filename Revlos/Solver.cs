using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Revlos
{
    public class Solver
    {
        private Board _board;

        public Solver(Board board)
        {
            _board = board;
        }

        public Board BackTracker(Board board)
        {
            Board finalBoard = null;
            return finalBoard;
        }

        public void CalculateHorizontalValues()
        {
            
        }
        
        public bool IsPossible(BoardSquare square, int value)
        {
            var row = square.GetRowIndex();
            var column = square.GetColumnIndex();
            var subBoard = square.GetSubBoard();
            
            return _board.GetRow(row).Contains<>(value) || 
                   _board.GetColumn(column).Contains<>(value) || 
                   _board.GetSubBoard(subBoard).Contains<>(value);
        }
    }
}
 