using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Revlos
{
    public class Backtrack
    {
        private readonly Board _board;
        private int steps;
        private readonly Stopwatch _stopwatch;

        public Backtrack(Board board)
        {
            _board = board;
            steps = 0;
            _stopwatch = new Stopwatch();
            
            _board.PrintBoard();
            _stopwatch.Start();
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
                        steps++;

                        if (Solve())
                            return true;

                        currentSquare.SetValue(0);
                    }

                    return false;
                }
            }
            _stopwatch.Stop();
            _board.PrintBoard();
            Console.WriteLine($"Steps: {steps}; Time taken: {_stopwatch.ElapsedMilliseconds}ms");
            return true;
        }

        private IEnumerable<BoardSquare> GetNeighbours(BoardSquare currentSquare)
        {
            var Row = currentSquare.GetRowIndex();
            var Column = currentSquare.GetColumnIndex();
            var SubBoard = currentSquare.GetSubBoard(Column, Row);

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