using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Backtrack
    {
        private readonly Board _board;

        public Backtrack(Board board)
        {
            _board = board;
        }

        public bool Solve()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var column = 0; column < 9; column++)
                {
                    var currentSquare = _board.GetBoardSquare(row, column);
                    if (!currentSquare.IsEmpty())
                        continue;

                    for (var number = 1; number <= 9; number++)
                    {
                        var placedValues = GetNeighbours(currentSquare);

                        if (!IsValidMove(placedValues, number))
                            continue;

                        currentSquare.SetValue(number);

                        if (Solve())
                            return true;

                        currentSquare.SetValue(0);
                    }

                    return false;
                }
            }

            _board.PrintBoard();
            return true;
        }

        private IEnumerable<BoardSquare> GetNeighbours(BoardSquare currentSquare)
        {
            var Row = currentSquare.GetRowIndex();
            var Column = currentSquare.GetColumnIndex();
            var SubBoard = currentSquare.GetSubBoard();

            var PlacedValues = new List<BoardSquare>();
            PlacedValues.AddRange(_board.GetRow(Row));
            PlacedValues.AddRange(_board.GetColumn(Column));
            PlacedValues.AddRange(_board.GetSubBoard(SubBoard));
            return PlacedValues;
        }

        private static bool IsValidMove(IEnumerable<BoardSquare> placedValues, int number)
        {
            return placedValues.All(value => !value.GetValue().Equals(number));
        }
    }
}