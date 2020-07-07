using System;
using System.Collections;
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

        public Board DancingLink()
        {
            Board finalBoard = null;
            
            return finalBoard;
        }
        
        public List<BoardSquare> CalculatePossibleValues(BoardSquare boardSquare, int value)
        {
            var row = boardSquare.GetRowIndex();
            var column = boardSquare.GetColumnIndex();
            var subBoard = boardSquare.GetSubBoard();
            
            var placedValues = new List<BoardSquare>();
            placedValues.AddRange(_board.GetRow(row));
            placedValues.AddRange(_board.GetColumn(column));
            placedValues.AddRange(_board.GetSubBoard(subBoard));
            
            var possibleValues = new HashSet<BoardSquare>();
            foreach (var square in placedValues)
            {
                possibleValues.Add(square);
            }
            return possibleValues.ToList();
        }
    }
}
