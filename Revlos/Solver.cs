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

        public void DancingLink()
        {
            _board.PrintBoard();
            do
            {
                for (var Row = 0; Row < 9; Row++)
                {
                    for (var Column = 0; Column < 9; Column++)
                    {
                        var CurrentSquare = _board.GetBoardSquare(Row, Column);
                        var IsEmpty = CurrentSquare.IsEmpty();

                        if (IsEmpty)
                            CurrentSquare.AddPossibleValue(CalculatePossibleValues(CurrentSquare));

                        if (CurrentSquare.GetPossibleValues().Count == 1)
                            CurrentSquare.SetValue();
                    }
                }

                Console.WriteLine($"Squares remaining: {_board.SquaresRemaining()}");
            } while (_board.SquaresRemaining() > 0);
            
            _board.PrintBoard();
        }
        
        private List<int> CalculatePossibleValues(BoardSquare boardSquare)
        {
            var Row = boardSquare.GetRowIndex();
            var Column = boardSquare.GetColumnIndex();
            var SubBoard = boardSquare.GetSubBoard();
            
            var PlacedValues = new List<BoardSquare>();
            PlacedValues.AddRange(_board.GetRow(Row));
            PlacedValues.AddRange(_board.GetColumn(Column));
            PlacedValues.AddRange(_board.GetSubBoard(SubBoard));
            
            var PossibleValues = new HashSet<int>(){1,2,3,4,5,6,7,8,9};
            foreach (var Square in PlacedValues)
            {
                PossibleValues.Remove(Square.GetValue());
            }
            return PossibleValues.ToList();
        }
    }
}
